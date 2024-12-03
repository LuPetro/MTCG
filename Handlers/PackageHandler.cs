using Swen1.MTCG_Petrovic.Models;
using Swen1.MTCG_Petrovic.Server;
using Swen1.MTCG_Petrovic.Security;
using System.Text.Json.Nodes;

namespace Swen1.MTCG_Petrovic.Handlers
{
    public class PackageHandler : Handler, IHandler
    {
        public override bool Handle(HttpSvrEventArgs e)
        {
            if (e.Path.TrimEnd('/', ' ', '\t') == "/packages" && e.Method == "POST")
            {
                return CreatePackage(e);
            }

            return false;
        }

        private bool CreatePackage(HttpSvrEventArgs e)
        {
            JsonObject reply = new() { ["success"] = false, ["message"] = "Ungültige Anfrage." };
            int status = HttpStatusCode.BAD_REQUEST;

            try
            {
                (bool Success, User? User) auth = Token.Authenticate(e);
                if (!auth.Success || auth.User?.Username != "admin")
                {
                    status = HttpStatusCode.UNAUTHORIZED;
                    reply["message"] = "Nicht autorisiert.";
                }
                else
                {
                    JsonNode? json = JsonNode.Parse(e.Payload);
                    if (json != null && json is JsonArray packages)
                    {
                        foreach (JsonNode? node in packages)
                        {
                            if (node is not JsonObject package)
                            {
                                continue;
                            }

                            if (package["Cards"] is not JsonArray cardArray)
                            {
                                continue;
                            }

                            // Typen explizit angeben
                            List<Card> cards = new();
                            foreach (JsonNode? cardNode in cardArray)
                            {
                                if (cardNode is not JsonObject card)
                                {
                                    continue;
                                }

                                string type = card["Type"]!.ToString();
                                Card newCard = type == "monster"
                                    ? new MonsterCard(
                                        (string)card["Id"]!,
                                        (string)card["Name"]!,
                                        (int)card["Damage"]!,
                                        (string)card["Element"]!)
                                    : new SpellCard(
                                        (string)card["Id"]!,
                                        (string)card["Name"]!,
                                        (int)card["Damage"]!,
                                        (string)card["Element"]!);

                                cards.Add(newCard);
                            }

                            Package.Create((string)package["Id"]!, cards);
                        }

                        status = HttpStatusCode.CREATED;
                        reply["success"] = true;
                        reply["message"] = "Pakete erstellt.";
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
