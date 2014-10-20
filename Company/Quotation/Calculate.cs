using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using Sage.CRM.Controls;
using Sage.CRM.Data;
using Sage.CRM.UI;

namespace Quotation
{
    class Calculate:Web
    {
        public override void BuildContents()
        {
            try {
                /*查找当前数据库中该产品最新价格
                         *必须在该条记录保存前查询 */
                QuerySelect qs = this.GetQuery();
                string productinfoid = Dispatch.ContentField("qtdt_productinfoid");
                double thislocalaount = Convert.ToDouble(Dispatch.ContentField("qtdt_localeamount"));
                double thisforeignamount = Convert.ToDouble(Dispatch.ContentField("qtdt_foreignamount"));
                string select = @"select qtdt_localeamount,qtdt_foreignamount,quta_opportunityid from  QuotationDetail where qtdt_UpdatedDate =
                        (select MAX( qtdt_UpdatedDate) from QuotationDetail 
                        left join Quotation on quta_QuotationID = qtdt_qutaid
                        left join Opportunity on Oppo_OpportunityId = quta_opportunityid  
                        where qtdt_productinfoid = " + productinfoid + @" and quta_opportunityid = 12 and Oppo_Deleted is null and quta_Deleted is null and qtdt_Deleted is null) ";
                qs.SQLCommand = select;
                qs.ExecuteReader();
                double qtdt_localeamount = 0, qtdt_foreignamount = 0;
                string quta_opportunityid = string.Empty;
                if (!qs.Eof())
                {
                    qtdt_localeamount = Convert.ToDouble(qs.FieldValue("qtdt_localeamount"));
                    qtdt_foreignamount = Convert.ToDouble(qs.FieldValue("qtdt_foreignamount"));
                    quta_opportunityid = qs.FieldValue("quta_opportunityid");
                }
                double resultlocal = thislocalaount - qtdt_localeamount;
                double resultforeign = thisforeignamount = qtdt_foreignamount;
                string updatesql = @"update Opportunity set oppo_qutaprice = ISNULL(oppo_qutaprice,0) + " + resultlocal + " where Oppo_OpportunityId =" + quta_opportunityid;
                qs.SQLCommand = updatesql;
                qs.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
            }
        }
    }
}
