using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Data;
using Sage.CRM.Controls;
using Sage.CRM.UI;

namespace OppoTrack.DataPages {
    public class OppoTrackDataPageDelete : Web {
        public override void BuildContents() {
            AddContent(HTML.Form());
            try {
                string hMode = Dispatch.EitherField("HiddenMode");
                string id = Dispatch.EitherField("optr_OppoTrackId");
                int errorflag = 0;
                string errormessage = string.Empty;
                Record OppoTrack = FindRecord("OppoTrack", "optr_OppoTrackId=" + id);
                string optr_opportunityid = OppoTrack.GetFieldAsString("optr_opportunityid");
                string oppoid = Dispatch.EitherField("key7");
                EntryGroup OppoTrackNewEntry = new EntryGroup("OppoTrackNewEntry");
                OppoTrackNewEntry.Fill(OppoTrack);

                AddTabHead("Delete OppoTrack");
                if (hMode == "Delete") {
                    OppoTrack.DeleteRecord = true;
                    OppoTrack.SaveChanges();

                    QuerySelect qs = this.GetQuery();
                    qs.SQLCommand = @"Update Opportunity set oppo_nexttrackdate = (select MAX(optr_nexttrackdate) from OppoTrack where optr_deleted is null and optr_opportunityid=" + optr_opportunityid + @")
                        ,oppo_trackdate = (select MAX(optr_date) from OppoTrack where optr_deleted is null and optr_opportunityid=" + optr_opportunityid + @" )
                        ,oppo_targetclose = (select optr_targetclose  from OppoTrack where optr_OppoTrackId = " + OppoTrack.RecordId.ToString() + @" )
                        ,oppo_forecast = (select optr_forecast from OppoTrack where optr_OppoTrackId = " + OppoTrack.RecordId.ToString() + @") 
                        ,oppo_certainty = (select optr_certainty  from OppoTrack where optr_OppoTrackId = " + OppoTrack.RecordId.ToString() + @") where oppo_opportunityid=" + optr_opportunityid;
                    qs.ExecuteNonQuery();

                    string url = string.Empty;
                    if (string.IsNullOrEmpty(oppoid))
                        url = UrlDotNet("SalesMenu", "RunOppoTrack") + "&J=OppoTrack&T=SalesManagement";
                    else
                        url = UrlDotNet(ThisDotNetDll, "RunListPage") + "&J=OppoTrack&T=Opportunity";
                    Dispatch.Redirect(url);
                }
                if (errorflag != -1) {

                    AddContent(HTML.InputHidden("HiddenMode", ""));
                    VerticalPanel vpMainPanel = new VerticalPanel();
                    vpMainPanel.AddAttribute("width", "100%");
                    string sUrl = "javascript:document.EntryForm.HiddenMode.value='Delete';";
                    OppoTrackNewEntry.GetHtmlInViewMode(OppoTrack);
                    vpMainPanel.Add(OppoTrackNewEntry);
                    AddContent(vpMainPanel);
                    AddSubmitButton("ConfirmDelete", "Delete.gif", sUrl);
                    string url = UrlDotNet(ThisDotNetDll, "RunDataPage") + "&optr_OppoTrackId=" + id + "&J=Summary";
                    AddUrlButton("Cancel", "cancel.gif", url);
                }

            } catch (Exception e) {
                AddError(e.Message);
            }
        }
    }
}
