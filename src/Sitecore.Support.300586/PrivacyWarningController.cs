using Sitecore.Support.XA.Feature.Compliancy.Repositories;
using Sitecore.XA.Foundation.Mvc.Controllers;

namespace Sitecore.Support.XA.Feature.Compliancy.Controllers
{
  public class PrivacyWarningController : StandardController
  {
    protected IPrivacyWarningRepository Repository { get; }

    public PrivacyWarningController(IPrivacyWarningRepository repository)
    {
      Repository = repository;
    }

    protected override object GetModel()
    {
      return Repository.GetModel();
    }
  }
}