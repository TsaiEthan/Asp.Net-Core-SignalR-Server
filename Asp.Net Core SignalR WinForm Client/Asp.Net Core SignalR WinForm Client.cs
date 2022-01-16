using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using SetData;

namespace Asp.Net_Core_SignalR_WinForm_Client
{
	public partial class SignalRClient : Form
	{
		#region 初始化
		//新建 AppData 物件
		private AppData appdata = new AppData();
		HubConnection connection;
		int ccc = 1;

		public SignalRClient()
		{
			InitializeComponent();
			initBackgroundWorker();
		}
		private async void SignalRClient_Load(object sender, EventArgs e)
		{
			CheckStatebutton.PerformClick();
			await Task.Delay(1000);//延遲1秒
			btnConnect.PerformClick();
			CheckStatebutton.PerformClick();
		}
		private void SignalRClient_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (connection != null && connection.State == HubConnectionState.Connected)
				connection.StopAsync();
		}
		#endregion
		#region 自訂函式
		private void EstablishConnection()
		{
			connection = new HubConnectionBuilder()
				.WithUrl(appdata.ServerURL)
				.WithAutomaticReconnect(new RetryPolicy())
				.Build();
			//心跳檢查
			connection.KeepAliveInterval = TimeSpan.FromSeconds(2);
			connection.On<string, string>("SendMessageToWinForm", (name, data) =>
			{
				if (txtChatHistory.InvokeRequired)
				{
					txtChatHistory.Invoke(new Action(() => ShowMsg(ref name, ref data)));
				}
				else
				{
					ShowMsg(ref name, ref data);
				}
			});
			connection.On<string>("HeartBeatFromServer", data =>
			{
				if (data != "ServerHB")
				{
					//HeartBeatFromClient.CancelAsync();
				}
				else
				{
					txtChatHistoryInvokeRequired(false, data);
				}
			});
			/*
			connection.On<string>("Heartbeat", data =>
			{
				if (txtChatHistory.InvokeRequired)
				{
					txtChatHistory.Invoke(new Action(() =>
					{
						ShowMsg("Heartbeat from Server", data);
						ShowMsg("              WinForm Time", DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
					}));
				}
				else
				{
					ShowMsg("Heartbeat from Server", data);
					ShowMsg("              WinForm Time", DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
				}
			});
			*/
			//若重新連接中，顯示訊息
			connection.Reconnecting += error =>
			{
				//Debug.Assert(connection.State == HubConnectionState.Reconnecting);
				// Notify users the connection was lost and the client is reconnecting.
				// Start queuing or dropping messages.
				txtChatHistoryInvokeRequired(true, $"連接狀態：{connection.State}");
				return Task.CompletedTask;
			};
			//若重新連接完成，顯示訊息
			connection.Reconnected += error =>
			{
				//Debug.Assert(connection.State == HubConnectionState.Reconnecting);
				// Notify users the connection was lost and the client is reconnecting.
				// Start queuing or dropping messages.
				txtChatHistoryInvokeRequired(true, $"連接狀態：{connection.State}");
				txtChatHistoryInvokeRequired(false, $"重新連接完成，新的Connection ID：{connection.ConnectionId}");
				HeartBeatFromClient.RunWorkerAsync();
				return Task.CompletedTask;
			};
			//若斷線，執行動作
			connection.Closed += error =>
			{
				//Debug.Assert(connection.State == HubConnectionState.Reconnecting);
				// Notify users the connection was lost and the client is reconnecting.
				// Start queuing or dropping messages.
				txtChatHistoryInvokeRequired(true, $"連接狀態：{connection.State}");
				return Task.CompletedTask;
			};
			ConnectToServer(connection);
		}
		private void txtChatHistoryInvokeRequired(bool checkstate, string msg)
		{
			if (txtChatHistory.InvokeRequired)
			{
				txtChatHistory.Invoke(new Action(() =>
				{
					if (checkstate == true)
						CheckState();
					ShowMsg(msg);
				}));
			}
			else
			{
				if (checkstate == true)
					CheckState();
				ShowMsg(msg);
			}
		}
		private void CheckState()
		{
			ShowState_textBox.Clear();
			if (connection != null)
			{
				if (connection.State == HubConnectionState.Connecting)
					ShowState_textBox.ForeColor = Color.Fuchsia;
				else if (connection.State == HubConnectionState.Reconnecting)
					ShowState_textBox.ForeColor = Color.Blue;
				else if (connection.State == HubConnectionState.Connected)
					ShowState_textBox.ForeColor = Color.Green;
				else
					ShowState_textBox.ForeColor = Color.Red;
				ShowState_textBox.AppendText(connection.State.ToString());
			}
			else
			{
				ShowState_textBox.ForeColor = Color.Red;
				ShowState_textBox.AppendText("Connect Once First");
			}
		}
		private async void ConnectToServer(HubConnection connection)
		{
			try
			{
				if (connection == null || connection.State != HubConnectionState.Connected)
				{
					await connection.StartAsync();
					HeartBeatFromClient.RunWorkerAsync();
					CheckState();
					ShowMsg($"連接狀態：{connection.State}");
					ShowMsg("Connection ID ： " + connection.ConnectionId);
					ShowMsg("Connected to " + appdata.ServerURL + " Successfully");
				}
			}
			catch (Exception ex)
			{
				ShowMsg("Error while connecting the SignalR Server Message: " + ex.Message);
			}
		}
		private Task HubConnection_Closed(Exception ex)
		{
			return Task.Run(() =>
			{
				CheckState();
				ShowMsg("DisConnected Accidentally");
				ShowMsg(ex.Message);
			});
		}
		private void ShowMsg(string message)
		{
			txtChatHistory.ScrollToCaret();
			txtChatHistory.AppendText("【" + message + "】\n");
		}
		private void ShowMsg(ref string eventName, ref string message)
		{
			txtChatHistory.ScrollToCaret();
			txtChatHistory.AppendText(eventName + "：" + message + "\n");
		}
		private async void ShowMsgToEveryOne(HubConnection connection, string name, string msg)
		{
			await connection.InvokeAsync("SendToEveryOne", name, msg);
			//ShowMsg("ShowMsgToEveryOne Done");
		}
		#endregion
		#region 按鈕事件
		private void btnConnect_Click(object sender, EventArgs e)
		{
			if (connection == null || connection.State != HubConnectionState.Connected)
			{
				EstablishConnection();
			}
		}
		private async void btnDisConnect_Click(object sender, EventArgs e)
		{
			//連線中才執行
			if (connection != null && connection.State == HubConnectionState.Connected)
			{
				try
				{
					await connection.StopAsync();
					CheckState();
					ShowMsg("DisConnected Successfully");
				}
				catch (Exception ex)
				{
					ShowMsg("發生錯誤：" + ex.ToString());
				}
			}
			else
			{
				ShowMsg("連接狀態：Disconnected");
			}
		}
		private void CheckStatebutton_Click(object sender, EventArgs e)
		{
			CheckState();
			//ShowMsg("CheckState Button Click");
		}
		private void btnSend_Click(object sender, EventArgs e)
		{
			txtChatMessage.Text = "SignalR Test";
			//連線狀態下才執行送訊息
			if (connection != null && connection.State == HubConnectionState.Connected)
				try
				{
					ShowMsgToEveryOne(connection, "WinForms", txtChatMessage.Text + " " + ccc.ToString());
				}
				catch (Exception ex)
				{
					throw ex;
				}
			//未連線則顯示請連線訊息
			else
				ShowMsg("Please Connect First!!");
			ccc++;
		}
		#endregion
	}
	//實現IRetryPolicy介面
	class RetryPolicy : IRetryPolicy
	{
		/// <summary>
		/// 重連規則：重連次數<50：間隔1s;重試次數<60:間隔2s;重試次數>70:間隔3s
		/// </summary>
		/// <param name="retryContext"></param>
		/// <returns></returns>
		public TimeSpan? NextRetryDelay(RetryContext retryContext)
		{
			if (retryContext.ElapsedTime > TimeSpan.FromSeconds(90))//重試次數>90秒,取消重新連接
			{
				return null;
			}
			else if (retryContext.PreviousRetryCount < 50)//重試次數<50,間隔1s
			{
				return new TimeSpan(0, 0, 1);
			}
			else if (retryContext.PreviousRetryCount < 60)//重試次數<60:間隔2s
			{
				return new TimeSpan(0, 0, 2);
			}
			else //重試次數>70:間隔3s
			{
				return new TimeSpan(0, 0, 3);
			}
		}
	}
}
