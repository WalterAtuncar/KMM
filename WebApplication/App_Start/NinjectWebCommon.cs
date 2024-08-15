using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using Service.Contracts;
using Service.Contracts.Perfil;
using Service.Contracts.Proveedor;
using Service.Logic;
using Service.Logic.Contract;
using Service.Services;
using System;
using System.Web;
using WebApplication.App_Start;


[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace WebApplication.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                kernel.Bind<IProveedorWebApiServiceLogic>().To<ProveedorWebApiServiceLogic>();
                kernel.Bind<IProveedorWebApiService>().To<ProveedorWebApiService>();

                kernel.Bind<ISociedadServiceLogic>().To<SociedadServiceLogic>();
                kernel.Bind<ISociedadService>().To<SociedadService>();
                

                kernel.Bind<ISolicitudServiceLogic>().To<SolicitudServiceLogic>();
                kernel.Bind<IProveedorService>().To<ProveedorService>();
                kernel.Bind<ISolicitudService>().To<SolicitudService>();

                kernel.Bind<ILiquidacionServiceLogic>().To<LiquidacionServiceLogic>();
                kernel.Bind<ILiquidacionService>().To<LiquidacionService>();


                kernel.Bind<ICentroCostoServiceLogic>().To<CentroCostoServiceLogic>();
                kernel.Bind<ICentroCostoService>().To<CentroCostoService>();

                kernel.Bind<IUsuarioServiceLogic>().To<UsuarioServiceLogic>();

                kernel.Bind<IUsersServiceLogic>().To<UsersServicesLogic>();
                kernel.Bind<IUsersService>().To<UsersService>();

                kernel.Bind<IReporteServiceLogic>().To<ReporteServiceLogic>();
                kernel.Bind<IReporteService>().To<ReporteService>();

                kernel.Bind<IPerfilServiceLogic>().To<PerfilServiceLogic>();
                kernel.Bind<IPerfilService>().To<PerfilService>();

                kernel.Bind<IUsuarioCentroCostoServiceLogic>().To<UsuarioCentroCostoServiceLogic>();
                kernel.Bind<IUsuarioCentroCostoService>().To<UsuarioCentroCostoService>();

                kernel.Bind<IDestinoServiceLogic>().To<DestinoServiceLogic>();
                kernel.Bind<IDestinoService>().To<DestinoService>();

                kernel.Bind<IMenuPaginasServiceLogic>().To<MenuPaginasServiceLogic>();
                kernel.Bind<IMenuPaginasService>().To<MenuPaginasService>();

                kernel.Bind<ITipoDestinoService>().To<TipoDestinoService>();

                kernel.Bind<IEmailService>().To<EmailService>();

                kernel.Bind<ITipoServicioService>().To<TipoServicioService>();

                kernel.Bind<IMotivoCreacionSolicitudService>().To<MotivoCreacionSolicitudService>();

                kernel.Bind<IBeneficiadoServiceLogic>().To<BeneficiadoServiceLogic>();
                kernel.Bind<IBeneficiadoService>().To<BeneficiadoService>();

                kernel.Bind<IProveedorServiceLogic>().To<ProveedorServiceLogic>();
                kernel.Bind<IEstadoProveedorService>().To<EstadoProveedorService>();
                kernel.Bind<IServicioProveedorTaxiService>().To<ServicioProveedorTaxiService>();

             

                //kernel.Bind(x => x
                //    .FromAssembliesMatching("*")
                //    .SelectAllClasses()
                //    .BindDefaultInterface()
                //    );


                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
    }
}