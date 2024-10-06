﻿using System.Collections.Generic;
using Fridayfrietday.Models;

namespace Fridayfrietday.ViewModels
{
    public class ProductReviewViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public List<Sauce> AvailableSauces { get; set; }
        public List<int> SelectedSauceIds { get; set; } = new List<int>();

        public Product SelectedProduct;
        public IEnumerable<Review> Reviews { get; set; }
    }
}
