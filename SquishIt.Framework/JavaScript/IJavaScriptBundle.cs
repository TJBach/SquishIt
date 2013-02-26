using SquishIt.Framework.Base;

namespace SquishIt.Framework.JavaScript
{
    /// <summary>
    /// Re-added interface to allow old code to work with with the new squish-it version without modification.
    /// </summary>
    public interface IJavaScriptBundle : IBundleBase<JavaScriptBundle>
    {
        JavaScriptBundle WithDeferredLoad();
    }
}
