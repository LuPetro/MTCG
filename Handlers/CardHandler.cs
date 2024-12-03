using Swen1.MTCG_Petrovic.Models;
using Swen1.MTCG_Petrovic.Security;
using Swen1.MTCG_Petrovic.Server;
using System.Text.Json.Nodes;

namespace Swen1.MTCG_Petrovic.Handlers
{
    public class CardHandler : Handler, IHandler
    {
        public override bool Handle(HttpSvrEventArgs e)
        {
            if (e.Path.TrimEnd('/', ' ', '\t') == "/cards" && e.Method == "GET")
            {
                return GetCards(e);
            }

            return false;
        }

        private bool GetCards(HttpSvrEventArgs e)
        {
            JsonObject reply = new() { ["success"] = false, ["message"] = "Ungültige Anfrage." };
            int status = HttpStatusCode.BAD_REQUEST;

            try
            {
                // Benutzer authentifizieren
                (bool Success, Models.User? User) auth = Token.Authenticate(e);
                if (!auth.Success || auth.User == null)
                {
                    status = HttpStatusCode.UNAUTHORIZED;
                    reply["message"] = "Nicht autorisiert.";
                }
                else
                {
                    // Sicherstellen, dass Cards nicht null ist
                    if (auth.User.Cards == null || !auth.User.Cards.Any())
                    {
                        status = HttpStatusCode.BAD_REQUEST;
                        reply["message"] = "Keine Karten gefunden.";
                    }
                    else
                    {
                        // Karten serialisieren
                        status = HttpStatusCode.OK;
                        reply["success"] = true;
                        reply["cards"] = new JsonArray(auth.User.Cards.Select(c => new JsonObject
                        {
                            ["Id"] = c.Id,
                            ["Name"] = c.Name,
                            ["Damage"] = c.Damage,
                            ["Element"] = c.Element
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
    }
}
