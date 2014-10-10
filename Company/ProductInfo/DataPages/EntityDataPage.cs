using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace ProductInfo.DataPages
{
    public class ProductInfoDataPage : DataPage
    {

        public ProductInfoDataPage()
            : base("ProductInfo", "pdin_ProductInfoID", "ProductInfoNewEntry")
        {
            
            this.EditButton = false;
            this.ContinueButton = false;
        }

        public override void BuildContents()
        {
            try
            {
                GetTabs("ProductInfo", "ProductInfo Summary");
                /* Add your code here */
                if (CurrentUser.IsAdmin())
                    AddUrlButton("Edit","Edit.gif",UrlDotNet(ThisDotNetDll,"RunDataPageEdit"));
                AddUrlButton("Continue", "Continue.gif", UrlDotNet(ThisDotNetDll, "RunListPage") + "&J=ProductInfoList&T=SystemSettingMenu");
                base.BuildContents();
            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
    }
}