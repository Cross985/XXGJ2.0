using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;
using Sage.CRM.Data;

namespace Follow.DataPages
{
    public class FollowDataPageNew : DataPageNew
    {
        public FollowDataPageNew()
            : base("Follow", "foll_FollowID", "FollowNewEntry")
        {
            this.CancelButton = false;
        }
        string compid = string.Empty;
        
        public override void BuildContents()
        {
            try
            {
                //GetTabs("Follow", "Follow");
                base.AddTabHead("Follow");
                //compid = Dispatch.EitherField("comp_companyid");
             
                compid = Dispatch.EitherField("key1");
               
                //AddInfo(compid+"1");
                base.BuildContents();
                if (!string.IsNullOrEmpty(compid))
                    AddUrlButton("Cancel", "Cancel.gif", UrlDotNet("Company", "RunFollowList") + "&comp_companyid=" + compid + "&J=Follow&T=Company");
                else
                    AddUrlButton("Cancel", "Cancel.gif", UrlDotNet("Company", "RunFollowMenuList") + "&J=Follow&T=CompanyMangement");
               
            }
            catch (Exception error)
            {
                this.AddError(error.Message);
            }
        }
       
        public override void AfterSave(EntryGroup screen)
        {
           
            //Record FollRec = base.FindCurrentRecord("Follow");
            //screen.Fill(FollRec);
            //FollRec.SaveChanges();
            Record foll = screen.getRecord;
            string id = foll.RecordId.ToString();
            string compid = foll.GetFieldAsString("foll_companyid");
            string date = foll.GetFieldAsDateTime("foll_followdate").ToString("yyyy-MM-dd");
            QuerySelect qs = this.GetQuery();
            qs.SQLCommand = "select comp_shortname from Company where comp_companyid = "+compid;
            qs.ExecuteReader();
            string code = qs.FieldValue("comp_shortname");
            string name = code+ "/" + date;
            qs.SQLCommand = "update Follow set foll_Name = '" + name + "' where foll_followid =" + id;
            qs.ExecuteNonQuery();
            string foll_createoppo = foll.GetFieldAsString("foll_createoppo");
            if (foll_createoppo.ToLower() == "y")
                Dispatch.Redirect(Url("1190"));
            else
                Dispatch.Redirect(UrlDotNet("Company", "RunFollowList") + "&123&comp_companyid=" + compid + "&J=Follow&T=Company");
        }
    }
}