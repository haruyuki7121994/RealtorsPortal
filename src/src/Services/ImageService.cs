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
        public void addImages(Image images)
        {
            Image newRegion = context.Images.SingleOrDefault(a => a.Id.Equals(images.Id));
            if (newRegion == null)
            {
                context.Images.Add(images);
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public void deleteImages(int id)
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
            return context.Images.ToList();
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

        public void updateImages(Image images)
        {
            Image editImages = context.Images.SingleOrDefault(a => a.Id.Equals(images.Id));
            if (editImages != null)
            {
                editImages.Url = images.Url;
                editImages.Thumbnail_url = images.Thumbnail_url;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
