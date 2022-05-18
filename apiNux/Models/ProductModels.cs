using apiNux.Domain;
using apiNux.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace apiNux.Models
{
    public class ProductViewModel
    {
        public ProductViewModel(IQueryable<Product> Products, Pager Pager)
        {
            this.Pager = Pager;
            this.Products = Products.Skip((this.Pager.CurrentPage - 1) * this.Pager.PageSize).Take(this.Pager.PageSize).ToList();
        }
        public List<Product> Products { get; set; }

        public Pager Pager { get; set; }

    }
    public class ProductAddModel
    {
        public Product product { get; set; }
    }

    public class ProductFilterModel
    {
        public int? Page { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<int> Status { get; set; }
        public int? CategoryId { get; set; }
        public string Search { get; set; }
    }
}
