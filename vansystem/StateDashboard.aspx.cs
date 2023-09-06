using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vansystem
{
    public partial class StateDashboard : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringStr"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTabels();
               
            }
        }

        private void GetTabels()
        {
            string username = Session["user_id"].ToString();
            string user_uuid = Session["uuid"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("getdivision"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@operation", "GetDivision");
                        cmd.Parameters.AddWithValue("@f_id", 4);
                        cmd.Parameters.AddWithValue("@user_id", username);
                        cmd.Parameters.AddWithValue("@uuid", user_uuid);

                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 120;



                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVDivision.DataSource = dt;
                            GVDivision.DataBind();
                        }


                       
                    }
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static List<object> loadDivisionStatus(string total_plots, string total_surveyed, string total_pending)
        {
            DBErrorLog db = new DBErrorLog();
            string strQUery = "select count(distinct pm.[PlotId]) as total_plots, count(distinct sm.plot_id) as total_surveyed,(count(distinct pm.[PlotId]) - count(distinct sm.plot_id)) as pending_plots from [dbo].[tblPlot] as pm inner join [dbo].[tblCompartment] as cm on cm.CompartmentId = pm.CompartmentId inner join [dbo].[tblBlock] as bm on bm.BlockId = cm.BlockId inner join [dbo].[tblRange] as rm on rm.RangeId = bm.RangeId inner join [dbo].[tblDivision] as dm on dm.DivisionId = rm.DivisionID inner join user_division_relation as ud on ud.division_id = dm.DivisionId and ud.user_uuid = user_uuid left join survrymaster as sm on sm.plot_id = pm.[PlotId] and sm.f_id in (2, 3) group by dm.DivisionId,dm.DivisionName order by dm.DivisionId,dm.DivisionName";
            DataSet ds = db.getResultset(strQUery, "", "", "");
            DataTable dtEQ = new DataTable();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dtEQ = ds.Tables[0];
            }


            List<object> Status = new List<object>();
            Status.Add(new object[]
            {
                "Division Status","Total"
            });

            if (dtEQ.Rows.Count > 0)
            {
                for (int i = 0; i < dtEQ.Rows.Count; i++)
                {
                    Status.Add(new object[]
                    {
                        dtEQ.Rows[i]["Status"].ToString(),
                        Convert.ToDecimal(dtEQ.Rows[i]["Total"].ToString()),

                    });
                }
            }
            else
            {
                Status.Add(new object[]
                {
                    "Division Status",0
                });
            }

            return Status;
        }



    }
}