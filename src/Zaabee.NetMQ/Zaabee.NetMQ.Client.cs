using NetMQ;
using NetMQ.Sockets;

namespace Zaabee.NetMQ
{
    public partial class ZaabeeNetMqClient
    {
        public void Test()
        {
            using (var rep1 = new ResponseSocket("@tcp://*:5001"))
            using (var rep2 = new ResponseSocket("@tcp://*:5002"))
            using (var poller = new NetMQPoller {rep1, rep2})
            {
                // these event will be raised by the Poller
                rep1.ReceiveReady += (s, a) =>
                {
                    // receive won't block as a message is ready
                    var msg = a.Socket.ReceiveFrameString();
                    // send a response
                    a.Socket.SendFrame("Response");
                };
                rep2.ReceiveReady += (s, a) =>
                {
                    // receive won't block as a message is ready
                    var msg = a.Socket.ReceiveFrameString();
                    // send a response
                    a.Socket.SendFrame("Response");
                };
                // start polling (on this thread)
                poller.Run();
                foreach (var o in poller)
                    if (o is INetMQSocket s)
                        s.Dispose();
            }
        }
    }
}