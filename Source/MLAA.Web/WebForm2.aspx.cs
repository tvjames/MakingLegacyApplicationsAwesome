using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLAA.Data.Linq2Sql;

namespace MLAA.Web
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public WebForm2ViewModel ViewModel { get; set; }
    }

    public class WebForm2ViewModel
    {
        public WebForm2ViewModel()
        {
            Students = new EnrolmentManager().SearchStudents("");
        }

        public Student[] Students { get; set; }
    }
}