using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace CINotifier.Hubs
{
    [HubName("builds")]
    public class BuildsHub : Hub
    {
    }
}