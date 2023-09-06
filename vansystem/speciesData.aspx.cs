using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace vansystem
{
    public partial class speciesData : System.Web.UI.Page
    {
        clsJson objjson = new clsJson();
        NameValueCollection nvc = new NameValueCollection();
        clsConnnection clscon = new clsConnnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                nvc.Clear();
                nvc.Add("@Operation", "GetallHabits");

                DataTable dthabits = clscon.fnExecuteProcedureSelectWithCondtion("[VanIT].[dbo].[SpDataUpload]", nvc);

                if (dthabits.Rows.Count > 0 && dthabits != null)
                {

                    load(dthabits);

                }

                nvc.Clear();
                nvc.Add("@Operation", "GetSpeciescount");
                nvc.Add("@Userid", "adilabad@van");
                DataTable dtcheck = clscon.fnExecuteProcedureSelectWithCondtion("[VanIT].[dbo].[SpDataUpload]", nvc);

                if (dtcheck.Rows[0]["count"].ToString() != "0" && dtcheck.Rows[0]["count"].ToString() != null || dtcheck.Rows[0]["count"].ToString() != "null")
                {
                    alreadyuploaded.Visible = true;
                    toupload.Visible = false;
                }
                else
                {
                    alreadyuploaded.Visible = false;
                    toupload.Visible = true;
                    loadtopMostParentList();
                }

            }
        }

        public static string Allhabits = string.Empty;
        protected string load(DataTable dthabits)
        {
            if (dthabits.Rows.Count > 0 && dthabits != null)
            {
                Allhabits = string.Empty;

                for (int i = 0; i < dthabits.Rows.Count; i++)
                {
                    if (Allhabits == "")
                    {
                        Allhabits = "<tr> <td>" + dthabits.Rows[i]["name"].ToString() + "</td><td>" + dthabits.Rows[i]["id"].ToString() + "</td><td>" + dthabits.Rows[i]["type"].ToString() + "</td></tr>";
                    }
                    else
                    {
                        Allhabits += "<tr> <td>" + dthabits.Rows[i]["name"].ToString() + "</td><td>" + dthabits.Rows[i]["id"].ToString() + "</td><td>" + dthabits.Rows[i]["type"].ToString() + "</td></tr>";
                    }
                }

            }

            return Allhabits;
        }

        public static string topMostParentList = string.Empty;
        protected string loadtopMostParentList()
        {
            topMostParentList = "";
            nvc.Clear();
            nvc.Add("@Operation", "GetUserdetails");
            nvc.Add("@Userid", "adilabad@van");
            nvc.Add("@Password", "van@adilabad");

            DataTable dtuser = clscon.fnExecuteProcedureSelectWithCondtion("[VanIT].[dbo].[SpDataUpload]", nvc);
            topMostParentName.InnerText = dtuser.Rows[0]["name"].ToString();
            if (dtuser.Rows.Count > 0 && dtuser != null)
            {
                float statecode = (float)Convert.ToDouble(dtuser.Rows[0]["state_code"]);
                nvc.Clear();
                nvc.Add("@Operation", "Getforest_boundary_hierarchy");
                nvc.Add("@state_code", statecode.ToString());
                DataTable dttables = clscon.fnExecuteProcedureSelectWithCondtion("[VanIT].[dbo].[SpDataUpload]", nvc);
                if (dttables.Rows.Count > 0 && dttables != null)
                {
                    for (int i = 0; i < dttables.Rows.Count; i++)
                    {
                        string value = dttables.Rows[i]["name"].ToString();
                        if (topMostParentList == "")
                        {

                            topMostParentList = "<option value=" + value + ">" + value + "</option><br>";
                        }
                        else
                        {
                            topMostParentList += "<option value=" + value + ">" + value + "</option><br>";
                        }
                    }
                }
            }

            return topMostParentList;
        }





        protected void upload_Click(object sender, EventArgs e)
        {
            string filePath = ConfigurationManager.AppSettings["filePath"].ToString();
            string[] filePaths = Directory.GetFiles(filePath);
            DataTable dt = new DataTable();
            foreach (string file in filePaths)
            {

                string csvData;
                using (StreamReader sr = new StreamReader(file))
                {
                    csvData = sr.ReadToEnd().ToString();
                    string[] row = csvData.Split('\n');
                    for (int i = 0; i < row.Count() - 1; i++)
                    {
                        string[] rowData = row[i].Split(',');
                        {
                            if (i == 0)
                            {
                                for (int j = 0; j < rowData.Count(); j++)
                                {
                                    dt.Columns.Add(rowData[j].Trim());
                                }
                            }
                            else
                            {
                                DataRow dr = dt.NewRow();
                                for (int k = 0; k < rowData.Count(); k++)
                                {
                                    dr[k] = rowData[k].ToString();
                                }
                                dt.Rows.Add(dr);
                            }
                        }
                    }


                }

                int Count = 0;
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    string habitype = dt.Rows[i]["habit type"].ToString().ToLower();
                    nvc.Add("@Operation", "GetHabitId");
                    nvc.Add("@habitType", habitype);
                    DataTable dthabit = clscon.fnExecuteProcedureSelectWithCondtion("[VanIT].[dbo].[SpDataUpload]", nvc);
                    if (dthabit.Rows.Count > 0 && dthabit != null)
                    {
                        string habitid = dthabit.Rows[0]["id"].ToString();

                        nvc.Add("@Operation", "InsertSpecies");
                        nvc.Add("@Userid", "adilabad@van");
                        nvc.Add("@parent_id", "adilabad@van");
                        nvc.Add("@habit_id", habitid);
                        nvc.Add("@species_id", dt.Rows[i]["species id"].ToString());
                        nvc.Add("@localname", dt.Rows[i]["local name"].ToString());
                        nvc.Add("@scientificname", dt.Rows[i]["scientific name"].ToString());


                        DataTable dtspecies = clscon.fnExecuteProcedureSelectWithCondtion("[VanIT].[dbo].[SpDataUpload]", nvc);
                        Count += Count;
                    }
                }

                if (Count > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "setFlash(['Species data upload successfull.','success'])", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "setFlash(['Error: Invalid file structure. Please check column names and thier order.','danger']);", true);
                }

            }
        }

        protected void deleteSpeciesData_Click(object sender, EventArgs e)
        {
            nvc.Add("@Operation", "DeleteSpecies");
            nvc.Add("@Userid", "adilabad@van");

            DataTable dtdelete = clscon.fnExecuteProcedureSelectWithCondtion("[VanIT].[dbo].[SpDataUpload]", nvc);
            if (dtdelete.Rows.Count > 0 && dtdelete != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "setFlash(['Species data upload successfull.','success'])", true);
            }

        }
    }
}