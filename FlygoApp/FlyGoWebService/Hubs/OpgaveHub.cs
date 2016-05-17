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

        public void BroadcastKorrektSvar(int roleId)
        {
            Clients.All.KorrektSvar(roleId);
        }

        public void BroadcastForsinketSvar(int roleId, TimeSpan time)
        {
            Clients.All.ForsinketSvar(roleId,time);
        }

        public void BroadcastFejlSvar(int roleId)
        {
            Clients.All.FejlSvar(roleId);
        }
    }
}