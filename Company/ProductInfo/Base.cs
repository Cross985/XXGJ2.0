using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;
using ProductInfo.DataPages;

/********************************************* 
 * Sage CRM Application Extension for Entity: 
 * 
 * Name      : ProductInfo  
 * Created On: 2014/8/22 15:10:44
 * Created By: Steven
 * 
 ************************************/

namespace ProductInfo
{
    //REQURED - static class AppFactory is required!
    public static class AppFactory
    {
        public static void RunSearchPage(ref Web AretVal)
        {
            AretVal = new ProductInfoSearchPage();
        }

        public static void RunListPage(ref Web AretVal)
        {
            AretVal = new ProductInfoListPage();
        }

        public static void RunDataPage(ref Web AretVal)
        {
            AretVal = new ProductInfoDataPage();
        }

        public static void RunDataPageEdit(ref Web AretVal)
        {
            AretVal = new ProductInfoDataPageEdit();
        }

        public static void RunDataPageNew(ref Web AretVal)
        {
            AretVal = new ProductInfoDataPageNew();
        }

        public static void RunDataPageDelete(ref Web AretVal)
        {
            AretVal = new ProductInfoDataPageDelete();
        }
    }
}