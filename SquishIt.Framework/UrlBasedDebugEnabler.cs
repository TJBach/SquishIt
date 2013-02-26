using System;

namespace SquishIt.Framework
{
    public static class UrlBasedDebugEnabler
    {
        private const string DEBUG_MODE_PARAM = "DebugMode";

        //This gets called when creating bundles now too, and if this happens before we are in a request
        //it will throw an exception
        private static bool RequestHasQueryStrings()
        {
            bool queryStringsAvailiable;

            try
            {
                queryStringsAvailiable = HttpContext.Current != null && HttpContext.Current.Request.QueryString.HasKeys();
            }
            catch
            {
                queryStringsAvailiable = false;
            }

            return queryStringsAvailiable;
        }

        public static bool IsDebugEnabled()
        {
            if (RequestHasQueryStrings())
            {
                string debugMode = HttpContext.Current.Request.QueryString[DEBUG_MODE_PARAM];
                if (!string.IsNullOrEmpty(debugMode))
                {
                    bool isDebug;
                    Boolean.TryParse(debugMode, out isDebug);
                    return isDebug;
                }
            }

            return false;
        }
    }
}