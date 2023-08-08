using CommonLayer.Model;
using ManagerLayer.Interface;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Service
{
    public class ReviewBL:IReviewBL
    {
        public readonly IReviewRL reviewRL;

        public ReviewBL(IReviewRL reviewRL)
        {
            this.reviewRL = reviewRL;
        }

        public ReviewTable AddReview(ReviewModel reviewModel)
        {
            try
            {
                return this.reviewRL.AddReview(reviewModel);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
