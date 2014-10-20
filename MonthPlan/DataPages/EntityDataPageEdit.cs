using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace MonthPlan.DataPages {
    public class MonthPlanDataPageEdit : DataPageEdit {
        public MonthPlanDataPageEdit()
            : base("MonthPlan", "mopl_MonthPlanId", "MonthPlanNewEntry") {
                this.CancelButton = false;
        }

        public override void BuildContents() {
            try {

                /* Add your code here */
                GetTabs("MonthPlan", "Summary");
                base.BuildContents();
                AddUrlButton("Cancel","Cancel.gif",UrlDotNet(ThisDotNetDll,"RunDataPage"));
            } catch (Exception error) {
                this.AddError(error.Message);
            }
        }
    }
}