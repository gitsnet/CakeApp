using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Category;
using CakeApp.API.Models.Category;
using Core.Category;
using System.Web.Http.Cors;

namespace CakeApp.API.Controllers.Category
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("API")]
    
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService _categoriesService;
        public string SiteURL = System.Configuration.ConfigurationManager.AppSettings["SiteURL"].ToString();

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
       
        public HttpResponseMessage Get()
        {

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var categoryDetails = _categoriesService.GetAllCategories().Where(p => p.Status).OrderBy(t => t.Priority);

                List<CategoryModel> CategoryModelList = new List<CategoryModel>();
                if (categoryDetails != null)
                {
                    foreach (var item in categoryDetails)
                    {
                        CategoryModel model = new CategoryModel();
                        model.CategoryID = item.CategoryID;
                        model.CategoryName = item.CategoryName;
                        model.Title = item.Title;

                        model.Priority = item.Priority.ToString();
                        if (item.CategoryImageName != null)
                            model.DefaultImage.ImageName = SiteURL + "Content/Assets/CategoryImages/" + item.CategoryImageName;
                        else
                            model.DefaultImage.ImageName = "";

                        model.Tag.TagName = item.Tags.TagName;
                        model.Tag.TagID = item.Tags.TagID.ToString();

                        CategoryModelList.Add(model);
                    }
                    response = Request.CreateResponse(HttpStatusCode.OK, CategoryModelList);
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
       

    }
}
