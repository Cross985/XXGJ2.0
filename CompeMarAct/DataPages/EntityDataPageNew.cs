using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace CompeMarAct.DataPages {
    public class CompeMarActDataPageNew : DataPageNew {
        public CompeMarActDataPageNew()
            : base("CompeMarAct", "cmac_CompeMarActId", "CompeMarActNewEntry") {
                this.CancelButton = false;
        }

        public override void BuildContents() {
            try {
                AddTabHead("CompeMarAct");
                /* Add your code here */

                base.BuildContents();
                AddUrlButton("Cancel", "Cancel.gif", UrlDotNet("MarketingMenu", "RunCompeMarAct") + "&J=CompeMarAct&T=MarketingManagement");
            } catch (Exception error) {
                this.AddError(error.Message);
            }
        }
    }
}