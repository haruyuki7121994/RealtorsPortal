using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class NotificationService : INotificationService
    {
        private RealtorContext context;
        public NotificationService(RealtorContext context)
        {
            this.context = context;
        }
        public void addNoti(Notification notifications)
        {
            context.Notifications.Add(notifications);
            context.SaveChanges();
        }

        public void deleteNoti(int id)
        {
            Notification notifications = context.Notifications.SingleOrDefault(a => a.Id.Equals(id));
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

        public List<Notification> findAll()
        {
            return context.Notifications.OrderByDescending(x => x.Id).ToList();
        }

        public Notification fineOne(int id)
        {
            Notification notifications = context.Notifications.SingleOrDefault(a => a.Id.Equals(id));
            if (notifications != null)
            {
                return notifications;
            }
            else
            {
                return null;
            }
        }

        public void updateNoti(Notification notifications)
        {
            Notification editnotifications = context.Notifications.SingleOrDefault(a => a.Id.Equals(notifications.Id));
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
