using DeweySystem.Core;

namespace DeweySystem.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelyCommand WelcomePageCommand { get; set; }

        public RelyCommand ReplaceBookViewCommand { get; set; }

        public RelyCommand IdentifyAreaViewCommand { get; set; }

        public RelyCommand FindingCallNumberCommand { get; set; }

        public RelyCommand ProgressBarManagerCommand { get; set; }

        public RelyCommand HomeViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }

        public viewModelWelcomePage WelcomePageVM { get; set; }

        public viewModelReplaceBook ReplaceBookVM { get; set; }

        public viewModelIdentifyArea IdentifyAreaVM { get; set; }

        public viewModelFindingCallNumbers FindingCallNumbersVM { get; set; }

        public ProgressBarManager ProgressBarManagerVM { get; set; }


        private object _currentView;


        //Setting the currentView to a value and calling the onPropertyChanged method
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            //Instaniating the 5 viewModels
            HomeVM = new HomeViewModel();
            WelcomePageVM = new viewModelWelcomePage();
            ReplaceBookVM = new viewModelReplaceBook();
            IdentifyAreaVM = new viewModelIdentifyArea();
            FindingCallNumbersVM = new viewModelFindingCallNumbers();
            ProgressBarManagerVM = new ProgressBarManager();

            CurrentView = HomeVM;

            HomeViewCommand = new RelyCommand(o =>
            {
                CurrentView = HomeVM;
            });

            WelcomePageCommand = new RelyCommand(o =>
            {
                CurrentView = WelcomePageVM;
            });

            ReplaceBookViewCommand = new RelyCommand(o =>
            {
                CurrentView = ReplaceBookVM;
            });

            IdentifyAreaViewCommand = new RelyCommand(o =>
            {
                CurrentView = IdentifyAreaVM;
            });

            FindingCallNumberCommand = new RelyCommand(o =>
            {
                CurrentView = FindingCallNumbersVM;
            });

            ProgressBarManagerCommand = new RelyCommand(o =>
            {
                CurrentView = ProgressBarManagerVM;
            });

        }
    }
}
