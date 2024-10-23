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
        private IGetConnection _getConnection;
        private ConnectionCredentials _connectionCredentials;
        
        public ConnectionViewModel()
        {
            // Initialiserer _getConnection her
            _getConnection = new EstablishConnection();
        }
        
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
            
            
            // Create a new ConnectionCredentials object from the input values
            _connectionCredentials = new ConnectionCredentials(ServerInput, PortInput, UserInput, PasswordInput);

            bool connected = _getConnection.Connect(_connectionCredentials);

            if (connected)
            {
                connectionEstablished = true;
            }
            
            Debug.WriteLine(_connectionCredentials);
            Debug.WriteLine(connected.ToString());
            
            return connectionEstablished;
        }
        
        
        private bool Authenticate()
        {
            bool authenticationEstablished = false;
            
            bool authenticated = _getConnection.AuthenticateUser(_connectionCredentials);

            if (authenticated)
            {
                authenticationEstablished = true;
            }
            
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
        
        

        /*
        // Dependency-injected service for getting user information
        private readonly GetUserInformation_UC0 _getUserInformation;

        public ConnectionViewModel(GetUserInformation_UC0 getUserInformation)
        {
            _getUserInformation = getUserInformation;

            // Optional: Load user credentials when the ViewModel is initialized
            //LoadUserCredentials();
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
                SaveSettings();
                ChangeView();
            }
        });


        // Method to save settings (create a new ConnectionCredentials instance)
        private void SaveSettings()
        {
            _connectionCredentials = new ConnectionCredentials(ServerInput, PortInput, UserInput, PasswordInput);
            Debug.WriteLine(_connectionCredentials.ToString()); // For debugging purposes
            // You may add additional logic to save credentials to a database or configuration file here
        }

        // Method to change the current ViewModel (switch to the UserViewModel)
        private void ChangeView()
        {
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
    */

