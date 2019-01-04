using System;
using System.Linq;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.XA.Feature.Compliancy.Models;
using Sitecore.XA.Feature.Compliancy.Repositories;
using Sitecore.XA.Foundation.Multisite;
using Sitecore.XA.Foundation.Multisite.LinkManagers;
using Sitecore.XA.Foundation.Mvc.Repositories.Base;
using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;

namespace Sitecore.Support.XA.Feature.Compliancy.Repositories
{
  public class PrivacyWarningRepository : ModelRepository, IPrivacyWarningRepository
  {
    private readonly IMultisiteContext _multisiteContext;

    public PrivacyWarningRepository()
    {
      _multisiteContext = DependencyInjection.ServiceLocator.ServiceProvider.GetService<IMultisiteContext>();
    }

    private Item _privacyWarning;
    private PrivacyWarningType? _privacyWarningType;

    protected Item PrivacyWarning
    {
      get
      {
        if (_privacyWarning == null)
        {
          Item settingsItem = _multisiteContext?.GetSettingsItem(PageContext.Current);
          _privacyWarning = settingsItem?.Children.SingleOrDefault(i => i.InheritsFrom(Sitecore.XA.Feature.Compliancy.Templates.PrivacyWarning.ID));
        }

        return _privacyWarning;
      }
    }

    protected PrivacyWarningType? PrivacyWarningTypeSetting
    {
      get
      {
        if (_privacyWarningType == null)
        {
          _privacyWarningType = PrivacyWarning.Fields[Sitecore.XA.Feature.Compliancy.Templates.PrivacyWarning.Fields.PrivacyWarningType].ToEnum<PrivacyWarningType>();
        }

        return _privacyWarningType ?? PrivacyWarningType.AcceptOnClose;
      }
    }

    protected virtual bool ShowPrivacyWarning()
    {
      if (PrivacyWarning == null)
      {
        return false;
      }

      if (!PrivacyWarningTypeSetting.HasValue || PrivacyWarningTypeSetting == PrivacyWarningType.Hidden)
      {
        return false;
      }

      var cookieWhitelist = PrivacyWarning[Sitecore.XA.Feature.Compliancy.Templates.PrivacyWarning.Fields.Whitelist];
      if (!cookieWhitelist.Contains(PageContext.Current.ID.ToString()) && !CookieExists())
      {
        if (PrivacyWarningTypeSetting == PrivacyWarningType.ShowOnce)
        {
          HttpCookie privacyWarning = new HttpCookie("privacy-notification", "1");
          DateTime now = DateTime.UtcNow;
          privacyWarning.Expires = now.AddYears(1);
          HttpContext.Current?.Response.Cookies.Add(privacyWarning);
        }
        return true;
      }

      return false;
    }

    protected virtual bool CookieExists()
    {
      HttpCookie policyCookie = HttpContext.Current?.Request.Cookies.Get("privacy-notification");
      return policyCookie != null && policyCookie.Value == "1";
    }

    public override IRenderingModelBase GetModel()
    {
      Sitecore.Support.XA.Feature.Compliancy.Models.PrivacyWarningRenderingModel privacyWarningModel = new Sitecore.Support.XA.Feature.Compliancy.Models.PrivacyWarningRenderingModel { ShowPrivacyWarning = ShowPrivacyWarning() };

      FillBaseProperties(privacyWarningModel);

      if ((privacyWarningModel.ShowPrivacyWarning || IsEdit) && (PrivacyWarning != null))
      {
        privacyWarningModel.SelectedPrivacyWarningType = PrivacyWarningTypeSetting.Value;
        privacyWarningModel.PrivacyWarningContentItem = PrivacyWarning;
        privacyWarningModel.PrivacyWarningButtonText = PrivacyWarning[Sitecore.XA.Feature.Compliancy.Templates.PrivacyWarning.Fields.PrivacyWarningButtonText];
        privacyWarningModel.PrivacyWarningLearnMoreButtonText = new LinkItem(PrivacyWarning[Sitecore.XA.Feature.Compliancy.Templates.PrivacyWarning.Fields.LearnMoreTarget]).Text;
        privacyWarningModel.PrivacyWarningLearnMoreTarget = new LinkItem(PrivacyWarning[Sitecore.XA.Feature.Compliancy.Templates.PrivacyWarning.Fields.LearnMoreTarget]).TargetUrl;
      }

      return privacyWarningModel;
    }
  }
}