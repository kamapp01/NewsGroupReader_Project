using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using MVVM_ICOM_INOTIFY.Application;
using MVVM_ICOM_INOTIFY.Entities;
using MVVM_ICOM_INOTIFY.Infrastructure;
using MVVM_ICOM_INOTIFY.InterfaceAdapter;

namespace MVVM_ICOM_INOTIFY.Presentation.ViewModel
{
    public class ConnectionViewModel : ViewModelBase
    {
        private ConnectionCredentials? _connectionCredentials;
        private readonly GetConnection_US1 _getConnectionUs1 = new GetConnection_US1(EstablishConnection.Instance);
        
        
        private string _serverInput;
        public string ServerInput
        {
            get => _serverInput;
            set
            {
                if (_serverInput != value)
                {
                    _serverInput = value;
                    OnPropertyChanged(); // Notificer UI om ændring
                }
            }
        }

        private string _portInput;
        public string PortInput
        {
            get => _portInput;
            set
            {
                if (_portInput != value)
                {
                    _portInput = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _userInput;
        public string UserInput
        {
            get => _userInput;
            set
            {
                if (_userInput != value)
                {
                    _userInput = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _passwordInput;
        public string PasswordInput
        {
            get => _passwordInput;
            set
            {
                if (_passwordInput != value)
                {
                    _passwordInput = value;
                    OnPropertyChanged();
                }
            }
        }
        
        

        // Command for handling the OK button press
        public ICommand OkPressedCommand => new CommandBase((object param) =>
        {
            if (string.IsNullOrWhiteSpace(ServerInput) || string.IsNullOrWhiteSpace(PortInput) ||
                string.IsNullOrWhiteSpace(UserInput) || string.IsNullOrWhiteSpace(PasswordInput))
            {
                MessageBox.Show("Please fill all fields");
            }
            else
            {
                bool connectedToServer = EstablishConnectionToServer();

                if (connectedToServer)
                {
                    bool authenticated = Authenticate();

                    if (authenticated)
                    {
                        SaveCredentials();
                        ChangeView();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Connection to server failed. Please try again.");
                    CleanTextBoxes();
                }
            }
        });

       

        private void CleanTextBoxes()
        {
            ServerInput = "";
            PortInput = "";
            UserInput = "";
            PasswordInput = "";
        }


        private bool EstablishConnectionToServer()
        {
            bool connectionEstablished = false;
            
            // creates a new ConnectionCredentials object from the input values
            _connectionCredentials = new ConnectionCredentials(ServerInput, PortInput, UserInput, PasswordInput);
            
            
            bool connected = _getConnectionUs1.Connect(_connectionCredentials);

            if (connected)
            {
                connectionEstablished = true;
            }
            
            Debug.WriteLine($"Connection established: {connectionEstablished.ToString()}");
            
            return connectionEstablished;
        }
        
        
        private bool Authenticate()
        {
            bool authenticationEstablished = false;

            bool authenticated = _getConnectionUs1.Authenticate(_connectionCredentials);

            if (authenticated)
            {
                authenticationEstablished = true;
            }
            
            Debug.WriteLine($"Authentication established: {authenticationEstablished.ToString()}");
            return authenticationEstablished;
        }
        
        
        private void SaveCredentials()
        {
            UserSession.Server = ServerInput;
            UserSession.Port = PortInput;
            UserSession.UserName = UserInput;
        }
        
        
        private void ChangeView()
        {
            // Logic to change the view
            var viewModelType = typeof(UserViewModel);

            // Check if the ViewModel is already registered
            if (!ViewModelController.Instance.GetAllViewModels().ContainsKey(viewModelType))
            {
                // If it's not registered, create and register a new instance
                var userViewModel = new UserViewModel();
                ViewModelController.Instance.RegistryViewModel(userViewModel);
            }

            // If ViewModel is already registered, switch ViewModel
            ViewModelController.Instance.SetCurrentViewModel(viewModelType);
        }
    }
}


