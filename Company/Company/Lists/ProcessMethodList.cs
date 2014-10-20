using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Data;

namespace Company
{
    class ProcessMethodList : ListPage
    {
        public ProcessMethodList()
            : base("ProcessMethod", "ProcessMethodGrid", "ProcessMethodSearchBox")
        {
            this.UseTabs = false;
            string compid = this.FindCurrentRecord("Company").RecordId.ToString();
            this.ResultsGrid.Filter = "pmet_Deleted is null and pmet_companyid=" + compid;
        }
        public override void BuildContents()
        {
            GetTabs("Company", "ProcessMethod");
            base.BuildContents();
        }

        public override void AddNewButton()
        {
            string compid = this.FindCurrentRecord("Company").RecordId.ToString();
            AddUrlButton("New", "New.gif", UrlDotNet("Company", "RunProcessMethodAdd") + "&comp_companyid=" + compid);
            //base.AddNewButton();
        }
    }
}
