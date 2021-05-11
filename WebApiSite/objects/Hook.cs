using System.Collections.Generic;

namespace TryToWebApi.objects
{
    public static class Hook
    {
        public static readonly List<string> SessionTokens = new();

        public static bool IsAdminSessionRegistered(string sessionToken)
        {
            return SessionTokens.Contains(sessionToken);
        }
    }
}