using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
//using System.Net.Http;
//using System.Web.Http;
using System.Web.Mvc;
using CakeApp.Areas.Admin.Models.Product;
using Core.Tag;
using Service.Tag;
using Service.Category;
using Core.Category;
using Service.Product;
using Service.Group;
using Core.Group;
using Service.Infos;
using Core.Infos;
using Service.Size;
using Core.Size;
//using CakeApp.Areas.Admin.Models.Product;
using Core.Product;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
//using Core.Product;
using Service.infos;

namespace CakeApp.Areas.Admin.Controllers.Product
{
    public class ProductsController : BaseController
    {
        private readonly ITagService _tagService;
        private readonly ICategoriesService _categoriesService;
        private readonly IProductsService _productsService;
        private readonly IGroupService _groupService;
        private readonly IInfoGroupsService _infoGroupsService;
        private readonly ISizeGroupService _sizeGroupService;
        private readonly IProductCategoriesService _productCategoriesService;
        private readonly IProductInfoService _productInfoService;
        private readonly IProductPriceService _productPriceService;
        private readonly IProductSizesService _productSizesService;
        private readonly IProductImagesService _productImagesService;
        private readonly IInfoGroupsService _iinfoGroupsService;
        private readonly ISizeGroupService _isizeGroupService;
        private readonly IProductLogService _productLogService;

        public ProductsController(
            ITagService tagService,
            ICategoriesService categoriesService,
            IProductsService productsService,
            IGroupService groupService,
            IInfoGroupsService infoGroupsService,
            ISizeGroupService sizeGroupService,
            IProductCategoriesService productCategoriesService,
            IProductInfoService productInfoService,
            IProductPriceService productPriceService,
            IProductSizesService productSizesService,
            IProductImagesService productImagesService,
            IInfoGroupsService iinfoGroupsService,
            ISizeGroupService isizeGroupService,
            IProductLogService productLogService

                        )
        {

            _tagService = tagService;
            _categoriesService = categoriesService;
            _productsService = productsService;
            _groupService = groupService;
            _infoGroupsService = infoGroupsService;
            _sizeGroupService = sizeGroupService;
            _productCategoriesService = productCategoriesService;
            _productInfoService = productInfoService;
            _productPriceService = productPriceService;
            _productSizesService = productSizesService;
            _productImagesService = productImagesService;
            _iinfoGroupsService = iinfoGroupsService;
            _isizeGroupService = isizeGroupService;
            _productLogService = productLogService;

        }

        public ActionResult ProductList()
        {


            List<ProductModel> model = new List<ProductModel>();
            try
            {


                //IList<Products> productList = _productsService.GetAllProducts().OrderByDescending(t => t.Priority).ToList();

                IList<Products> productList = _productsService.GetAllProducts().OrderBy(t => t.Priority).ToList();


                if (productList.Count > 0)
                {
                    foreach (var product in productList)
                    {
                        string categoryname = null;


                        IList<ProductCategories> productcategoryList = _productCategoriesService.GetAllProductsCategoryByQueryable().Where(t => t.ProductID == product.ProductID).ToList();

                        if (productcategoryList.Count > 0)
                        {
                            foreach (var item in productcategoryList)
                            {

                                Categories category = new Categories();
                                category.CategoryID = Convert.ToInt64(item.CategoryID);
                                category.CategoryName = item.Categories.CategoryName;
                                categoryname += category.CategoryName + ',';

                                //Groups group = new Groups();
                                //group.GroupID = Convert.ToInt64(item.GroupID);
                                //group.GroupName = item.Groups.GroupName;
                                //groupname += group.GroupName + ',';

                            }

                            categoryname = categoryname.Trim(',');
                        }
                        else
                        {
                            categoryname = "";

                        }

                        model.Add(new ProductModel
                        {
                            ProductID = product.ProductID,
                            ProductName = product.ProductName,
                            Title = product.Title,
                            TagID = product.TagID.ToString(),
                            TagName = product.Tags.TagName,
                            Priority = product.Priority,
                            GroupID = product.GroupID.ToString(),
                            GroupName = product.Groups.GroupName,
                            CategoryNames = categoryname,
                            Status = (product.Status) == true ? "Enable" : "Disable",


                        });


                    }
                }
                View(model);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }

            return View(model);


        }

        public ActionResult AddProduct()
        {
            ProductModel model = new ProductModel();
            IList<Tags> TagList = _tagService.GetAllTags().Where(t => t.TagName != null).ToList();
            IList<Categories> CategoryList = _categoriesService.GetAllCategories().Where(t => t.CategoryName != null).ToList();
            IList<Groups> GroupList = _groupService.GetAllGroups().Where(t => t.GroupName != null).ToList();

            model.Priority = 0;

            //int maxPriority = 0;
            //var details = _productsService.GetAllProducts();
            //if (details != null && details.Count > 0)
            //{
            //    maxPriority = details.Max(t => t.Priority);

            //}
            //if (maxPriority == null || maxPriority == 0)
            //    model.Priority = 10;
            //else
            //    model.Priority = maxPriority + 10;

            //var detailsImg = _productImagesService.GetAllProductImages().ToList();
            //int maxPriorityImg = 0;
            //if (detailsImg != null && detailsImg.Count > 0)
            //{
            //    maxPriorityImg = detailsImg.Max(t => t.Priority);

            //}

            //if (maxPriorityImg == null || maxPriorityImg == 0)
            //    model.ImagePriority = 10;
            //else
            //    model.ImagePriority = maxPriorityImg + 10;

            if (TagList.Count > 0)
            {
                foreach (var item in TagList)
                {
                    model.TagList.Add(new Tags
                    {
                        TagID = item.TagID,
                        TagName = item.TagName
                    });
                }

            }

            if (CategoryList.Count > 0)
            {
                foreach (var item in CategoryList)
                {
                    model.CategoryList.Add(new Categories
                    {
                        CategoryID = item.CategoryID,
                        CategoryName = item.CategoryName
                    });
                }

            }

            if (GroupList.Count > 0)
            {
                foreach (var item in GroupList)
                {
                    model.GroupList.Add(new Groups
                    {
                        GroupID = item.GroupID,
                        GroupName = item.GroupName
                    });
                }

            }

            model.ProductImageList = null;


            //var prodImageList = _productImagesService.GetAllProductImagesByQueryable().Where(t=>t.ProductID);
            //if (prodImageList != null)
            //{
            //    foreach (var item in prodImageList)
            //    {
            //        model.ProductImageList.Add(new ProductImages
            //        {
            //            ProductImageID = item.ProductImageID,
            //            ProductID = item.ProductID,
            //            ImageName=item.ImageName,
            //            Title=item.Title,
            //            ImageAlt=item.ImageAlt,
            //            Priority=item.Priority,
            //            Status=item.Status
            //        });
            //    }

            //}

            return View(model);
        }

        [HttpGet]
        public ActionResult EditProduct(string id)
        {
            ProductModel model = new ProductModel();
            long ID = Convert.ToInt64(id);


            var varEditProduct = _productsService.GetAllProductsByQueryable().Where(t => t.ProductID == ID).FirstOrDefault();

            if (varEditProduct != null)
            {

                model.ProductID = varEditProduct.ProductID;
                model.ProductName = varEditProduct.ProductName;
                model.Title = varEditProduct.Title;
                model.TagID = varEditProduct.TagID.ToString();
                model.Priority = varEditProduct.Priority;
                model.GroupID = varEditProduct.GroupID.ToString();
                //model.AdditionalInfo = varEditProduct.AdditionalInfo;
                //model.Instructions = varEditProduct.Instructions;

                model.ImageName = varEditProduct.ProductImageName;
                //model.ProductImageList.AddRange(varEditProduct.ProductImages);
                if (varEditProduct.Status == true)
                {
                    model.Status = "1";
                }
                else
                {
                    model.Status = "0";
                }
                if (varEditProduct.statusAdditionalInfo == true)
                {
                    model.statusAdditionalInfo = true;
                }
                else
                {
                    model.statusAdditionalInfo = false;
                }
                if (varEditProduct.statusInstructions == true)
                {
                    model.statusInstructions = true;
                }
                else
                {
                    model.statusInstructions = false;
                }

            }
            if (model.ProductID > 0)
            {
                List<ProductCategories> productcategoryList = _productCategoriesService.GetAllProductsCategoryByQueryable().Where(t => t.ProductID == ID).ToList();
                model.ProductCategoriesList = productcategoryList;

            }


            if (model.ProductID > 0)
            {
                List<ProductInfo> productinfoList = _productInfoService.GetAllProductsInfoByQueryable().Where(t => t.ProductID == ID).ToList();
                model.ProductInfoList = productinfoList;


            }


            long groupid = Convert.ToInt64(model.GroupID);
            IList<InfoGroups> InfoGroupsList = _iinfoGroupsService.GetAllInfoGroupsByQueryable().Where(t => t.GroupID == groupid).ToList();

            if (InfoGroupsList.Count > 0)
            {

                foreach (var item in InfoGroupsList)
                {

                    ProductInfoModel modelinfo = new ProductInfoModel();
                    modelinfo.InfoID = item.Info.InfoID;
                    modelinfo.InfoName = item.Info.InfoName;
                    model.InfoList.Add(modelinfo);

                }

            }

            if (model.ProductID > 0)
            {
                List<ProductSizes> productsizeList = _productSizesService.GetAllProductsSizeByQueryable().Where(t => t.ProductID == ID).ToList();
                model.ProductSizesList = productsizeList;

            }
            IList<SizeGroup> SizeGroupsList = _isizeGroupService.GetAllSizeGroupsByQueryable().Where(t => t.GroupID == groupid).ToList();
            if (SizeGroupsList.Count > 0)
            {

                foreach (var item in SizeGroupsList)
                {

                    ProductSizeModel modelsize = new ProductSizeModel();
                    modelsize.SizeID = item.Sizes.SizeID;
                    modelsize.Size = item.Sizes.Size;
                    model.SizeList.Add(modelsize);

                }

            }


            if (model.ProductID > 0)
            {
                List<ProductPrice> productpriceList = _productPriceService.GetAllProductsPriceByQueryable().Where(t => t.ProductID == ID).ToList();
                model.ProductPriceList = productpriceList;

            }


            IList<Tags> TagList = _tagService.GetAllTags().Where(t => t.TagName != null).ToList();


            if (TagList.Count > 0)
            {
                foreach (var item in TagList)
                {
                    model.TagList.Add(new Tags
                    {
                        TagID = item.TagID,
                        TagName = item.TagName
                    });
                }

            }
            IList<Groups> GroupList = _groupService.GetAllGroups().Where(t => t.GroupName != null).ToList();


            if (GroupList.Count > 0)
            {
                foreach (var item in GroupList)
                {
                    model.GroupList.Add(new Groups
                    {
                        GroupID = item.GroupID,
                        GroupName = item.GroupName
                    });
                }

            }

            IList<Categories> CategoryList = _categoriesService.GetAllCategories().Where(t => t.CategoryName != null).ToList();


            if (CategoryList.Count > 0)
            {
                foreach (var item in CategoryList)
                {
                    model.CategoryList.Add(new Categories
                    {
                        CategoryID = item.CategoryID,
                        CategoryName = item.CategoryName
                    });
                }

            }



            return View(model);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model != null)
                    {
                        //Products product = new Products();
                        var varEditProduct = _productsService.GetAllProductsByQueryable().Where(t => t.ProductID == model.ProductID);

                        if (varEditProduct != null)
                        {
                            if (varEditProduct.Where(t => t.ProductID == model.ProductID).Count() != 0)
                            {

                                if (Request.Files.Count > 0)
                                {
                                    var Idfile = Request.Files[0];
                                    if (Idfile != null && Idfile.ContentLength > 0)
                                    {
                                        var filename = Path.GetFileName(Idfile.FileName);
                                        string extension = Path.GetExtension(Idfile.FileName);
                                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".bmp" || extension.ToLower() == ".png" || extension.ToLower() == ".gif")
                                        {
                                            string fName = DateTime.Now.ToString("yyyyMMdd_hhssff") + extension;
                                            var path = Path.Combine(Server.MapPath("~/Content/Assets/ProductImages/"), fName);
                                            Idfile.SaveAs(path);

                                            model.ImageName = fName;

                                            Products product = new Products();

                                            product.ProductID = model.ProductID;
                                            product.ProductName = model.ProductName;
                                            product.Title = model.Title;
                                            product.TagID = Convert.ToInt64(model.TagID);
                                            product.Priority = Convert.ToInt32(model.Priority);
                                            product.GroupID = Convert.ToInt64(model.GroupID);
                                           
                                            //product.ProductImageName = model.ImageName;

                                            product.ProductImageName = fName; ;
                                            product.Status = Convert.ToBoolean(model.Status == "1" ? true : false);
                                            product.statusAdditionalInfo = model.statusAdditionalInfo;
                                            product.statusInstructions = model.statusInstructions;

                                            _productsService.updateProducts(product);

                                            if (model.ProductID > 0)
                                            {
                                                List<ProductCategories> productcategoryList = _productCategoriesService.GetAllProductsCategoryByQueryable().Where(t => t.ProductID == model.ProductID).ToList();
                                                model.ProductCategoriesList = productcategoryList;
                                                string categories = model.CategoryIDs;
                                                categories = categories.Trim(',');
                                                string[] strcategories = categories.Split(',');

                                                _productCategoriesService.deleteProductCategories(model.ProductID);
                                                foreach (var item in strcategories)
                                                {
                                                    ProductCategories prodductcategory = new ProductCategories();
                                                    prodductcategory.ProductID = product.ProductID;
                                                    prodductcategory.CategoryID = Convert.ToInt64(item);

                                                    _productCategoriesService.insertNewProductCategory(prodductcategory);
                                                }

                                            }

                                            if (model.ProductID > 0)
                                            {
                                                List<ProductInfo> productinfoList = _productInfoService.GetAllProductsInfoByQueryable().Where(t => t.ProductID == model.ProductID).ToList();

                                                model.ProductInfoList = productinfoList;
                                                string infos = model.InfoIDs;
                                                infos = infos.Trim(',');
                                                string[] strinfos = infos.Split(',');

                                                _productInfoService.deleteProductInfos(model.ProductID);

                                                foreach (var item in strinfos)
                                                {
                                                    ProductInfo productinfo = new ProductInfo();
                                                    productinfo.ProductID = product.ProductID;
                                                    productinfo.InfoID = Convert.ToInt64(item);

                                                    _productInfoService.insertNewProductInfo(productinfo);

                                                }

                                            }

                                            if (model.ProductID > 0)
                                            {
                                                List<ProductSizes> productsizeList = _productSizesService.GetAllProductsSizeByQueryable().Where(t => t.ProductID == model.ProductID).ToList();
                                                model.ProductSizesList = productsizeList;

                                                List<ProductPrice> productpriceList = _productPriceService.GetAllProductsPriceByQueryable().Where(t => t.ProductID == model.ProductID).ToList();
                                                model.ProductPriceList = productpriceList;


                                                _productSizesService.deleteProductSizes(model.ProductID);
                                                _productPriceService.deleteProductPrices(model.ProductID);

                                                string sizes = model.SizeIDs;
                                                sizes = sizes.Trim(',');
                                                string[] strsizeprice = sizes.Split(',');
                                                List<string> strLi = new List<string>();
                                                List<string> sizeLi = new List<string>();
                                                List<string> priceLi = new List<string>();

                                                for (int i = 0; i < strsizeprice.Length; i++)
                                                {
                                                    string sizepr_i = strsizeprice[i];
                                                    strLi.Add(sizepr_i);
                                                    string[] strarr = strLi[i].Split('|');
                                                    sizeLi.Add(strarr[0]);
                                                    priceLi.Add(strarr[1]);


                                                    ProductSizes prosize = new ProductSizes();

                                                    prosize.ProductID = product.ProductID;
                                                    prosize.SizeID = Convert.ToInt64(sizeLi[i]);


                                                    _productSizesService.insertNewProductSize(prosize);

                                                    ProductPrice proprice = new ProductPrice();

                                                    proprice.ProductID = product.ProductID;
                                                    proprice.GroupID = Convert.ToInt64(model.GroupID);
                                                    proprice.InfoIDs = model.InfoIDs.Trim(',');
                                                    proprice.SizeID = Convert.ToInt64(sizeLi[i]);
                                                    proprice.Price = Convert.ToDecimal(priceLi[i]);

                                                    _productPriceService.insertNewProductPrice(proprice);

                                                }

                                            }


                                            if (model.ProductID > 0)
                                            {
                                                {
                                                    List<ProductLog> productLog = _productLogService.GetAllProductsLogByQueryable().Where(t => t.ProductID == model.ProductID).ToList();

                                                    if (productLog != null || productLog.Count > 0)
                                                    {

                                                        ProductLog productlog = new ProductLog();
                                                        productlog.ProductID = model.ProductID;
                                                        productlog.IsAdded = false;
                                                        productlog.IsUpdated = true;
                                                        productlog.IsDeleted = false;
                                                        productlog.DateOfModification = DateTime.UtcNow;

                                                        _productLogService.insertNewProductLog(productlog);
                                                    }
                                                }

                                            }



                                            return RedirectToAction("ProductList", "Products", new { @area = "Admin" });
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("", "Select an valid Image file");
                                        }
                                    }
                                    else
                                    {
                                        Products product = new Products();

                                        product.ProductID = model.ProductID;
                                        product.ProductName = model.ProductName;
                                        product.Title = model.Title;
                                        product.TagID = Convert.ToInt64(model.TagID);
                                        product.Priority = Convert.ToInt32(model.Priority);
                                        product.GroupID = Convert.ToInt64(model.GroupID);
                                        
                                        product.ProductImageName = model.ImageName;

                                        product.Status = Convert.ToBoolean(model.Status == "1" ? true : false);
                                        product.statusAdditionalInfo = model.statusAdditionalInfo;
                                        product.statusInstructions = model.statusInstructions;

                                        _productsService.updateProducts(product);

                                        if (model.ProductID > 0)
                                        {
                                            List<ProductCategories> productcategoryList = _productCategoriesService.GetAllProductsCategoryByQueryable().Where(t => t.ProductID == model.ProductID).ToList();
                                            model.ProductCategoriesList = productcategoryList;
                                            string categories = model.CategoryIDs;
                                            categories = categories.Trim(',');
                                            string[] strcategories = categories.Split(',');

                                            _productCategoriesService.deleteProductCategories(model.ProductID);
                                            foreach (var item in strcategories)
                                            {
                                                ProductCategories prodductcategory = new ProductCategories();
                                                prodductcategory.ProductID = product.ProductID;
                                                prodductcategory.CategoryID = Convert.ToInt64(item);

                                                _productCategoriesService.insertNewProductCategory(prodductcategory);
                                            }

                                        }

                                        if (model.ProductID > 0)
                                        {
                                            List<ProductInfo> productinfoList = _productInfoService.GetAllProductsInfoByQueryable().Where(t => t.ProductID == model.ProductID).ToList();

                                            model.ProductInfoList = productinfoList;
                                            string infos = model.InfoIDs;
                                            infos = infos.Trim(',');
                                            string[] strinfos = infos.Split(',');

                                            _productInfoService.deleteProductInfos(model.ProductID);

                                            foreach (var item in strinfos)
                                            {
                                                ProductInfo productinfo = new ProductInfo();
                                                productinfo.ProductID = product.ProductID;
                                                productinfo.InfoID = Convert.ToInt64(item);

                                                _productInfoService.insertNewProductInfo(productinfo);

                                            }

                                        }

                                        if (model.ProductID > 0)
                                        {
                                            List<ProductSizes> productsizeList = _productSizesService.GetAllProductsSizeByQueryable().Where(t => t.ProductID == model.ProductID).ToList();
                                            model.ProductSizesList = productsizeList;

                                            List<ProductPrice> productpriceList = _productPriceService.GetAllProductsPriceByQueryable().Where(t => t.ProductID == model.ProductID).ToList();
                                            model.ProductPriceList = productpriceList;


                                            _productSizesService.deleteProductSizes(model.ProductID);
                                            _productPriceService.deleteProductPrices(model.ProductID);

                                            string sizes = model.SizeIDs;
                                            sizes = sizes.Trim(',');
                                            string[] strsizeprice = sizes.Split(',');
                                            List<string> strLi = new List<string>();
                                            List<string> sizeLi = new List<string>();
                                            List<string> priceLi = new List<string>();

                                            for (int i = 0; i < strsizeprice.Length; i++)
                                            {
                                                string sizepr_i = strsizeprice[i];
                                                strLi.Add(sizepr_i);
                                                string[] strarr = strLi[i].Split('|');
                                                sizeLi.Add(strarr[0]);
                                                priceLi.Add(strarr[1]);


                                                ProductSizes prosize = new ProductSizes();

                                                prosize.ProductID = product.ProductID;
                                                prosize.SizeID = Convert.ToInt64(sizeLi[i]);


                                                _productSizesService.insertNewProductSize(prosize);

                                                ProductPrice proprice = new ProductPrice();

                                                proprice.ProductID = product.ProductID;
                                                proprice.GroupID = Convert.ToInt64(model.GroupID);
                                                proprice.InfoIDs = model.InfoIDs.Trim(',');
                                                proprice.SizeID = Convert.ToInt64(sizeLi[i]);
                                                proprice.Price = Convert.ToDecimal(priceLi[i]);

                                                _productPriceService.insertNewProductPrice(proprice);

                                            }

                                        }


                                        if (model.ProductID > 0)
                                        {
                                            {
                                                List<ProductLog> productLog = _productLogService.GetAllProductsLogByQueryable().Where(t => t.ProductID == model.ProductID).ToList();

                                                if (productLog != null || productLog.Count > 0)
                                                {

                                                    ProductLog productlog = new ProductLog();
                                                    productlog.ProductID = model.ProductID;
                                                    productlog.IsAdded = false;
                                                    productlog.IsUpdated = true;
                                                    productlog.IsDeleted = false;
                                                    productlog.DateOfModification = DateTime.UtcNow;

                                                    _productLogService.insertNewProductLog(productlog);
                                                }
                                            }

                                        }
                                        return RedirectToAction("ProductList", "Products", new { @area = "Admin" });

                                    }

                                }

                                else
                                {
                                    Products product = new Products();

                                    product.ProductID = model.ProductID;
                                    product.ProductName = model.ProductName;
                                    product.Title = model.Title;
                                    product.TagID = Convert.ToInt64(model.TagID);
                                    product.Priority = Convert.ToInt32(model.Priority);
                                    product.GroupID = Convert.ToInt64(model.GroupID);
                                    //product.AdditionalInfo = model.AdditionalInfo;
                                    //product.Instructions = model.Instructions;
                                    product.ProductImageName = model.ImageName;

                                    product.Status = Convert.ToBoolean(model.Status == "1" ? true : false);
                                    product.statusAdditionalInfo = model.statusAdditionalInfo;
                                    product.statusInstructions = model.statusInstructions;

                                    _productsService.updateProducts(product);

                                    if (model.ProductID > 0)
                                    {
                                        List<ProductCategories> productcategoryList = _productCategoriesService.GetAllProductsCategoryByQueryable().Where(t => t.ProductID == model.ProductID).ToList();
                                        model.ProductCategoriesList = productcategoryList;
                                        string categories = model.CategoryIDs;
                                        categories = categories.Trim(',');
                                        string[] strcategories = categories.Split(',');

                                        _productCategoriesService.deleteProductCategories(model.ProductID);
                                        foreach (var item in strcategories)
                                        {
                                            ProductCategories prodductcategory = new ProductCategories();
                                            prodductcategory.ProductID = product.ProductID;
                                            prodductcategory.CategoryID = Convert.ToInt64(item);

                                            _productCategoriesService.insertNewProductCategory(prodductcategory);
                                        }

                                    }

                                    if (model.ProductID > 0)
                                    {
                                        List<ProductInfo> productinfoList = _productInfoService.GetAllProductsInfoByQueryable().Where(t => t.ProductID == model.ProductID).ToList();

                                        model.ProductInfoList = productinfoList;
                                        string infos = model.InfoIDs;
                                        infos = infos.Trim(',');
                                        string[] strinfos = infos.Split(',');

                                        _productInfoService.deleteProductInfos(model.ProductID);

                                        foreach (var item in strinfos)
                                        {
                                            ProductInfo productinfo = new ProductInfo();
                                            productinfo.ProductID = product.ProductID;
                                            productinfo.InfoID = Convert.ToInt64(item);

                                            _productInfoService.insertNewProductInfo(productinfo);

                                        }

                                    }

                                    if (model.ProductID > 0)
                                    {
                                        List<ProductSizes> productsizeList = _productSizesService.GetAllProductsSizeByQueryable().Where(t => t.ProductID == model.ProductID).ToList();
                                        model.ProductSizesList = productsizeList;

                                        List<ProductPrice> productpriceList = _productPriceService.GetAllProductsPriceByQueryable().Where(t => t.ProductID == model.ProductID).ToList();
                                        model.ProductPriceList = productpriceList;


                                        _productSizesService.deleteProductSizes(model.ProductID);
                                        _productPriceService.deleteProductPrices(model.ProductID);

                                        string sizes = model.SizeIDs;
                                        sizes = sizes.Trim(',');
                                        string[] strsizeprice = sizes.Split(',');
                                        List<string> strLi = new List<string>();
                                        List<string> sizeLi = new List<string>();
                                        List<string> priceLi = new List<string>();

                                        for (int i = 0; i < strsizeprice.Length; i++)
                                        {
                                            string sizepr_i = strsizeprice[i];
                                            strLi.Add(sizepr_i);
                                            string[] strarr = strLi[i].Split('|');
                                            sizeLi.Add(strarr[0]);
                                            priceLi.Add(strarr[1]);


                                            ProductSizes prosize = new ProductSizes();

                                            prosize.ProductID = product.ProductID;
                                            prosize.SizeID = Convert.ToInt64(sizeLi[i]);


                                            _productSizesService.insertNewProductSize(prosize);

                                            ProductPrice proprice = new ProductPrice();

                                            proprice.ProductID = product.ProductID;
                                            proprice.GroupID = Convert.ToInt64(model.GroupID);
                                            proprice.InfoIDs = model.InfoIDs.Trim(',');
                                            proprice.SizeID = Convert.ToInt64(sizeLi[i]);
                                            proprice.Price = Convert.ToDecimal(priceLi[i]);

                                            _productPriceService.insertNewProductPrice(proprice);

                                        }

                                    }


                                    if (model.ProductID > 0)
                                    {
                                        {
                                            List<ProductLog> productLog = _productLogService.GetAllProductsLogByQueryable().Where(t => t.ProductID == model.ProductID).ToList();

                                            if (productLog != null || productLog.Count > 0)
                                            {

                                                ProductLog productlog = new ProductLog();
                                                productlog.ProductID = model.ProductID;
                                                productlog.IsAdded = false;
                                                productlog.IsUpdated = true;
                                                productlog.IsDeleted = false;
                                                productlog.DateOfModification = DateTime.UtcNow;

                                                _productLogService.insertNewProductLog(productlog);
                                            }
                                        }

                                    }
                                    return RedirectToAction("ProductList", "Products", new { @area = "Admin" });

                                }

                            }
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Not updated");
                    }

                    IList<Groups> GroupList = _groupService.GetAllGroups().Where(t => t.GroupName != null).ToList();

                    if (GroupList.Count > 0)
                    {
                        foreach (var item in GroupList)
                        {
                            model.GroupList.Add(new Groups
                            {
                                GroupID = item.GroupID,
                                GroupName = item.GroupName
                            });
                        }

                    }

                    IList<Tags> TagList = _tagService.GetAllTags().Where(t => t.TagName != null).ToList();


                    if (TagList.Count > 0)
                    {
                        foreach (var item in TagList)
                        {
                            model.TagList.Add(new Tags
                            {
                                TagID = item.TagID,
                                TagName = item.TagName
                            });
                        }

                    }

                    if (model.ProductID > 0)
                    {
                        List<ProductCategories> productcategoryList = _productCategoriesService.GetAllProductsCategoryByQueryable().Where(t => t.ProductID == model.ProductID).ToList();
                        model.ProductCategoriesList = productcategoryList;

                    }


                    IList<Categories> CategoryList = _categoriesService.GetAllCategories().Where(t => t.CategoryName != null).ToList();

                    if (CategoryList.Count > 0)
                    {
                        foreach (var item in CategoryList)
                        {
                            model.CategoryList.Add(new Categories
                            {
                                CategoryID = item.CategoryID,
                                CategoryName = item.CategoryName
                            });
                        }

                    }

                    if (model.ProductID > 0)
                    {
                        List<ProductInfo> productinfoList = _productInfoService.GetAllProductsInfoByQueryable().Where(t => t.ProductID == model.ProductID).ToList();
                        model.ProductInfoList = productinfoList;


                    }


                    long groupid = Convert.ToInt64(model.GroupID);
                    IList<InfoGroups> InfoGroupsList = _iinfoGroupsService.GetAllInfoGroupsByQueryable().Where(t => t.GroupID == groupid).ToList();

                    if (InfoGroupsList.Count > 0)
                    {

                        foreach (var item in InfoGroupsList)
                        {

                            ProductInfoModel modelinfo = new ProductInfoModel();
                            modelinfo.InfoID = item.Info.InfoID;
                            modelinfo.InfoName = item.Info.InfoName;
                            model.InfoList.Add(modelinfo);

                        }

                    }

                    if (model.ProductID > 0)
                    {
                        List<ProductSizes> productsizeList = _productSizesService.GetAllProductsSizeByQueryable().Where(t => t.ProductID == model.ProductID).ToList();
                        model.ProductSizesList = productsizeList;

                    }
                    IList<SizeGroup> SizeGroupsList = _isizeGroupService.GetAllSizeGroupsByQueryable().Where(t => t.GroupID == groupid).ToList();
                    if (SizeGroupsList.Count > 0)
                    {

                        foreach (var item in SizeGroupsList)
                        {

                            ProductSizeModel modelsize = new ProductSizeModel();
                            modelsize.SizeID = item.Sizes.SizeID;
                            modelsize.Size = item.Sizes.Size;
                            model.SizeList.Add(modelsize);

                        }

                    }


                    if (model.ProductID > 0)
                    {
                        List<ProductPrice> productpriceList = _productPriceService.GetAllProductsPriceByQueryable().Where(t => t.ProductID == model.ProductID).ToList();
                        model.ProductPriceList = productpriceList;

                    }





                }
                else
                {
                    ModelState.AddModelError("", "Not updated");
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);

        }


        [HttpPost]
        public ActionResult AddProduct(ProductModel model)
        {
            try
            {
                //if (Request.Files.Count > 0)
                //{
                if (ModelState.IsValid)
                {

                    var productExists = _productsService.GetAllProductsByQueryable();
                    if (productExists != null)
                    {
                        if (productExists.Where(t => t.Title == model.Title).Count() == 0)
                        {

                            if (productExists.Where(t => t.Priority == model.Priority).Count() == 0)
                            {

                                if (Request.Files.Count > 0)
                                {
                                    var Idfile = Request.Files[0];
                                    if (Idfile != null && Idfile.ContentLength > 0)
                                    {
                                        var filename = Path.GetFileName(Idfile.FileName);
                                        string extension = Path.GetExtension(Idfile.FileName);
                                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".bmp" || extension.ToLower() == ".png" || extension.ToLower() == ".gif")
                                        {
                                            string fName = DateTime.Now.ToString("yyyyMMdd_hhssff") + extension;
                                            var path = Path.Combine(Server.MapPath("~/Content/Assets/ProductImages/"), fName);
                                            Idfile.SaveAs(path);

                                            Products product = new Products();

                                            product.ProductName = model.ProductName;
                                            product.Title = model.Title;
                                            product.TagID = Convert.ToInt64(model.TagID);
                                            //product.Tags.TagName = model.TagName;
                                            product.Priority = Convert.ToInt32(model.Priority);
                                            product.GroupID = Convert.ToInt64(model.GroupID);
                                            //product.AdditionalInfo = model.AdditionalInfo;
                                            //product.Instructions = model.Instructions;
                                            product.ProductImageName = fName;

                                            product.AddedBy = 2;
                                            product.Status = Convert.ToBoolean(model.Status == "1" ? true : false);

                                            product.statusAdditionalInfo = model.statusAdditionalInfo;
                                            product.statusInstructions = model.statusInstructions;

                                            _productsService.insertNewProducts(product);
                                            model.ProductID = product.ProductID;
                                            model.ImageName = fName;
                                            WaterMark(model.ProductID);



                                            var GetProductDetails = _productsService.GetAllProductsByQueryable().Where(p => p.ProductID == product.ProductID).FirstOrDefault();
                                            var vartagname = _tagService.GetAllTagsByQueryable().Where(t => t.TagID == GetProductDetails.TagID).FirstOrDefault();
                                            //if (GetProductDetails != null)
                                            //{
                                            //bool isImageSaved = SaveImages(model.Title, product.ProductID, vartagname.TagName);
                                            //if (isImageSaved == true)
                                            //{
                                            if (model.ProductID > 0)
                                            {
                                                string categories = model.CategoryIDs;
                                                categories = categories.Trim(',');
                                                string[] strcategories = categories.Split(',');

                                                foreach (var item in strcategories)
                                                {

                                                    ProductCategories procat = new ProductCategories();
                                                    procat.ProductID = product.ProductID;
                                                    procat.CategoryID = Convert.ToInt64(item);
                                                    _productCategoriesService.insertNewProductCategory(procat);
                                                }
                                            }

                                            if (model.ProductID > 0)
                                            {
                                                string infos = model.InfoIDs;
                                                infos = infos.Trim(',');
                                                string[] strinfos = infos.Split(',');

                                                foreach (var item in strinfos)
                                                {

                                                    ProductInfo proinfo = new ProductInfo();
                                                    proinfo.ProductID = product.ProductID;
                                                    proinfo.InfoID = Convert.ToInt64(item);
                                                    _productInfoService.insertNewProductInfo(proinfo);

                                                }


                                            }


                                            if (model.ProductID > 0)
                                            {

                                                string sizes = model.SizeIDs;
                                                sizes = sizes.Trim(',');
                                                string[] strsizeprice = sizes.Split(',');
                                                List<string> strLi = new List<string>();
                                                List<string> sizeLi = new List<string>();
                                                List<string> priceLi = new List<string>();

                                                for (int i = 0; i < strsizeprice.Length; i++)
                                                {
                                                    string sizepr_i = strsizeprice[i];
                                                    strLi.Add(sizepr_i);
                                                    string[] strarr = strLi[i].Split('|');
                                                    sizeLi.Add(strarr[0]);
                                                    priceLi.Add(strarr[1]);


                                                    ProductSizes prosize = new ProductSizes();

                                                    prosize.ProductID = product.ProductID;
                                                    prosize.SizeID = Convert.ToInt64(sizeLi[i]);


                                                    _productSizesService.insertNewProductSize(prosize);

                                                    ProductPrice proprice = new ProductPrice();

                                                    proprice.ProductID = product.ProductID;
                                                    proprice.GroupID = Convert.ToInt64(model.GroupID);
                                                    proprice.InfoIDs = model.InfoIDs.Trim(',');
                                                    proprice.SizeID = Convert.ToInt64(sizeLi[i]);
                                                    proprice.Price = Convert.ToDecimal(priceLi[i]);

                                                    _productPriceService.insertNewProductPrice(proprice);

                                                }

                                            }
                                            if (model.ProductID > 0)
                                            {
                                                List<ProductLog> productLog = _productLogService.GetAllProductsLogByQueryable().Where(t => t.ProductID == model.ProductID).ToList();

                                                if (productLog == null || productLog.Count == 0)
                                                {

                                                    ProductLog productlog = new ProductLog();
                                                    productlog.ProductID = model.ProductID;
                                                    productlog.IsAdded = true;
                                                    productlog.IsUpdated = false;
                                                    productlog.IsDeleted = false;
                                                    productlog.DateOfModification = DateTime.UtcNow;

                                                    _productLogService.insertNewProductLog(productlog);
                                                }
                                            }

                                            return RedirectToAction("ProductList", "Products", new { @area = "Admin" });

                                        }
                                        else
                                        {
                                            ModelState.AddModelError("", "Choose a valid image type file.");
                                        }
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("", "File is Corrupt.");
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Choose an image file.");
                                }

                                //return Json(model, JsonRequestBehavior.AllowGet);


                            }
                            else
                            {
                                ModelState.AddModelError("", "Priority value already exists");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", model.Title + " already exists");
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Not inserted");
                }
                //}
                //else
                //{
                //    ModelState.AddModelError("", "Select an Image");

                //}
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            IList<Groups> GroupList = _groupService.GetAllGroups().Where(g => g.GroupName != null).ToList();

            if (GroupList.Count > 0)
            {
                foreach (var item in GroupList)
                {
                    model.GroupList.Add(new Groups
                    {
                        GroupID = item.GroupID,
                        GroupName = item.GroupName
                    });
                }

            }

            IList<Tags> TagList = _tagService.GetAllTags().Where(t => t.TagName != null).ToList();


            if (TagList.Count > 0)
            {
                foreach (var item in TagList)
                {
                    model.TagList.Add(new Tags
                    {
                        TagID = item.TagID,
                        TagName = item.TagName
                    });
                }

            }

            IList<Categories> CategoryList = _categoriesService.GetAllCategories().Where(t => t.CategoryName != null).ToList();

            if (CategoryList.Count > 0)
            {
                foreach (var item in CategoryList)
                {
                    model.CategoryList.Add(new Categories
                    {
                        CategoryID = item.CategoryID,
                        CategoryName = item.CategoryName
                    });
                }

            }


            return View(model);

        }

        public ActionResult ProductImage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetInfos(long id)
        {
            try
            {
                var varinfogroupLi = _infoGroupsService.GetAllInfoGroupsByQueryable().Where(t => t.GroupID == id);
                List<Info> infoLi = new List<Info>();
                if (varinfogroupLi != null)
                {
                    foreach (var item in varinfogroupLi)
                    {
                        Info info = new Info();
                        info.InfoID = item.Info.InfoID;
                        info.InfoName = item.Info.InfoName;
                        infoLi.Add(info);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Error");

                }

                return Json(infoLi, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();


            }
        }



        [HttpGet]
        public ActionResult GetSizes(long id)
        {
            try
            {
                var varsizegroupLi = _sizeGroupService.GetAllSizeGroupsByQueryable().Where(t => t.GroupID == id);

                List<Sizes> sizeLi = new List<Sizes>();
                if (varsizegroupLi != null)
                {
                    foreach (var item in varsizegroupLi)
                    {
                        Sizes size = new Sizes();

                        size.SizeID = item.Sizes.SizeID;
                        size.Size = item.Sizes.Size;
                        sizeLi.Add(size);

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Error");

                }

                return Json(sizeLi, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();


            }
        }




        [HttpGet]
        public ActionResult GetInfosAndSizes(long id)
        {
            try
            {
                var varsizegroupLi = _sizeGroupService.GetAllSizeGroupsByQueryable().Where(t => t.GroupID == id);
                var varinfogroupLi = _infoGroupsService.GetAllInfoGroupsByQueryable().Where(t => t.GroupID == id);

                List<Sizes> sizeLi = new List<Sizes>();
                List<Info> infoLi = new List<Info>();

                InfoSize obj = new InfoSize();

                if (varsizegroupLi != null)
                {
                    foreach (var item in varsizegroupLi)
                    {
                        Sizes size = new Sizes();

                        size.SizeID = item.Sizes.SizeID;
                        size.Size = item.Sizes.Size;
                        sizeLi.Add(size);

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Error");

                }
                if (varinfogroupLi != null)
                {
                    foreach (var item in varinfogroupLi)
                    {
                        Info info = new Info();
                        info.InfoID = item.Info.InfoID;
                        info.InfoName = item.Info.InfoName;
                        infoLi.Add(info);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Error");

                }
                obj.InfoList = infoLi;
                obj.SizeList = sizeLi;

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();


            }
        }

        [HttpGet]
        public ActionResult ImageList(string id)
        {
            try
            {
                long ProductID = Convert.ToInt64(id);

                var prodImageList = _productImagesService.GetAllProductImagesByQueryable().Where(t => t.ProductID == ProductID).ToList();

                //if (prodImageList != null)
                //{
                //    foreach (var item in prodImageList)
                //    {
                //        model.ProductImageList.Add(new ProductImages
                //        {
                //            ProductImageID = item.ProductImageID,
                //            ProductID = item.ProductID,
                //            ImageName = item.ImageName,
                //            Title = item.Title,
                //            ImageAlt = item.ImageAlt,
                //            Priority = item.Priority,
                //            Status = item.Status
                //        });
                //    }

                //}






                return Json(prodImageList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();


            }
        }


        [HttpPost]
        private ActionResult WaterMark(long ProductID)
        {

            try
            {
                string watermarkText = "";
                string ProductTitle = "";
                string TagText = "";
                string mainHTML="";
                string previewHTML = "";
                string resultHTML = "";

                var GetProductDetails = _productsService.GetAllProductsByQueryable().Where(p => p.ProductID == ProductID).FirstOrDefault();
                List<ProductInfo> infoList = _productInfoService.GetAllProductsInfoByQueryable().Where(t => t.ProductID == GetProductDetails.ProductID).ToList();
                List<string> infoLi = new List<string>();
                foreach (var item in infoList)
                {
                    string info = item.Info.InfoName;
                    infoLi.Add(info);
                }

                List<ProductSizes> sizeList = _productSizesService.GetAllProductsSizeByQueryable().Where(t => t.ProductID == GetProductDetails.ProductID).ToList();
                List<string> sizeLi = new List<string>();
                foreach (var item in sizeList)
                {
                    string size = item.Sizes.Size;
                    sizeLi.Add(size);
                }

                  mainHTML = @"<div class='col-md-7'>
                                       <table width='100%' cellspacing='0' cellpadding='0' border='0' class='table table-bordered table-striped'>
                                        <tbody>
                                            <tr>
                                                <th>Lot Number</th>
                                                <td>-</td>
                                            </tr>
                                            <tr>
                                                <th>Registration</th>
                                                <td><span class='regno'>{0}</span></td>
                                            </tr>
                                            <tr>
                                                <th>Sale</th>
                                                <td>-</td>
                                            </tr>
                                            <tr>
                                                <th>Vendor</th>
                                                <td>-</td>
                                            </tr>
                                            <tr>
                                                <th>Make</th>
                                                <td>{1}</td>
                                            </tr>
                                            <tr>
                                                <th>Model</th>
                                                <td>{2}</td>
                                            </tr>
                                            <tr>
                                                <th>Colour</th>
                                                <td>{3}</td>
                                            </tr>
                                            <tr>
                                                <th>Description</th>
                                                <td>{4}</td>
                                            </tr>
                                            <tr>
                                                <th>Doors</th>
                                                <td>-</td>
                                            </tr>
                                            <tr>
                                                <th>Fuel Type</th>
                                                <td>{5}</td>
                                            </tr>
                                            <tr>
                                                <th>Transmission</th>
                                                <td>{6}</td>
                                            </tr>
                                            <tr>
                                                <th>Milleage</th>
                                                <td>{7}</td>
                                            </tr>
                                            <tr>
                                                <th>Former Keepers</th>
                                                <td>-</td>
                                            </tr>
                                            <tr>
                                                <th>Taxed To</th>
                                                <td>-</td>
                                            </tr>
                                            <tr>
                                                <th>MOT To</th>
                                                <td>{8}</td>
                                            </tr>
                                            <tr>
                                                <th>Service History</th>
                                                <td>{9}</td>
                                            </tr>
                                            <tr>
                                                <th>Remarks</th>
                                                <td>-</td>
                                            </tr>
                                            <tr>
                                                <th>Other Details</th>
                                                <td><button class='btn btn-success' type='button'>V5</button></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>";
                        resultHTML = string.Format(mainHTML,
                            GetProductDetails.RegistrationNumber,
                            CarSellerVehicleInfoDetails.Make.Makename,
                            CarSellerVehicleInfoDetails.Model.Modelname,
                            CarSellerVehicleInfoDetails.Color,
                            CarSellerVehicleInfoDetails.Description,
                            strFuelTypes.Trim(','),
                            CarSellerVehicleInfoDetails.TransmissionType.Type,
                            CarSellerVehicleInfoDetails.ExactMileage,
                           Convert.ToDateTime(CarSellerVehicleInfoDetails.MOTExpiryDate).ToString("dd/MM/yyyy"),
                            CarSellerVehicleInfoDetails.ServiceHistory,
                            UserName
                            );
                

                 previewHTML = string.Format(main, Header, AllimageDetails + resultHTML);
       










                if (GetProductDetails != null)
                {
                    var tagname = _tagService.GetAllTagsByQueryable().Where(t => t.TagID == GetProductDetails.TagID).FirstOrDefault();

                    TagText = tagname.TagName;
                }
                if (GetProductDetails != null)
                {

                    ProductTitle = GetProductDetails.Title;
                    watermarkText = ProductTitle;

                }

                bool flagDelete = false;

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var Idfile = Request.Files[0];
                    //var Idfile = Request.Files[i];
                    if (Idfile != null && Idfile.ContentLength > 0)
                    {
                        string extension = Path.GetExtension(Idfile.FileName);
                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".bmp" || extension.ToLower() == ".gif" || extension.ToLower() == ".jpeg")
                        {
                            using (Bitmap bmp = new Bitmap(Idfile.InputStream, false))
                            {
                                using (Graphics grp = Graphics.FromImage(bmp))
                                {

                                    SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(255, 180, 35, 203));

                                    LinearGradientBrush linGrBrush = new LinearGradientBrush(new Point(0, 10), new Point(250, 10), Color.FromArgb(255, 232, 202, 229), Color.FromArgb(128, 255, 255, 255));

                                    Font font = new System.Drawing.Font("Arial", 18, FontStyle.Bold, GraphicsUnit.Pixel);
                                    SizeF textSize = new SizeF();
                                    textSize = grp.MeasureString(watermarkText, font);


                                    Point position = new Point(20, (bmp.Height - ((int)textSize.Height + 20)));

                                    Rectangle rect = new Rectangle(20, (bmp.Height - ((int)textSize.Height + 30)), ((int)textSize.Width + 20), 40);

                                    Pen blackPen = new Pen(Color.Transparent, 0);
                                    grp.FillRectangle(linGrBrush, rect);
                                    grp.DrawRectangle(blackPen, rect);
                                    grp.DrawString(watermarkText, font, semiTransBrush, position);


                                    /****************************Tags********************************************************/

                                    SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(255, 255, 255, 255));

                                    LinearGradientBrush linGrBrush2 = new LinearGradientBrush(new Point(0, 10), new Point(250, 10), Color.FromArgb(255, 232, 202, 229), Color.FromArgb(128, 255, 255, 255));
                                    //LinearGradientBrush linGrBrush = new LinearGradientBrush(new PointF((float)0.007, (float)0.439), new PointF((float)1.09, (float)0.439), Color.FromArgb(255, 232, 202, 229), Color.FromArgb(128, 255, 255, 255));
                                    //Set the Font and its size.
                                    Font font2 = new System.Drawing.Font("Arial", 18, FontStyle.Bold, GraphicsUnit.Pixel);

                                    //Determine the size of the Watermark text.
                                    SizeF textSize2 = new SizeF();
                                    textSize2 = grp.MeasureString(TagText, font2);

                                    //Position the text and draw it on the image.
                                    //Point position = new Point((bmp.Width - ((int)textSize.Width + (bmp.Width-100))), (bmp.Height - ((int)textSize.Height + 10)));

                                    Point position2 = new Point((bmp.Width - ((int)textSize2.Width + 20)), (bmp.Height - ((int)textSize2.Height + 20)));
                                    //Point position2 = new Point(20, (bmp.Height - ((int)textSize2.Height + 60)));
                                    // grp.DrawString("hello", new Font("Arial", 36), new SolidBrush(Color.FromArgb(255, 0, 0)), new Point(20, 20));
                                    Rectangle rect2 = new Rectangle((bmp.Width - ((int)textSize2.Width + 20)), (bmp.Height - ((int)textSize2.Height + 30)), ((int)textSize2.Width + 10), 40);
                                    SolidBrush SolidBrushTag = new SolidBrush(Color.FromArgb(255, 255, 0, 0));
                                    Pen blackPen2 = new Pen(Color.Blue, 4);
                                    grp.FillRectangle(SolidBrushTag, rect2);
                                    grp.DrawRectangle(blackPen2, rect2);

                                    grp.DrawString(TagText, font2, semiTransBrush2, position2);

                                    using (MemoryStream memoryStream = new MemoryStream())
                                    {
                                        bmp.Save(memoryStream, ImageFormat.Png);
                                        memoryStream.Position = 0;
                                        string fName = ProductTitle + "-" + (i + 1) + ".png";

                                        var path = Path.Combine(Server.MapPath("~/Content/Assets/ProductImages/"), fName);
                                        if (System.IO.File.Exists(path))
                                        {
                                            System.IO.File.Delete(path);
                                        }
                                        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
                                        memoryStream.WriteTo(file);
                                        file.Close();
                                        if (!flagDelete)
                                        {
                                            _productImagesService.deleteProductImage(ProductID);
                                            flagDelete = true;
                                        }


                                        if (GetProductDetails != null)
                                        {

                                            Products prodimage = new Products();

                                            prodimage.ProductID = ProductID;
                                            // prodimage.ProductImageName = fName;
                                            prodimage.ProductName = GetProductDetails.ProductName;
                                            prodimage.Title = GetProductDetails.Title;
                                            prodimage.TagID = Convert.ToInt64(GetProductDetails.TagID);
                                            //product.Tags.TagName = model.TagName;
                                            prodimage.Priority = Convert.ToInt32(GetProductDetails.Priority);
                                            prodimage.GroupID = Convert.ToInt64(GetProductDetails.GroupID);
                                            //product.AdditionalInfo = model.AdditionalInfo;
                                            //product.Instructions = model.Instructions;
                                            prodimage.ProductImageName = fName;

                                            prodimage.AddedBy = 2;
                                            prodimage.Status = Convert.ToBoolean(GetProductDetails.Status);

                                            prodimage.statusAdditionalInfo = GetProductDetails.statusAdditionalInfo;
                                            prodimage.statusInstructions = GetProductDetails.statusInstructions;

                                            _productsService.updateProducts(prodimage);

                                            memoryStream.Dispose();
                                        }

                                    }
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Select a valid Image file.");
                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Select an Image file.");
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(false, JsonRequestBehavior.AllowGet);
                //return Json(true, JsonRequestBehavior.AllowGet);


            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Select an Image file.");
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            // return View(model);

        }



        [HttpPost]
        public ActionResult SaveImages(ProductModel model)
        {

            try
            {
                string watermarkText = "";
                string ProductTitle = "";
                string TagText = "";

                var lastproductdetails = _productsService.GetAllProductsByQueryable().OrderByDescending(t => t.ProductID).FirstOrDefault();

                if (lastproductdetails != null)
                {
                    long prodid = lastproductdetails.ProductID;
                    watermarkText = lastproductdetails.Title;
                    ProductTitle = lastproductdetails.Title;
                    var GetProductDetails = _productsService.GetAllProductsByQueryable().Where(p => p.ProductID == lastproductdetails.ProductID).FirstOrDefault();
                    if (GetProductDetails != null)
                    {
                        var tagname = _tagService.GetAllTagsByQueryable().Where(t => t.TagID == GetProductDetails.TagID).FirstOrDefault();

                        TagText = tagname.TagName;
                    }


                }



                //string watermarkText = ProductTitle;
                //string TagText = Tag;
                bool flagDelete = false;
                //if (Request.Files.Count > 0)
                //{
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var Idfile = Request.Files[0];
                    //var Idfile = Request.Files[i];
                    if (Idfile != null && Idfile.ContentLength > 0)
                    {
                        string extension = Path.GetExtension(Idfile.FileName);
                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".bmp" || extension.ToLower() == ".gif" || extension.ToLower() == ".jpeg")
                        {
                            using (Bitmap bmp = new Bitmap(Idfile.InputStream, false))
                            {
                                using (Graphics grp = Graphics.FromImage(bmp))
                                {

                                    SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(255, 180, 35, 203));

                                    LinearGradientBrush linGrBrush = new LinearGradientBrush(new Point(0, 10), new Point(250, 10), Color.FromArgb(255, 232, 202, 229), Color.FromArgb(128, 255, 255, 255));

                                    Font font = new System.Drawing.Font("Arial", 18, FontStyle.Bold, GraphicsUnit.Pixel);
                                    SizeF textSize = new SizeF();
                                    textSize = grp.MeasureString(watermarkText, font);


                                    Point position = new Point(20, (bmp.Height - ((int)textSize.Height + 20)));

                                    Rectangle rect = new Rectangle(20, (bmp.Height - ((int)textSize.Height + 30)), ((int)textSize.Width + 20), 40);

                                    Pen blackPen = new Pen(Color.Transparent, 0);
                                    grp.FillRectangle(linGrBrush, rect);
                                    grp.DrawRectangle(blackPen, rect);
                                    grp.DrawString(watermarkText, font, semiTransBrush, position);


                                    /****************************Tags********************************************************/

                                    SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(255, 255, 255, 255));

                                    LinearGradientBrush linGrBrush2 = new LinearGradientBrush(new Point(0, 10), new Point(250, 10), Color.FromArgb(255, 232, 202, 229), Color.FromArgb(128, 255, 255, 255));
                                    //LinearGradientBrush linGrBrush = new LinearGradientBrush(new PointF((float)0.007, (float)0.439), new PointF((float)1.09, (float)0.439), Color.FromArgb(255, 232, 202, 229), Color.FromArgb(128, 255, 255, 255));
                                    //Set the Font and its size.
                                    Font font2 = new System.Drawing.Font("Arial", 18, FontStyle.Bold, GraphicsUnit.Pixel);

                                    //Determine the size of the Watermark text.
                                    SizeF textSize2 = new SizeF();
                                    textSize2 = grp.MeasureString(TagText, font2);

                                    //Position the text and draw it on the image.
                                    //Point position = new Point((bmp.Width - ((int)textSize.Width + (bmp.Width-100))), (bmp.Height - ((int)textSize.Height + 10)));

                                    Point position2 = new Point((bmp.Width - ((int)textSize2.Width + 20)), (bmp.Height - ((int)textSize2.Height + 20)));
                                    //Point position2 = new Point(20, (bmp.Height - ((int)textSize2.Height + 60)));
                                    // grp.DrawString("hello", new Font("Arial", 36), new SolidBrush(Color.FromArgb(255, 0, 0)), new Point(20, 20));
                                    Rectangle rect2 = new Rectangle((bmp.Width - ((int)textSize2.Width + 20)), (bmp.Height - ((int)textSize2.Height + 30)), ((int)textSize2.Width + 10), 40);
                                    SolidBrush SolidBrushTag = new SolidBrush(Color.FromArgb(255, 255, 0, 0));
                                    Pen blackPen2 = new Pen(Color.Blue, 4);
                                    grp.FillRectangle(SolidBrushTag, rect2);
                                    grp.DrawRectangle(blackPen2, rect2);

                                    grp.DrawString(TagText, font2, semiTransBrush2, position2);

                                    using (MemoryStream memoryStream = new MemoryStream())
                                    {
                                        bmp.Save(memoryStream, ImageFormat.Png);
                                        memoryStream.Position = 0;
                                        string fName = ProductTitle + "-" + (i + 1) + ".png";

                                        var path = Path.Combine(Server.MapPath("~/Content/Assets/ProductImages/"), fName);
                                        if (System.IO.File.Exists(path))
                                        {
                                            System.IO.File.Delete(path);
                                        }
                                        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
                                        memoryStream.WriteTo(file);
                                        file.Close();
                                        if (!flagDelete)
                                        {
                                            _productImagesService.deleteProductImage(model.ProductID);
                                            flagDelete = true;
                                        }

                                        ProductImages prodimage = new ProductImages();
                                        //prodimage.ProductID = model.ProductID;
                                        prodimage.ProductID = lastproductdetails.ProductID;
                                        prodimage.ImageName = fName;
                                        // prodimage.Status = true;
                                        prodimage.ImageAlt = model.ImageAlt;
                                        prodimage.Title = model.ImageTitle;
                                        prodimage.Priority = Convert.ToInt32(model.ImagePriority);
                                        prodimage.Status = Convert.ToBoolean(model.ImageStatus == "1" ? true : false);
                                        //prodimage.Status = Convert.ToBoolean(model.ImageStatus);
                                        _productImagesService.insertNewProductImages(prodimage);
                                        memoryStream.Dispose();

                                        var prodImageList = _productImagesService.GetAllProductImagesByQueryable().Where(t => t.ProductID == lastproductdetails.ProductID).ToList();
                                        if (prodImageList != null)
                                        {
                                            foreach (var item in prodImageList)
                                            {
                                                model.ProductImageList.Add(new ProductImages
                                                {
                                                    ProductImageID = item.ProductImageID,
                                                    ProductID = item.ProductID,
                                                    ImageName = item.ImageName,
                                                    Title = item.Title,
                                                    ImageAlt = item.ImageAlt,
                                                    Priority = item.Priority,
                                                    Status = item.Status
                                                });
                                            }

                                        }



                                    }
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Select a valid Image file.");
                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Select an Image file.");
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(model, JsonRequestBehavior.AllowGet);
                //return Json(true, JsonRequestBehavior.AllowGet);


            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Select an Image file.");
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            // return View(model);

        }



        [HttpPost]
        public ActionResult DeleteProduct(long id)
        {
            try
            {
                Products productExists = _productsService.GetAllProductsByQueryable().Where(t => t.ProductID == id).FirstOrDefault();

                List<ProductCategories> productcategoryExists = _productCategoriesService.GetAllProductsCategoryByQueryable().Where(t => t.ProductID == id).ToList();

                List<ProductInfo> productinfoExists = _productInfoService.GetAllProductsInfoByQueryable().Where(t => t.ProductID == id).ToList();

                List<ProductPrice> productpriceExists = _productPriceService.GetAllProductsPriceByQueryable().Where(t => t.ProductID == id).ToList();

                List<ProductSizes> productsizeExists = _productSizesService.GetAllProductsSizeByQueryable().Where(t => t.ProductID == id).ToList();

                List<ProductImages> productimageExists = _productImagesService.GetAllProductImagesByQueryable().Where(t => t.ProductID == id).ToList();


                if (productExists != null)
                {
                    if (productcategoryExists.Count > 0 && productinfoExists.Count > 0 && productpriceExists.Count > 0 && productsizeExists.Count > 0 && productimageExists.Count > 0)
                    {
                        _productCategoriesService.deleteProductCategories(id);
                        _productInfoService.deleteProductInfos(id);
                        _productPriceService.deleteProductPrices(id);
                        _productSizesService.deleteProductSizes(id);
                        _productImagesService.deleteProductImage(id);

                        _productsService.deleteProduct(productExists);

                        return Json("true", JsonRequestBehavior.AllowGet);
                    }
                    else if (productcategoryExists.Count > 0 && productinfoExists.Count > 0 && productpriceExists.Count == 0 && productsizeExists.Count == 0 && productimageExists.Count == 0)
                    {
                        _productCategoriesService.deleteProductCategories(id);
                        _productInfoService.deleteProductInfos(id);

                        _productsService.deleteProduct(productExists);

                        return Json("true", JsonRequestBehavior.AllowGet);


                    }
                    else if (productcategoryExists.Count > 0 && productinfoExists.Count > 0 && productpriceExists.Count == 0 && productsizeExists.Count > 0 && productimageExists.Count == 0)
                    {
                        _productCategoriesService.deleteProductCategories(id);
                        _productInfoService.deleteProductInfos(id);

                        _productSizesService.deleteProductSizes(id);


                        _productsService.deleteProduct(productExists);

                        return Json("true", JsonRequestBehavior.AllowGet);

                    }
                    else if (productcategoryExists.Count > 0 && productinfoExists.Count > 0 && productpriceExists.Count > 0 && productsizeExists.Count > 0 && productimageExists.Count == 0)
                    {
                        _productCategoriesService.deleteProductCategories(id);
                        _productInfoService.deleteProductInfos(id);
                        _productPriceService.deleteProductPrices(id);
                        _productSizesService.deleteProductSizes(id);


                        _productsService.deleteProduct(productExists);

                        return Json("true", JsonRequestBehavior.AllowGet);

                    }
                    else if (productcategoryExists.Count == 0 && productinfoExists.Count == 0 && productpriceExists.Count == 0 && productsizeExists.Count == 0 && productimageExists.Count == 0)
                    {
                        _productsService.deleteProduct(productExists);
                        return Json("true", JsonRequestBehavior.AllowGet);

                    }

                    else
                    {
                        return Json("false", JsonRequestBehavior.AllowGet);

                    }


                }
                else
                {

                    return Json("Not found", JsonRequestBehavior.AllowGet);

                }


            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


    }
    public class InfoSize
    {
        public List<Info> InfoList { get; set; }
        public List<Sizes> SizeList { get; set; }
    }


}
