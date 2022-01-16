using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

namespace Asp.Net_Core_SignalR_Server.Hubs
{
	public class DataHub : Hub
	{
		public async Task SendToEveryOne(string eventName, string message)
		{
			SendToServer(eventName, message);
			await SendToLineBot(eventName, message);
			await SendToWinForm(eventName, message);
			await SendToConsole(eventName, message);
		}
		public async Task HeartBeatToClient(string message)
		{
			SendToServer(Context.ConnectionId, message);
			if (message == "ClientHB")
				message = "ServerHB";
			await Clients.Client(Context.ConnectionId).SendAsync("HeartBeatFromServer", message);
		}

		public void SendToServer(string eventName, string message)
		{
			Console.WriteLine("【" + eventName + "：" + message + "】");
		}
		public async Task SendToLineBot(string eventName, string message)
		{
			await Clients.All.SendAsync("SendMessageToLineBot", eventName, message);
		}
		public async Task SendToWinForm(string eventName, string message)
		{
			await Clients.All.SendAsync("SendMessageToWinForm", eventName, message);
		}
		public async Task SendToConsole(string eventName, string message)
		{
			await Clients.All.SendAsync("SendMessageToConsole", eventName, message);
		}
		public override async Task OnConnectedAsync()
		{
			Console.WriteLine($"建立連接{Context.ConnectionId}");
			ConnectedUser.connections.Add(Context.ConnectionId);
			//await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
			await base.OnConnectedAsync();
		}
		public override async Task OnDisconnectedAsync(Exception exception)
		{
			Console.WriteLine($"斷開連接{Context.ConnectionId}");
			ConnectedUser.connections.Remove(Context.ConnectionId);
			//await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
			await base.OnDisconnectedAsync(exception);
		}
	}
	public static class ConnectedUser
	{
		public static List<string> connections = new List<string>();
	}
}