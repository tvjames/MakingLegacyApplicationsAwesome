namespace MLAA.Web
{
    public class BasePage<TViewModel> where TViewModel : class, new()
    {
        public BasePage()
        {
            ViewModel = new TViewModel();
        }

        public TViewModel ViewModel { get; set; }
    }
}