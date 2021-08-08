using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class NotificationsServices : INotificationsServices
    {
        private RealtorContext context;
        public NotificationsServices(RealtorContext context)
        {
            this.context = context;
        }
        public void addNoti(Notifications notifications)
        {
            context.Notifications.Add(notifications);
            context.SaveChanges();
        }

        public void deleteNoti(int id)
        {
            Notifications notifications = context.Notifications.SingleOrDefault(a => a.Id.Equals(id));
            if (notifications != null)
            {
                context.Notifications.Remove(notifications);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<Notifications> findAll()
        {
            return context.Notifications.ToList();
        }

        public Notifications fineOne(int id)
        {
            Notifications notifications = context.Notifications.SingleOrDefault(a => a.Id.Equals(id));
            if (notifications != null)
            {
                return notifications;
            }
            else
            {
                return null;
            }
        }

        public void updateNoti(Notifications notifications)
        {
            Notifications editnotifications = context.Notifications.SingleOrDefault(a => a.Id.Equals(notifications.Id));
            if (editnotifications != null)
            {
                editnotifications.FromEmail = notifications.FromEmail;
                editnotifications.ToEmail = notifications.ToEmail;
                editnotifications.Reason = notifications.Reason;
                editnotifications.Description = notifications.Description;
                

                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
