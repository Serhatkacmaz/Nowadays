using Autofac;
using Microsoft.AspNetCore.Authentication;
using Nowadays.Core.Repositories;
using Nowadays.Core.Services;
using Nowadays.Core.UnitOfWorks;
using Nowadays.Repository;
using Nowadays.Repository.Repositories;
using Nowadays.Repository.UnitOfWorks;
using Nowadays.Service.Mapping;
using System.Reflection;
using Module = Autofac.Module;

namespace Nowadays.Api.Modules;

public class RepositoryAndServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(GenericRepository<>))
            .As(typeof(IGenericRepository<>))
            .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(Nowadays.Service.Services.Service<,>))
            .As(typeof(IService<,>))
            .InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();

        var apiAssembly = Assembly.GetExecutingAssembly();
        var repositoryAssembly = Assembly.GetAssembly(typeof(NowadaysContext));
        var serviceAssembly = Assembly.GetAssembly(typeof(GeneralMapping));

        builder.RegisterAssemblyTypes(apiAssembly, repositoryAssembly, serviceAssembly)
            .Where(x => x.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(apiAssembly, repositoryAssembly, serviceAssembly)
            .Where(x => x.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        //-> InstancePerLifetimeScope => scope
        //-> InstancePerDependency => transit
    }
}