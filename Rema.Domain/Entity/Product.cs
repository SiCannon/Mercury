using System;
using System.Data;

namespace Rema.Domain.Entity
{
    public class Product
    {
        public string Site { get; set; }
        public string Code { get; set; }
        public string UserCode { get; set; }
        public string Barcode { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string CompanyCode { get; set; }
        public string LabelCode { get; set; }
        public string ConfigCode { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public Product()
        {

        }

        public Product(IDataRecord row)
        {
            Site = Helpers.Database.GetString(row, "PRODUCT_SITE");
            Code = Helpers.Database.GetString(row, "PRODUCT_CODE");
            UserCode = Helpers.Database.GetString(row, "PRODUCT_USERCODE");
            Barcode = Helpers.Database.GetString(row, "PRODUCT_BARCODE");
            Title = Helpers.Database.GetString(row, "PRODUCT_TITLE");
            Artist = Helpers.Database.GetString(row, "PRODUCT_ARTIST");
            CompanyCode = Helpers.Database.GetString(row, "COMPANY_CODE");
            LabelCode = Helpers.Database.GetString(row, "LABEL_CODE");
            ConfigCode = Helpers.Database.GetString(row, "CONFIG_CODE");
            ReleaseDate = Helpers.Database.GetDateTime(row, "PRODUCT_ACTUALRELDATE");
        }
    }
}
