using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vansystem
{
    public partial class DraftVolume2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var FolderPath = ConfigurationManager.ConnectionStrings["filePath"].ConnectionString;
            string staticfileName = "WorkingPlan-VolumnI.docx";
            string staticpath = string.Empty;

            //Path of the File to be downloaded.
            staticpath = Path.Combine(FolderPath, "Template", staticfileName);

            //Content Type and Header.
            Response.ContentType = "application/docx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + staticfileName);

            //Writing the File to Response Stream.
            Response.WriteFile(staticpath);

            //Flushing the Response.
            Response.Flush();
            Response.End();

        }
    }
}