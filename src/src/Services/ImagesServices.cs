using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class ImagesServices : IImagesServices
    {
        private RealtorContext context;
        public ImagesServices(RealtorContext context)
        {
            this.context = context;
        }
        public void addImages(Images images)
        {
            Images newRegion = context.Images.SingleOrDefault(a => a.Id.Equals(images.Id));
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
            Images images = context.Images.SingleOrDefault(a => a.Id.Equals(id));
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

        public List<Images> findAll()
        {
            return context.Images.ToList();
        }

        public Images fineOne(int id)
        {
            Images images = context.Images.SingleOrDefault(a => a.Id.Equals(id));
            if (images != null)
            {
                return images;
            }
            else
            {
                return null;
            }
        }

        public void updateImages(Images images)
        {
            Images editImages = context.Images.SingleOrDefault(a => a.Id.Equals(images.Id));
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
