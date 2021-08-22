using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class ImageService : IImageService
    {
        private RealtorContext context;
        public ImageService(RealtorContext context)
        {
            this.context = context;
        }
        public void addImage(Image image)
        {
            context.Images.Add(image);
            context.SaveChanges();
        }

        public void deleteImage(int id)
        {
            Image images = context.Images.SingleOrDefault(a => a.Id.Equals(id));
            if (images != null)
            {
                context.Images.Remove(images);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<Image> findAll()
        {
            return context.Images.OrderByDescending(x => x.Id).ToList();
        }

        public Image fineOne(int id)
        {
            Image images = context.Images.SingleOrDefault(a => a.Id.Equals(id));
            if (images != null)
            {
                return images;
            }
            else
            {
                return null;
            }
        }

        public void updateImage(Image image)
        {
            Image editImages = context.Images.SingleOrDefault(a => a.Id.Equals(image.Id));
            if (editImages != null)
            {
                editImages.Url = image.Url;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public List<Image> FindByPropertyId(int property_id)
        {
            return context.Images.Where(i => i.Property_id.Equals(property_id)).ToList();
        }
    }
}
