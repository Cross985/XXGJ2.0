using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Data;

namespace MonthPlan.DataPages {
    public class MonthPlanDataPageNew : DataPageNew {
        public MonthPlanDataPageNew()
            : base("MonthPlan", "mopl_MonthPlanId", "MonthPlanNewEntry") {
                this.CancelButton = false;
                this.UseTabs = false;
        }

        public override void BuildContents() {
            try {

                /* Add your code here */
                AddTabHead("MonthPlan");

                base.BuildContents();
                AddUrlButton("cancel", "Cancel.gif", UrlDotNet("WorkMenu","RunMonthPlan")+"&J=MonthPlan&T=User");
            } catch (Exception error) {
                this.AddError(error.Message);
            }
        }
        public override void AfterSave(Sage.CRM.Controls.EntryGroup screen)
        {
            Record Rec = screen.getRecord;
            screen.Fill(Rec);
            Rec.SaveChanges();
            string startdate = Rec.GetFieldAsDateTime("mopl_startdate").ToString("yyyy-MM-dd");
            string userid = Rec.GetFieldAsString("mopl_userid");
            QuerySelect qs = this.GetQuery();
            qs.SQLCommand = "select user_LastName from Users where user_userid=" + userid;
            qs.ExecuteReader();
            string username = qs.FieldValue("user_LastName");
            string name = username + "/" + startdate;
            qs.SQLCommand = "Update MonthPlan set mopl_Name ='" + name + "' where mopl_MonthPlanid =" + Rec.RecordId.ToString() ;
            qs.ExecuteNonQuery();
            base.AfterSave(screen);

        }
    }
}