using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;
using Sage.CRM.UI;
using Sage.CRM.Data;

namespace Company
{
    public class ProcessMethodAdd : Web
    {
        public override void BuildContents()
        {
            AddContent(HTML.Form());

            try
            {
                string hMode = Dispatch.EitherField("HiddenMode");
                string comp_companyid = Dispatch.EitherField("comp_companyid");
                int errorflag = 0;
                string errormessage = string.Empty;
                EntryGroup ProcessMethodNewEntry = new EntryGroup("ProcessMethodNewEntry");
                //CostAdjustmentProductNewEntry.Title = "Add CostAdjustmentProduct"; 
                Entry bred_busreportidEntry = ProcessMethodNewEntry.GetEntry("pmet_companyid");
                if (bred_busreportidEntry != null)
                {
                    bred_busreportidEntry.DefaultValue = comp_companyid;
                    bred_busreportidEntry.ReadOnly = true;
                }

                AddTabHead("ProcessMethod");
                if (hMode == "Save")
                {
                    Record ProcessMethod = new Record("ProcessMethod");
                    ProcessMethodNewEntry.Fill(ProcessMethod);
                    if (ProcessMethodNewEntry.Validate())
                    {
                        ProcessMethod.SaveChanges();
                        string pmet_ProcessMethodid = ProcessMethod.RecordId.ToString();
                        string url = UrlDotNet(ThisDotNetDll, "RunProcessMethodSummary") + "&pmet_ProcessMethodid=" + pmet_ProcessMethodid + "&J=Summary";
                        url = url.Replace("Key37", "ProcessMethodid");
                        url = url + "&Key37=" + pmet_ProcessMethodid;
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
                    if (errorflag == 2)
                    {
                        AddError(errormessage);
                    }

                    AddContent(HTML.InputHidden("HiddenMode", ""));
                    VerticalPanel vpMainPanel = new VerticalPanel();
                    vpMainPanel.AddAttribute("width", "100%");
                    string sUrl = "javascript:document.EntryForm.HiddenMode.value='Save';";
                    ProcessMethodNewEntry.GetHtmlInEditMode();
                    vpMainPanel.Add(ProcessMethodNewEntry);
                    AddContent(vpMainPanel);
                    AddSubmitButton("Save", "Save.gif", sUrl);
                    string url = UrlDotNet("Company", "RunProcessMethodList") + "&comp_companyid=" + comp_companyid + "&J=Summary";
                    url = url.Replace("Key37", "ProcessMethodid");
                    url = url + "&Key37=" + comp_companyid;
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
