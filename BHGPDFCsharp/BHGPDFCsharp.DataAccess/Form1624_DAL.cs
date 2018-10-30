using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace BHGPDFCsharp
{
    public class Form1624_DAL
    {

        string strConnectionString = System.Configuration.ConfigurationSettings.AppSettings.Get("ConnectionString");

        public DataSet Form1624(string strStartDate, string strEndDate)
        {

            SqlParameter[] parameters = new SqlParameter[1];
            System.Data.DataSet ds = new DataSet();
            //parameters[0] = new SqlParameter("@StartDate", SqlDbType.DateTime);
            //parameters[1] = new SqlParameter("@EndDate", SqlDbType.DateTime);

            if (strStartDate != null)
            {
                if (strStartDate.Length >= 0)
                {
                    parameters[0].Value = strStartDate.Trim();
                }
                else
                {
                    parameters[0].Value = string.Empty;
                }
            }
            if (strEndDate != null)
            {
                if (strEndDate.Length >= 0)
                {
                    parameters[1].Value = strEndDate.Trim();
                }
                else
                {
                    parameters[1].Value = string.Empty;
                }

            }
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Form1624_pdf_andy", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 6000;
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(ds);
                    connection.Close();

                }

                return ds;

            }

            catch (Exception ex)
            {
                throw new Exception("Error Occured while Retrieving the FORM data - " + ex.Message.ToString());
            }
            finally
            {

            }

        }
    }
}
