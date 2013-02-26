using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SquishIt.Framework.Renderers
{
    public class CacheRenderer: IRenderer
    {
        readonly string prefix;
        readonly string name;
        private string fileName;
        static readonly Dictionary<string, string> cache = new Dictionary<string, string>();
        static readonly ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
        //Added so we can lookup files by the file name too.
        private static Dictionary<string, string> filecache = new Dictionary<string, string>();

        public CacheRenderer(string prefix, string name, string fileName)
        {
            this.prefix = prefix;
            this.name = name;
            this.fileName = fileName;
        }

        public void Render(string content, string outputFile)
        {
            readerWriterLockSlim.EnterWriteLock();
            try
            {
                //Store the file name in the cache
                if (fileName.Contains("#"))
                {
                    // Need to adjust key to include the generated hash
                    // Example:
                    //      outputFile = "/bundle/style/output_234234234234234.js"
                    //      fileName = "output_#.js"
                    var parts = fileName.Split('#');
                    var hash = parts.Aggregate(outputFile, (current, part) => current.Replace(part, string.Empty));
                    var i = hash.LastIndexOfAny(new[] { '/', '\\' });
                    if (i > 0)
                    {
                        hash = hash.Remove(0, i + 1);
                    }
                    fileName = fileName.Replace("#", hash);
                }
                filecache[prefix + "_" + fileName] = content;

                cache[prefix + "_" + name] = content;
            }
            finally
            {
                readerWriterLockSlim.ExitWriteLock();
            }
        }

        public static string Get(string prefix, string name)
        {
            readerWriterLockSlim.EnterReadLock();
            try
            {
                return cache[prefix + "_" + name];
            }
            finally
            {
                readerWriterLockSlim.ExitReadLock();
            }
        }

        //Locate cache by file name
        public static string GetFile(string prefix, string fileName)
        {
            readerWriterLockSlim.EnterReadLock();
            try
            {
                return filecache[prefix + "_" + fileName];
            }
            finally
            {
                readerWriterLockSlim.ExitReadLock();
            }
        }
    }
}