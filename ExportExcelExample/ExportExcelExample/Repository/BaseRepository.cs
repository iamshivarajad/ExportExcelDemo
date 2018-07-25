using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ExportExcelExample.Repository
{
    public abstract class BaseRepository
    {
        protected string connectionString = WebConfigurationManager.ConnectionStrings["collegeDB"].ConnectionString;
    }
}