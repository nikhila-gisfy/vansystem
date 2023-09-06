using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace vansystem
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        SqlConnection con = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = (Button)sender;

                string buttonId = clickedButton.ID;
                string imageNamehzd = @"C:\Gisfy\Projects\New Code\vansystem\vansystem\vansystem\images\division.png";
                string imageNamehzd1 = @"C:\Gisfy\Projects\New Code\vansystem\vansystem\vansystem\images\blankimage.png";
                string imageNamehzd2 = @"C:\Gisfy\Projects\New Code\vansystem\vansystem\vansystem\images\range.png";
                string imageNamehzd3 = @"C:\Gisfy\Projects\New Code\vansystem\vansystem\vansystem\images\block.png";
                string divisionname = "Adilabad";
                string layername = "division";



                string imageUrl = new Uri(imageNamehzd).AbsoluteUri;
                string imageUrl1 = new Uri(imageNamehzd1).AbsoluteUri;
                string imageUrl2 = new Uri(imageNamehzd2).AbsoluteUri;
                string imageUrl3 = new Uri(imageNamehzd3).AbsoluteUri;
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                if (buttonId == "btnSubmit1")
                {
                    ReportParameter rp1 = new ReportParameter("layername", layername);
                    ReportParameter rp2 = new ReportParameter("divisionname", divisionname);
                    ReportParameter rp3 = new ReportParameter("divisionimage", imageUrl);
                    ReportParameter rp4 = new ReportParameter("emptyimage", imageUrl1);
                    ReportParameter rp5 = new ReportParameter("rangeimage", imageUrl2);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("Maps.rdlc");
                    ReportViewer1.LocalReport.ReportPath = "Maps.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4,rp5 });
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (buttonId == "btnSubmit2")
                {
                    ReportParameter rp1 = new ReportParameter("layername", layername);
                    ReportParameter rp2 = new ReportParameter("divisionname", divisionname);
                    ReportParameter rp3 = new ReportParameter("divisionimage", imageUrl);
                    ReportParameter rp4 = new ReportParameter("emptyimage", imageUrl1);
                    ReportParameter rp5 = new ReportParameter("rangeimage", imageUrl2);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("Maps.rdlc");
                    ReportViewer1.LocalReport.ReportPath = "Maps.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (buttonId == "btnSubmit3")
                {
                    ReportParameter rp1 = new ReportParameter("layername", layername);
                    ReportParameter rp2 = new ReportParameter("divisionname", divisionname);
                    ReportParameter rp3 = new ReportParameter("divisionimage", imageUrl);
                    ReportParameter rp4 = new ReportParameter("emptyimage", imageUrl1);
                    ReportParameter rp5 = new ReportParameter("rangeimage", imageUrl1);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("Maps.rdlc");
                    ReportViewer1.LocalReport.ReportPath = "Maps.rdlc";
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
                    ReportViewer1.LocalReport.Refresh();
                }





            }
            catch (Exception ex)
            {

                throw ex;
            }
           

        }
       
    }
}