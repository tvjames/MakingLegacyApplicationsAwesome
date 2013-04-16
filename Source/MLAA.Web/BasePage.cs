using System;
using System.Web.UI;

namespace MLAA.Web
{
    public class BasePage<TViewModel> : Page
        where TViewModel : class
    {
        public BasePage()
        {
            ViewModel = Global.Container.Resolve<TViewModel>();
        }

        public TViewModel ViewModel { get; set; }

        protected override void OnUnload(EventArgs e)
        {
            Global.Container.Release(ViewModel);
        }
    }
}