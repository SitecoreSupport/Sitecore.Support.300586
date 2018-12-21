using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.XA.Feature.Compliancy.Models;
using Sitecore.XA.Foundation.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Support.XA.Feature.Compliancy.Models
{
  public class CookieWarningRenderingModel : RenderingModelBase
  {
    public Sitecore.Data.Items.Item CookieWarningContentItem
    {
      get;
      set;
    }

    public string CookieWarningButtonText
    {
      get;
      set;
    }

    public string CookieWarningLearnMoreButtonText
    {
      get;
      set;
    }

    public string CookieWarningLearnMoreTarget
    {
      get;
      set;
    }

    public CookieWarningType SelectedCookieWarningType
    {
      get;
      set;
    }

    public bool ShowCookieWarning
    {
      get;
      set;
    }

    public bool ShowCloseButton
    {
      get
      {
        if (SelectedCookieWarningType != CookieWarningType.Permisive)
        {
          return SelectedCookieWarningType == CookieWarningType.AcceptOnClose;
        }
        return true;
      }
    }
  }
}