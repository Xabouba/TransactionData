using AssignmentTransaction.Content;
using AssignmentTransaction.Content.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AssignmentTransaction
{
    public partial class UploadFile : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadCsvDataToDatabase(object sender, EventArgs e)
        {
            try
            {
                UploadCsvFile();
            }
            catch (Exception ex)
            {
                HandleMsg(divMessage,"error", "Error! " + ex.Message);

            }
        }


        private void UploadCsvFile()
        {
            string errors = "";
            int i = 0, j = 0;
            FileUpload inputFile = flucsv;
            if (inputFile.HasFile)
            {
                string contentType = inputFile.PostedFile.ContentType.ToLower();
                var Extension = inputFile.FileName.Substring(inputFile.FileName.IndexOf('.') + 1).ToLower();
                if (Extension == "csv")
                {
                    DataTable csvData = DataAccess.InitDataTable();

                    using (StreamReader reader = new StreamReader(File.OpenRead(Server.MapPath("~/CSV/" + Path.GetFileName(inputFile.FileName)))))
                    {
                        
                        while (!reader.EndOfStream)
                        {
                            i++;
                            var line = reader.ReadLine();
                            var values = line.Split(',');


                            int test = ErrorCode.TestCSVLine(values.ToArray());
                            if (test == 0)
                            {
                                DataRow dataRow = csvData.NewRow();
                                // Use Trim to remove blank spaces
                                dataRow["Account"] = values[0].Trim();
                                dataRow["Description"] = values[1].Trim();
                                dataRow["CurrencyCode"] = values[2].Trim();
                                dataRow["Amount"] = values[3].Trim();
                                csvData.Rows.Add(dataRow);
                            }
                            else
                            {
                                j++;
                                errors += "Error on row " + i + ": " + ErrorCode.TestIntDetails(test) + "! <br/>";
                            }
                        }
                        DataAccess.InsertDataIntoSQLServerUsingSQLBulkCopy(csvData);
                    }

                    if (i > 0){
                        HandleMsg(divSuccess, "message-success", "Results: " + (i - j) + "/" + i +" rows correctly imported!");
                    }
                    if (j > 0)
                    {
                        HandleMsg(divMessage,"message-error", errors);
                    }
                }
                else
                {
                    HandleMsg(divMessage,"error", "Please select a valid CSV file!!");
                }
            }
        }
        private void HandleMsg(HtmlGenericControl control, string _class, string _msg)
        {
            control.Style.Value = "display:block;";
            control.Attributes.Add("class", _class);
            control.InnerHtml = _msg;
        }
    }
}