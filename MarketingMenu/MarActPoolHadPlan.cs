using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;

namespace MarketingMenu
{
    /// <summary>
    /// 市场活动池
    /// </summary>
    public class MarActPoolHadPlan :Sage.CRM.WebObject.ListPage
    {
        public MarActPoolHadPlan()
            : base("MarActPool", "MarActPoolGrid", "MarActPoolFilterBox")
        {
            this.ResultsGrid.RowsPerScreen = 50;
            this.ResultsGrid.Filter = "mapo_deleted is null and mapo_MarActPoolID in (select mapl_MarActPoolid from MarActPlan where mapl_Deleted is null)";

        }

        public override void GetTabs()
        {
            string strTopContent = "<TABLE WIDTH=100%><TR><TD CLASS=TOPHEADING WIDTH=60><IMG SRC='" + CurrentUser.VirtualImgPath() + "Icons/MarActPool.gif' BORDER=0 ALIGN=MIDDLE></td><td CLASS=VIEWBOXCAPTION><SPAN CLASS=TOPCAPTION>市场活动</SPAN></td></tr></table>";
            AddTopContent(strTopContent);
            GetTabs("MarketingManagement", "MarActPoolPlanned");            
        }

        public override void AddNewButton()
        {
            if (CurrentUser.HasRights(Sage.PermissionType.Insert, "MarActPool") || CurrentUser.IsAdmin())
            {
                AddUrlButton("New", "New.gif", UrlDotNet("MarActPool", "RunDataPageNew"));
            }
        }
    }

}

