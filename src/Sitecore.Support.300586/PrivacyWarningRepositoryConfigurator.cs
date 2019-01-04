using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.DependencyInjection;

namespace Sitecore.Support
{
  public class PrivacyWarningRepositoryConfigurator : Sitecore.DependencyInjection.IServicesConfigurator
  {
    public void Configure(IServiceCollection serviceCollection)
    {
      serviceCollection.AddTransient(typeof(Sitecore.Support.XA.Feature.Compliancy.Controllers.PrivacyWarningController));
    }
  }
}