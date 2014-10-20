using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;
using Sage.CRM.Data;
using Sage.CRM.UI;

namespace Quotation.DataPages
{
    public class QuotationDataPageDelete : Web
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
                string hMode = Dispatch.EitherField("HiddenMode");
                string quta_opportunityid = qutaRec.GetFieldAsString("quta_opportunityid");
                if (hMode == "Delete") {

                    qutaRec.DeleteRecord = true;
                    qutaRec.SaveChanges();
                    
                    QuerySelect qs = this.GetQuery();
                    if (!string.IsNullOrEmpty(quta_opportunityid))
                    {
                        qs.SQLCommand = "Update Opportunity set oppo_qutaprice= (select sum(quta_localeamount) from Quotation where quta_deleted is null and quta_updateoppo = 'Y' and quta_opportunityid = " + quta_opportunityid + " ) where oppo_Opportunityid =" + quta_opportunityid;
                        qs.ExecuteNonQuery();
                    }
                    qs.SQLCommand = "Update QuotationDetail set qtdt_deleted = '1' where qtdt_qutaid =" + qutaid;
                    qs.ExecuteNonQuery();
                    Dispatch.Redirect(UrlDotNet(ThisDotNetDll, "RunListPage") + "&J=Quotation&T=Opportunity");
                }



                AddContent(HTML.InputHidden("HiddenMode",""));
                qutacompEntry.GetHtmlInViewMode(qutaRec);
                qutaEntry.GetHtmlInViewMode(qutaRec);
                VerticalPanel vp = new VerticalPanel();
                vp.AddAttribute("width", "100%");
                vp.Add(qutacompEntry);
                vp.Add(qutaEntry);
                

                AddContent(vp);
                
                string url = string.Empty;
             
                url = UrlDotNet(ThisDotNetDll, "RunDataPage") + "&quta_Quotationid=" + qutaid + "&J=OppoTrack&T=Opportunity";
                AddSubmitButton("ConfirmDelete", "Delete.gif", "javascript:document.EntryForm.HiddenMode.value='Delete';");
                AddUrlButton("Cancel", "Cancel.gif", url);
            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
    }
}