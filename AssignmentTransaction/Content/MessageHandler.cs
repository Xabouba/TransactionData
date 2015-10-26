using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace AssignmentTransaction.Content
{
    abstract class MessageHandler
    {
        public static void HandleMsg(HtmlGenericControl control, string _class, string _msg)
        {
            control.Style.Value = "display:block;";
            control.Attributes.Add("class", _class);
            control.InnerHtml = _msg;
        }
    }
}