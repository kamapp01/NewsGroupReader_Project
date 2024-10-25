namespace MVVM_ICOM_INOTIFY.Presentation.ViewModel;

public class ViewModelController 
    {
        // Static field to hold the singleton instance
        private static ViewModelController? _instance;

        // Property to access the singleton instance
        public static ViewModelController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ViewModelController();
                }
                return _instance;
            }
        }
        // reference to current application instance
        private readonly App _app; 
        
        // dict for storing all registered view models to be able to search by type
        private readonly Dictionary<Type, ViewModelBase> _viewModels;


        // Private constructor to prevent instantiation from outside
        private ViewModelController()
        {
            _viewModels = new Dictionary<Type, ViewModelBase>();
            _app = FetchCurrentApp(); 
        }

        private App FetchCurrentApp()
        {
            return ((App)App.Current); 
        }
        
        
        
        /// <summary>
        /// Method for registering a view model
        /// </summary>
        /// <param name="viewModel"> viewmodel to register</param>
        public void RegistryViewModel(ViewModelBase viewModel)
        {
            if (_viewModels.ContainsKey(viewModel.GetType()))
            {
                // Returnér uden at kaste en undtagelse, hvis allerede registreret
                return; 
            }

            _viewModels.Add(viewModel.GetType(), viewModel);
        }


        /// <summary>
        /// Method for removing a viewmodel from dict. of viewmodels 
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <exception cref="Exception">throws if type does not exist in the dict.</exception>
        public void UnRegistryViewModel(Type viewModelType)
        {
            
            if (!_viewModels.ContainsKey(viewModelType))
            {
                throw new Exception($"ViewModel {viewModelType.Name} is not registered");
            }
            
            _viewModels.Remove(viewModelType);
            
        }

        /// <summary>
        /// Method for setting the current viewmodel 
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <exception cref="Exception">throw an exception if the viewmodel does not exist in the dict.</exception>
        public void SetCurrentViewModel(Type viewModelType)
        {

            if (!_viewModels.ContainsKey(viewModelType))
            {
                throw new Exception($"ViewModel {viewModelType.Name} is not registered");
            }
            
            _app.CurrentViewModel = _viewModels[viewModelType];
        }
        
        /// <summary>
        /// Get all registered view models
        /// </summary>
        /// <returns>A dictionary of view model types and instances</returns>
        public Dictionary<Type, ViewModelBase> GetAllViewModels()
        {
            return _viewModels;
        }
    }
    