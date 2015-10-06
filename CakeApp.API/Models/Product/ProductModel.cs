using System.ComponentModel.DataAnnotations;
using Core.Product;
using System.Collections.Generic;
using Core.Tag;
using Core.Group;
using Core.Category;
using Core.Infos;
using Core.Size;


namespace CakeApp.API.Models.Product
{
    public class ProductModel
    {
        public ProductModel() {
            //ProductImageList = new List<ProductImageModel>();
            CategoryList = new List<CategoriesModel>();
            InfoList = new List<InfoModel>();
            SizeList = new List<SizesModel>();
            DefaultImage = new ProductImageModel();
            Tag = new TagModel();
            Group = new GroupsModel();
            PriceList = new List<ProductPriceModel>(); 


        }

        public long ProductID { get; set; }       
        public string ProductName { get; set; }      
        public string Title { get; set; }
        public ProductImageModel DefaultImage { get; set; }
        //public List<ProductImageModel> ProductImageList { get; set; }
        public TagModel Tag { get; set; }
        public GroupsModel Group { get; set; }
        public List<CategoriesModel> CategoryList { get; set; }
        public List<InfoModel> InfoList { get; set; }
        public List<SizesModel> SizeList { get; set; }
        public string AdditionalInfo { get; set; }
        public string Instructions { get; set; }
        public string Priority { get; set; }
        public List<ProductPriceModel> PriceList { get; set; }
        //public bool Status { get; set; }

    }
    public class ProductImageModel
    {
        public string ImageName { get; set; }
        //public string ImageAlt { get; set; }
    }
    public class TagModel
    {
        public string TagName { get; set; }
        public string TagID { get; set; }
    }
    public class GroupsModel
    {
        public string GroupName { get; set; }
        public string GroupID { get; set; }
    }
    public class CategoriesModel
    {
        public string CategoryName { get; set; }
        public string CategoryID { get; set; }
        public string CategoryImage { get; set; }
    }
    public class InfoModel
    {
        public string InfoName { get; set; }
        public string InfoID { get; set; }
    }
    public class SizesModel
    {
        public string SizeName { get; set; }
        public string SizeID { get; set; }
    }
    public class ProductPriceModel
    {
        public string SizeName { get; set; }
        public string SizeID { get; set; }
        public string Price { get; set; }

    }

   
}