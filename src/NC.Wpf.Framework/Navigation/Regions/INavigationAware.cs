#if !HAS_UNO_WINUI && !HAS_WINUI

using NC.Wpf.Core.Navigation.Regions;

namespace NC.Wpf.Framework.Navigation.Regions
{
    /// <summary>
    /// Provides a way for objects involved in navigation to be notified of navigation activities.
    /// </summary>
    /// <remarks>
    /// Provides compatibility for Legacy Prism.Wpf apps.
    /// </remarks>
    public interface INavigationAware : IRegionAware
    {
    }
}
#endif
