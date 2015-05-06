using System;

namespace IntelliSun.Aim
{
    public static class IntentExtensions
    {
        public static IIntentSetPriority<T> Site<T>(this IIntentSetSitePriority<T> syntax, string siteKey)
        {
            if (syntax == null)
                throw new ArgumentNullException("syntax");

            if (siteKey == null)
                throw new ArgumentNullException("siteKey");

            var site = new StringIntentSite(siteKey);
            return syntax.Site(site);
        }

        public static IIntent<T> Site<T>(this IIntentSetSite<T> syntax, string siteKey)
        {
            if (syntax == null)
                throw new ArgumentNullException("syntax");

            if (siteKey == null)
                throw new ArgumentNullException("siteKey");

            var site = new StringIntentSite(siteKey);
            return syntax.Site(site);
        }

        public static IIntentSetPriority<T> Site<T, TEnum>(this IIntentSetSitePriority<T> syntax, TEnum siteKey)
        {
            if (syntax == null)
                throw new ArgumentNullException("syntax");

            if (siteKey == null)
                throw new ArgumentNullException("siteKey");

            var site = new StringIntentSite(siteKey.ToString());
            return syntax.Site(site);
        }

        public static IIntent<T> Site<T, TEnum>(this IIntentSetSite<T> syntax, TEnum siteKey)
        {
            if (syntax == null)
                throw new ArgumentNullException("syntax");

            if (siteKey == null)
                throw new ArgumentNullException("siteKey");

            var site = new StringIntentSite(siteKey.ToString());
            return syntax.Site(site);
        }
    }
}
