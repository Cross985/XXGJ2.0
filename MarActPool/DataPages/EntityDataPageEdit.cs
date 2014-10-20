using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace MarActPool.DataPages {
    public class MarActPoolDataPageEdit : DataPageEdit {
        public MarActPoolDataPageEdit()
            : base("MarActPool", "mapo_MarActPoolId", "MarActPoolNewEntry") {
                this.CancelButton = false;
        }

        public override void BuildContents() {
            try {
                GetTabs("MarActPool", "Summary");
                /* Add your code here */

                base.BuildContents();
                AddUrlButton("Cancel","Cancel.gif",UrlDotNet(ThisDotNetDll,"RunDataPage"));
            } catch (Exception error) {
                this.AddError(error.Message);
            }
        }
    }
}