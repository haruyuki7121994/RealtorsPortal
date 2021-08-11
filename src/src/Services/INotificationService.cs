using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface INotificationService
    {
        List<Notification> findAll();
        Notification fineOne(int id);
        void addNoti(Notification notifications);
        void updateNoti(Notification notifications);
        void deleteNoti(int id);
    }
}
