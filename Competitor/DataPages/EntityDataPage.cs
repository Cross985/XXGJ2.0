using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Data;
using Sage.CRM.Controls;
using Sage.CRM.UI;

namespace Competitor.DataPages
{
    public class CompetitorDataPage: Web
    {

        public override void PreBuildContents()
        {
            GetTabs("Competitor", "Summary");
            AddTopContent(GetCustomEntityTopFrame("Competitor"));
            base.PreBuildContents();
        }
        //public override void GetTabs()
        //{
        //    string strTopContent = "<TABLE WIDTH=100%><TR><TD CLASS=TOPHEADING WIDTH=60><IMG SRC='" + CurrentUser.VirtualImgPath() + "Icons/Equipment.gif' BORDER=0 ALIGN=MIDDLE></td><td CLASS=VIEWBOXCAPTION><SPAN CLASS=TOPCAPTION>竞争对手</SPAN></td></tr></table>";
        //    AddTopContent(strTopContent);
        //    GetTabs("Competitor", "Summary");
        //}

        public override void BuildContents()
        {
            try
            {
                string compid = Dispatch.EitherField("key1");

                string cpet_competitorid = Dispatch.EitherField("cpet_competitorid");
                if (string.IsNullOrEmpty(cpet_competitorid))
                {
                    cpet_competitorid = Dispatch.EitherField("key58");
                }

                Record Competitor = FindRecord("Competitor", "cpet_competitorid=" + cpet_competitorid);

                EntryGroup screenBusReport = new EntryGroup("CompetitorNewEntry");
                screenBusReport.Title = "Competitor";
                screenBusReport.Fill(Competitor);

               // string status = Competitor.GetFieldAsString("cept_status");

                VerticalPanel vpMainPanel = new VerticalPanel();
                vpMainPanel.AddAttribute("width", "100%");
                vpMainPanel.Add(screenBusReport);

                List CpetProductGrid = new List("CpetProductGrid");
                CpetProductGrid.Filter = "cppr_deleted is null and cppr_competitorid=" + cpet_competitorid;
                CpetProductGrid.RowsPerScreen = 500;
                CpetProductGrid.ShowNavigationButtons = true;
                CpetProductGrid.PadBottom = false;
                vpMainPanel.Add(CpetProductGrid);

                List MarketActivityGrid = new List("MarketActivityGrid");
                MarketActivityGrid.Filter = "mact_deleted is null and mact_competitorid=" + cpet_competitorid;
                MarketActivityGrid.RowsPerScreen = 500;
                MarketActivityGrid.ShowNavigationButtons = true;
                MarketActivityGrid.PadBottom = false;
                vpMainPanel.Add(MarketActivityGrid);

                List CompetitionPersonList = new List("CompetitionPersonList");
                CompetitionPersonList.Filter = "dper_deleted is null and dper_competitorid=" + cpet_competitorid;
                CompetitionPersonList.RowsPerScreen = 500;
                CompetitionPersonList.ShowNavigationButtons = true;
                CompetitionPersonList.PadBottom = false;
                vpMainPanel.Add(CompetitionPersonList);

                AddContent(vpMainPanel);
                int userid = CurrentUser.UserId;
                //if (userid == 137 || userid == 43 || userid == 130 || CurrentUser.IsAdmin())
                //{
                    AddUrlButton("Edit", "Edit.gif", UrlDotNet(ThisDotNetDll, "RunDataPageEdit") + "&cpet_competitorid=" + cpet_competitorid);
                    AddUrlButton("Delete", "Delete.gif", UrlDotNet(ThisDotNetDll, "RunDataPageDelete") + "&cpet_competitorid=" + cpet_competitorid);
               // }
                AddUrlButton("Add CpetProduct", "new.gif", UrlDotNet(ThisDotNetDll, "RunCpetProductAdd") + "&cpet_competitorid=" + cpet_competitorid);
                AddUrlButton("Add MarketActivity", "new.gif", UrlDotNet(ThisDotNetDll, "RunMarketActivityAdd") + "&cpet_competitorid=" + cpet_competitorid);
                AddUrlButton("Add Person", "new.gif", UrlDotNet(ThisDotNetDll, "RunPersonAdd") + "&cpet_competitorid=" + cpet_competitorid);
                if(string.IsNullOrEmpty(compid))
                    AddUrlButton("Continue", "Continue.gif", UrlDotNet("IntelligenceMenu", "RunCompetitor") + "&J=Competitor&T=CompanyMangement");
                else
                    AddUrlButton("Continue", "Continue.gif", UrlDotNet(ThisDotNetDll, "RunListPage") + "&J=Competitor&T=Company");
                //AddWorkflowButtons("Competitor");
            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
    }
}