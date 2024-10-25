using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using MVVM_ICOM_INOTIFY.Application;
using MVVM_ICOM_INOTIFY.Infrastructure;

namespace MVVM_ICOM_INOTIFY.Presentation.ViewModel;

public class UserViewModel : ViewModelBase
{
    private readonly DownloadListGroups_US2 _downloadListGroups = new DownloadListGroups_US2(new DownloadGroupList());
    private List<string> _listOfNewsGroups;

    private readonly DownloadGroupHeadline _downloadGroupHeadlines = new DownloadGroupHeadline(new DownloadHeadlines());
    private List<string> _listOfNewsHeadlines;
    
    private ObservableCollection<string> _observableListOfNewsGroups;
    public ObservableCollection<string> ObservableListOfNewsGroups
    {
        get { return _observableListOfNewsGroups; }
        set
        {
            _observableListOfNewsGroups = value;
            OnPropertyChanged();
        }
    }
    
    private string _selectedNewsGroup;
    public string SelectedNewsGroup
    {
        get { return _selectedNewsGroup; }
        set
        {
            _selectedNewsGroup = value;
            OnPropertyChanged();
            
            // checks if a newsgroup is selected before method GetAllHeadlines is executed
            if (!string.IsNullOrEmpty(_selectedNewsGroup))
            {
                Debug.WriteLine($"\nSelected news group: {_selectedNewsGroup}");
                GetAllHeadlines();
            }
        }
    }
    
    
    private string _selectedHeadline;
    public string SelectedHeadline
    {
        get { return _selectedHeadline; }
        set
        {
            _selectedHeadline = value;
            OnPropertyChanged();
        }
    }


    private ObservableCollection<string> _observableListOfHeadlines;
    public ObservableCollection<string> ObservableListOfNewsHeadlines
    {
        get { return _observableListOfHeadlines; }
        set
        {
            _observableListOfHeadlines = value;
            OnPropertyChanged();
        }
    }
    
    

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
            OnPropertyChanged(nameof(ElementVisibility));       // updates visibility
        }
    }
    
    
    public UserViewModel()
    {
        _isElementVisible = false;
        SetInformation();
        GetAllGroups();
    }

    
    //TODO: REMEMBER! _isElementVisible = true; when content is readable
    public Visibility ElementVisibility => IsElementVisible ? Visibility.Visible : Visibility.Collapsed;
    

    private void SetInformation()
    {
        _userName = $"Username: {UserSession.UserName}";
        _serverName = $"Server name: {UserSession.Server}";
        _portNo = $"Port no: {UserSession.Port}";
    }
    
    
    private void GetAllGroups()
    {
        _listOfNewsGroups = _downloadListGroups.DownloadGroupList();
        
        ObservableListOfNewsGroups = new ObservableCollection<string>(_listOfNewsGroups);
    }
    
    private void GetAllHeadlines()
    {
        _listOfNewsHeadlines = _downloadGroupHeadlines.DownloadHeadlineList(_selectedNewsGroup);
        
        ObservableListOfNewsHeadlines = new ObservableCollection<string>(_listOfNewsHeadlines);
    }
    
    
    
    
   
}