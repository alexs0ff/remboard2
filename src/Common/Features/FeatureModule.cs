using Autofac;

namespace Common.Features
{
    public abstract class FeatureModule:Module
    {
        private readonly RegistrationKind _registrationKind;

        protected FeatureModule(RegistrationKind registrationKind)
        {
            _registrationKind = registrationKind;
        }

        protected abstract void RegisterModel(ContainerBuilder builder);

        protected abstract void RegisterServices(ContainerBuilder builder);

        protected override void Load(ContainerBuilder builder)
        {
            if (_registrationKind==RegistrationKind.Services)
            {
                RegisterServices(builder);
            }

            if (_registrationKind == RegistrationKind.Models)
            {
                RegisterModel(builder);
            }
        }
    }
}
