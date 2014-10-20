using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace Contract.DataPages
{
    public class ContractListPage : ListPage
    {
        public ContractListPage()
            : base("Contract", "ContractGrid", "ContractFilterBox")
        {

        }
        public override void GetTabs()
        {
            string strTopContent = "<TABLE WIDTH=100%><TR><TD CLASS=TOPHEADING WIDTH=60><IMG SRC='" + CurrentUser.VirtualImgPath() + "Icons/Competitor.gif' BORDER=0 ALIGN=MIDDLE></td><td CLASS=VIEWBOXCAPTION><SPAN CLASS=TOPCAPTION>合同列表</SPAN></td></tr></table>";
            AddTopContent(strTopContent);

            this.UseEntityTabs = false;
            this.GetTabs("SalesManagement", "Contract");

        }
        public override void BuildContents()
        {
            try
            { // GetTabs("SalesManagement","Contract");
            
                base.BuildContents();
            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
        public override void AddNewButton()
        {
            AddUrlButton("New", "New.gif", UrlDotNet(ThisDotNetDll, "RunDataPageNew"));
        }

    }
}