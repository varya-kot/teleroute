using System;
using System.Collections.Generic;
using System.Linq;

namespace Teleroute.Send
{
    public sealed class SendBatch<C> : ISend<C>
    {
        private readonly IEnumerable<ISend<C>> sends;

        public SendBatch(params ISend<C>[] sends)
            : this(sends.ToList()) { }

        public SendBatch(IEnumerable<ISend<C>> sends)
        {
            this.sends = sends;
        }

        //todo logger
        public void Send(C client)
        {
            foreach (ISend<C> send in sends)
            {
                try
                {
                    send.Send(client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
