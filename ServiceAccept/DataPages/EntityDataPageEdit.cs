using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace ServiceAccept.DataPages {
    public class ServiceAcceptDataPageEdit : DataPageEdit {
        public ServiceAcceptDataPageEdit()
            : base("ServiceAccept", "seac_ServiceAcceptId", "ServiceAcceptNewEntry") {
                this.CancelButton = false;
        }

        public override void BuildContents() {
            try {

                /* Add your code here */
                GetTabs("ServiceAccept", "Summary");
                base.BuildContents();
                AddUrlButton("Cancel", "Cancel.gif", UrlDotNet(ThisDotNetDll, "RunDataPage"));
            } catch (Exception error) {
                this.AddError(error.Message);
            }
        }
    }
}