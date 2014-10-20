using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace CompeMarAct.DataPages {
    public class CompeMarActDataPageEdit : DataPageEdit {
        public CompeMarActDataPageEdit()
            : base("CompeMarAct", "cmac_CompeMarActId", "CompeMarActNewEntry") {
                this.CancelButton = false;
        }

        public override void BuildContents() {
            try {
                GetTabs("CompeMarAct", "Summary");
                /* Add your code here */
    
                base.BuildContents();
                AddUrlButton("Cancel","Cancel.gif",UrlDotNet(ThisDotNetDll,"RunDataPage"));
            } catch (Exception error) {
                this.AddError(error.Message);
            }
        }
    }
}