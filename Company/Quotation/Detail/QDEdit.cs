﻿using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;
using Sage.CRM.Data;
using Sage.CRM.UI;


namespace Quotation
{
    public class QDEdit : Web
    {
        public override void BuildContents()
        {
            try
            {
                AddContent(HTML.Form());
                string qdid = Dispatch.EitherField("qtdt_QuotationDetailID");
                EntryGroup QDEntry = new EntryGroup("QuotationDetailNewEntry");
                QDEntry.Title = "报价明细";
                Record QDRec = FindRecord("QuotationDetail", "qtdt_QuotationDetailID=" + qdid);
                string qutaid = Dispatch.EitherField("quta_Quotationid");
                if (string.IsNullOrEmpty(qutaid))
                    qutaid = QDRec.GetFieldAsString("qtdt_qutaID");
                Record QutaRec = FindRecord("Quotation", "quta_Quotationid=" + qutaid);
                string exchange = QutaRec.GetFieldAsString("quta_exchange");
                if (string.IsNullOrEmpty(exchange) || exchange == "0")
                    exchange = "1";
                AddContent(HTML.InputHidden("exchange", exchange));
                string currency = QutaRec.GetFieldAsString("quta_currencysid");
                if (!string.IsNullOrEmpty(currency))
                {
                    Record currRec = FindRecord("Currencys", "curr_CurrencysId=" + currency);
                    string currname = currRec.GetFieldAsString("curr_des");
                    AddContent(HTML.InputHidden("currency", currname));
                }

               
                string hMode = Dispatch.EitherField("HiddenMode");
                
                int errflag = 0;
                AddTabHead("QuotationDetail");
                //GetTabs("QuotationDetail", "QuotationDetail Summary");
                if (hMode == "Save")
                {                    
                    QDEntry.Fill(QDRec);
                    if (QDEntry.Validate() == true)
                    {
                        QDRec.SetField("quta_Quotationid", qutaid);

                        QDRec.SaveChanges();
                        QuerySelect qs = this.GetQuery();
                        string qtdtid = QDRec.RecordId.ToString();
                        double thislocalaount = Convert.ToDouble(Dispatch.ContentField("qtdt_localeamount"));
                        qs.SQLCommand = "exec crm_UpdateOpportunityQutaPrice @quotationid=" + qutaid;
                        qs.ExecuteNonQuery();
//                        string productinfoid = Dispatch.ContentField("qtdt_productinfoid");
//                        double thislocalaount = Convert.ToDouble(Dispatch.ContentField("qtdt_localeamount"));
//                        double thisforeignamount = Convert.ToDouble(Dispatch.ContentField("qtdt_foreignamount"));
//                        string select = @"select qtdt_localeamount,qtdt_foreignamount,quta_opportunityid from  QuotationDetail where qtdt_UpdatedDate =
//                        (select MAX( qtdt_UpdatedDate) from QuotationDetail 
//                        left join Quotation on quta_QuotationID = qtdt_qutaid
//                        left join Opportunity on Oppo_OpportunityId = quta_opportunityid  
//                        where qtdt_productinfoid = " + productinfoid + @" and quta_opportunityid = 12 and Oppo_Deleted is null and quta_Deleted is null and qtdt_Deleted is null) ";
//                        qs.SQLCommand = select;
//                        qs.ExecuteReader();
//                        double qtdt_localeamount = 0, qtdt_foreignamount = 0;
//                        string quta_opportunityid = string.Empty;
//                        if (!qs.Eof())
//                        {
//                            qtdt_localeamount = Convert.ToDouble(qs.FieldValue("qtdt_localeamount"));
//                            qtdt_foreignamount = Convert.ToDouble(qs.FieldValue("qtdt_foreignamount"));
//                            quta_opportunityid = qs.FieldValue("quta_opportunityid");
//                        }
//                        double resultlocal = thislocalaount - qtdt_localeamount;
//                        double resultforeign = thisforeignamount = qtdt_foreignamount;
//                        string updatesql = @"update Opportunity set oppo_qutaprice = ISNULL(oppo_qutaprice,0) + " + resultlocal + " where Oppo_OpportunityId =" + quta_opportunityid;
//                        qs.SQLCommand = updatesql;
//                        qs.ExecuteNonQuery();


                        //QuerySelect qs = this.GetQuery();
                        qs.SQLCommand = @"Update Quotation set quta_localeamount = (select sum(qtdt_localeamount) from QuotationDetail where qtdt_deleted is null and qtdt_qutaid= " + qutaid + @")
                        ,quta_foreignamount =  (select sum(qtdt_foreignamount) from QuotationDetail where qtdt_deleted is null and qtdt_qutaid= " + qutaid + @")    where quta_Quotationid=" + qutaid;
                        qs.ExecuteNonQuery();
                        Dispatch.Redirect(UrlDotNet(ThisDotNetDll, "RunDataPage") + "&quta_Quotationid=" + qutaid);
                    }

                }
                if (errflag != -1)
                {
                    AddContent(HTML.InputHidden("HiddenMode", ""));
                    QDEntry.GetHtmlInEditMode(QDRec);
                    VerticalPanel vp = new VerticalPanel();
                    vp.AddAttribute("width", "100%");
                    vp.Add(QDEntry);
                    AddContent(vp);

                    string url = "javascript:document.EntryForm.HiddenMode.value='Save';";
                    AddSubmitButton("Save", "Save.gif", url);
                    AddUrlButton("Delete", "Delete.gif", UrlDotNet(ThisDotNetDll, "RunQDDelete") + "&quta_Quotationid=" + qutaid + "&qtdt_QuotationDetailID=" + qdid);
                   
                    AddUrlButton("Cancel", "cancel.gif", UrlDotNet(ThisDotNetDll, "RunDataPage") + "&quta_Quotationid=" + qutaid);
                }

            }
            catch (Exception ex)
            {
                AddError(ex.Message + "USAddPage");
            }
        }

    }
}
