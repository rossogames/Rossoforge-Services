using Rossoforge.Services.Service;

namespace Rossoforge.Services.Samples.ServiceTemplate
{
    public class TemplateService : ITemplateService, IInitializable
    {
        private TemplateDataService _dataService;

        public TemplateService(TemplateDataService dataService)
        {
            _dataService = dataService;
        }

        public void Initialize()
        {
        }
    }
}
