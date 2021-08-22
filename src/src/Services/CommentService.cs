﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class CommentService : ICommentService
    {
        private RealtorContext context;
        public CommentService(RealtorContext context)
        {
            this.context = context;
        }
        public void addComment(Comment comment)
        {
            
                context.Comments.Add(comment);
                context.SaveChanges();
            
        }

        public void deleteComment(int id)
        {
            Comment comment = context.Comments.SingleOrDefault(a => a.Id.Equals(id));
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

        public List<Comment> findAll()
        {
            return context.Comments.OrderByDescending(x => x.Id).ToList();
        }

        public Comment fineOne(int id)
        {
            Comment comment = context.Comments.SingleOrDefault(a => a.Id.Equals(id));
            if (comment != null)
            {
                return comment;
            }
            else
            {
                return null;
            }
        }

        public void updateComment(Comment comment)
        {
            Comment editComment = context.Comments.SingleOrDefault(a => a.Id.Equals(comment.Id));
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

        public List<Comment> FindByPropId(int id)
        {
            return context.Comments.Where(c => c.Property_id.Equals(id)).OrderByDescending(x => x.Id).ToList();
        }
    }
}
