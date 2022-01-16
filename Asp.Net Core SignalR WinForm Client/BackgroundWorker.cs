using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Asp.Net_Core_SignalR_WinForm_Client
{
    public partial class SignalRClient
    {
        private BackgroundWorker HeartBeatFromClient;

        private void initBackgroundWorker()
        {
            HeartBeatFromClient = new BackgroundWorker();
            HeartBeatFromClient.WorkerSupportsCancellation = true;
            HeartBeatFromClient.DoWork += new DoWorkEventHandler(HeartBeatFromClient_DoWork);
        }
        //背景執行
        private async void HeartBeatFromClient_DoWork(object sender, DoWorkEventArgs e)
        {
            while (connection.State == HubConnectionState.Connected)
            {
                await Task.Delay(2000);//延遲2秒
                try
                {
                    await HeartBeatToServer();
                }
                catch (Exception ex)
                {
                    if (connection.State == HubConnectionState.Connected)
                    {
                        HeartBeatFromClient.CancelAsync();
                        txtChatHistoryInvokeRequired(false, "發生錯誤：" + ex.Message);
                    }
                }

                /*
                await HeartBeatToServer().ContinueWith(async t =>
                {
                    if (!t.IsFaulted)
                        txtChatHistoryInvokeRequired(false, "Hello");
                    else
                    {
                        HeartBeatFromClient.CancelAsync();
                        await connection.StopAsync();
                        txtChatHistoryInvokeRequired(false, "發生錯誤：" + t);
                        EstablishConnection();
                    }
                });
                */
            }
        }
        private async Task HeartBeatToServer()
		{
			await connection.InvokeAsync("HeartBeatToClient", "ClientHB");
		}
    }
}
