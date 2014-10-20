using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;
using Sage.CRM.Data;
using Sage.CRM.UI;

namespace Quotation.DataPages
{
    public class QuotationDataPage : Web
    {
        public override void BuildContents()
        {
            try
            {
                AddContent(HTML.Form());
                string qutaid = Dispatch.EitherField("quta_Quotationid");
                if (string.IsNullOrEmpty(qutaid))
                    qutaid = Dispatch.EitherField("key58");
                
                EntryGroup qutacompEntry = new EntryGroup("QuotationCompanyEntry");
                qutacompEntry.Title = "商机客户";
                EntryGroup qutaEntry = new EntryGroup("QuotationNewEntry");
                qutaEntry.Title = "报价信息";
                Record qutaRec = FindRecord("Quotation", "quta_Quotationid=" + qutaid);
                qutacompEntry.Fill(qutaRec);
                qutaEntry.Fill(qutaRec);
                GetTabs("Quotation", "Quotation Summary");

               
                List UseList = new List("QuotationDetailGrid");
                UseList.Filter = "qtdt_deleted is null and qtdt_qutaid =" + qutaid;
                UseList.PadBottom = false;
                UseList.RowsPerScreen = 50;
                qutacompEntry.GetHtmlInViewMode(qutaRec);
                qutaEntry.GetHtmlInViewMode(qutaRec);
                VerticalPanel vp = new VerticalPanel();
                vp.AddAttribute("width", "100%");
                vp.Add(qutacompEntry);
                vp.Add(qutaEntry);
                vp.Add(UseList);
              
                AddContent(vp);
                //string quta_opportunityid = qutaRec.GetFieldAsString("quta_opportunityid");
                //if (!string.IsNullOrEmpty(quta_opportunityid))
                //{
                //    QuerySelect qs = this.GetQuery();
                //    qs.SQLCommand = "Update Opportunity set oppo_qutaprice= (select sum(quta_localeamount) from Quotation where quta_deleted is null and quta_updateoppo = 'Y' and quta_opportunityid = " + quta_opportunityid + " ) where oppo_Opportunityid =" + quta_opportunityid;
                //    qs.ExecuteNonQuery();
                //}
                AddUrlButton("Edit", "Edit.gif", UrlDotNet(ThisDotNetDll, "RunDataPageEdit") + "&quta_Quotationid=" + qutaid);
                AddUrlButton("添加报价明细", "New.gif", UrlDotNet(ThisDotNetDll, "RunQDAdd") + "&quta_Quotationid=" + qutaid);
                string url = string.Empty;
                string oppoid = Dispatch.EitherField("key7");
                if (string.IsNullOrEmpty(oppoid))
                    url = UrlDotNet("SalesMenu", "RunQuotation") + "&J=Quotation&T=SalesManagement";
                else
                    url = UrlDotNet(ThisDotNetDll, "RunListPage") + "&J=OppoTrack&T=Opportunity";
                AddUrlButton("Cancel", "Cancel.gif", url);
                AddWorkflowButtons("Quotation");
            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
    }
}