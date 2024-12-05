using Swen1.MTCG_Petrovic.Server;
using System.Reflection.Metadata;
using System.Text.Json.Nodes;

namespace Swen1.MTCG_Petrovic.Handlers
{
    public class SessionHandler : Handler, IHandler
    {
        public override bool Handle(HttpSvrEventArgs e)
        {
            if (e.Path.TrimEnd('/', ' ', '\t') == "/sessions" && e.Method == "POST")
            {
                return CreateSession(e);
            }

            return false;
        }

        private bool CreateSession(HttpSvrEventArgs e)
        {
            JsonObject reply = new() { ["success"] = false, ["message"] = "Ungültige Anfrage." };
            int status = HttpStatusCode.BAD_REQUEST;

            try
            {
                JsonNode? json = JsonNode.Parse(e.Payload);
                if (json != null)
                {
                    string username = (string)json["Username"]!;
                    string password = (string)json["Password"]!;

                    (bool success, Models.User? user) = Models.User.Authenticate(username, password);

                    if (success)
                    {
                        string token = Security.Token.GenerateToken(user!);
                        status = HttpStatusCode.OK;
                        reply = new JsonObject()
                        {
                            ["success"] = true,
                            ["message"] = "Login erfolgreich.",
                            ["token"] = token
                        };
                    }
                    else
                    {
                        status = HttpStatusCode.UNAUTHORIZED;
                        reply = new JsonObject() { ["success"] = false, ["message"] = "Ungültige Zugangsdaten." };
                    }
                }
            }
            catch (Exception ex)
            {
                reply = new JsonObject() { ["success"] = false, ["message"] = ex.Message };
            }

            e.Reply(status, reply.ToJsonString());
            return true;
        }
    }
}
