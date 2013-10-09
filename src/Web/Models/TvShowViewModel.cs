using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class TvShowViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Collection<RatingViewModel> Ratings { get; set; }


        public int AverageRating
        {
            get { return Ratings.Any() ? (int)Math.Round(Ratings.Average(rating => rating.Rating)) : 0; }
        }
        public TvShowViewModel()
        {
            Ratings = new Collection<RatingViewModel>();
        }

        public int RatingForUser(string username)
        {
            var rating = Ratings.SingleOrDefault(r => r.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
            if (rating == null) return 0;
            return rating.Rating;
        }

        internal void AddRating(string username, int rating)
        {
            var existingRating = Ratings.SingleOrDefault(r => r.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
            if (existingRating == null)
            {
                Ratings.Add(new RatingViewModel()
                {
                    Rating = rating,
                    UserName = username
                });
            }
            else
            {
                existingRating.Rating = rating;
            }
        }
    }
}