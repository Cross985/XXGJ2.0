using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace MonthPlan.DataPages {
    public class MonthPlanDataPageDelete : DataPageDelete {
        public MonthPlanDataPageDelete()
            : base("MonthPlan", "mopl_MonthPlanId", "MonthPlanNewEntry") {
                this.CancelButton = false;
        }
        public override void BuildContents()
        {
            GetTabs("MonthPlan", "Summary");
            base.BuildContents();
            //base.DeleteMethod = "123";
            AddUrlButton("Cancel","Cancel.gif",UrlDotNet(ThisDotNetDll,"RunDataPage"));
        }
        public override void AfterSave(Sage.CRM.Controls.EntryGroup screen)
        {
            base.AfterSave(screen);
        }
    }
}