using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoLayer.Interface;
using RepoLayer.Entity;
using RepoLayer.Context;
using Microsoft.Extensions.Configuration;
using CommonLayer.Model;

namespace RepoLayer.Service
{
    public class ReviewRL:IReviewRL
    {
        private readonly FundooDBContext fundooDB;
        private readonly IConfiguration configuration;

        public ReviewRL(FundooDBContext fundooDB, IConfiguration configuration)
        {
            this.fundooDB = fundooDB;
            this.configuration = configuration;
        }

        public ReviewTable AddReview(ReviewModel reviewModel)
        {
            ReviewTable table = new ReviewTable();
            table.ReviewComment = reviewModel.ReviewComment;
            table.ReviewRating = reviewModel.ReviewRating;

            fundooDB.ReviewTable.Add(table);
            var result = fundooDB.SaveChanges();
            if(result != 0)
            {
                return table;
            }
            else
            {
                return null;
            }
            
        }
    }
}
