using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EGIS.Controls;
using EGIS.ShapeFileLib;
using EGIS.Projections;
using System.Configuration;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Web.UI.WebControls;
using vansystem.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Collections;
using Microsoft.Ajax.Utilities;

namespace vansystem.Models
{
    public class shapefileconverter:clsConnnection
    {

        public string toShapeToDataBase(string shapefilename, string extractPath)
        {
            string msg = string.Empty;
            DataTable dttemp = new DataTable();
            //DataTable table = new DataTable("MyDataTable");
            try
            {


                //string myShapeFile = ConfigurationManager.ConnectionStrings["Root"].ConnectionString;
                //string path = myShapeFile + "vw_division_achampet";
                string paths = extractPath + shapefilename;


                ShapeFile obj = new ShapeFile(paths);
                obj.Name = shapefilename;
                string[] attributeName = obj.GetAttributeFieldNames();
                //string[] attributeFields = obj.GetAttributeFieldValues(1);
                List<string> columnXValues = new List<string>();

                string coloumns = "";

                for (int i = 0; i < attributeName.Count(); i++)
                {
                    coloumns += "," + attributeName[i] + " nvarchar(max)";
                }

                dttemp.Columns.Clear();
                dttemp.Rows.Clear();

                DataColumn workColumn = dttemp.Columns.Add("ObjectId", typeof(Int32));
                workColumn.AutoIncrement = true;
                workColumn.AutoIncrementSeed = 1;
                workColumn.AutoIncrementStep = 1;
                dttemp.PrimaryKey = new DataColumn[] { dttemp.Columns["ObjectId"] };
                if (attributeName.Length > 0)
                {
                    foreach (string xname in attributeName)
                    {
                        columnXValues.Add(xname);
                        dttemp.Columns.Add(xname);

                    }

                }

                //for (int i = 0; i < attributeName.Length; i++)
                //{
                //    string[] s = obj.GetRecords(i);
                //    if (s.Length > 0)
                //    {



                //        table.Columns.Add(attributeName[i]);
                //        DataColumn workColumn1 = table.Columns.Add("ObjectId", typeof(Int32));
                //        workColumn1.AutoIncrement = true;
                //        workColumn1.AutoIncrementSeed = 1;
                //        workColumn1.AutoIncrementStep = 1;
                //        table.PrimaryKey = new DataColumn[] { table.Columns["ObjectId"] };

                //        foreach (string value in s)
                //        {



                //            table.Rows.Add(value);

                //        }

                //        dttemp.Merge(table);

                //    }

                //}


                //table.Columns.Add("Geom_txt");

                //ShapeFileEnumerator sfEnum = obj.GetShapeFileEnumerator();

                //for (int r = 0; r < obj.RecordCount; ++r)//while (sfEnum.MoveNext())
                //{

                //    var location = obj.GetShapeDataD(r);
                //    int k = 0;
                //    foreach (PointD[] value in location)
                //    {

                //        string x = value[k].X.ToString();
                //        string y = value[k].Y.ToString();
                //        string xy = y + ":" + x;

                //        table.Rows.Add(xy);

                //        k = k + 1;
                //    }


                //}
                ////dttemp.Merge(table);
                RectangleD[] location = obj.GetShapeExtentsD();

                if (location.Length > 0)
                {
                    DataTable table = new DataTable("MyDataTable");
                    table.Columns.Add("Geom_txt");
                    DataColumn workColumn1 = table.Columns.Add("ObjectId", typeof(Int32));
                    workColumn1.AutoIncrement = true;
                    workColumn1.AutoIncrementSeed = 1;
                    workColumn1.AutoIncrementStep = 1;
                    table.PrimaryKey = new DataColumn[] { table.Columns["ObjectId"] };

                    foreach (RectangleD value in location)
                    {

                        string x = value.X.ToString();
                        string y = value.Y.ToString();
                        string xy = y + ":" + x;

                        table.Rows.Add(xy);

                    }

                    dttemp.Merge(table);


                }
                //ShapeFile sf = new ShapeFile("D:\\Division-shapefilezip\\Divisions\\extracted\\1734251765\\vw_division_achampet.shp");
                //DbfReader dbfReader = new DbfReader("D:\\Division-shapefilezip\\Divisions\\extracted\\1734251765\\vw_division_achampet.dbf");

                ////create a new ShapeFileWriter
                //ShapeFileWriter sfw;
                //sfw = ShapeFileWriter.CreateWriter(".", "highways", sf.ShapeType,
                //    dbfReader.DbfRecordHeader.GetFieldDescriptions());
                //try
                //{
                //    // Get a ShapeFileEnumerator from the shapefile
                //    // and read each record
                //    ShapeFileEnumerator sfEnum = sf.GetShapeFileEnumerator();
                //    while (sfEnum.MoveNext())
                //    {
                //        // get the raw point data
                //        PointD[] points = sfEnum.Current[0];
                //        //get the DBF record
                //        string[] fields = dbfReader.GetFields(sfEnum.CurrentShapeIndex);
                //        //check whether to add the record to the new shapefile.
                //        //(in this example, field zero contains the road type)
                //        if (string.Compare(fields[0].Trim(), "61", true) == 0)
                //        {
                //            //sfw.AddRecord(points, points.Length, fields);
                //            table.Rows.Add(points);
                //        }
                //    }
                //}
                //finally
                //{
                //    //close the shapefile, shapefilewriter and dbfreader
                //    sfw.Close();
                //    sf.Close();
                //    dbfReader.Close();
                //}
                if (dttemp.Rows.Count > 0)
                {
                    string msg1 = CreateTable(coloumns, shapefilename);
                    if (msg1 == "Success")
                    {
                        string msg2 = Bulkinsert(dttemp, shapefilename);
                    }
                }




            }
            catch (Exception ex)
            {
            }
            return msg;
        }

        public string CreateTable(string coloumnnames, string tablename)
        {
            string mgs = string.Empty;
            fnOpenConnection();
            objCom = new SqlCommand("[dbo].[sp_drop_table_if_exists]", objCon);
            objCom.CommandType = CommandType.StoredProcedure;
            objCom.Parameters.AddWithValue("@Operation", "CreateTable");
            objCom.Parameters.AddWithValue("@table_name", tablename);
            objCom.Parameters.AddWithValue("@coloumns", coloumnnames);



            try
            {
                objCom.CommandTimeout = 0;
                objCom.ExecuteNonQuery();

                fnCloseConnection();
                mgs = "Success";
            }
            catch (Exception ex)
            {
                fnCloseConnection();
                mgs = "Error";
            }
            return mgs;
        }

        public string Bulkinsert(DataTable dt, string tablename)
        {
            string mgs = string.Empty;
            try
            {
                fnOpenConnection();
                SqlBulkCopy objbulk = new SqlBulkCopy(objCon);
                objbulk.DestinationTableName = tablename;
                objbulk.WriteToServer(dt);
                fnCloseConnection();
                mgs = "Success";
            }
            catch (Exception EX)
            {
                mgs = "Error";

                fnCloseConnection();
            }
            return mgs;
        }
        public String CheckTableExistis(string tableName)
        {
            string msgs = string.Empty;



            fnOpenConnection();
            objCom = new SqlCommand("[dbo].[sp_drop_table_if_exists]", objCon);
            objCom.CommandType = CommandType.StoredProcedure;
            objCom.Parameters.AddWithValue("@Operation", "Droptable");
            objCom.Parameters.AddWithValue("@table_name", tableName);




            try
            {
                objCom.CommandTimeout = 0;
                objCom.ExecuteNonQuery();

                fnCloseConnection();
                msgs = "Success";
            }
            catch (Exception ex)
            {
                fnCloseConnection();
                msgs = "Error";
            }


            return msgs;

        }



    }
}