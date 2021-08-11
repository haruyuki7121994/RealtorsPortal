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
        void addImages(Image images);
        void updateImages(Image images);
        void deleteImages(int id);
    }
}
