using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Data;
using Sage.CRM.Controls;
using Sage.CRM.UI;

namespace Company
{
    public class ProcessMethodSummary : Web
    {
        public override void PreBuildContents()
        {
            GetTabs("ProcessMethod", "Summary");
            AddTopContent(GetCustomEntityTopFrame("ProcessMethod"));
            base.PreBuildContents();
        }
        public override void BuildContents()
        {
            try
            {
                string pmet_ProcessMethodid = Dispatch.EitherField("pmet_ProcessMethodid");

                Record ProcessMethod = FindRecord("ProcessMethod", "pmet_ProcessMethodid=" + pmet_ProcessMethodid);
                string pmet_companyid = ProcessMethod.GetFieldAsString("pmet_companyid");

               
                int userid = CurrentUser.UserId;
                EntryGroup DecoratePersonNewEntry = new EntryGroup("ProcessMethodNewEntry");

                DecoratePersonNewEntry.Title = "ProcessMethod";
                DecoratePersonNewEntry.Fill(ProcessMethod);

                VerticalPanel vpMainPanel = new VerticalPanel();
                vpMainPanel.AddAttribute("width", "100%");
                vpMainPanel.Add(DecoratePersonNewEntry);
                AddContent(vpMainPanel);

                string urldelete = base.UrlDotNet(base.ThisDotNetDll, "RunProcessMethodEdit") + "&pmet_ProcessMethodid=" + pmet_ProcessMethodid;
                    base.AddUrlButton("Edit", "Edit.gif", urldelete);


                    string url = UrlDotNet(ThisDotNetDll, "RunProcessMethodList") + "&comp_companyid=" + pmet_companyid;
                    url = url.Replace("Key37", "ProcessMethodid");
                url = url + "&Key37=" + pmet_companyid;
                this.AddUrlButton("cancel", "PrevCircle.gif", url);
                
            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
    }
}