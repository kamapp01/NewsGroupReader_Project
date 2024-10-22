using System.Windows.Input;

namespace MVVM_ICOM_INOTIFY.Presentation.ViewModel;

public class GreenViewModel : ViewModelBase
{

    public ICommand ChangeViewCommand => new CommandBase((object commandParameter) =>
    {
        {
            var viewModelType = typeof(YellowViewModel);

            // checks if the ViewModel is already registered
            if (!ViewModelController.Instance.GetAllViewModels().ContainsKey(viewModelType))
            {
                // if it's not registered, create and register a new instance
                var yellowViewModel = new YellowViewModel();
                ViewModelController.Instance.RegistryViewModel(yellowViewModel);
            }

            // if viewModel already registered, switch ViewModel 
            ViewModelController.Instance.SetCurrentViewModel(viewModelType);
        }

    });


}