using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IImageService
    {
        List<Image> findAll();
        Image fineOne(int id);
        List<Image> FindByPropertyId(int property_id);
        void addImage(Image image);
        void updateImage(Image image);
        void deleteImage(int id);
    }
}
