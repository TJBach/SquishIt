namespace SquishIt.Framework
{
    public static class StringExtensions
    {
        public static string ReplaceLast(this string replaceOn, string toReplace, string replaceWith)
        {
            int indexOfLastInstance = replaceOn.LastIndexOf(toReplace);
            int lengthOfToReplace = toReplace.Length;

            return replaceOn
                .Remove(indexOfLastInstance, lengthOfToReplace)
                .Insert(indexOfLastInstance, replaceWith);
        }
    }
}
