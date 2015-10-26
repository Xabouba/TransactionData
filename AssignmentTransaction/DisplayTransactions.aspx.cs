using AssignmentTransaction.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssignmentTransaction
{
    public partial class DisplayTransactions : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        /// <summary>
        /// Webmethod to return listof all transaction data
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<Dictionary<string, object>> GetAllTransaction()
        {
            return DataAccess.GetListTransactionData();
        }
    }
}