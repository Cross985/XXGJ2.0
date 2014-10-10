using System;
using System.Collections.Generic;
using System.Text;
using Sage.CRM.WebObject;

namespace ProductInfo.DataPages
{
    public class ProductInfoSearchPage : SearchPage
    {
        public ProductInfoSearchPage()
            : base("ProductInfoSearchBox", "ProductInfoGrid")
        {
        }
    }
}