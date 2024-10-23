using System.Windows;
using MVVM_ICOM_INOTIFY.Infrastructure;

namespace MVVM_ICOM_INOTIFY.Presentation.ViewModel;

public class UserViewModel : ViewModelBase
{
    private string _serverName;
    public string ServerName
    {
        get { return _serverName; }
        set 
        { 
            _serverName = value;
            OnPropertyChanged();
        }
    }
    
    private string _portNo;
    public string PortNo
    {
        get { return _portNo; }
        set 
        { 
            _portNo = value;
            OnPropertyChanged();
        }
    }
    
    private string _userName;
    public string UserName
    {
        get { return _userName; }
        set 
        { 
            _userName = value;
            OnPropertyChanged();
        }
    }
    
    private bool _isElementVisible;
    public bool IsElementVisible
    {
        get { return _isElementVisible; }
        set
        {
            _isElementVisible = value;
            OnPropertyChanged(nameof(IsElementVisible));
            OnPropertyChanged(nameof(ElementVisibility)); // Opdaterer synlighed
        }
    }
    
    
    public UserViewModel()
    {
        _isElementVisible = false;
        SetInformation();
        
        //TODO: HUSK AT ÆNDRE _isElementVisible = true; når content skal læses i textblock
    }
    
    public Visibility ElementVisibility => IsElementVisible ? Visibility.Visible : Visibility.Collapsed;
    

    private void SetInformation()
    {
        _userName = $"Username: {UserSession.UserName}";
        _serverName = $"Server name: {UserSession.Server}";
        _portNo = $"Port no: {UserSession.Port}";
    }
    
    
   
}