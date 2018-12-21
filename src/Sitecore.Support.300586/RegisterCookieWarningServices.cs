using Microsoft.Extensions.DependencyInjection;
using Sitecore.XA.Feature.Compliancy.Repositories;
using Sitecore.XA.Foundation.IOC.Pipelines.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Support.XA.Feature.Compliancy.Pipelines.IoC
{
  public class RegisterCookieWarningServices : IocProcessor
  {
    public override void Process(IocArgs args)
    {
      args.ServiceCollection.AddTransient<ICookieWarningRepository, Repositories.CookieWarningRepository>();
    }

  }
}