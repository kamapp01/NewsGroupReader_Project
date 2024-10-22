using System.ComponentModel;
using System.Runtime.CompilerServices;
using MVVM_ICOM_INOTIFY.Presentation.ViewModel;

namespace MVVM_ICOM_INOTIFY.Presentation;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application, INotifyPropertyChanged
{

    private ViewModelBase? _currentViewModel;
    public ViewModelBase? CurrentViewModel
    { 
        get => _currentViewModel; 
        set => SetField(ref _currentViewModel, value); 
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}