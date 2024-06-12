using Microsoft.Extensions.Localization;
using System.ComponentModel;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Properties;

namespace Elogic.Wamgroup.Sito2023.NetCore.Localization
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private string resourceKey = string.Empty;
        public HttpContext _httpContext => new HttpContextAccessor().HttpContext;

        public LocalizedDisplayNameAttribute(string resourceKey) : base(resourceKey)
        {
            this.resourceKey = resourceKey;
        }

        public override string DisplayName
        {
            get
            {
                LocalizationHelperService service = _httpContext.RequestServices.GetService(typeof(LocalizationHelperService)) as LocalizationHelperService;
                if (service != null)
                {
                    return service.Localize(resourceKey);
                }
                return resourceKey;
            }
        }
    }
}
