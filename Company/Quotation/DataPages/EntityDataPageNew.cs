using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;
using Sage.CRM.Data;
using Sage.CRM.UI;

namespace Quotation.DataPages
{
    public class QuotationDataPageNew : Web
    {

        public override void BuildContents()
        {
            try
            {

                AddContent(HTML.Form());

                EntryGroup qutacompEntry = new EntryGroup("QuotationCompanyEntry");
                qutacompEntry.Title = "商机客户";
                EntryGroup qutaEntry = new EntryGroup("QuotationNewEntry");
                qutaEntry.Title = "报价信息";
                string oppoid = Dispatch.EitherField("oppo_OpportunityId");
                if (string.IsNullOrEmpty(oppoid))
                    oppoid = Dispatch.EitherField("key7");
                AddTabHead("Quotation");
                int errflag = 0;
                string errmsg = string.Empty;
                string hMode = Dispatch.EitherField("HiddenMode");
                if (hMode == "Save")
                {
                    Record QutaRec = new Record("Quotation");
                    qutacompEntry.Fill(QutaRec);
                    int days = Convert.ToInt32( Dispatch.ContentField("quta_days"));
                    if (days < 3)
                    {
                        //AddError(days.ToString());
                        errflag = 1;
                        errmsg = "交货天数不可小于3天！";
                    }
                    string quta_oppoid = Dispatch.ContentField("quta_opportunityid");
                    string quta_qutatype = Dispatch.ContentField("quta_qutatype");
                    if (string.IsNullOrEmpty(quta_oppoid) && quta_qutatype != "1" && quta_qutatype != "2")
                    {
                        errflag = 1;
                        errmsg = "请选择商机！";
                    }
                    

                    qutaEntry.Fill(QutaRec);
                    if (qutaEntry.Validate() == true && qutacompEntry.Validate() && errflag == 0)
                    {
                        string code = "C";
                        DateTime now = DateTime.Now;
                        string year = now.Year.ToString();
                        string month = now.Month.ToString();
                        string day = now.Day.ToString();
                        code += year + month + day;
                        QuerySelect qs = this.GetQuery();
                        qs.SQLCommand = "Select Count(*)+1 as cnt from Quotation where quta_code like '" + code + "%'";
                        qs.ExecuteReader();
                        int cnt = 0;
                        if (!qs.Eof())
                            cnt = Convert.ToInt32(qs.FieldValue("cnt"));
                        code += "9"+ cnt.ToString().PadLeft(4, '0');

                        QutaRec.SetField("quta_code", code);
                        //QutaRec.SetField("quta_OpportunityId",oppoid);
                        QutaRec.SaveChanges();

                        //QuerySelect qs = this.GetQuery();
                        if ((quta_qutatype == "1" || quta_qutatype == "2") && string.IsNullOrEmpty(quta_oppoid))
                        {
                            //create opportunity
                            string opponame = "报价单"+code+"自动生成商机";
                            string oppo_type = "3";//订货
                            string oppo_countryin = Dispatch.ContentField("quta_type");
                            switch (oppo_countryin)
                            {
                                case"2101": oppo_countryin = "in";
                                    break;
                                case"2102": oppo_countryin = "out";
                                    break;
                                default: oppo_countryin = "in"; break;
                            }
                            string oppo_assigneduserid = Dispatch.ContentField("quta_userid");
                            string oppo_stage = "payment";
                            string oppo_status = "In Progress";
                            string oppo_certainty = "100";
                            string oppo_primarycompanyid = Dispatch.ContentField("quta_companyid");
                            Record OppoRec = new Record("Opportunity");
                            OppoRec.SetField("oppo_type", oppo_type);
                            OppoRec.SetField("oppo_description", opponame);
                            OppoRec.SetField("oppo_countryin", oppo_countryin);
                            OppoRec.SetField("oppo_assigneduserid", oppo_assigneduserid);
                            OppoRec.SetField("oppo_stage", oppo_stage);
                            OppoRec.SetField("oppo_status", oppo_status);
                            OppoRec.SetField("oppo_certainty", oppo_certainty);
                            OppoRec.SetField("oppo_primarycompanyid", oppo_primarycompanyid);
                            OppoRec.SetField("oppo_createdate",DateTime.Now);
                            OppoRec.SaveChanges();

                            quta_oppoid = OppoRec.RecordId.ToString();
                            QutaRec.SetField("quta_OpportunityId", quta_oppoid);
                            QutaRec.SaveChanges();
                        }

                        string quta_opportunityid = QutaRec.GetFieldAsString("quta_opportunityid");
                        qs.SQLCommand = "Update Opportunity set oppo_qutaprice= (select sum(quta_localeamount) from Quotation where quta_deleted is null and quta_updateoppo = 'Y' and quta_opportunityid = " + quta_opportunityid + " ) where oppo_Opportunityid =" + quta_opportunityid;
                        qs.ExecuteNonQuery();
                        Dispatch.Redirect(UrlDotNet(ThisDotNetDll, "RunDataPage") + "&quta_Quotationid=" + QutaRec.RecordId.ToString());
                    }

                }
                if (errflag != -1)
                {
                    if (errflag == 1)
                        AddError(errmsg);
                    AddContent(HTML.InputHidden("HiddenMode", ""));
                    qutacompEntry.GetHtmlInEditMode();
                    qutaEntry.GetHtmlInEditMode();
                    VerticalPanel vp = new VerticalPanel();
                    vp.AddAttribute("width", "100%");
                    vp.Add(qutacompEntry);
                    vp.Add(qutaEntry);
                    AddContent(vp);
                    AddSubmitButton("Save", "Save.gif", "javascript:document.EntryForm.HiddenMode.value='Save';");
                    string url = string.Empty;
                    if (string.IsNullOrEmpty(oppoid))
                        url = UrlDotNet("SalesMenu", "RunQuotation") + "&J=Quotation&T=SalesManagement";
                    else
                        url = UrlDotNet(ThisDotNetDll, "RunListPage") + "&J=OppoTrack&T=Opportunity";
                    AddUrlButton("Cancel", "Cancel.gif", url);
                }
            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
    }
}