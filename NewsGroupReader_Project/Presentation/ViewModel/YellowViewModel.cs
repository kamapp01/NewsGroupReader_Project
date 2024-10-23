using System.Windows.Input;

namespace MVVM_ICOM_INOTIFY.Presentation.ViewModel;

public class YellowViewModel : ViewModelBase
{
    public ICommand ChangeViewCommandYellowView => new CommandBase((object commandParameter) =>
    {
        {
            var viewModelType = typeof(UserViewModel);

            // checks if the ViewModel is already registered
            if (!ViewModelController.Instance.GetAllViewModels().ContainsKey(viewModelType))
            {
                // if it's not registered, create and register a new instance
                var userViewModel = new UserViewModel();
                ViewModelController.Instance.RegistryViewModel(userViewModel);
            }

            // if viewModel already registered, switch ViewModel 
            ViewModelController.Instance.SetCurrentViewModel(viewModelType);
        }

    });


    
}