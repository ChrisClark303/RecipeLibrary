using Autofac;
using RecipeLibrary.Core;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace RecipeLibrary.Api
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = ThisAssembly.GetReferencedAssemblies()
                 .Where(asm => asm.Name.StartsWith("RecipeLibrary"))
                 .Select(asm => Assembly.Load(asm));

            builder.RegisterAssemblyTypes(assemblies.ToArray())
                   .AsImplementedInterfaces();

            base.Load(builder);
        }
    }

    public class SettingsModule : Module
    {
        private readonly AppSettings _settings;

        public SettingsModule(AppSettings settings)
        {
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<MongoSettings>((ctx )=> _settings.MongoSettings)
                .As<IMongoSettings>()
                .SingleInstance();
        }
    }
}
