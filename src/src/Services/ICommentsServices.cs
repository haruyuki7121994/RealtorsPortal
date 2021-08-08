using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface ICommentsServices
    {
        List<Comments> findAll();
        Comments fineOne(int id);
        void addComment(Comments comment);
        void updateComment(Comments comment);
        void deleteComment(int id);
    }
}
