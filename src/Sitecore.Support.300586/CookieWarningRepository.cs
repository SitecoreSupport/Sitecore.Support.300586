using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.XA.Foundation.Multisite.LinkManagers;
using Sitecore.XA.Foundation.Mvc.Repositories.Base;

namespace Sitecore.Support.XA.Feature.Compliancy.Repositories
{
  public class CookieWarningRepository : Sitecore.XA.Feature.Compliancy.Repositories.CookieWarningRepository
  {

    public override IRenderingModelBase GetModel()
    {
      Sitecore.Support.XA.Feature.Compliancy.Models.CookieWarningRenderingModel cookieWarningRenderingModel = new Sitecore.Support.XA.Feature.Compliancy.Models.CookieWarningRenderingModel
      {
        ShowCookieWarning = ShowCookieWarning()
      };
      FillBaseProperties(cookieWarningRenderingModel);
      if ((cookieWarningRenderingModel.ShowCookieWarning || IsEdit) && CookieWarning != null)
      {
        cookieWarningRenderingModel.SelectedCookieWarningType = CookieWarningTypeSetting.Value;
        cookieWarningRenderingModel.CookieWarningContentItem = CookieWarning;
        cookieWarningRenderingModel.CookieWarningButtonText = CookieWarning[Sitecore.XA.Feature.Compliancy.Templates.CookieWarning.Fields.CookieWarningButtonText];
        cookieWarningRenderingModel.CookieWarningLearnMoreButtonText = new LinkItem(CookieWarning[Sitecore.XA.Feature.Compliancy.Templates.CookieWarning.Fields.LearnMoreTarget]).Text;
        cookieWarningRenderingModel.CookieWarningLearnMoreTarget = new LinkItem(CookieWarning[Sitecore.XA.Feature.Compliancy.Templates.CookieWarning.Fields.LearnMoreTarget]).TargetUrl;
      }
      return cookieWarningRenderingModel;
    }
  }
}