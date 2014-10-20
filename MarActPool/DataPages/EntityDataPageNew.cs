using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace MarActPool.DataPages {
    public class MarActPoolDataPageNew : DataPageNew {
        public MarActPoolDataPageNew()
            : base("MarActPool", "mapo_MarActPoolId", "MarActPoolNewEntry") {
                this.UseTabs = false;
                this.CancelButton = false;
        }

        public override void BuildContents() {
            try {

                /* Add your code here */
                AddTabHead("MarActPool");
                base.BuildContents();
                AddUrlButton("Cancel", "Cancel.gif", UrlDotNet("MarketingMenu", "RunMarActPool") + "&J=MarActPool&T=MarketingManagement");
            } catch (Exception error) {
                this.AddError(error.Message);
            }
        }
    }
}

