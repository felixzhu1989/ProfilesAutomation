using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace ProfilesAutoDrawing.ViewModel
{
    /// <summary>
    /// MVVM
    /// </summary>
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ImportDataViewModel>();




        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public ImportDataViewModel ImportData=>ServiceLocator.Current.GetInstance<ImportDataViewModel>();



        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}