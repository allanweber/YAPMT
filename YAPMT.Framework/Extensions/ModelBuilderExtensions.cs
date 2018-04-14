using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace YAPMT.Framework.Extensions
{
    public static class ModelBuilderExtensions
    {
        private static readonly Type EntityTypeConfigurationTypeDefinition = typeof(IEntityTypeConfiguration<>);

        public static void ApplyConfiguration(this ModelBuilder modelBuilder)
        {
            var entityTypeConfigurationTypesList = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.FullName.Contains("Infrastructure"))
                .SelectMany(assembly => assembly.GetTypes()
                    .Where(type => type.GetInterfaces()
                        .Any(interfaceType =>
                            interfaceType.IsGenericType &&
                            interfaceType.GetGenericTypeDefinition() == EntityTypeConfigurationTypeDefinition)));

            entityTypeConfigurationTypesList
                .ForEach(type => modelBuilder.ApplyConfiguration(type));
        }

        public static void ApplyConfiguration(this ModelBuilder modelBuilder, Type configurationType)
        {
            if (configurationType == null)
                throw new ArgumentNullException(nameof(configurationType));

            var entityTypeConfiguration = Activator.CreateInstance(configurationType);

            ApplyConfiguration(modelBuilder, entityTypeConfiguration);
        }

        public static void ApplyConfiguration(this ModelBuilder modelBuilder, object configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var applyConfigurationMethodInfo = GetApplyConfigurationMethodInfo(modelBuilder, configuration);

            applyConfigurationMethodInfo?.Invoke(modelBuilder, new object[] { configuration });
        }

        private static MethodInfo GetApplyConfigurationMethodInfo(ModelBuilder modelBuilder, object entityTypeConfiguration)
        {
            var entityType = GetEntityType(entityTypeConfiguration.GetType(), EntityTypeConfigurationTypeDefinition);

            if (entityType == null)
                return null;

            var applyConfigurationMethodInfo = modelBuilder.GetType().GetMethods()
                .FirstOrDefault(methodInfo =>
                    methodInfo.Name.Equals(nameof(modelBuilder.ApplyConfiguration)) &&
                    methodInfo.GetParameters().Count() == 1 &&
                    methodInfo.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == EntityTypeConfigurationTypeDefinition);

            return applyConfigurationMethodInfo?.MakeGenericMethod(entityType);
        }

        private static Type GetEntityType(Type configurationType, Type entityTypeConfigurationTypeDefinition)
        {
            var baseEntityTypeConfigurationType = configurationType.GetInterfaces()
                .FirstOrDefault(type =>
                    type.IsGenericType &&
                    type.GetGenericTypeDefinition() == entityTypeConfigurationTypeDefinition);

            return baseEntityTypeConfigurationType?.GenericTypeArguments?.FirstOrDefault();
        }
    }
}
