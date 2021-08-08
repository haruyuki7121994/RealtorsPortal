using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface INotificationsServices
    {
        List<Notifications> findAll();
        Notifications fineOne(int id);
        void addNoti(Notifications notifications);
        void updateNoti(Notifications notifications);
        void deleteNoti(int id);
    }
}
