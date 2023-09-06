using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vansystem
{
    public partial class GISReportGenerator : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            


            string piclayer = "";
            string imageNamehzd2 = "";
            string imageNamehzd = @"C:\Downloads\outputcheck.png";
            string imageNamehz = @"C:\Downloads\outputstate.png";
            string linkname = Session["layername"].ToString();
            string image = "";
            string layer2 = "";
            string layername = "";
            string division = Session["division"].ToString();
            string divisionlowercase = division.ToLower();
            string minx = Session["minx"].ToString();
            string miny = Session["miny"].ToString();
            string maxx = Session["maxx"].ToString();
            string maxy = Session["maxy"].ToString();



            if (linkname == "divisionlink")
            {
                layername = "Division";
                layer2 = "";
                image = "white.jpg";
            }
            else if (linkname == "rangelink")
            {
                layername = "Ranges";
                layer2 = ",cite%3Avw_range_" + divisionlowercase + "";
                image = "range.png";
                piclayer = "range";
            }
            else if (linkname == "blocklink")
            {
                layername = "Blocks";
                layer2 = ",cite%3Avw_block_" + divisionlowercase + "";
                image = "block.png";
                piclayer = "block";
            }
            else if (linkname == "compartmentlink")
            {
                layername = "Compartments";
                layer2 = ",cite%3Avw_compartment_" + divisionlowercase + "";
                image = "compartment.png";
                piclayer = "compartment";
            }
            else if (linkname == "plotlink")
            {
                layername = "Plots";
                layer2 = ",cite%3Avw_plot_" + divisionlowercase + "";
                image = "plot.png";
                piclayer = "plot";
            }
           
            string imageNamehzd1 = @"C:\Gisfy\Projects\New Code\vansystem\vansystem\vansystem\images\division_bkp.png";
            imageNamehzd2 = @"C:\Gisfy\Projects\New Code\vansystem\vansystem\vansystem\images\" + image + "";

            string imageUrl1 = new Uri(imageNamehzd1).AbsoluteUri;
            string imageUrl2 = new Uri(imageNamehzd2).AbsoluteUri;
            //download map

            //string commandn = "curl -o \"" + imageNamehzd + "\" \"http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&version=1.1.0&request=GetMap&layers=cite%3Avw_division_" + divisionlowercase + "" + layer2 + "&bbox=78.26277160644531%2C19.35351181030282%2C78.97780609130857%2C19.916971206665103&width=600&height=600&srs=EPSG%3A4326&styles=&format=image/png\"";
            string commandn = "curl -o \"" + imageNamehzd + "\" \"http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&version=1.1.0&request=GetMap&layers=cite%3Avw_division_" + divisionlowercase + "" + layer2 + "&bbox=" + minx + "%2C" + miny + "%2C" + maxx + "%2C" + maxy + "&width=600&height=600&srs=EPSG%3A4326&styles=&format=image/png\"";
            try
            {
                ProcessStartInfo startInfon = new ProcessStartInfo();
                startInfon.FileName = "cmd.exe";
                startInfon.Arguments = "/c " + commandn;
                startInfon.WindowStyle = ProcessWindowStyle.Hidden;
                startInfon.RedirectStandardOutput = true;
                startInfon.UseShellExecute = false;
                Process processn = new Process();
                processn.StartInfo = startInfon;
                processn.Start();
                processn.WaitForExit();
                string output = processn.StandardOutput.ReadToEnd();
            }
            catch (Exception ex)
            {

            }

            //download state and division map
            

            string commands = "curl -o \"" + imageNamehz + "\" \"http://3.7.34.230:8080/geoserver/cite/wms?service=WMS&version=1.1.0&request=GetMap&layers=cite%3Avw_state_Telangana,cite%3Avw_division_" + divisionlowercase + "&bbox=77.23575275400003%2C15.836003294000022%2C81.32264131500006%2C19.91680698600004&width=768&height=766&srs=EPSG%3A4326&styles=&format=image/png\"";
            try
            {
                ProcessStartInfo startInfon = new ProcessStartInfo();
                startInfon.FileName = "cmd.exe";
                startInfon.Arguments = "/c " + commands;
                startInfon.WindowStyle = ProcessWindowStyle.Hidden;
                startInfon.RedirectStandardOutput = true;
                startInfon.UseShellExecute = false;
                Process processn = new Process();
                processn.StartInfo = startInfon;
                processn.Start();
                processn.WaitForExit();
                string output = processn.StandardOutput.ReadToEnd();
            }
            catch (Exception ex)
            {

            }
            //Attatch downloaded map into rdlc

            string imageUrl = new Uri(imageNamehzd).AbsoluteUri;
            string imageUrl7 = new Uri(imageNamehz).AbsoluteUri;

            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;

            ReportParameter rp1 = new ReportParameter("layername", layername);
            ReportParameter rp2 = new ReportParameter("divisionname", division);
            ReportParameter rp3 = new ReportParameter("divisionimage", imageUrl1);
            ReportParameter rp4 = new ReportParameter("rangeimage", imageUrl2);
            ReportParameter rp5 = new ReportParameter("ImageUrl", imageUrl);
            ReportParameter rp6 = new ReportParameter("piclayer", "division");
            ReportParameter rp7 = new ReportParameter("piclayer1", piclayer);
            ReportParameter rp8 = new ReportParameter("statelayer", imageUrl7);

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("MapReports.rdlc");
            ReportViewer1.LocalReport.ReportPath = "MapReports.rdlc";
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
            //ReportViewer1.LocalReport.Refresh();


            //download rdlc as pdf
            string pdfname = layername + " in " + division + " Division";
            byte[] pdfBytes = ReportViewer1.LocalReport.Render("PDF");
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + pdfname + ".pdf");
            Response.OutputStream.Write(pdfBytes, 0, pdfBytes.Length);
            Response.Flush();
            //Response.End();

            Thread.Sleep(2000);

            //delete image
            if (File.Exists(imageNamehzd))
            {
                File.Delete(imageNamehzd);

            }
            //delete state image
            if (File.Exists(imageNamehz))
            {
                File.Delete(imageNamehz);

            }


        }
    }
}