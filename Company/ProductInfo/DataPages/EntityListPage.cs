using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace ProductInfo.DataPages
{
    public class ProductInfoListPage : ListPage
    {
        public ProductInfoListPage()
            : base("ProductInfo", "ProductInfoGrid", "ProductInfoSearchBox")
        {
            //FilterByField = "";
            //FilterByContextId = (int)Sage.KeyList.UserId;

        }

        public override void BuildContents()
        {
            try
            {
                //GetTabs("SystemSettingMenu", "ProductInfoList");
                /* Add your code here */

                base.BuildContents();
            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
        public override void AddNewButton()
        {
            //base.AddNewButton();
            if(CurrentUser.IsAdmin())
                AddUrlButton("New","New.gif",UrlDotNet(ThisDotNetDll,"RunDataPageNew"));
        }
    }
}