using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace Maintenance.DataPages {
    public class MaintenanceDataPageEdit : DataPageEdit {
        public MaintenanceDataPageEdit()
            : base("Maintenance", "mate_MaintenanceId", "MaintenanceNewEntry")
        {
            this.CancelButton = false;
        }

        public override void BuildContents() {
            try {
                GetTabs("Maintenance", "Summary");
                /* Add your code here */

                base.BuildContents();
                AddUrlButton("Cancel","Cancel.gif",UrlDotNet(ThisDotNetDll,"RunDataPage"));
            } catch (Exception error) {
                this.AddError(error.Message);
            }
        }
    }
}