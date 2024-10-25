using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using MVVM_ICOM_INOTIFY.Entities;
using MVVM_ICOM_INOTIFY.Infrastructure.Service;
using MVVM_ICOM_INOTIFY.InterfaceAdapter;

namespace MVVM_ICOM_INOTIFY.Infrastructure;

public class EstablishConnection : IGetConnection
{

    // static field to hold the singleton instance
    private static EstablishConnection? _instance;

    // property to access the singleton instance
    public static EstablishConnection Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EstablishConnection();
            }

            return _instance;
        }
    }

    // private constructor to prevent instantiation from outside
    private EstablishConnection()
    {
    }


    private HandleCommunication _handleCommunication = new HandleCommunication();
    private const string CarriageReturnLineFeed = "\r\n";


    public TcpClient TcpClient { get; private set; }
    public NetworkStream NetworkStream { get; set; }

    private bool _connected = false;
    private bool _authenticated = false;

    public bool Connect(ConnectionCredentials credentials)
    {
        try
        {
            TcpClient = new TcpClient(credentials.ServerInput, Convert.ToInt32(credentials.PortInput));
            NetworkStream = TcpClient.GetStream();
            
            // Indstil timeout til uendelig for læsning og skrivning
            NetworkStream.ReadTimeout = Timeout.Infinite;  // Uendelig timeout for læsning
            NetworkStream.WriteTimeout = Timeout.Infinite; // Uendelig timeout for skrivning
            
            Debug.WriteLine("Connecting to server...");

            string response = _handleCommunication.ReadFromServer();
            Debug.WriteLine($"Server: {response}");

            if (response.StartsWith("200"))
            {
                _connected = true;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine($"[Connect] - {e.Message}");
            return _connected;
        }

        Debug.WriteLine("Connection established");
        return _connected;
    }


    public bool AuthenticateUser(ConnectionCredentials credentials)
    {
        try
        {
            _handleCommunication.SendToServer($"AUTHINFO USER {credentials.UserInput}{CarriageReturnLineFeed}");
            string response = _handleCommunication.ReadFromServer();
            Debug.WriteLine($"Server: {response}");

            if (response.StartsWith("381"))
            {
                _handleCommunication.SendToServer($"AUTHINFO PASS {credentials.PasswordInput}{CarriageReturnLineFeed}");
                response = _handleCommunication.ReadFromServer();
                Debug.WriteLine($"Server: {response}");


                if (response.StartsWith("281"))
                {
                    Debug.WriteLine("Authentication successful!");
                    _authenticated = true;
                }
                else
                {
                    Debug.WriteLine("Authentication failed.");
                    return _authenticated;
                }

            }
            else
            {
                Debug.WriteLine("Failed to proceed with authentication: Unexpected response.");
            }

            return _authenticated;
        }
        catch (Exception e)
        {
            Debug.WriteLine($"[AuthenticateUser] - {e.Message}");
            return _authenticated;
        }
    }

}
