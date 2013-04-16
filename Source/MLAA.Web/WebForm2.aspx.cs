namespace MLAA.Web
{
    public partial class WebForm2 : BasePage<WebForm2ViewModel>
    {
    }

    public class BasePage<TViewModel> where TViewModel : class, new()
    {
        public BasePage()
        {
            ViewModel = new TViewModel();
        }

        public TViewModel ViewModel { get; set; }
    }
}