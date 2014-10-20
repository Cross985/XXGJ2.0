using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;
using Sage.CRM.UI;
using Sage.CRM.Data;

namespace OppoTrack.DataPages {
    public class OppoTrackDataPageNew : Web {
        public override void BuildContents() {
            AddContent(HTML.Form());

            try {
                string hMode = Dispatch.EitherField("HiddenMode");
                string optr_opportunityid = Dispatch.EitherField("Key7");
                int errorflag = 0;
                string errormessage = string.Empty;
                EntryGroup OppoTrackNewEntry = new EntryGroup("OppoTrackNewEntry");
                //CostAdjustmentProductNewEntry.Title = "Add CostAdjustmentProduct"; 
                Entry optr_opportunityidEntry = OppoTrackNewEntry.GetEntry("optr_opportunityid");
                if (optr_opportunityidEntry != null && optr_opportunityid != null)
                {
                    optr_opportunityidEntry.DefaultValue = optr_opportunityid;
                    optr_opportunityidEntry.ReadOnly = true;
                }
                else optr_opportunityidEntry.ReadOnly = false;

                AddTabHead("OppoTrack");
                if (hMode == "Save") {
                    Record OppoTrack = new Record("OppoTrack");
                    OppoTrackNewEntry.Fill(OppoTrack);
                    if (OppoTrackNewEntry.Validate()) {

                        OppoTrack.SaveChanges();
                        QuerySelect qs = this.GetQuery();
                        string oppoid = OppoTrack.GetFieldAsString("optr_opportunityid");
                        qs.SQLCommand = @"Update Opportunity set oppo_nexttrackdate = (select MAX(optr_nexttrackdate) from OppoTrack where optr_deleted is null and optr_opportunityid=" + oppoid + @")
                        ,oppo_trackdate = (select MAX(optr_date) from OppoTrack where optr_deleted is null and optr_opportunityid=" + oppoid + @" )
                        ,oppo_targetclose = (select optr_targetclose  from OppoTrack where optr_OppoTrackId = " + OppoTrack.RecordId.ToString() + @" )
                        ,oppo_forecast = (select optr_forecast from OppoTrack where optr_OppoTrackId = " + OppoTrack.RecordId.ToString() + @") 
                        ,oppo_certainty = (select optr_certainty  from OppoTrack where optr_OppoTrackId = " + OppoTrack.RecordId.ToString() + @") where oppo_opportunityid=" + oppoid;
                        qs.ExecuteNonQuery();

                        string url = UrlDotNet(ThisDotNetDll, "RunDataPage") + "&optr_oppotrackid=" + OppoTrack.GetFieldAsString("optr_oppotrackid") + "&J=Summary";
                        Dispatch.Redirect(url);
                        errorflag = -1;
                    } else {
                        errorflag = 1;
                    }
                }
                if (errorflag != -1) {
                    if (errorflag == 2) {
                        AddError(errormessage);
                    }

                    AddContent(HTML.InputHidden("HiddenMode", ""));
                    VerticalPanel vpMainPanel = new VerticalPanel();
                    vpMainPanel.AddAttribute("width", "100%");
                    string sUrl = "javascript:document.EntryForm.HiddenMode.value='Save';";
                    OppoTrackNewEntry.GetHtmlInEditMode();
                    vpMainPanel.Add(OppoTrackNewEntry);
                    AddContent(vpMainPanel);
                    AddSubmitButton("Save", "Save.gif", sUrl);
                    string url = string.Empty;
                    if (string.IsNullOrEmpty(optr_opportunityid))
                        url = UrlDotNet("SalesMenu", "RunOppoTrack") + "&J=OppoTrack&T=SalesManagement";
                    else
                        url = UrlDotNet(ThisDotNetDll, "RunListPage") + "&J=OppoTrack&T=Opportunity";
                    AddUrlButton("Cancel", "cancel.gif", url );
                }

            } catch (Exception e) {
                AddError(e.Message);
            }
        }

    }
}
