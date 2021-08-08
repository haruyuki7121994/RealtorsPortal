using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IImagesServices
    {
        List<Images> findAll();
        Images fineOne(int id);
        void addImages(Images images);
        void updateImages(Images images);
        void deleteImages(int id);
    }
}
