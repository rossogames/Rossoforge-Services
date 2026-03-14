using Rossoforge.Core.Services;

namespace Rossoforge.Services.Samples.ServiceTemplate
{
    public class TemplateService : ITemplateService, IInitializable
    {
        private TemplateServiceData _serviceData;

        public TemplateService(TemplateServiceData serviceData)
        {
            _serviceData = serviceData;
        }

        public void Initialize()
        {
        }
    }
}
