using Core.Category;
using Core.Group;
using Core.Tag;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Core.Product;
using Core.Infos;
using Core.Size;

namespace CakeApp.Areas.Admin.Models.Product
{
    public class ProductModel
    {

        public ProductModel()
        {
            TagList = new List<Tags>();
            GroupList = new List<Groups>();
            CategoryList = new List<Categories>();
            InfoGroupsList = new List<InfoGroups>();
            SizeGroupList = new List<SizeGroup>();
            InfoList = new List<ProductInfoModel>();
            ProductCategoriesList = new List<ProductCategories>();
            ProductInfoList = new List<ProductInfo>();
            ProductSizesList = new List<ProductSizes>();
            ProductPriceList = new List<ProductPrice>();
            ProductImageList = new List<ProductImages>();
            SizeList = new List<ProductSizeModel>();
            


        }
        public long ProductID { get; set; }
        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        //public string AdditionalInfo { get; set; }
        //public string Instructions { get; set; }


        [Required(ErrorMessage = "Tag is required.")]
        public string TagID { get; set; }
        public string TagName { get; set; }

        //[Required(ErrorMessage = "Category Name is required.")]
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }

        public string CategoryNames { get; set; }

         [Required(ErrorMessage = "At least One Category must be selected.")]
        public string CategoryIDs { get; set; }

        [Required(ErrorMessage = "Group is required.")]
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        //public string GroupIDs { get; set; }

        [Required(ErrorMessage = "At least One Info must be selected.")]
        public string InfoIDs { get; set; }

        [Required(ErrorMessage = "At least One Size  must be selected and Price for the size must be given")]
        public string SizeIDs { get; set; }



        public string Prices { get; set; }



        public long ProductImageID { get; set; }

        //[Required(ErrorMessage = "Image is required")]
        public string ImageName { get; set; }

       
        public string ImageTitle { get; set; }

       
        public string ImageAlt { get; set; }

     
      
        public int? ImagePriority { get; set; }

        public string ImageStatus { get; set; }


       
        



        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int? Priority { get; set; }
        public string Price { get; set; }
        public string Status { get; set; }
        public bool statusAdditionalInfo { get; set; }
        public bool statusInstructions { get; set; }
        public string GroupNames { get; set; }


        public List<ProductCategories> ProductCategoriesList { get; set; }
        public List<ProductInfo> ProductInfoList { get; set; }
        public List<ProductSizes> ProductSizesList { get; set; }
        public List<ProductPrice> ProductPriceList { get; set; }
        public List<ProductImages> ProductImageList { get; set; }



        public List<Categories> CategoryList { get; set; }
        public List<Tags> TagList { get; set; }
        public List<Groups> GroupList { get; set; }
        public List<InfoGroups> InfoGroupsList { get; set; }
        public List<SizeGroup> SizeGroupList { get; set; }
        public List<ProductInfoModel> InfoList { get; set; }
        public List<ProductSizeModel> SizeList { get; set; }

    }
    public class ProductInfoModel
    {

        public long InfoID { get; set; }
        public string InfoName { get; set; }
    }

    public class ProductSizeModel
    {

        public long SizeID { get; set; }
        public string Size { get; set; }
    }

    //public class ProductImageodel
    //{

    //    public long ProductImageID { get; set; }
    //    public long ProductID { get; set; }
    //    public string ImageName { get; set; }
    //    [Required(ErrorMessage = "Title is required")]
    //    public string ImageTitle { get; set; }

    //    [Required(ErrorMessage = "Alternative Text is required.")]
    //    public string ImageAlt { get; set; }

    //    [Required]
    //    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
    //    public int? ImagePriority { get; set; }

    //    public string ImageStatus { get; set; }

    //}
}