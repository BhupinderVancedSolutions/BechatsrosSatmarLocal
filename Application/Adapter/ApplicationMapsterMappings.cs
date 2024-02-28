using Domain.Entities;
using DTO.Request;
using Mapster;

namespace Application.Adapter
{
    public static class ApplicationMapsterMappings
    {
        public static void Configure()
        {
            var config = TypeAdapterConfig.GlobalSettings;
            
            
        }
    }
    public class MappingConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<PaymentProvider, PaymentProviderRequest>.NewConfig();
        }
    }

}
