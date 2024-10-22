using System.Windows.Input;

namespace MVVM_ICOM_INOTIFY.Presentation.ViewModel;

public class YellowViewModel : ViewModelBase
{
    public ICommand ChangeViewCommandYellowView => new CommandBase((object commandParameter) =>
    {
        {
            var viewModelType = typeof(GreenViewModel);

            // checks if the ViewModel is already registered
            if (!ViewModelController.Instance.GetAllViewModels().ContainsKey(viewModelType))
            {
                // if it's not registered, create and register a new instance
                var greenViewModel = new GreenViewModel();
                ViewModelController.Instance.RegistryViewModel(greenViewModel);
            }

            // if viewModel already registered, switch ViewModel 
            ViewModelController.Instance.SetCurrentViewModel(viewModelType);
        }

    });


    
}