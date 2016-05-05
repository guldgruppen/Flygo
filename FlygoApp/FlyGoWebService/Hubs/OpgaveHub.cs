using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlyGoWebService.Models;
using Microsoft.AspNet.SignalR;

namespace FlyGoWebService.Hubs
{
    public class OpgaveHub : Hub
    {
        public void BroadcastOpgave(FlyRute rute)
        {
            Clients.All.Broadcast(rute);
        }

        public void BroadcastSvar(int roleId)
        {
            Clients.All.Svar(roleId);
        }
    }
}