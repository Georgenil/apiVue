using apiNux.Utils;
using System;

namespace apiNux.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Payment { get; set; }
        public string QuoteObservation { get; set; }
        public string BuyObservation { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int? DocumentId { get; set; }
        public Document? UploadDocument { get; set; }
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
