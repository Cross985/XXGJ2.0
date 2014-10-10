using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;

namespace ProductInfo.DataPages
{
    public class ProductInfoDataPageEdit : DataPageEdit
    {
        public ProductInfoDataPageEdit()
            : base("ProductInfo", "pdin_ProductInfoID", "ProductInfoNewEntry")
        {
            this.CancelButton = false;
            
        }
        string compid = string.Empty;
        public override void BuildContents()
        {
            try
            {
                GetTabs("ProductInfo", "ProductInfo");
                /* Add your code here */
                compid = Dispatch.EitherField("comp_companyid");
                if (string.IsNullOrEmpty(compid))
                    compid = Dispatch.EitherField("key1");
                /* Add your code here */
                base.BuildContents();

                AddUrlButton("Cancel", "Cancel.gif", UrlDotNet("Company", "RunDataPage") + "&comp_companyid=" + compid);

               
            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
        public override void AfterSave(EntryGroup screen)
        {
            //Entry comp = screen.GetEntry("pdin_companyid");
            //string compid = Dispatch.EitherField("comp_companyid");
            //if (string.IsNullOrEmpty(compid))
            //    compid = Dispatch.EitherField("key1");
            //comp.DefaultValue = compid;
            //Record pdinRec = base.FindCurrentRecord("ProductInfo");
            //pdinRec.SetField("pdin_companyid",compid);
            //pdinRec.SaveChanges();
            //base.AfterSave(screen);
            Dispatch.Redirect(UrlDotNet(ThisDotNetDll, "RunDataPage") + "&J=Summary");
        }
    }
}