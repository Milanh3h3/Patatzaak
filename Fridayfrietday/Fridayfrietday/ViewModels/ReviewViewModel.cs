using Fridayfrietday.Models;
using System.Collections.Generic;

namespace Fridayfrietday.ViewModels
{
    public class ReviewViewModel
    {
        public IEnumerable<Review> Reviews { get; set; }
        public Review NewReview { get; set; }
    }
}
