using CakeApp.API.Models.Product;
using Core.Product;
using Service.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;


namespace CakeApp.API.Controllers.Product
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("API/Product")]
    public class ProductController : ApiController
    {
        private readonly IProductsService _productsService;
        private readonly IProductPriceService _productPriceService;
        public string SiteURL = System.Configuration.ConfigurationManager.AppSettings["SiteURL"].ToString();
        public ProductController(IProductsService productsService, IProductPriceService productPriceService)
        {
            _productsService = productsService;
            _productPriceService = productPriceService;
        }
        //
        // GET: /Product/


        [HttpGet]
        [Route("Product")]
        public HttpResponseMessage Get()
        {

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var productDetails = _productsService.GetAllProducts().Where(p => p.Status).OrderByDescending(p => p.Priority);
                List<ProductModel> ProductModelList = new List<ProductModel>();
                if (productDetails != null)
                {
                    foreach (var item in productDetails)
                    {
                        ProductModel model = new ProductModel();
                        model.ProductID = item.ProductID;
                        model.ProductName = item.ProductName;
                        model.Title = item.Title;
                        model.AdditionalInfo = item.AdditionalInfo;
                        model.Instructions = item.Instructions;
                        model.Priority = item.Priority.ToString();
                        if (item.ProductImageName != null)
                            model.DefaultImage.ImageName = SiteURL + "Content/Assets/ProductImages/" + item.ProductImageName;
                        else
                            model.DefaultImage.ImageName = "";

                        //if (item.ProductImages.Count>0)
                        //    model.DefaultImage.ImageName = SiteURL + "Content/Assets/ProductImages/" + item.ProductImages.FirstOrDefault().ImageName;
                        model.Tag.TagName = item.Tags.TagName;
                        model.Tag.TagID = item.Tags.TagID.ToString();
                        model.Group.GroupID = item.Groups.GroupID.ToString();
                        model.Group.GroupName = item.Groups.GroupName;
                        //if (item.ProductImages.Count > 0)
                        //{
                        //    foreach (var img in item.ProductImages)
                        //    {
                        //        model.ProductImageList.Add(new ProductImageModel {
                        //            ImageName = SiteURL + "Content/Assets/ProductImages/" + img.ImageName
                        //        });
                        //    }
                        //}

                        if (item.ProductCategories.Count > 0)
                        {
                            foreach (var cat in item.ProductCategories)
                            {
                                model.CategoryList.Add(new CategoriesModel
                                {
                                    CategoryID = cat.CategoryID.ToString(),
                                    CategoryName = cat.Categories.CategoryName,
                                    CategoryImage = SiteURL + "Content/Assets/CategoryImages/" + cat.Categories.CategoryImages.ImageName
                                });
                            }
                        }
                        if (item.ProductInfo.Count > 0)
                        {
                            foreach (var val in item.ProductInfo)
                            {
                                model.InfoList.Add(new InfoModel
                                {
                                    InfoID = val.InfoID.ToString(),
                                    InfoName = val.Info.InfoName
                                });
                            }
                        }
                        if (item.ProductSizes.Count > 0)
                        {
                            foreach (var val in item.ProductSizes)
                            {
                                model.SizeList.Add(new SizesModel
                                {
                                    SizeID = val.SizeID.ToString(),
                                    SizeName = val.Sizes.Size
                                });
                            }
                        }


                        ProductModelList.Add(model);
                    }
                    response = Request.CreateResponse(HttpStatusCode.OK, ProductModelList);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
                }


                return response;
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.Forbidden, ex.InnerException.Message);
            }
            return response;
        }


        [HttpGet]
        [Route("Getproducts/{sortBy}/{categoryid}/{noOfRecord}/{pageno}")]
        public HttpResponseMessage Getproducts(string sortBy, string categoryid, string noOfRecord, string pageno)
        {

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                List<Products> productDetails = new List<Products>();
                long CategoryID = Convert.ToInt64(categoryid);
                int NoOfRecord = Convert.ToInt32(noOfRecord);
                int PageNo = Convert.ToInt32(pageno);
                int totalNoOfRecord = _productsService.GetAllProductsByQueryable().Where(p => p.Status).Count();

                int skip = 0;
                skip = (PageNo - 1) * NoOfRecord;

                if ((NoOfRecord * PageNo) <= totalNoOfRecord)
                {

                    if (sortBy.ToLower() == "price")
                    {
                        //if (orderBy.ToLower() == "asc")
                        //{
                        //    productDetails = _productsService.GetAllProducts().Where(p => p.Status).OrderBy(p => p.ProductPrice.Any()).ToList();
                        //}
                        //if (orderBy.ToLower() == "desc")
                        //{
                        //    productDetails = _productsService.GetAllProducts().Where(p => p.Status).OrderByDescending(p => p.ProductPrice.FirstOrDefault().Price).ToList();
                        //}

                        productDetails = _productsService.GetAllProducts().Where(p => p.Status).ToList();
                        if (productDetails != null)
                        {

                            
                            switch (PageNo)
                            {
                                case 1:
                                    productDetails = _productsService.GetAllProductsByQueryable().Where(t => t.ProductCategories.FirstOrDefault().CategoryID == CategoryID).Skip(skip).Take(NoOfRecord).ToList();
                                    break;
                                case 2:
                                    Console.WriteLine("Case 2");
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;
                            }




                            productDetails = _productsService.GetAllProductsByQueryable().Where(t => t.ProductCategories.FirstOrDefault().CategoryID == CategoryID).Take(NoOfRecord).ToList();

                        }

                    }
                }


                List<ProductModel> ProductModelList = new List<ProductModel>();
                if (productDetails != null)
                {
                    foreach (var item in productDetails)
                    {
                        ProductModel model = new ProductModel();
                        model.ProductID = item.ProductID;
                        model.ProductName = item.ProductName;
                        model.Title = item.Title;
                        model.AdditionalInfo = item.AdditionalInfo;
                        model.Instructions = item.Instructions;
                        model.Priority = item.Priority.ToString();
                        if (item.ProductImageName != null)
                            model.DefaultImage.ImageName = SiteURL + "Content/Assets/ProductImages/" + item.ProductImageName;
                        else
                            model.DefaultImage.ImageName = "";



                        //if (item.ProductImages.Count>0)
                        //    model.DefaultImage.ImageName = SiteURL + "Content/Assets/ProductImages/" + item.ProductImages.FirstOrDefault().ImageName;
                        model.Tag.TagName = item.Tags.TagName;
                        model.Tag.TagID = item.Tags.TagID.ToString();
                        model.Group.GroupID = item.Groups.GroupID.ToString();
                        model.Group.GroupName = item.Groups.GroupName;
                        //if (item.ProductImages.Count > 0)
                        //{
                        //    foreach (var img in item.ProductImages)
                        //    {
                        //        model.ProductImageList.Add(new ProductImageModel
                        //        {
                        //            ImageName = SiteURL + "Content/Assets/ProductImages/" + img.ImageName
                        //        });
                        //    }
                        //}

                        if (item.ProductCategories.Count > 0)
                        {
                            foreach (var cat in item.ProductCategories)
                            {
                                model.CategoryList.Add(new CategoriesModel
                                {
                                    CategoryID = cat.CategoryID.ToString(),
                                    CategoryName = cat.Categories.CategoryName,
                                    CategoryImage = SiteURL + "Content/Assets/CategoryImages/" + cat.Categories.CategoryImages.ImageName
                                });
                            }
                        }
                        if (item.ProductInfo.Count > 0)
                        {
                            foreach (var val in item.ProductInfo)
                            {
                                model.InfoList.Add(new InfoModel
                                {
                                    InfoID = val.InfoID.ToString(),
                                    InfoName = val.Info.InfoName
                                });
                            }
                        }
                        if (item.ProductSizes.Count > 0)
                        {
                            foreach (var val in item.ProductSizes)
                            {
                                model.SizeList.Add(new SizesModel
                                {
                                    SizeID = val.SizeID.ToString(),
                                    SizeName = val.Sizes.Size
                                });
                            }
                        }

                        if (item.ProductPrice.Count > 0)
                        {
                            foreach (var val in item.ProductPrice)
                            {
                                model.PriceList.Add(new ProductPriceModel
                                {
                                    SizeID = val.SizeID.ToString(),
                                    SizeName = val.Sizes.Size,
                                    Price = val.Price.ToString()
                                });
                            }
                        }


                        ProductModelList.Add(model);
                    }

                    //var ProductModelListSorted = ProductModelList.OrderByDescending(p => p.PriceList.FirstOrDefault().Price);
                    response = Request.CreateResponse(HttpStatusCode.OK, ProductModelList);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
                }


                return response;
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.Forbidden, ex.InnerException.Message);
            }
            return response;
        }







        [HttpGet]
        [Route("Product/{Id}")]
        public HttpResponseMessage GetByID(string Id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (!string.IsNullOrEmpty(Id))
            {
                long ID = Convert.ToInt64(Id);

                try
                {
                    var item = _productsService.GetAllProducts().Where(p => p.Status && p.ProductID == ID).FirstOrDefault();

                    if (item != null)
                    {

                        ProductModel model = new ProductModel();
                        model.ProductID = item.ProductID;
                        model.ProductName = item.ProductName;
                        model.Title = item.Title;
                        model.AdditionalInfo = item.AdditionalInfo;
                        model.Instructions = item.Instructions;
                        model.Priority = item.Priority.ToString();
                        if (item.ProductImageName != null)
                            model.DefaultImage.ImageName = SiteURL + "Content/Assets/ProductImages/" + item.ProductImageName;
                        else
                            model.DefaultImage.ImageName = "";

                        //if (item.ProductImages.Count > 0)
                        //    model.DefaultImage.ImageName = SiteURL + "Content/Assets/ProductImages/" + item.ProductImages.FirstOrDefault().ImageName;
                        model.Tag.TagName = item.Tags.TagName;
                        model.Tag.TagID = item.Tags.TagID.ToString();
                        model.Group.GroupID = item.Groups.GroupID.ToString();
                        model.Group.GroupName = item.Groups.GroupName;
                        //if (item.ProductImages.Count > 0)
                        //{
                        //    foreach (var img in item.ProductImages)
                        //    {
                        //        model.ProductImageList.Add(new ProductImageModel
                        //        {
                        //            ImageName = SiteURL + "Content/Assets/ProductImages/" + img.ImageName
                        //        });
                        //    }
                        //}

                        if (item.ProductCategories.Count > 0)
                        {
                            foreach (var cat in item.ProductCategories)
                            {
                                model.CategoryList.Add(new CategoriesModel
                                {
                                    CategoryID = cat.CategoryID.ToString(),
                                    CategoryName = cat.Categories.CategoryName,
                                    CategoryImage = SiteURL + "Content/Assets/CategoryImages/" + cat.Categories.CategoryImages.ImageName
                                });
                            }
                        }
                        if (item.ProductInfo.Count > 0)
                        {
                            foreach (var val in item.ProductInfo)
                            {
                                model.InfoList.Add(new InfoModel
                                {
                                    InfoID = val.InfoID.ToString(),
                                    InfoName = val.Info.InfoName
                                });
                            }
                        }
                        if (item.ProductSizes.Count > 0)
                        {
                            foreach (var val in item.ProductSizes)
                            {
                                model.SizeList.Add(new SizesModel
                                {
                                    SizeID = val.SizeID.ToString(),
                                    SizeName = val.Sizes.Size
                                });
                            }
                        }




                        response = Request.CreateResponse(HttpStatusCode.OK, model);
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
                    }


                    return response;
                }
                catch (Exception ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.Forbidden, ex.InnerException.Message);
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Check Service Link");
            }
            return response;
        }
        //[HttpGet]
        //[Route("{id}")]
        //public HttpResponseMessage GetByID()
        //{
        //    HttpResponseMessage response = new HttpResponseMessage();

        //    return response;
        //}


    }

    public class SearchProducts
    {
        public string sortBy;
        public string orderBy;
        public string searchString;
        public string category;
        public string noOfRecords;
        public string pageno;

    }
}