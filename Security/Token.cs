using System.Collections.Generic;

namespace Swen1.MTCG_Petrovic.Security
{
    public static class Token
    {
        private static Dictionary<string, Models.User> _tokens = new();

        public static string GenerateToken(Models.User user)
        {
            if (Program.DEBUG_MODE)
            {
                string token = $"{user.Username}-testToken";
                _tokens[token] = user;
                return token;
            }

            string randomToken = Guid.NewGuid().ToString();
            _tokens[randomToken] = user;
            return randomToken;
        }

        public static (bool Success, Models.User? User) Authenticate(string token)
        {
            if (_tokens.TryGetValue(token, out Models.User? user))
            {
                return (true, user);
            }
            return (false, null);
        }

        public static (bool Success, Models.User? User) Authenticate(Server.HttpSvrEventArgs e)
        {
            foreach (var header in e.Headers)
            {
                if (header.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase) &&
                    header.Value.StartsWith("Bearer "))
                {
                    string token = header.Value.Substring(7).Trim();
                    return Authenticate(token);
                }
            }
            return (false, null);
        }
    }
}
