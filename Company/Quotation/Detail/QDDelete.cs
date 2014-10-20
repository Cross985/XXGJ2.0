﻿using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Data;
using Sage.CRM.Controls;

namespace Quotation
{
    public class QuotationDetailDelete : Web
    {
        //    public QuotationDetailDataPageDelete()
        //        : base("Follow", "foll_FollowID", "FollowNewEntry")
        //    {
        //        this.CancelButton = false;
        //        this.DeleteButton = false;
        //    }

        public override void BuildContents()
        {
            try
            {
                AddContent(HTML.Form());
                string qutaid = Dispatch.EitherField("quta_Quotationid");
                if (string.IsNullOrEmpty(qutaid))
                    qutaid = Dispatch.EitherField("key37");
                string QuotationDetailid = Dispatch.EitherField("qtdt_QuotationDetailID");
                Record rec = FindRecord("QuotationDetail", "qtdt_QuotationDetailID=" + QuotationDetailid);
                EntryGroup QuotationDetailentry = new EntryGroup("QuotationDetailNewEntry");
                QuotationDetailentry.Fill(rec);
                AddTabHead("QuotationDetail");
                //GetTabs("QuotationDetail", "QuotationDetail");
                string hMode = Dispatch.EitherField("HiddenMode");
                if (hMode == "Save")
                {
                    Record qutarec = FindRecord("Quotation", "quta_Quotationid=" + qutaid);
                    string oppoid = qutarec.GetFieldAsString("quta_opportunityid");
                    //Record rec =  base.FindCurrentRecord("QuotationDetail");
                    
                    double thislocal = rec.GetFieldAsDouble("qtdt_localeamount");
                    QuerySelect qs = this.GetQuery();
                    qs.SQLCommand = "Update Opportunity set oppo_qutaprice = case when (ISNULL(oppo_qutaprice," + thislocal + ")-" + thislocal + ") < 0 then 0 else (ISNULL(oppo_qutaprice," + thislocal + ")-" + thislocal + ") end  where Oppo_OpportunityId =" + oppoid;
                    qs.ExecuteNonQuery();

                    rec.DeleteRecord = true;
                    string code = rec.GetFieldAsString("qtdt_code");
                    rec.SaveChanges();
                    
                    //
                    qs.SQLCommand = "Update QuotationDetail set qtdt_code=qtdt_code-1 where qtdt_code >'" + code + "' and qtdt_deleted is null and qtdt_qutaid =" + qutaid;
                    qs.ExecuteNonQuery();

                    qs.SQLCommand = @"Update Quotation set quta_localeamount = (select sum(qtdt_localeamount) from QuotationDetail where qtdt_deleted is null and qtdt_qutaid= " + qutaid + @")
                        ,quta_foreignamount =  (select sum(qtdt_foreignamount) from QuotationDetail where qtdt_deleted is null and qtdt_qutaid= " + qutaid + @")    where quta_Quotationid=" + qutaid;
                    qs.ExecuteNonQuery();
                    Dispatch.Redirect(UrlDotNet(ThisDotNetDll, "RunDataPage") + "&quta_Quotationid=" + qutaid);
                }


                /* Add your code here */
                
                AddContent(HTML.InputHidden("HiddenMode", ""));
                QuotationDetailentry.GetHtmlInViewMode(rec);
                AddContent(QuotationDetailentry);
                AddSubmitButton("确认删除", "delete.gif", "javascript:document.EntryForm.HiddenMode.value='Save';");
                AddUrlButton("Cancel", "Cancel.gif", UrlDotNet(ThisDotNetDll, "RunDataPage") + "&quta_Quotationid=" + qutaid);

            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }

    }
}