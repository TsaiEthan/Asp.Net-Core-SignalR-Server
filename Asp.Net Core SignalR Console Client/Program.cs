using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using SetData;
using System.ComponentModel;

namespace Asp.Net_Core_SignalR_Console_Client
{
    public class Program
    {
        //新建 AppData 物件
        private static AppData appdata = new AppData();
        private static HubConnection connection;
        /*
        private static BackgroundWorker HeartBeatFromClient;
        
        
        public BackgroundWorker HeartBeatFromClient
        {
            get { return _HeartBeatFromClient; }
            set { _HeartBeatFromClient = value; }
        }
        
        public HubConnection connection
        {
            get { return _connection; }
            set { _connection = value; }
        }
        

        private static void initBackgroundWorker()
        {
            HeartBeatFromClient = new BackgroundWorker();
            HeartBeatFromClient.WorkerSupportsCancellation = true;
            HeartBeatFromClient.DoWork += new DoWorkEventHandler(HeartBeatFromClient_DoWork);
        }
        //背景執行
        private static async void HeartBeatFromClient_DoWork(object sender, DoWorkEventArgs e)
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
                    //if (connection.State != HubConnectionState.Connected)
                    {
                        HeartBeatFromClient.CancelAsync();
                        Console.WriteLine("發生錯誤：" + ex.Message);
                    }
                }
            }
        }
        */
        private static async Task HeartBeatToServer()
        {
            await connection.InvokeAsync("HeartBeatToClient", "ClientHB");
        }
        public static async Task Main(string[] args)
        {
            //initBackgroundWorker();
            Console.WriteLine("Asp.net Core SignalR Client Application Start!");
            connection = new HubConnectionBuilder()
                .WithUrl(appdata.ServerURL)
                .WithAutomaticReconnect(new RetryPolicy())
                .Build();
            //心跳檢查
            connection.KeepAliveInterval = TimeSpan.FromSeconds(2);
            connection.On<string, string>("SendMessageToConsole", (name, data) =>
                OnSend(ref name, ref data)
                );
            //來自SignalR Server的心跳檢查訊息
            /*
            connection.On<string>("HeartBeatFromServer", data =>
            {
                if (data != "ServerHB")
                {
                    //HeartBeatFromClient.CancelAsync();
                }
                else
                {
                    Console.WriteLine(data);
                }
            });
            */
            //若重新連接中，顯示訊息
            connection.Reconnecting += error =>
            {
                //Debug.Assert(connection.State == HubConnectionState.Reconnecting);
                // Notify users the connection was lost and the client is reconnecting.
                // Start queuing or dropping messages.
                Console.WriteLine($"【連接狀態：{connection.State}】");
                return Task.CompletedTask;
            };
            //若重新連接完成，顯示訊息
            connection.Reconnected += error =>
            {
                //Debug.Assert(connection.State == HubConnectionState.Reconnecting);
                // Notify users the connection was lost and the client is reconnecting.
                // Start queuing or dropping messages.				
                Console.WriteLine($"【連接狀態：{connection.State}】");
                Console.WriteLine($"【重新連接完成，新的ConnectionId：{connection.ConnectionId}】");
                //HeartBeatFromClient.RunWorkerAsync();
                return Task.CompletedTask;
            };
            //若斷線，執行動作
            connection.Closed += HubConnection_Closed;
            ConnectToServer(ref connection);
            if (connection != null && connection.State == HubConnectionState.Connected)
            {
                ShowMsgToEveryOne(ref connection, "Console", "Connected to SignalR Server Successfully");
            }

waiting:
            var command = Console.ReadLine();

            if (command == "exit")
            {
                await connection.StopAsync();
                Console.WriteLine($"連接狀態：{connection.State}");
                goto waiting;
            }
            else if (command == "connect")
            {
                ConnectToServer(ref connection);
                goto waiting;
            }
            else if (command == "break")
            {
                //Console.ReadLine();// 使畫面停住
                Console.WriteLine("【按任意鍵結束......】");
                //可按任意鍵結束畫面
                Console.ReadKey();
            }
            else
            {
                if (connection != null && connection.State == HubConnectionState.Connected)
                {
                    ShowMsgToEveryOne(ref connection, "Console", command);
                }
                else
                    Console.WriteLine("【Connection State = Disconnected】");
                goto waiting;
            }
        }

        private static void ShowMsgToEveryOne(ref HubConnection connection, string name, string msg)
        {
            connection.InvokeAsync("SendToEveryOne", name, msg);
        }

        private static void ConnectToServer(ref HubConnection connection)
        {
            try
            {
                if (connection == null || connection.State != HubConnectionState.Connected)
                {
                    connection.StartAsync().Wait();
                    Console.WriteLine($"【連接狀態：{connection.State}】");
                    Console.WriteLine("【Connection ID ： " + connection.ConnectionId + "】");
                    Console.WriteLine("【Connected to " + appdata.ServerURL + " Successfully】");
                    //HeartBeatFromClient.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("【Error while connecting the SignalR Server Message: " + ex.Message + "】");
            }
        }
        private static Task HubConnection_Closed(Exception arg)
        {
            return Task.Run(() =>
            {
                Console.WriteLine("【DisConnected Successfully】");
                //Console.WriteLine(arg.Message);
            });
        }
        private static void OnSend(ref string eventName, ref string message)
        {
            Console.WriteLine(eventName + "：" + message);
        }
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
