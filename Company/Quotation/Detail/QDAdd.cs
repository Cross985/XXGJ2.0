using System;
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
                //报价单别 国内、国外
                string inout = QutaRec.GetFieldAsString("quta_type");
                
                AddContent(HTML.InputHidden("exchange", exchange));
                string hMode = Dispatch.EitherField("HiddenMode");
                EntryGroup QDEntry = new EntryGroup("QuotationDetailNewEntry");
                QDEntry.Title = "报价明细";
                int errflag = 0;
                AddTabHead("QuotationDetail");

                string productinfoid = Dispatch.ContentField("qtdt_productinfoid");
                double discount = 100;
                if (!string.IsNullOrEmpty(productinfoid) && productinfoid != "0")
                {
                    Record prodRec = FindRecord("ProductInfo","pdin_ProductInfoId=" + productinfoid);
                    string Name = string.Empty;
                    if (inout == "2101")
                        Name = prodRec.GetFieldAsString("pdin_Name");
                    else Name = prodRec.GetFieldAsString("pdin_Englishname");
                    string Standard = prodRec.GetFieldAsString("pdin_standard");
                    string pdin_marketprice = prodRec.GetFieldAsString("pdin_marketprice");
                    //国内小类
                    string pdin_prodtypeid = prodRec.GetFieldAsString("pdin_prodtypeid");
                    if (string.IsNullOrEmpty(pdin_prodtypeid))
                        pdin_prodtypeid = "0";
                    //国外小类
                    string pdin_prodtype2 = prodRec.GetFieldAsString("pdin_prodtype2");
                    if (string.IsNullOrEmpty(pdin_prodtype2))
                        pdin_prodtype2 = "0";
                    //MOQ
                    string MOQ = prodRec.GetFieldAsString("pdin_moq");
                    if (string.IsNullOrEmpty(MOQ))
                        MOQ = "0";
                    //客户折扣 根据报价产品所属产品小类，确认客户该小类折扣
                    string compid = QutaRec.GetFieldAsString("quta_companyid");
                    QuerySelect qs = this.GetQuery();
                    qs.SQLCommand = "select prpi_discount from ProductPrice  where prpi_Deleted is null and prpi_Status = 'InProgress' and prpi_companyid = " + compid + " and (prpi_prodcategoryid  = " + pdin_prodtypeid + " or prpi_prodcategoryid = " + pdin_prodtype2 + ")";
                    qs.ExecuteReader();
                    if (qs.Eof())
                        discount = 100;
                    else
                        discount = Convert.ToDouble( qs.FieldValue("prpi_discount"));
                   
                    //AddInfo(Name + "---" + Standard);
               
                    for (int i = 0; i < QDEntry.Count; i++)
                    {
                        string field = QDEntry[i].Name;
                        if (field == "qtdt_productname")
                            QDEntry[i].DefaultValue = Name;
                        else if (field == "qtdt_prodtype")
                            QDEntry[i].DefaultValue = Standard;
                        else if (field == "qtdt_discount")
                            QDEntry[i].DefaultValue = discount.ToString();
                        else if (field == "qtdt_price")
                            QDEntry[i].DefaultValue = pdin_marketprice;
                        else if (field == "qtdt_moq")
                            QDEntry[i].DefaultValue = MOQ;
                        else
                            QDEntry[i].DefaultValue = Dispatch.ContentField(QDEntry[i].Name);
                    }

                }
                //int errflag = 0;
                string errmsg = "";

                if (hMode == "Save")
                {
                    Record QDRec = new Record("QuotationDetail");
                    QDEntry.Fill(QDRec);
                    double dc = Convert.ToDouble( Dispatch.ContentField("qtdt_discount"));
                    if (dc < discount && discount != 100)
                    {
                        errflag = 1;
                        errmsg += "折扣率不可低于客户最低折扣！";
                    }
                    if (QDEntry.Validate() == true && errflag == 0)
                    {
                        QDRec.SetField("qtdt_qutaid", qutaid);
                        /*查找当前数据库中该产品最新价格
                         *必须在该条记录保存前查询 */
                        QuerySelect qs = this.GetQuery();
                       
                        //string productinfoid = Dispatch.ContentField("qtdt_productinfoid");
                        double thislocalaount = Convert.ToDouble(Dispatch.ContentField("qtdt_localeamount"));

                        
                        //double thisforeignamount = Convert.ToDouble(Dispatch.ContentField("qtdt_foreignamount"));
//                        string select = @"select qtdt_localeamount,qtdt_foreignamount,quta_opportunityid from  QuotationDetail where qtdt_UpdatedDate =
//                        (select MAX( qtdt_UpdatedDate) from QuotationDetail 
//                        left join Quotation on quta_QuotationID = qtdt_qutaid
//                        left join Opportunity on Oppo_OpportunityId = quta_opportunityid  
//                        where qtdt_productinfoid = " + productinfoid+@" and quta_opportunityid = 12 and Oppo_Deleted is null and quta_Deleted is null and qtdt_Deleted is null) ";
//                        qs.SQLCommand = select;
//                        qs.ExecuteReader();
//                        double qtdt_localeamount=0, qtdt_foreignamount=0;
//                        string quta_opportunityid = string.Empty;
//                        if (!qs.Eof())
//                        {
//                            qtdt_localeamount = Convert.ToDouble(qs.FieldValue("qtdt_localeamount"));
//                            qtdt_foreignamount = Convert.ToDouble(qs.FieldValue("qtdt_foreignamount"));
//                            quta_opportunityid = qs.FieldValue("quta_opportunityid");
//                        }
//                        double resultlocal = thislocalaount - qtdt_localeamount;
//                        double resultforeign = thisforeignamount = qtdt_foreignamount;
//                        string updatesql = @"update Opportunity set oppo_qutaprice = ISNULL(oppo_qutaprice,0) + "+resultlocal+" where Oppo_OpportunityId =" + quta_opportunityid;
//                        qs.SQLCommand = updatesql;
//                        qs.ExecuteNonQuery();


                        QDRec.SaveChanges();
                        string qtdtid = QDRec.RecordId.ToString();
                        qs.SQLCommand = "exec crm_UpdateOpportunityQutaPrice @quotationid=" + qutaid;
                        qs.ExecuteNonQuery();
                        
                        
                        //更新报价明细序号
                        qs.SQLCommand = "Update QuotationDetail set qtdt_code=(select count(*) from QuotationDetail where qtdt_deleted is null and qtdt_qutaid=" + qutaid + ") where qtdt_QuotationDetailId = " + qtdtid;
                        qs.ExecuteNonQuery();
                        //更新报价单上汇总价格
                        qs.SQLCommand = @"Update Quotation set quta_localeamount = (select sum(qtdt_localeamount) from QuotationDetail where qtdt_deleted is null and qtdt_qutaid= " + qutaid + @")
                        ,quta_foreignamount =  (select sum(qtdt_foreignamount) from QuotationDetail where qtdt_deleted is null and qtdt_qutaid= " + qutaid + @")    where quta_Quotationid=" + qutaid;
                        qs.ExecuteNonQuery();
                        Dispatch.Redirect(UrlDotNet(ThisDotNetDll, "RunQDAdd") + "&quta_Quotationid=" + qutaid);
                    }
 
                }
                if (errflag != -1)
                {
                    if (errflag == 1)
                        AddError(errmsg);
                    List UseList = new List("QuotationDetailGrid");
                    UseList.Filter = "qtdt_deleted is null and qtdt_qutaid =" + qutaid;
                    UseList.PadBottom = false;
                    UseList.RowsPerScreen = 50;
                    
                    AddContent(HTML.InputHidden("HiddenMode",""));
                    QDEntry.GetHtmlInEditMode();
                    VerticalPanel vp = new VerticalPanel();
                    vp.AddAttribute("width","100%");
                    vp.Add(QDEntry);
                    vp.Add(UseList);
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
