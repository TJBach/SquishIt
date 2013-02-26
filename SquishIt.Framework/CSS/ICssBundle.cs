using SquishIt.Framework.Base;
using SquishIt.Framework.CSS;

namespace SquishIt.Framework.Css
{
    /// <summary>
    /// Re-added interface to allow old code to work with with the new squish-it version without modification.
    /// </summary>
    public interface ICssBundle : IBundleBase<CSSBundle>
    {
        CSSBundle ProcessImports();
        CSSBundle AppendHashForAssets();
    }
}