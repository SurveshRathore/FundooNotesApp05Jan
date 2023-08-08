using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IReviewBL
    {
        public ReviewTable AddReview(ReviewModel reviewModel);
    }
}
