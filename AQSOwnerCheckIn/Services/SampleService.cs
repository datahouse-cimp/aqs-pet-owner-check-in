using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AQSOwnerCheckIn.Controllers;
using AQSOwnerCheckIn.Extensions;
using AQSOwnerCheckIn.Models;
using Hansen.Billing;
using Microsoft.Ajax.Utilities;
using Hansen.CDR.Use;
using log4net;
using ILog = log4net.ILog;

namespace AQSOwnerCheckIn.Services
{
    public class SampleService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SampleService));

        public static async Task<TaskResult> SampleQuery(SampleController.SearchCriteria s)
        {
            Logger.Debug(string.Format("Method called."));

            string coreQuery = "SELECT C.CNTCTKEY 'ContactKey' FROM RESOURCES.CONTACT C " +
                "INNER JOIN RESOURCES.CNTCTID CNT ON CNT.IDKEY = C.IDKEY " +
                "WHERE CNT.NAMELAST LIKE '%' + @CRITERIA + '%'";

            using (var connection = new SqlConnection(InforConfig.IpsDatabaseConnectionString))
            {
                try
                {
                    Logger.Debug(string.Format("Execute SQL query: {0}", coreQuery));
                    var command = new SqlCommand(coreQuery, connection);
                    command.Parameters.AddWithValue("@CRITERIA", s.Criteria);

                    connection.Open();
                    var reader = command.ExecuteReader();

                    var records = new List<int>();

                    while (reader.Read())
                    {
                        if (!(reader["ContactKey"] is DBNull)) records.Add(Convert.ToInt32(reader["ContactKey"]));
                    }

                    // Select query successful
                    reader.Close();

                    var data = new
                    {
                        records
                    };

                    return TaskResult.Success(data);
                }
                catch (Exception e)
                {
                    // Log exception
                    return TaskResult.Failure(e.Message, e.StackTrace);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}