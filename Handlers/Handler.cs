using Swen1.MTCG_Petrovic.Server;
using System.Reflection;
using System.Reflection.Metadata;

namespace Swen1.MTCG_Petrovic.Handlers
{
    public abstract class Handler : IHandler
    {
        private static List<IHandler>? _handlers = null;

        private static List<IHandler> GetHandlers()
        {
            List<IHandler> rval = new();
            foreach (Type i in Assembly.GetExecutingAssembly().GetTypes()
                                        .Where(m => m.IsAssignableTo(typeof(IHandler)) && (!m.IsAbstract)))
            {
                IHandler? h = (IHandler?)Activator.CreateInstance(i);
                if (h != null)
                {
                    rval.Add(h);
                }
            }

            return rval;
        }

        public static async Task HandleEvent(HttpSvrEventArgs e)
        {
            await Task.Run(() =>
            {
                _handlers ??= GetHandlers();

                foreach (IHandler i in _handlers)
                {
                    if (i.Handle(e)) return;
                }

                e.Reply(HttpStatusCode.BAD_REQUEST, "{\"success\":false,\"message\":\"Bad request\"}");
            });
        }

        public abstract bool Handle(HttpSvrEventArgs e);
    }
}
