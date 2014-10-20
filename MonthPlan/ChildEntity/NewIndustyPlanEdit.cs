using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Data;
using Sage.CRM.Controls;
using Sage.CRM.UI;
using Sage.CRM.Utils;

namespace MonthPlan
{
    public class NewIndustyPlanEdit : Web
    {
        public override void BuildContents()
        {
            AddContent(HTML.Form());

            try
            {
                string hMode = Dispatch.EitherField("HiddenMode");
                string nipl_NewIndustyPlan = Dispatch.EitherField("nipl_NewIndustyPlan");
                int errorflag = 0;
                string errormessage = string.Empty;
                Record DealerDetail = FindRecord("NewIndustyPlan", "nipl_NewIndustyPlan=" + nipl_NewIndustyPlan);
                string nipl_monthplanid = DealerDetail.GetFieldAsString("nipl_monthplanid");
                EntryGroup DealerDetailNewEntry = new EntryGroup("NewIndustyPlanNewEntry");
                DealerDetailNewEntry.Fill(DealerDetail);
                Entry IntentionOrderidEntry = DealerDetailNewEntry.GetEntry("nipl_monthplanid");

                IntentionOrderidEntry.ReadOnly = true;

                //AddTabHead("Person");
                //GetTabs("Person", "Person Summary");
                AddTabHead("NewIndustyPlan");
                if (hMode == "Save")
                {

                    ////double original =Person.GetFieldAsDouble("");		    
                    DealerDetailNewEntry.Fill(DealerDetail);
                    ////double  =Person.GetFieldAsDouble("");

                    if (DealerDetailNewEntry.Validate())
                    {
                        DealerDetail.SaveChanges();

                        ////Record BusReport= FindRecord("BusReport", "cpet_competitorid=" + cpet_competitorid);
                        ////double  = BusReport.GetFieldAsDouble("");                        
                        //// =  + - original;
                        ////BusReport.SetField("", );
                        ////BusReport.SaveChanges();

                        string url = UrlDotNet(ThisDotNetDll, "RunDataPage") + "&mopl_MonthPlanId=" + nipl_monthplanid + "&J=Summary";
                        url = url.Replace("Key37", "NewIndustyPlan");
                        url = url + "&Key37=" + nipl_monthplanid;
                        Dispatch.Redirect(url);
                        errorflag = -1;
                    }
                    else
                    {
                        errorflag = 1;
                    }

                }
                if (errorflag != -1)
                {

                    AddContent(HTML.InputHidden("HiddenMode", ""));
                    VerticalPanel vpMainPanel = new VerticalPanel();
                    vpMainPanel.AddAttribute("width", "100%");
                    string sUrl = "javascript:document.EntryForm.HiddenMode.value='Save';";
                    DealerDetailNewEntry.GetHtmlInEditMode(DealerDetail);
                    vpMainPanel.Add(DealerDetailNewEntry);
                    AddContent(vpMainPanel);
                    AddSubmitButton("Save", "Save.gif", sUrl);
                    string urldelete = base.UrlDotNet(base.ThisDotNetDll, "RunDealerDetailDelete") + "&nipl_NewIndustyPlan=" + nipl_NewIndustyPlan;
                    //base.AddUrlButton("Delete", "Delete.gif", urldelete);
                    string url = UrlDotNet(ThisDotNetDll, "RunDataPage") + "&mopl_MonthPlanId=" + nipl_monthplanid + "&J=Summary";
                    url = url.Replace("Key37", "NewIndustyPlan");
                    url = url + "&Key37=" + nipl_monthplanid;
                    AddUrlButton("Cancel", "cancel.gif", url);
                }

            }
            catch (Exception e)
            {
                AddError(e.Message);
            }
        }
    }
}
