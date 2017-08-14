using System;
using System.IO.Ports; 
using System.Runtime.Remoting.Messaging; 
using System.Drawing;
using System.Windows.Forms;
//using COMPortTerminal;
//using COMPortTerminal.Properties;


// namespace COMPortTerminal
namespace CodeProjectSerialComms
{
    /// <summary>
    /// Routines for finding and accessing COM ports.
    /// </summary>
    

    public class ComPorts  
    {
        const int PACKETSIZE = 20; // 

        private const string ModuleName = "ComPorts"; 
        
        //  Shared members - do not belong to a specific instance of the class.        
        internal static bool comPortExists; 
        internal static string[] myPortNames; 
        internal static string noComPortsMessage = "No COM ports found. Please attach a COM-port device."; 
        
        internal delegate void UserInterfaceDataEventHandler( string action, string formText, Color textColor ); 
        internal static event UserInterfaceDataEventHandler UserInterfaceData; 
        
        //  Non-shared members - belong to a specific instance of the class.        
        internal delegate bool WriteToComPortDelegate( string textToWrite );
              
        // internal WriteToComPortDelegate WriteToComPortDelegate1;      
        
        //  Local variables available as Properties.

        private bool m_ParameterChanged;
        private bool m_PortChanged;
        private bool m_PortOpen;
        private SerialPort m_PreviousPort = new SerialPort();
        private int m_ReceivedDataLength;
        private int m_SavedBitRate = 9600;
        private Handshake m_SavedHandshake = Handshake.None ;
        private string m_SavedPortName = "";
        private SerialPort m_SelectedPort = new SerialPort();
        

        internal bool ParameterChanged 
        { 
            get 
            { 
                return m_ParameterChanged; 
            } 
            set 
            { 
                m_ParameterChanged = value; 
            } 
        } 
        
        internal bool PortChanged 
        { 
            get 
            { 
                return m_PortChanged; 
            } 
            set 
            { 
                m_PortChanged = value; 
            } 
        }         
      
        internal bool PortOpen 
        { 
            get 
            { 
                return m_PortOpen; 
            } 
            set 
            { 
                m_PortOpen = value; 
            } 
        }         
       
        internal SerialPort PreviousPort 
        { 
            get 
            { 
                return m_PreviousPort; 
            } 
            set 
            { 
                m_PreviousPort = value; 
            } 
        }         
       
        internal int ReceivedDataLength 
        { 
            get 
            { 
                return m_ReceivedDataLength; 
            } 
            set 
            { 
                m_ReceivedDataLength = value; 
            } 
        }         
       
        internal int SavedBitRate 
        { 
            get 
            { 
                return m_SavedBitRate; 
            } 
            set 
            { 
                m_SavedBitRate = value; 
            } 
        }         
        
        internal Handshake SavedHandshake 
        { 
            get 
            { 
                return m_SavedHandshake; 
            } 
            set 
            { 
                m_SavedHandshake = value; 
            } 
        }         
       
        internal string SavedPortName 
        { 
            get 
            { 
                return m_SavedPortName; 
            } 
            set 
            { 
                m_SavedPortName = value; 
            } 
        }         
       
        internal SerialPort SelectedPort 
        { 
            get 
            { 
                return m_SelectedPort; 
            } 
            set 
            { 
                m_SelectedPort = value; 
            } 
        } 
        
        /// <summary>
        /// If the COM port is open, close it.
        /// </summary>
        /// 
        /// <param name="portToClose"> the SerialPort object to close </param>  

        internal void CloseComPort( SerialPort portToClose ) 
        { 
            try 
            {
                UserInterfaceData?.Invoke("DisplayStatus", "", Color.Black);

                object transTemp0 = portToClose; 
                if (  !(  transTemp0 == null ) ) 
                {                     
                    if ( portToClose.IsOpen ) 
                    {                         
                        portToClose.Close();
                        UserInterfaceData?.Invoke("DisplayPortSettings", "", Color.Black);
                    } 
                }                
           }
                                      
            catch ( InvalidOperationException ex ) 
            {                 
                ParameterChanged = true; 
                PortChanged = true; 
                DisplayException( ModuleName, ex );                 
            } 
            catch ( UnauthorizedAccessException ex ) 
            {                 
                ParameterChanged = true; 
                PortChanged = true; 
                DisplayException( ModuleName, ex );                 
            } 
            catch ( System.IO.IOException ex ) 
            {                 
                ParameterChanged = true; 
                PortChanged = true; 
                DisplayException( ModuleName, ex ); 
            }             
        } 


        /// <summary>
        /// Provide a central mechanism for displaying exception information.
        /// Display a message that describes the exception.
        /// </summary>
        /// 
        /// <param name="ex"> The exception </param> 
        /// <param name="moduleName" > the module where the exception was raised. </param>
        
        private void DisplayException( string moduleName, Exception ex ) 
        {
            string errorMessage = null; 
            
            errorMessage = "Exception: " + ex.Message + " Module: " + moduleName + ". Method: " + ex.TargetSite.Name;

            UserInterfaceData?.Invoke("DisplayStatus", errorMessage, Color.Red);

            //  To display errors in a message box, uncomment this line:
            //  MessageBox.Show(errorMessage)
        }        

        /// <summary>
        /// Respond to error events.
        /// </summary>
        
        private void ErrorReceived( object sender, SerialErrorReceivedEventArgs e ) 
        { 
            SerialError SerialErrorReceived1 = 0; 
            
            SerialErrorReceived1 = e.EventType; 
            
            switch ( SerialErrorReceived1 ) 
            {
                case SerialError.Frame:
                    Console.WriteLine( "Framing error." ); 
                    
                    break;
                case SerialError.Overrun:
                    Console.WriteLine( "Character buffer overrun." ); 
                    
                    break;
                case SerialError.RXOver:
                    Console.WriteLine( "Input buffer overflow." ); 
                    
                    break;
                case SerialError.RXParity:
                    Console.WriteLine( "Parity error." ); 
                    
                    break;
                case SerialError.TXFull:
                    Console.WriteLine( "Output buffer full." ); 
                    break;
            }            
        }         

        /// <summary>
        /// Find the PC's COM ports and store parameters for each port.
        /// Use saved parameters if possible, otherwise use default values.  
        /// </summary>
        /// 
        /// <remarks> 
        /// The ports can change if a USB/COM-port converter is attached or removed,
        /// so this routine may need to run multiple times.
        /// </remarks>
        
       internal static void FindComPorts() 
        { 
            myPortNames = SerialPort.GetPortNames(); 
            
            //  Is there at least one COM port?
            
            if ( myPortNames.Length > 0 ) 
            {                 
                comPortExists = true; 
                Array.Sort( myPortNames );                 
            } 
            else 
            { 
                //  No COM ports found.
                
                comPortExists = false; 
            }             
        }             

    }
} 
