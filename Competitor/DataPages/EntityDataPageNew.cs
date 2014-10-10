using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Data;
using Sage.CRM.Controls;
using Sage.CRM.UI;

namespace Competitor.DataPages
{
    public class CompetitorDataPageNew : DataPageNew
    {
        public CompetitorDataPageNew()
            : base("Competitor", "cpet_competitorid", "CompetitorNewEntry")
        {
            this.CancelButton = false;
            UseTabs = false;
        }

        public override void BuildContents()
        {
            try
            {
                string compid = Dispatch.EitherField("key1");
                /* Add your code here */
                AddTabHead("Competitor");
                this.EntryGroups[0].Title = "Competitor";
                base.BuildContents();
                if(string.IsNullOrEmpty(compid))
                    AddUrlButton("Cancel", "cancel.gif", UrlDotNet("IntelligenceMenu", "RunCompetitor"));
                else
                    AddUrlButton("Continue", "Continue.gif", UrlDotNet(ThisDotNetDll, "RunListPage") + "&J=Competitor&T=Company");

            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
    }
}