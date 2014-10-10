using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace Competitor.DataPages
{
    public class CompetitorDataPageEdit : DataPageEdit
    {
        public CompetitorDataPageEdit()
            : base("Competitor", "cpet_competitorid", "CompetitorNewEntry")
        {
            //this.CancelMethod = "RunDataPage";
            this.DeleteButton = false;
            this.CancelButton = false;
        }

        public override void BuildContents()
        {
            try
            {

                /* Add your code here */
                AddTabHead("Competitor");
                this.EntryGroups[0].Title = "Competitor";
                base.BuildContents();
                int userid = CurrentUser.UserId;
                if (userid == 137 || userid == 43 || userid == 130 || CurrentUser.IsAdmin())
                {
                    AddUrlButton("Delete", "Delete.gif", UrlDotNet("Competitor", "RunDataPageDelete") + "&J=Summary&T=Competitor");
                    
                }
                AddUrlButton("Cancel", "cancel.gif", UrlDotNet("Competitor", "RunDataPage") + "&J=Summary&T=Competitor");


            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
        //public override void AddDeleteButton()
        //{
        //    int userid = CurrentUser.UserId;
        //    if (userid == 137 || userid == 43 || userid == 130 || CurrentUser.IsAdmin())
        //    {
        //        AddUrlButton("Delete", "Delete.gif", UrlDotNet("Competitor", "RunDataPageDelete") + "&J=Summary&T=Competitor");
        //    }
        //}

    }
}