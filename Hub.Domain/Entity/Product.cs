using System;

namespace Hub.Domain.Entity
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Title { get; set; }

        public Artist Artist { get; set; }
    }
}
