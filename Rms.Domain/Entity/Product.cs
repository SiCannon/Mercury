using System;

namespace Rms.Domain.Entity
{
    using RemaProduct = Rema.Domain.Entity.Product;

    class Product
    {
        public int? ProductId { get; set; }
        public string OriginalId { get; set; }
        public string CatalogNumber { get; set; }
        public string Barcode { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public Company Company { get; set; }
        public Label Label { get; set; }
        public Format Format { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public int? CompanyId { get; set; }
        public int? LabelId { get; set; }
        public int? FormatId { get; set; }

        public Product()
        {

        }

        public Product(RemaProduct product)
        {
            CatalogNumber = product.UserCode;
            OriginalId = string.Format("{0}.{1}", product.Site, product.Code);
            Barcode = product.Barcode;
            Title = product.Title;
            Artist = product.Artist;
            //Company??
            //Label??
            //Format??
            ReleaseDate = product.ReleaseDate;
        }
    }
}
