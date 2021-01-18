using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using BookStore.API.Connections;
using BookStore.API.Controllers;
using BookStore.API.Logs;
using BookStore.API.Models;
using BookStore.API.Repositories;
using BookStore.API.Services;
using BookStore.API.UnitOfWorks;
using log4net;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.API.Autofac
{
  public static class UseAutoFacServiceProviderAdapter
  {
    public static IServiceCollection AddAutofacAdapter(this IServiceCollection services)
    {
      return services.AddAutofac();
    }

    public static void RegisterSelf(this ContainerBuilder builder)
    {
      ILifetimeScope lifetimeScope = null;
      builder.RegisterBuildCallback(c => lifetimeScope = c);
      builder.Register(c => lifetimeScope).SingleInstance().AsSelf();
    }

    public static void ConfigureAutofac(this ILifetimeScope container)
    {
    }
  }

  public class AutofacRegisterModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      var loggerInterception = typeof(LoggerInterceptor);
      var loggerControllerInterception = typeof(LoggerControllerInterceptor);

      builder.RegisterType<Log4NetAdapter>().As<ILogger>().SingleInstance();

      builder.RegisterType<LoggerInterceptor>().InstancePerLifetimeScope();
      builder.RegisterType<LoggerControllerInterceptor>().InstancePerLifetimeScope();

      builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>().EnableInterfaceInterceptors().InterceptedBy(loggerInterception).InstancePerLifetimeScope();

      builder.RegisterType<GenericRepository<BookCategory>>().As<IGenericRepository<BookCategory>>().EnableInterfaceInterceptors().InterceptedBy(loggerInterception).InstancePerLifetimeScope();
      builder.RegisterType<GenericRepository<Book>>().As<IGenericRepository<Book>>().EnableInterfaceInterceptors().InterceptedBy(loggerInterception).InstancePerLifetimeScope();

      builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().EnableInterfaceInterceptors().InterceptedBy(loggerInterception).InstancePerLifetimeScope();
      builder.RegisterType<BookRepository>().As<IBookRepository>().EnableInterfaceInterceptors().InterceptedBy(loggerInterception).InstancePerLifetimeScope();

      builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().EnableInterfaceInterceptors().InterceptedBy(loggerInterception).InstancePerLifetimeScope();

      builder.RegisterType<CategoryService>().As<ICategoryService>().EnableInterfaceInterceptors().InterceptedBy(loggerInterception).InstancePerLifetimeScope();
      builder.RegisterType<BookRepository>().As<IBookRepository>().EnableInterfaceInterceptors().InterceptedBy(loggerInterception).InstancePerLifetimeScope();

      builder.RegisterType<CategoriesController>().EnableClassInterceptors().InterceptedBy(loggerControllerInterception).PropertiesAutowired();
      builder.RegisterType<BooksController>().EnableClassInterceptors().InterceptedBy(loggerControllerInterception).PropertiesAutowired();

      builder.RegisterSelf();
    }
  }
}