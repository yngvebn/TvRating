using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class RatingViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
    }
}