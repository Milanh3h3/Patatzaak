using Fridayfrietday.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Fridayfrietday.ViewModels
{
    public class ReviewViewModel
    {
        [BindNever]
        public IEnumerable<Review> Reviews { get; set; }
        public Review NewReview { get; set; }
    }
}
