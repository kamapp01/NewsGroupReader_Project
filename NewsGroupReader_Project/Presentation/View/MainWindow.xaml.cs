using System.Windows;
using MVVM_ICOM_INOTIFY.Presentation.ViewModel;

namespace MVVM_ICOM_INOTIFY.Presentation.View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = ((App)App.Current);

        // Registrér og skift til GreenViewModel kun hvis den ikke allerede er registreret
        if (!ViewModelController.Instance.GetAllViewModels().ContainsKey(typeof(ConnectionViewModel)))
        {
            new ConnectionViewModel();
        }

        ViewModelController.Instance.SetCurrentViewModel(typeof(ConnectionViewModel));
    }

}