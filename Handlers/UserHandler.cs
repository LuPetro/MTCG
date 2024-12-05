using Swen1.MTCG_Petrovic.Server;
using Swen1.MTCG_Petrovic.Security;
using System.Text.Json.Nodes;
using Swen1.MTCG_Petrovic.Models;

namespace Swen1.MTCG_Petrovic.Handlers
{
    public class UserHandler : Handler, IHandler
    {
        public override bool Handle(HttpSvrEventArgs e)
        {
            if (e.Path.TrimEnd('/', ' ', '\t') == "/users" && e.Method == "POST")
            {
                return CreateUser(e);
            }
            else if (e.Path.StartsWith("/users/") && e.Method == "GET")
            {
                return GetUser(e);
            }

            return false;
        }

        private bool GetUser(HttpSvrEventArgs e)
        {
            JsonObject reply = new() { ["success"] = false, ["message"] = "Ungültige Anfrage." };
            int status = HttpStatusCode.BAD_REQUEST;

            try
            {
                (bool Success, User? User) auth = Token.Authenticate(e);
                if (!auth.Success)
                {
                    status = HttpStatusCode.UNAUTHORIZED;
                    reply["message"] = "Nicht autorisiert.";
                }
                else
                {
                    string username = e.Path[7..]; // Extrahiert den Benutzernamen aus "/users/{username}"
                    User? user = User.Get(username);

                    if (user == null)
                    {
                        status = HttpStatusCode.NOT_FOUND;
                        reply["message"] = "Benutzer nicht gefunden.";
                    }
                    else
                    {
                        status = HttpStatusCode.OK;
                        reply["success"] = true;
                        reply["username"] = user.Username;
                        reply["coins"] = user.Coins;
                        reply["cards"] = new JsonArray(user.Cards.Select(c => new JsonObject
                        {
                            ["Id"] = c.Id,  // Unterstützt jetzt
                            ["Name"] = c.Name,
                            ["Damage"] = c.Damage,
                            ["Element"] = c.Element  // Element wird ebenfalls angezeigt
                        }).ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                reply["message"] = ex.Message;
            }

            e.Reply(status, reply.ToJsonString());
            return true;
        }


        private bool CreateUser(HttpSvrEventArgs e)
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

                    Models.User.Register(username, password);

                    status = HttpStatusCode.OK;
                    reply["success"] = true;
                    reply["message"] = "Benutzer erstellt.";
                }
            }
            catch (Exception ex)
            {
                reply["success"] = false;
                reply["message"] = ex.Message;
            }

            e.Reply(status, reply.ToJsonString());
            return true;
        }
    }
}
