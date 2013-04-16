using System.Web.UI;

namespace MLAA.Web
{
    public class BasePage<TViewModel>: Page
        where TViewModel : class
    {
        public BasePage()
        {
            //ViewModel = new TViewModel();
            //FIXME this doesn't work any more. We need to create some magic. Will do that shortly :)
        }

        public TViewModel ViewModel { get; set; }
    }
}