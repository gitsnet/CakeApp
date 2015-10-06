using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Core.Infrastructure;
using Core.Data;
using Data;
using Core.Fakes;
using Service.User;
using Service.Category;
using Service.Tag;
using Service.Screensaver;
using Service.Group;
using Service.infos;
using Service.Infos;
using Service.Size;
using Service.FTPSetting;
using Service.Product;



namespace CakeApp
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //HTTP context and other related stuff
            builder.Register(c =>
                //register FakeHttpContext when HttpContext is not available
                HttpContext.Current != null ?
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                (new FakeHttpContext("~/") as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerHttpRequest();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());
            builder.RegisterType<ObjectContext>().As<IDbContext>().InstancePerHttpRequest();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerHttpRequest();
            builder.RegisterType<CategoriesService>().As<ICategoriesService>().InstancePerHttpRequest();
            builder.RegisterType<ScreensaverService>().As<IScreensaverService>().InstancePerHttpRequest();
            builder.RegisterType<TagService>().As<ITagService>().InstancePerHttpRequest();
            builder.RegisterType<GroupService>().As<IGroupService>().InstancePerHttpRequest();
            builder.RegisterType<InfosService>().As<IInfosService>().InstancePerHttpRequest();
            builder.RegisterType<CategoryImageService>().As<ICategoryImageService>().InstancePerHttpRequest();
            builder.RegisterType<InfoGroupsService>().As<IInfoGroupsService>().InstancePerHttpRequest();
            builder.RegisterType<SizeService>().As<ISizeService>().InstancePerHttpRequest();
            builder.RegisterType<SizeGroupService>().As<ISizeGroupService>().InstancePerHttpRequest();
            builder.RegisterType<FTPSettingService>().As<IFTPSettingService>().InstancePerHttpRequest();
            builder.RegisterType<ProductsService>().As<IProductsService>().InstancePerHttpRequest();
            builder.RegisterType<ProductSizesService>().As<IProductSizesService>().InstancePerHttpRequest();
            builder.RegisterType<ProductCategoriesService>().As<IProductCategoriesService>().InstancePerHttpRequest();
            builder.RegisterType<ProductInfoService>().As<IProductInfoService>().InstancePerHttpRequest();
            builder.RegisterType<ProductPriceService>().As<IProductPriceService>().InstancePerHttpRequest();
            builder.RegisterType<ProductImagesService>().As<IProductImagesService>().InstancePerHttpRequest();
            builder.RegisterType<ProductLogService>().As<IProductLogService>().InstancePerHttpRequest();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest();

        }

        public int Order
        {
            get { return 0; }
        }
    }

}
