using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;

namespace SalesMenu
{
    /// <summary>
    /// 报价
    /// </summary>
    public class OppoTrack : Sage.CRM.WebObject.ListPage
    {
        public OppoTrack()
            : base("OppoTrack", "OppoTrackGrid", "OppoTrackSearchBox")
        {
            this.ResultsGrid.RowsPerScreen = 50;
            this.ResultsGrid.Filter = "optr_deleted is null";

        }

        public override void GetTabs()
        {
            string strTopContent = "<TABLE WIDTH=100%><TR><TD CLASS=TOPHEADING WIDTH=60><IMG SRC='" + CurrentUser.VirtualImgPath() + "Icons/Opportunity.gif' BORDER=0 ALIGN=MIDDLE></td><td CLASS=VIEWBOXCAPTION><SPAN CLASS=TOPCAPTION>报价</SPAN></td></tr></table>";
            AddTopContent(strTopContent);
            GetTabs("SalesManagement", "OppoTrack");
        }

        public override void AddNewButton()
        {
            if (CurrentUser.HasRights(Sage.PermissionType.Insert, "OppoTrack") || CurrentUser.IsAdmin())
            {
                AddUrlButton("New", "New.gif", UrlDotNet("OppoTrack", "RunDataPageNew"));
            }
        }
    }

}

