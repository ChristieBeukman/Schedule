using GalaSoft.MvvmLight;

namespace Schedule.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        string test;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            test = "test";
        }

        public string Test
        {
            get
            {
                return test;
            }

            set
            {
                test = value;
                RaisePropertyChanged("Test");
            }
        }
    }
}