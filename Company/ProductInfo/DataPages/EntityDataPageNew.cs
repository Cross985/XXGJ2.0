using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;
using Sage.CRM.Data;

namespace ProductInfo.DataPages
{
    public class ProductInfoDataPageNew : DataPageNew
    {
        public ProductInfoDataPageNew()
            : base("ProductInfo", "pdin_ProductInfoID", "ProductInfoNewEntry")
        {
            this.CancelButton = false;
            
        }
        string compid = string.Empty;
        public override void BuildContents()
        {
            try
            {
                //GetTabs("ProductInfo", "ProductInfo");
               
                /* Add your code here */
                base.BuildContents();
                //AddUrlButton("Cancel", "Cancel.gif", UrlDotNet("Company", "RunDataPage") + "&comp_companyid=" + compid + "&J=Summary&T=CompanySummary");

               
            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
        public override void AfterSave(EntryGroup screen)
        {
            //Entry comp = screen.GetEntry("pdin_companyid");
            
            //comp.DefaultValue = compid;
            //Record pdinRec = base.FindCurrentRecord("ProductInfo");
            //pdinRec.SetField("pdin_companyid",compid);
            //pdinRec.SaveChanges();
            //base.AfterSave(screen);
            Dispatch.Redirect(UrlDotNet(ThisDotNetDll, "RunProductInfoList") );
        }
    }
}