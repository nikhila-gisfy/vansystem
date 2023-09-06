using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace vansystem
{
    public partial class SpeciesData_1 : System.Web.UI.Page
    {
        SqlConnection con;

        string sqlconn;
        string x1;
        string x2;
        protected void Page_Load(object sender, EventArgs e)
        {
            Load_Username();

        }
        private void connection()
        {
            sqlconn = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
            con = new SqlConnection(sqlconn);
        }

        protected void uploaddata_ServerClick(object sender, EventArgs e)
        {
            if (csv_file.HasFile)
            {

                // Get the filename and extension of the uploaded file
                string myShapeFile = ConfigurationManager.ConnectionStrings["Speciesfile"].ConnectionString;

                string fileName = System.IO.Path.GetFileName(csv_file.FileName);
                string fileExtension = System.IO.Path.GetExtension(fileName);
                string fname = string.Empty;


                // Create a unique filename to avoid overwriting existing files
                //string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                // Specify the folder where you want to save the uploaded file
                //string folderPath = Server.MapPath("~/D:\\savefilesfromcode/");
                string filename1 = fileName.Replace(".csv", "");
                fname = myShapeFile + filename1 + fileExtension;



                // Create the folder if it does not exist


                if (!File.Exists(fname))
                {

                    csv_file.SaveAs(fname);
                    GetTable(fname);

                }

                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('File already exists!');", true);

                }
            }
        }
        public void GetTable(string path)
        {
            {
                DataTable tblcsv = new DataTable();

                //creating columns
                //tblcsv.Columns.Add("SpeciesId");
                tblcsv.Columns.Add("Habitid");
                tblcsv.Columns.Add("Localname");
                tblcsv.Columns.Add("Scientificname");
                //tblcsv.Columns.Add("Category");
                //tblcsv.Columns.Add("MinGirth");


                //getting full file path of Uploaded file  
                string CSVFilePath = path;
                //Reading All text  
                string ReadCSV = File.ReadAllText(CSVFilePath);



                //spliting row after new line  
                foreach (string csvRow in ReadCSV.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(csvRow))
                    {

                        //Adding each row into datatable  
                        tblcsv.Rows.Add();
                        int count = 0;
                        foreach (string FileRec in csvRow.Split(','))
                        {
                            tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;



                            count++;
                        }
                    }
                }
                tblcsv.Rows.RemoveAt(0);

                //GetMaxBeforeInsert();
                //Calling insert Functions  
                InsertCSVRecords(tblcsv);

            }
        }
        //Function to Insert Records  
        private void InsertCSVRecords(DataTable csvdt)
        {

            connection();
            //creating object of SqlBulkCopy    
            System.Data.SqlClient.SqlBulkCopy objbulk = new System.Data.SqlClient.SqlBulkCopy(con);
            //assigning Destination table name    
            objbulk.DestinationTableName = "tblSpecies_temp";

            //Mapping Table column
            //objbulk.ColumnMappings.Add("SpeciesId", "SpeciesId");
            objbulk.ColumnMappings.Add("Habitid", "Habitid");
            objbulk.ColumnMappings.Add("Localname", "Localname");
            objbulk.ColumnMappings.Add("Scientificname", "Scientificname");
            //objbulk.ColumnMappings.Add("Category", "Category");
            //objbulk.ColumnMappings.Add("MinGirth", "MinGirth");


            //Sobjbulk.ColumnMappings.Add("StateId", "StateId");
            //inserting Datatable Records to DataBase    
            con.Open();
            objbulk.WriteToServer(csvdt);
            con.Close();
            //GetMaxAfterInsert();
            //Update_Division();

            InsertIntoMainSpecies();

        }
       
        private void Load_Username()
        {
            //string username = Session["user_id"].ToString();
            lbldivision.Text = "Welcome " + "admin";

        }
       private void InsertIntoMainSpecies()
       {
            connection();
            SqlCommand cmdInsertData = new SqlCommand("sp_speciestemptable", con);
            cmdInsertData.CommandType = CommandType.StoredProcedure;
            cmdInsertData.Parameters.AddWithValue("@operation", "insert");
            cmdInsertData.Parameters.AddWithValue("@table_name", "tblSpecies_temp");
            con.Open();
            cmdInsertData.ExecuteNonQuery();
            con.Close();
       }
    }
}