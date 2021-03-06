﻿using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Data;
using Sage.CRM.Controls;
using Sage.CRM.UI;

namespace MonthPlan
{
    public class DealerDetailSummary : Web
    {
        public override void PreBuildContents()
        {
            GetTabs("DealerDetail", "Summary");
            AddTopContent(GetCustomEntityTopFrame("DealerDetail"));
            base.PreBuildContents();
        }
        public override void BuildContents()
        {
            try
            {
                string ddet_DealerDetailid = Dispatch.EitherField("ddet_DealerDetailid");

                Record DealerDetail = FindRecord("DealerDetail", "ddet_DealerDetailid=" + ddet_DealerDetailid);
                string ddet_monthplanid = DealerDetail.GetFieldAsString("ddet_monthplanid");

               
                int userid = CurrentUser.UserId;

                string urledit = base.UrlDotNet(base.ThisDotNetDll, "RunDealerDetailEdit") + "&ddet_DealerDetailid=" + ddet_DealerDetailid;
                    Dispatch.Redirect(urledit);


                    EntryGroup DecoratePersonNewEntry = new EntryGroup("DealerDetailNewEntry");

                DecoratePersonNewEntry.Title = "DealerDetail";
                DecoratePersonNewEntry.Fill(DealerDetail);

                VerticalPanel vpMainPanel = new VerticalPanel();
                vpMainPanel.AddAttribute("width", "100%");
                vpMainPanel.Add(DecoratePersonNewEntry);
                AddContent(vpMainPanel);

                string urldelete = base.UrlDotNet(base.ThisDotNetDll, "RunDealerDetailDelete") + "&ddet_DealerDetailid=" + ddet_DealerDetailid;
                    base.AddUrlButton("Delete", "Delete.gif", urldelete);


                    string url = UrlDotNet(ThisDotNetDll, "RunDataPage") + "&mopl_MonthPlanId=" + ddet_monthplanid;
                    url = url.Replace("Key37", "DealerDetailid");
                url = url + "&Key37=" + ddet_monthplanid;
                this.AddUrlButton("cancel", "PrevCircle.gif", url);
                
            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
    }
}