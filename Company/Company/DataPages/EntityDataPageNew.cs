using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;
using Sage.CRM.Data;
using Sage.CRM.UI;

namespace Company.DataPages
{
    public class CompanyDataPageNew : Web
    {

        public override void BuildContents()
        {
            try
            {

                AddContent(HTML.Form());
                //string compid = Dispatch.EitherField("comp_companyid");
                EntryGroup CompEntry = new EntryGroup("CompanyBoxLong");
                CompEntry.Title = "基础信息";
                EntryGroup DealEntry = new EntryGroup("CompanyDealEntry");
                DealEntry.Title = "交易信息";
                EntryGroup StatusEntry = new EntryGroup("CompanyStatusEntry");
                StatusEntry.Title = "状态信息";

                EntryGroup AddrEntry = new EntryGroup("CompanyAddressEntry");
                AddrEntry.Title = "地址信息";

                AddTabHead("Company");
                int errflag = 0;
                string errmsg = string.Empty;
                string hMode = Dispatch.EitherField("HiddenMode");
                if (hMode == "Save")
                {
                    Record CompRec = new Record("Company");
                    CompEntry.Fill(CompRec);
                    DealEntry.Fill(CompRec);
                    string comp_fullname = CompRec.GetFieldAsString("comp_fullname");
                    string comp_shortname = CompRec.GetFieldAsString("comp_shortname");

                    QuerySelect qs = this.GetQuery();
                    qs.SQLCommand = "select count(*) as cnt from Company where comp_deleted is null and (comp_shortname = '" + comp_shortname + "' or comp_fullname = '" + comp_fullname + "')";
                    qs.ExecuteReader();
                    int cnt = -1;
                    if (!qs.Eof())
                        cnt = Convert.ToInt32(qs.FieldValue("cnt"));

                    if (cnt > 0)
                    {
                        errflag = 1;
                        errmsg = "客户全称或快捷码重复！";
                    }
                   
                    if (CompEntry.Validate() == true  && DealEntry.Validate() && DealEntry.Validate() == true && StatusEntry.Validate() && errflag == 0)
                    {
                      
                        CompRec.SaveChanges();
                        Record AddrRec = new Record("Address");
                        AddrEntry.Fill(AddrRec);
                   
                        if (AddrEntry.Validate())
                        {
                            AddrRec.SaveChanges();
                        }
                        Dispatch.Redirect(Url("200") + "&J=Summary&T=Company&comp_companyid=" + CompRec.RecordId.ToString());
                    }


                }
                if (errflag != -1)
                {
                    if (errflag != 0)
                        AddError(errmsg);
                    AddContent(HTML.InputHidden("HiddenMode",""));
                    CompEntry.GetHtmlInEditMode();
                    DealEntry.GetHtmlInEditMode();
                    //StatusEntry.GetHtmlInEditMode();
                    VerticalPanel vp = new VerticalPanel();
                    vp.AddAttribute("width", "100%");
                    vp.Add(CompEntry);
                    vp.Add(DealEntry);
                    //vp.Add(StatusEntry);
                    AddContent(vp);
                    AddSubmitButton("Save", "Save.gif", "javascript:document.EntryForm.HiddenMode.value='Save';");
                    AddUrlButton("Cancel", "Cancel.gif", UrlDotNet(ThisDotNetDll, "RunListPage") + "&T=CompanyList");
                }
            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
    }
}