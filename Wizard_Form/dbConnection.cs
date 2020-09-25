using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Wizard_Form
{
    class dbConnection
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        private string con;
        public string MyConnection()
        {
            con = @"Data Source = .; Initial Catalog = WizardForm; UID = sa; PWD = 123;";
            return con;
        }
    }

    
}
