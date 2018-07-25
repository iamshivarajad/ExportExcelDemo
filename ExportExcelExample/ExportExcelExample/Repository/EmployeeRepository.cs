using ExportExcelExample.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ExportExcelExample.Repository
{
    public class EmployeeRepository : BaseRepository
    {
        public DataSet GetDataSetExportToExcel()
        {
            DataSet ds = new DataSet();
            var departments = new List<string>() { "CSE", "ISE", "EC", "ME" };

            foreach (var department in departments)
            {
                DataTable dt = new DataTable();
                dt = GetDataTableExportToExcel(department);
                ds.Tables.Add(dt);
            }

            return ds;
        }

        private DataTable GetDataTableExportToExcel(string departmentName)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("UspGetExcelData", con))
                {
                    var parameters = new List<StoredProcedureParameters>()
                    {
                        new StoredProcedureParameters(){Name = "@DepartmentName",Value= departmentName }
                    };

                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(parameter.Name, parameter.Value));
                    }

                    using (var sda = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

    }
}