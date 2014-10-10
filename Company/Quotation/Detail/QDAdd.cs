﻿using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;
using Sage.CRM.Data;
using Sage.CRM.UI;


namespace Quotation
{
    public class QDAdd:Web
    {
        public override void BuildContents()
        {
            try
            {
                AddContent(HTML.Form());

                string qutaid = Dispatch.EitherField("quta_Quotationid");
                Record QutaRec = FindRecord("Quotation", "quta_Quotationid=" + qutaid);
                string exchange = QutaRec.GetFieldAsString("quta_exchange");
                if (string.IsNullOrEmpty(exchange) || exchange == "0")
                    exchange = "1";
                string currency = QutaRec.GetFieldAsString("quta_currencysid");
                if (!string.IsNullOrEmpty(currency))
                {
                    Record currRec = FindRecord("Currencys", "curr_CurrencysId=" + currency);
                    string currname = currRec.GetFieldAsString("curr_des");
                    AddContent(HTML.InputHidden("currency", currname));
                }

                
                AddContent(HTML.InputHidden("exchange", exchange));
                string hMode = Dispatch.EitherField("HiddenMode");
                EntryGroup QDEntry = new EntryGroup("QuotationDetailNewEntry");
                QDEntry.Title = "报价明细";
                int errflag = 0;
                AddTabHead("QuotationDetail");

                string productinfoid = Dispatch.ContentField("qtdt_productinfoid");
                
                if (!string.IsNullOrEmpty(productinfoid) && productinfoid != "0")
                {
                    Record prodRec = FindRecord("ProductInfo","pdin_ProductInfoId=" + productinfoid);
                    string Name = prodRec.GetFieldAsString("pdin_Name");
                    string Standard = prodRec.GetFieldAsString("pdin_standard");
                    //AddInfo(Name + "---" + Standard);
               
                    for (int i = 0; i < QDEntry.Count; i++)
                    {
                        string field = QDEntry[i].Name;
                        if (field == "qtdt_productname")
                            QDEntry[i].DefaultValue = Name;
                        else if (field == "qtdt_productname")
                            QDEntry[i].DefaultValue = Standard;
                        else
                            QDEntry[i].DefaultValue = Dispatch.ContentField(QDEntry[i].Name);
                    }

                }

                if (hMode == "Save")
                {
                    Record QDRec = new Record("QuotationDetail");
                    QDEntry.Fill(QDRec);
                    if (QDEntry.Validate() == true)
                    {
                        QDRec.SetField("qtdt_qutaid", qutaid);
                        QDRec.SaveChanges();

                        string qtdtid = QDRec.RecordId.ToString();
                        QuerySelect qs = this.GetQuery();
                        qs.SQLCommand = "Update QuotationDetail set qtdt_code=(select count(*) from QuotationDetail where qtdt_deleted is null and qtdt_qutaid=" + qutaid + ") where qtdt_QuotationDetailId = " + qtdtid;
                        qs.ExecuteNonQuery();
                        qs.SQLCommand = @"Update Quotation set quta_localeamount = (select sum(qtdt_localeamount) from QuotationDetail where qtdt_deleted is null and qtdt_qutaid= " + qutaid + @")
                        ,quta_foreignamount =  (select sum(qtdt_foreignamount) from QuotationDetail where qtdt_deleted is null and qtdt_qutaid= " + qutaid + @")    where quta_Quotationid=" + qutaid;
                        qs.ExecuteNonQuery();
                        Dispatch.Redirect(UrlDotNet(ThisDotNetDll, "RunDataPage") + "&quta_Quotationid=" + qutaid);
                    }
 
                }
                if (errflag != -1)
                {
                    AddContent(HTML.InputHidden("HiddenMode",""));
                    QDEntry.GetHtmlInEditMode();
                    VerticalPanel vp = new VerticalPanel();
                    vp.AddAttribute("width","100%");
                    vp.Add(QDEntry);
                    AddContent(vp);

                    string url = "javascript:document.EntryForm.HiddenMode.value='Save';";
                    AddSubmitButton("Save","Save.gif",url);
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
