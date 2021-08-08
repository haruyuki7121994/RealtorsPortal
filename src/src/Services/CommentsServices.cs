using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class CommentsServices : ICommentsServices
    {
        private RealtorContext context;
        public CommentsServices(RealtorContext context)
        {
            this.context = context;
        }
        public void addComment(Comments comment)
        {
            
                context.Comments.Add(comment);
                context.SaveChanges();
            
        }

        public void deleteComment(int id)
        {
            Comments comment = context.Comments.SingleOrDefault(a => a.Id.Equals(id));
            if (comment != null)
            {
                context.Comments.Remove(comment);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<Comments> findAll()
        {
            return context.Comments.ToList();
        }

        public Comments fineOne(int id)
        {
            Comments comment = context.Comments.SingleOrDefault(a => a.Id.Equals(id));
            if (comment != null)
            {
                return comment;
            }
            else
            {
                return null;
            }
        }

        public void updateComment(Comments comment)
        {
            Comments editComment = context.Comments.SingleOrDefault(a => a.Id.Equals(comment.Id));
            if (editComment != null)
            {
                editComment.Title = comment.Title;
                editComment.Description = comment.Description;

                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
