using ClosedXML.Excel;
using ExportExcelExample.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExportExcelExample.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExportToExcel()
        {
            var sheetNames = new List<string>() { "CSE", "ISE", "EC", "ME" };
            string fileName = "Department.xlsx";

            EmployeeRepository employeeRepository = new EmployeeRepository();
            DataSet ds = employeeRepository.GetDataSetExportToExcel();

            XLWorkbook wbook = new XLWorkbook();

            for (int k = 0; k < ds.Tables.Count; k++)
            {
                DataTable dt = ds.Tables[k];
                IXLWorksheet Sheet = wbook.Worksheets.Add(sheetNames[k]);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Sheet.Cell(1, (i + 1)).Value = dt.Columns[i].ColumnName;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Sheet.Cell((i + 2), (j + 1)).Value = dt.Rows[i][j].ToString();
                    }
                }
            }

            Stream spreadsheetStream = new MemoryStream();
            wbook.SaveAs(spreadsheetStream);
            spreadsheetStream.Position = 0;

            return new FileStreamResult(spreadsheetStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = fileName };
        }

    }
}