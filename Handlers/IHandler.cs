using Swen1.MTCG_Petrovic.Server;

namespace Swen1.MTCG_Petrovic.Handlers
{
    public interface IHandler
    {
        /// <summary>
        /// Tries to handle a HTTP request.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        /// <returns>TRUE if the request was handled, otherwise FALSE.</returns>
        bool Handle(HttpSvrEventArgs e);
    }
}
