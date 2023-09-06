using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vansystem
{
    public partial class ReportsStateWise : System.Web.UI.Page
    {
        clsConnnection clscon = new clsConnnection();
        clsJson objjson = new clsJson();
        NameValueCollection nvc = new NameValueCollection();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btndivisionwise_Click(object sender, EventArgs e)
        {
            nvc.Clear();
            //nvc.Add("",);
            //clscon.fnExecuteProcedureSelectWithCondtion();

        }
    }
}