using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace CompeMarAct.DataPages {
    public class CompeMarActDataPageDelete : DataPageDelete {
        public CompeMarActDataPageDelete()
            : base("CompeMarAct", "cmac_CompeMarActId", "CompeMarActNewEntry") {
                this.CancelButton = false;
        }
        public override void BuildContents()
        {
            GetTabs("CompeMarAct", "Summary");
            base.BuildContents();
            AddUrlButton("Cancel", "Cancel.gif", UrlDotNet(ThisDotNetDll, "RunDataPage"));
        }
    }
}