using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface ICommentService
    {
        List<Comment> findAll();
        Comment fineOne(int id);
        void addComment(Comment comment);
        void updateComment(Comment comment);
        void deleteComment(int id);
    }
}
