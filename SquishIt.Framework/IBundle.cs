using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SquishIt.Framework.Base;
using SquishIt.Framework.CSS;
using SquishIt.Framework.Minifiers;
using SquishIt.Framework.Renderers;

namespace SquishIt.Framework
{
    /// <summary>
    /// Base interface for all bundles
    /// </summary>
    public interface IBundle
    {
        /// <summary>
        /// Clears the testing cache.
        /// </summary>
        void ClearTestingCache();

        /// <summary>
        /// Sets the path that will be used when the bundle is requested. For Javascript, the default is "~/bundle/script/"; for CSS, "~/bundle/style/".
        /// </summary>
        /// <param name="route">The route.</param>
        void SetCacheRoute(string route);

        /// <summary>
        /// Gets the cached content for a given key (bundle file name).
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string GetCachedContent(string key);
    }
}

namespace SquishIt.Framework.Css
{
    /// <summary>
    /// Interface for css bundle
    /// </summary>
    public interface ICssBundle : IBundleBase<CSSBundle>
    {
        CSSBundle ProcessImports();
        CSSBundle AppendHashForAssets();
    }
}

namespace SquishIt.Framework.JavaScript
{
    /// <summary>
    /// Interface for javascript bundle
    /// </summary>
    public interface IJavaScriptBundle : IBundleBase<JavaScriptBundle>
    {
        JavaScriptBundle WithDeferredLoad();
    }
}

namespace SquishIt.Framework.Base
{
    public interface IBundleBase<T> : IBundle
        where T : BundleBase<T>
    {
        T WithoutTypeAttribute();
        T Add(string filePath);
        T AddMinified(string filePath);
        T AddDirectory(string folderPath, bool recursive = true);
        T AddMinifiedDirectory(string folderPath, bool recursive = true);
        T AddString(string content);
        T AddString(string content, string extension);
        T AddMinifiedString(string content);
        T AddMinifiedString(string content, string extension);
        T AddString(string format, object[] values);
        T AddString(string format, string extension, object[] values);
        T AddRemote(string localPath, string remotePath);
        T AddRemote(string localPath, string remotePath, bool downloadRemote);
        T AddDynamic(string siteRelativePath);
        T AddRootEmbeddedResource(string localPath, string embeddedResourcePath);
        T AddEmbeddedResource(string localPath, string embeddedResourcePath);
        T RenderOnlyIfOutputFileMissing();
        T ForceDebug();
        T ForceDebugIf(Func<bool> predicate);
        T ForceRelease();
        T WithOutputBaseHref(string href);
        T WithReleaseFileRenderer(IRenderer renderer);
        T WithAttribute(string name, string value);
        T WithAttributes(Dictionary<string, string> attributes, bool merge = true);
        T WithMinifier<TMin>() where TMin : IMinifier<T>;
        T WithMinifier<TMin>(TMin minifier) where TMin : IMinifier<T>;
        T HashKeyNamed(string hashQueryStringKeyName);
        T WithoutRevisionHash();
        T WithPreprocessor(IPreprocessor instance);
        string Render(string renderTo);
        string RenderCachedAssetTag(string name);
        void AsNamed(string name, string renderTo);

        [Obsolete("Use AsNamed")]
        void AsNamedFile(string name, string renderTo);

        string AsCached(string name, string filePath);

        [Obsolete("Use AsCached")]
        string AsNamedCache(string name, string filePath);

        string RenderNamed(string name);
        string RenderCached(string name);
        void ClearCache();
    }
}
