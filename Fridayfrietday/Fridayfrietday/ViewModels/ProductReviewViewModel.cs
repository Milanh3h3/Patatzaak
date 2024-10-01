using System.Collections.Generic;
using Fridayfrietday.Models;

namespace Fridayfrietday.ViewModels
{
    public class ProductReviewViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
