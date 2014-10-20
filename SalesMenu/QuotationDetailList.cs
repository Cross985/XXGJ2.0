using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;

namespace SalesMenu
{
    /// <summary>
    /// 报价
    /// </summary>
    public class QuotationDetail : Sage.CRM.WebObject.ListPage
    {
        public QuotationDetail()
            : base("QuotationDetail", "QuotationDetailGrid", "QuotationDetailSearchBox")
        {
            this.ResultsGrid.RowsPerScreen = 50;
            this.ResultsGrid.Filter = "qtdt_deleted is null";

        }

        public override void GetTabs()
        {
            string strTopContent = "<TABLE WIDTH=100%><TR><TD CLASS=TOPHEADING WIDTH=60><IMG SRC='" + CurrentUser.VirtualImgPath() + "Icons/Opportunity.gif' BORDER=0 ALIGN=MIDDLE></td><td CLASS=VIEWBOXCAPTION><SPAN CLASS=TOPCAPTION>报价</SPAN></td></tr></table>";
            AddTopContent(strTopContent);
            GetTabs("SalesManagement", "QuotationDetail");
        }

        public override void AddNewButton()
        {
           
        }
    }

}

