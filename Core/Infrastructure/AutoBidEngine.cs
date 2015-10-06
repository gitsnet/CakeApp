using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
  public  class CakeAppEngine:IEngine
    {
       #region Fields

        private ContainerManager _containerManager;

        #endregion

        #region Ctor

        /// <summary>
		/// Creates an instance of the content engine using default settings and configuration.
		/// </summary>
		public CakeAppEngine() 
            : this(new ContainerConfigurer())
		{
		}

        public CakeAppEngine(ContainerConfigurer configurer)
		{
            InitializeContainer(configurer);         
		}
        
        #endregion

        #region Utilities

        private void RunStartupTasks()
        {
          //  var typeFinder = _containerManager.Resolve<ITypeFinder>();
        }
        
        private void InitializeContainer(ContainerConfigurer configurer)
        {
            var builder = new ContainerBuilder();

            _containerManager = new ContainerManager(builder.Build());
            configurer.Configure(this, _containerManager);
        }

        #endregion

        #region Methods        

        public T Resolve<T>() where T : class
		{
            return ContainerManager.Resolve<T>();
		}

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }
        
        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

		#endregion

        #region Properties

        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        #endregion
    }
}
