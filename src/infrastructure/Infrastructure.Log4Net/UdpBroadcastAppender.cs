using System.Net;
using System.Net.Sockets;
using System.Text;
using log4net.Appender;
using log4net.Core;

namespace Infrastructure.Log4Net
{
    public class UdpBroadcastAppender : AppenderSkeleton
    {
        public string BroadcastAddress { get; set; }
        public string Port { get; set; }

        protected override void Append(LoggingEvent loggingEvent)
        {
            using (var client = new UdpClient())
            {
                var ip = new IPEndPoint(IPAddress.Parse(BroadcastAddress), int.Parse(Port));
                var bytes = Encoding.ASCII.GetBytes(RenderLoggingEvent(loggingEvent));
                client.Send(bytes, bytes.Length, ip);
                client.Close();
            }
        }
    }
}