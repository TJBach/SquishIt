using System;
using System.Collections.Generic;
using SquishIt.Framework.Minifiers;
using SquishIt.Framework.Renderers;

namespace SquishIt.Framework.Base
{
    /// <summary>
    /// Only exists as hack to get the IJavaScript and ICssBundle interfaces working.  
    /// Once the quad framework code is updated these interfaces will no longer be needed.
    /// </summary>
    /// <typeparam name="T">The bundle base.</typeparam>
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
