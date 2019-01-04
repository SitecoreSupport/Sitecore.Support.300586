using Sitecore.Data.Items;
using Sitecore.XA.Feature.Compliancy.Models;
using Sitecore.XA.Foundation.Mvc.Models;

namespace Sitecore.Support.XA.Feature.Compliancy.Models
{
  public class PrivacyWarningRenderingModel : RenderingModelBase
  {
    public Item PrivacyWarningContentItem { get; set; }
    public string PrivacyWarningButtonText { get; set; }
    public string PrivacyWarningLearnMoreButtonText { get; set; }
    public string PrivacyWarningLearnMoreTarget { get; set; }
    public PrivacyWarningType SelectedPrivacyWarningType { get; set; }
    public bool ShowPrivacyWarning { get; set; }
    public bool ShowCloseButton => SelectedPrivacyWarningType == PrivacyWarningType.Permisive || SelectedPrivacyWarningType == PrivacyWarningType.AcceptOnClose;
  }
}