using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace DecorateComp.DataPages {
    public class DecorateCompDataPageNew : DataPageNew {
        public DecorateCompDataPageNew()
            : base("DecorateComp", "dcom_DecorateCompId", "DecorateCompNewEntry") {
                this.CancelButton = false;
        }

        public override void BuildContents() {
            try {

                /* Add your code here */
                AddTabHead("DecorateComp");
                base.BuildContents();
                AddUrlButton("Cancel", "Cancel.gif", UrlDotNet("MarketingMenu", "RunDecorateComp") + "&J=RunDecorateComp&T=MarketingManagement");
            } catch (Exception error) {
                this.AddError(error.Message);
            }
        }
    }
}