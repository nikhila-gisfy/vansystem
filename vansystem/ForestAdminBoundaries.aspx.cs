using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO.Compression;
using System.Configuration;
using System.Reflection.Emit;
using vansystem.Models;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using AjaxControlToolkit;
using System.Web.Security;
using System.Threading;

namespace vansystem
{
    public partial class ForestAdminBoundaries : System.Web.UI.Page
    {
        clsJson objjson = new clsJson();
        NameValueCollection nvc = new NameValueCollection();
        clsConnnection clscon = new clsConnnection();
        shapefileconverter objshp = new shapefileconverter();
        SqlConnection con = null;
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        public static string Products = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string user_id = Session["user_id"].ToString();
            string Password = Session["Password"].ToString();
            if (!IsPostBack)
            {
                //uploadshapefile.Visible = false;
                divisionupload.Visible = false;
                rangeupload.Visible = false;
                blockupload.Visible = false;
                compartupload.Visible = false;
                plotupload.Visible = false;
                nvc.Clear();
                nvc.Add("@Operation", "GetUserdetails");
                nvc.Add("@Userid", user_id);
                nvc.Add("@Password", Password);

                DataTable dtuser = clscon.fnExecuteProcedureSelectWithCondtion("[VanIT].[dbo].[SpDataUpload]", nvc);
                Products = string.Empty;
                if (dtuser.Rows.Count > 0 && dtuser != null)
                {

                    float statecode = (float)Convert.ToDouble(dtuser.Rows[0]["state_code"]);
                    nvc.Clear();
                    nvc.Add("@Operation", "Getforest_boundary_hierarchy");
                    nvc.Add("@state_code", statecode.ToString());
                    DataTable dttables = clscon.fnExecuteProcedureSelectWithCondtion("[VanIT].[dbo].[SpDataUpload]", nvc);
                    DataTable dttabledata = new DataTable();
                    if (dttables.Rows.Count > 0 && dttables != null)
                    {
                        load(dttables);
                    }
                }
                else
                {
                    //uploadshapefile.Visible = true;
                    divisionupload.Visible = true;
                    rangeupload.Visible = true;
                    blockupload.Visible = true;
                    compartupload.Visible = true;
                    plotupload.Visible = true;
                }



            }
        }


        protected string load(DataTable dttables)
        {
            Products = string.Empty;
            if (dttables.Rows.Count > 0 && dttables != null)
            {
                divisionupload.Visible = false;
                rangeupload.Visible = false;
                blockupload.Visible = false;
                compartupload.Visible = false;
                plotupload.Visible = false;
                //Products += "<div class='col-md-8 col-sm-12'><div class='tile'><h3 class='tile-title'>Upload Forest Admin Boundaries</h3><div class='tile-body'>";
                for (int i = 0; i < dttables.Rows.Count; i++)
                {
                    string tablename = dttables.Rows[i]["table_name"].ToString();
                    string tablenamenew = "";
                    if (tablename == "division_master")
                    {
                        tablenamenew = "tblDivision";
                    }
                    else if (tablename == "range_master")
                    {
                        tablenamenew = "tblRange";
                    }
                    else if (tablename == "block_master")
                    {
                        tablenamenew = "tblBlock";
                    }
                    else if (tablename == "compartment_master")
                    {
                        tablenamenew = "tblCompartment";
                    }
                    else if (tablename == "plot_master")
                    {
                        tablenamenew = "tblPlot";
                    }
                    string user_id = Session["user_id"].ToString();
                    nvc.Clear();
                    nvc.Add("@Operation", "GetTabledata");
                    nvc.Add("@Userid", user_id);
                    nvc.Add("@TableName", tablenamenew);
                    nvc.Add("@statement", tablename);
                    DataTable dttabledata = clscon.fnExecuteProcedureSelectWithCondtion("[VanIT].[dbo].[SpDataUpload]", nvc);
                    string name = "";
                    if (dttabledata.Rows.Count > 0 && dttabledata != null)
                    {

                        for (int n = 0; n < dttabledata.Rows.Count; n++)
                        {
                            if (name == "")
                            {
                                name = dttabledata.Rows[n]["name"].ToString();
                            }
                            else
                            {
                                name += ", " + dttabledata.Rows[n]["name"].ToString();
                            }
                        }

                        int j = i + 1;
                        if (Products == "")
                        {

                            Products = "<form id='uploadFAB_" + j + "' name='uploadFAB_" + j + "' method='post' action='/DataUpload/postForestAdminBoundaries' enctype='multipart/form-data'><div class='row data-upload-wrapper' style='margin-bottom: 20px; margin-right: 2px; margin-left: 2px;'><div class='row col-lg-12'> <div class='col-sm-1'><span class='badge-custom badge-light'>" + j + "</span></div><div class='col-md-9 col-sm-8' id='bndTypeContainer'><div class='alert alert-success' role='alert'><b>Boundaries of following " + dttables.Rows[i]["name"].ToString() + "(s) are already uploaded.</b><br/><div class='row'><div class='col-md-12'>" + name + "</div></div></div></div><div class='col-md-2 col-sm-3'></div><div class='col-md-12 validation-error'></div></div></div></form>";
                        }
                        else
                        {

                            Products += "<form id='uploadFAB_" + j + "' name='uploadFAB_" + j + "' method='post' action='/DataUpload/postForestAdminBoundaries' enctype='multipart/form-data'><div class='row data-upload-wrapper' style='margin-bottom: 20px; margin-right: 2px; margin-left: 2px;'><div class='row col-lg-12'> <div class='col-sm-1'><span class='badge-custom badge-light'>" + j + "</span></div><div class='col-md-9 col-sm-8' id='bndTypeContainer'><div class='alert alert-success' role='alert'><b>Boundaries of following " + dttables.Rows[i]["name"].ToString() + "(s) are already uploaded.</b><br/><div class='row'><div class='col-md-12'>" + name + "</div></div></div></div><div class='col-md-2 col-sm-3'></div><div class='col-md-12 validation-error'></div></div></div></form>";
                        }



                    }
                    else if (tablename == "division_master")
                    {
                        divisionupload.Visible = true;
                    }
                    else if (tablename == "range_master")
                    {
                        rangeupload.Visible = true;
                    }
                    else if (tablename == "block_master")
                    {
                        blockupload.Visible = true;
                    }
                    else if (tablename == "compartment_master")
                    {
                        compartupload.Visible = true;
                    }
                    else if (tablename == "plot_master")
                    {
                        plotupload.Visible = true;
                    }
                }
                //Products += "</div></div></div>";
            }
            else
            {
                //uploadshapefile.Visible = true;
                divisionupload.Visible = true;
                rangeupload.Visible = true;
                blockupload.Visible = true;
                compartupload.Visible = true;
                plotupload.Visible = true;
            }

            return Products;
        }

        protected void Uploadfile1_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (fileUploadControl.HasFile)
                {
                    string boundary = "";
                    HtmlButton clickedButton = (HtmlButton)sender;
                    if (clickedButton.ID == "Uploadfile1")
                    {
                        boundary = "Division";
                        // Code specific to Button 1
                    }

                    // Get the filename and extension of the uploaded file
                    string myShapeFile = ConfigurationManager.ConnectionStrings["ShapeFileLocation"].ConnectionString;
                    string Root = ConfigurationManager.ConnectionStrings["Root"].ConnectionString;
                    string fileName = Path.GetFileName(fileUploadControl.FileName);
                    string fileExtension = Path.GetExtension(fileName);
                    string fname = string.Empty;


                    // Create a unique filename to avoid overwriting existing files
                    //string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                    // Specify the folder where you want to save the uploaded file
                    //string folderPath = Server.MapPath("~/D:\\savefilesfromcode/");
                    string filename1 = fileName.Replace(".zip", "");
                    fname = myShapeFile + filename1 + fileExtension;

                    string user_id = Session["user_id"].ToString();
                    //inserting filename and boundary into db
                    insertfilename(filename1, user_id, boundary);

                    // Create the folder if it does not exist

                    if (File.Exists(fname))
                    {
                        File.Delete(fname);
                        if (Directory.Exists(fname))
                        {
                            Directory.Delete(fname, true);
                        }
                        fileUploadControl.SaveAs(fname);






                    }
                    if (!File.Exists(fname))
                    {

                        fileUploadControl.SaveAs(fname);

                    }




                    //Paths path = new Paths();   
                    //string root = myShapeFile + fileName;
                    string zipPath = fname;
                    Random rd = new Random();

                    int rand_num = rd.Next(1000000000, 2000000000);
                    string extractPath = Root + "\\";



                    //zip file exists or not 
                    //if exits extract to folder 
                    //search .shp file exits or not 
                    if (Directory.Exists(zipPath))
                    {
                        Directory.Delete(zipPath, true);

                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath);
                    //checking for folders and moving the files from inner folder to outer folder and delete inner folder

                    // Get the folder names within the source folder
                    string[] folderNames = Directory.GetDirectories(extractPath);
                    if (folderNames.Length > 0)
                    {
                        // Iterate through each folder
                        foreach (string folderPathn in folderNames)
                        {
                            // Get the file names within the current folder
                            string[] fileNames = Directory.GetFiles(folderPathn);

                            // Move each file to the destination folder
                            foreach (string filePath in fileNames)
                            {
                                string fileNamen = Path.GetFileName(filePath);
                                string destinationFilePath = Path.Combine(extractPath, fileNamen);
                                File.Move(filePath, destinationFilePath);
                            }

                            // Delete the current folder
                            Directory.Delete(folderPathn, recursive: true);
                        }

                    }
                    string folderPath = extractPath;
                    string searchPattern = "*.shp";

                    string[] filePaths = Directory.GetFiles(folderPath, searchPattern);

                    if (filePaths.Length > 0)
                    {
                        string tablename = (((fileName.Replace(myShapeFile, "")).Replace(fileExtension, "")));
                        string msg1 = objshp.CheckTableExistis(tablename);
                        if (msg1 == "Success")
                        {
                            string msg2 = objshp.toShapeToDataBase(tablename, extractPath);
                        }
                    }
                    else
                    {
                        // no .shp files were found
                    }
                    Thread.Sleep(3000);
                    Response.Redirect("dashboard.aspx", false);

                }
                if (fileUploadControl1.HasFile)
                {
                    string boundary = "";
                    HtmlButton clickedButton = (HtmlButton)sender;
                    if (clickedButton.ID == "Uploadfile2")
                    {
                        boundary = "Range";
                        // Code specific to Button 2
                    }

                    // Get the filename and extension of the uploaded file
                    string myShapeFile = ConfigurationManager.ConnectionStrings["ShapeFileLocation"].ConnectionString;
                    string Root = ConfigurationManager.ConnectionStrings["Root"].ConnectionString;
                    string fileName = Path.GetFileName(fileUploadControl1.FileName);
                    string fileExtension = Path.GetExtension(fileName);
                    string fname = string.Empty;


                    // Create a unique filename to avoid overwriting existing files
                    //string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                    // Specify the folder where you want to save the uploaded file
                    //string folderPath = Server.MapPath("~/D:\\savefilesfromcode/");
                    string filename1 = fileName.Replace(".zip", "");
                    fname = myShapeFile + filename1 + fileExtension;

                    string user_id = Session["user_id"].ToString();
                    //inserting filename and boundary into db
                    insertfilename(filename1, user_id, boundary);

                    // Create the folder if it does not exist

                    if (File.Exists(fname))
                    {
                        File.Delete(fname);
                        //if (Directory.Exists(fname))
                        //{
                        //    Directory.Delete(fname, true);
                        //}
                        fileUploadControl1.SaveAs(fname);






                    }
                    if (!File.Exists(fname))
                    {

                        fileUploadControl1.SaveAs(fname);

                    }




                    //Paths path = new Paths();   
                    //string root = myShapeFile + fileName;
                    string zipPath = fname;
                    Random rd = new Random();

                    int rand_num = rd.Next(1000000000, 2000000000);
                    string extractPath = Root + "\\";



                    //zip file exists or not 
                    //if exits extract to folder 
                    //search .shp file exits or not 
                    if (Directory.Exists(zipPath))
                    {
                        Directory.Delete(zipPath, true);

                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath);

                    //checking for folders and moving the files from inner folder to outer folder and delete inner folder

                    // Get the folder names within the source folder
                    string[] folderNames = Directory.GetDirectories(extractPath);
                    if (folderNames.Length > 0)
                    {
                        // Iterate through each folder
                        foreach (string folderPathn in folderNames)
                        {
                            // Get the file names within the current folder
                            string[] fileNames = Directory.GetFiles(folderPathn);

                            // Move each file to the destination folder
                            foreach (string filePath in fileNames)
                            {
                                string fileNamen = Path.GetFileName(filePath);
                                string destinationFilePath = Path.Combine(extractPath, fileNamen);
                                File.Move(filePath, destinationFilePath);
                            }

                            // Delete the current folder
                            Directory.Delete(folderPathn, recursive: true);
                        }

                    }
                    string searchPattern = "*.shp";
                    string folderPath = extractPath;
                    string[] filePaths = Directory.GetFiles(folderPath, searchPattern);

                    if (filePaths.Length > 0)
                    {
                        string tablename = (((fileName.Replace(myShapeFile, "")).Replace(fileExtension, "")));
                        string msg1 = objshp.CheckTableExistis(tablename);
                        if (msg1 == "Success")
                        {
                            string msg2 = objshp.toShapeToDataBase(tablename, extractPath);
                        }
                    }
                    else
                    {
                        // no .shp files were found
                    }
                    Thread.Sleep(3000);
                    Response.Redirect("dashboard.aspx", false);

                }
                if (fileUploadControl2.HasFile)
                {
                    string boundary = "";
                    HtmlButton clickedButton = (HtmlButton)sender;
                    if (clickedButton.ID == "Uploadfile3")
                    {
                        boundary = "Block";
                        // Code specific to Button 2
                    }

                    // Get the filename and extension of the uploaded file
                    string myShapeFile = ConfigurationManager.ConnectionStrings["ShapeFileLocation"].ConnectionString;
                    string Root = ConfigurationManager.ConnectionStrings["Root"].ConnectionString;
                    string fileName = Path.GetFileName(fileUploadControl2.FileName);
                    string fileExtension = Path.GetExtension(fileName);
                    string fname = string.Empty;


                    // Create a unique filename to avoid overwriting existing files
                    //string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                    // Specify the folder where you want to save the uploaded file
                    //string folderPath = Server.MapPath("~/D:\\savefilesfromcode/");
                    string filename1 = fileName.Replace(".zip", "");
                    fname = myShapeFile + filename1 + fileExtension;

                    string user_id = Session["user_id"].ToString();
                    //inserting filename and boundary into db
                    insertfilename(filename1, user_id, boundary);

                    // Create the folder if it does not exist

                    if (File.Exists(fname))
                    {
                        File.Delete(fname);
                        //if (Directory.Exists(fname))
                        //{
                        //    Directory.Delete(fname, true);
                        //}
                        fileUploadControl2.SaveAs(fname);






                    }
                    if (!File.Exists(fname))
                    {

                        fileUploadControl2.SaveAs(fname);

                    }




                    //Paths path = new Paths();   
                    //string root = myShapeFile + fileName;
                    string zipPath = fname;
                    Random rd = new Random();

                    int rand_num = rd.Next(1000000000, 2000000000);
                    string extractPath = Root + "\\";



                    //zip file exists or not 
                    //if exits extract to folder 
                    //search .shp file exits or not 
                    if (Directory.Exists(zipPath))
                    {
                        Directory.Delete(zipPath, true);

                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath);
                    //checking for folders and moving the files from inner folder to outer folder and delete inner folder

                    // Get the folder names within the source folder
                    string[] folderNames = Directory.GetDirectories(extractPath);
                    if (folderNames.Length > 0)
                    {
                        // Iterate through each folder
                        foreach (string folderPathn in folderNames)
                        {
                            // Get the file names within the current folder
                            string[] fileNames = Directory.GetFiles(folderPathn);

                            // Move each file to the destination folder
                            foreach (string filePath in fileNames)
                            {
                                string fileNamen = Path.GetFileName(filePath);
                                string destinationFilePath = Path.Combine(extractPath, fileNamen);
                                File.Move(filePath, destinationFilePath);
                            }

                            // Delete the current folder
                            Directory.Delete(folderPathn, recursive: true);
                        }

                    }
                    string folderPath = extractPath;
                    string searchPattern = "*.shp";

                    string[] filePaths = Directory.GetFiles(folderPath, searchPattern);

                    if (filePaths.Length > 0)
                    {
                        string tablename = (((fileName.Replace(myShapeFile, "")).Replace(fileExtension, "")));
                        string msg1 = objshp.CheckTableExistis(tablename);
                        if (msg1 == "Success")
                        {
                            string msg2 = objshp.toShapeToDataBase(tablename, extractPath);
                        }
                    }
                    else
                    {
                        // no .shp files were found
                    }
                    Thread.Sleep(3000);
                    Response.Redirect("dashboard.aspx", false);

                }
                if (fileUploadControl3.HasFile)
                {
                    string boundary = "";
                    HtmlButton clickedButton = (HtmlButton)sender;
                    if (clickedButton.ID == "Uploadfile4")
                    {
                        boundary = "Compartment";
                        // Code specific to Button 2
                    }

                    // Get the filename and extension of the uploaded file
                    string myShapeFile = ConfigurationManager.ConnectionStrings["ShapeFileLocation"].ConnectionString;
                    string Root = ConfigurationManager.ConnectionStrings["Root"].ConnectionString;
                    string fileName = Path.GetFileName(fileUploadControl3.FileName);
                    string fileExtension = Path.GetExtension(fileName);
                    string fname = string.Empty;


                    // Create a unique filename to avoid overwriting existing files
                    //string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                    // Specify the folder where you want to save the uploaded file
                    //string folderPath = Server.MapPath("~/D:\\savefilesfromcode/");
                    string filename1 = fileName.Replace(".zip", "");
                    fname = myShapeFile + filename1 + fileExtension;

                    string user_id = Session["user_id"].ToString();
                    //inserting filename and boundary into db
                    insertfilename(filename1, user_id, boundary);

                    // Create the folder if it does not exist

                    if (File.Exists(fname))
                    {
                        File.Delete(fname);
                        //if (Directory.Exists(fname))
                        //{
                        //    Directory.Delete(fname, true);
                        //}
                        fileUploadControl3.SaveAs(fname);






                    }
                    if (!File.Exists(fname))
                    {

                        fileUploadControl3.SaveAs(fname);

                    }




                    //Paths path = new Paths();   
                    //string root = myShapeFile + fileName;
                    string zipPath = fname;
                    Random rd = new Random();

                    int rand_num = rd.Next(1000000000, 2000000000);
                    string extractPath = Root + "\\";



                    //zip file exists or not 
                    //if exits extract to folder 
                    //search .shp file exits or not 
                    if (Directory.Exists(zipPath))
                    {
                        Directory.Delete(zipPath, true);

                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath);
                    //checking for folders and moving the files from inner folder to outer folder and delete inner folder

                    // Get the folder names within the source folder
                    string[] folderNames = Directory.GetDirectories(extractPath);
                    if (folderNames.Length > 0)
                    {
                        // Iterate through each folder
                        foreach (string folderPathn in folderNames)
                        {
                            // Get the file names within the current folder
                            string[] fileNames = Directory.GetFiles(folderPathn);

                            // Move each file to the destination folder
                            foreach (string filePath in fileNames)
                            {
                                string fileNamen = Path.GetFileName(filePath);
                                string destinationFilePath = Path.Combine(extractPath, fileNamen);
                                File.Move(filePath, destinationFilePath);
                            }

                            // Delete the current folder
                            Directory.Delete(folderPathn, recursive: true);
                        }

                    }
                    string folderPath = extractPath;
                    string searchPattern = "*.shp";

                    string[] filePaths = Directory.GetFiles(folderPath, searchPattern);

                    if (filePaths.Length > 0)
                    {
                        string tablename = (((fileName.Replace(myShapeFile, "")).Replace(fileExtension, "")));
                        string msg1 = objshp.CheckTableExistis(tablename);
                        if (msg1 == "Success")
                        {
                            string msg2 = objshp.toShapeToDataBase(tablename, extractPath);
                        }
                    }
                    else
                    {
                        // no .shp files were found
                    }
                    Thread.Sleep(3000);
                    Response.Redirect("dashboard.aspx", false);

                }
                if (fileUploadControl4.HasFile)
                {
                    string boundary = "";
                    HtmlButton clickedButton = (HtmlButton)sender;
                    if (clickedButton.ID == "Uploadfile5")
                    {
                        boundary = "Plot";
                        // Code specific to Button 2
                    }
                    // Get the filename and extension of the uploaded file
                    string myShapeFile = ConfigurationManager.ConnectionStrings["ShapeFileLocation"].ConnectionString;
                    string Root = ConfigurationManager.ConnectionStrings["Root"].ConnectionString;
                    string fileName = Path.GetFileName(fileUploadControl4.FileName);
                    string fileExtension = Path.GetExtension(fileName);
                    string fname = string.Empty;


                    // Create a unique filename to avoid overwriting existing files
                    //string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                    // Specify the folder where you want to save the uploaded file
                    //string folderPath = Server.MapPath("~/D:\\savefilesfromcode/");
                    string filename1 = fileName.Replace(".zip", "");
                    fname = myShapeFile + filename1 + fileExtension;

                    string user_id = Session["user_id"].ToString();
                    //inserting filename and boundary into db
                    insertfilename(filename1, user_id, boundary);

                    // Create the folder if it does not exist

                    if (File.Exists(fname))
                    {
                        File.Delete(fname);
                        //if (Directory.Exists(fname))
                        //{
                        //    Directory.Delete(fname, true);
                        //}
                        fileUploadControl4.SaveAs(fname);






                    }
                    if (!File.Exists(fname))
                    {

                        fileUploadControl4.SaveAs(fname);

                    }




                    //Paths path = new Paths();   
                    //string root = myShapeFile + fileName;
                    string zipPath = fname;
                    Random rd = new Random();

                    int rand_num = rd.Next(1000000000, 2000000000);
                    string extractPath = Root + "\\";



                    //zip file exists or not 
                    //if exits extract to folder 
                    //search .shp file exits or not 
                    if (Directory.Exists(zipPath))
                    {
                        Directory.Delete(zipPath, true);

                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath);
                    //checking for folders and moving the files from inner folder to outer folder and delete inner folder

                    // Get the folder names within the source folder
                    string[] folderNames = Directory.GetDirectories(extractPath);
                    if (folderNames.Length > 0)
                    {
                        // Iterate through each folder
                        foreach (string folderPathn in folderNames)
                        {
                            // Get the file names within the current folder
                            string[] fileNames = Directory.GetFiles(folderPathn);

                            // Move each file to the destination folder
                            foreach (string filePath in fileNames)
                            {
                                string fileNamen = Path.GetFileName(filePath);
                                string destinationFilePath = Path.Combine(extractPath, fileNamen);
                                File.Move(filePath, destinationFilePath);
                            }

                            // Delete the current folder
                            Directory.Delete(folderPathn, recursive: true);
                        }

                    }
                    string folderPath = extractPath;
                    string searchPattern = "*.shp";

                    string[] filePaths = Directory.GetFiles(folderPath, searchPattern);

                    if (filePaths.Length > 0)
                    {
                        string tablename = (((fileName.Replace(myShapeFile, "")).Replace(fileExtension, "")));
                        string msg1 = objshp.CheckTableExistis(tablename);
                        if (msg1 == "Success")
                        {
                            string msg2 = objshp.toShapeToDataBase(tablename, extractPath);
                        }
                    }
                    else
                    {
                        // no .shp files were found
                    }
                    Thread.Sleep(3000);
                    Response.Redirect("dashboard.aspx", false);

                }
            }
            catch (Exception ex)
            {

                //Label1.Text = ex.Message;
            }
        }

        public string insertfilename(string filename, string user_id, string boundary)
        {
            string mgs = string.Empty;
            try
            {
                string formattedDate = DateTime.Now.ToString("yyyy-MM-dd");
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_shapefile_queue", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StatementType", "Insert");
                        cmd.Parameters.AddWithValue("@filename", filename);
                        cmd.Parameters.AddWithValue("@boundary", boundary);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Parameters.AddWithValue("@date", formattedDate);
                        con.Open();

                        int i = cmd.ExecuteNonQuery();
                        con.Close();
                        if (i > 0)
                        {
                            mgs = "Success";

                        }



                    }
                }
                //string formattedDate = DateTime.Now.ToString("yyyy-MM-dd");
                //con = new SqlConnection(constr);
                //SqlCommand cmd = new SqlCommand("sp_shapefile_queue", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@filename", filename);
                //cmd.Parameters.AddWithValue("@boundary", boundary);
                //cmd.Parameters.AddWithValue("@user_id", user_id);
                //cmd.Parameters.AddWithValue("@date", formattedDate);
                //con.Open();
                //int i = cmd.ExecuteNonQuery();
                //con.Close();
                //if (i > 0)
                //{
                //    mgs = "Success";
                //}

            }
            catch (Exception EX)
            {
                mgs = "Error";


            }
            return mgs;
        }


    }
}