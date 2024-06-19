[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebTest.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebTest.App_Start.NinjectWebCommon), "Stop")]

namespace WebTest.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using LoginSystemAndNews.Interfaces;
    using LoginSystemAndNews.DataAccess;
    using LoginSystemAndNews.BusinessLogic;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IMemberRepository>().To<MemberRepository>();
            kernel.Bind<ILoginTimeRepository>().To<LoginTimeLogRepository>();
            kernel.Bind<IMemberService>().To<MemberService>();
            kernel.Bind<ILoginTimeLogService>().To<LoginTimeLogService>();
        }
    }
}