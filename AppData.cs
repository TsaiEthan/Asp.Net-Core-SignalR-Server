using System;

namespace SetData
{
    public class AppData
    {
        //設定SignalR Server網址
        private string _ServerURL = "https://aspnetcoresignalrserver20220110.azurewebsites.net/myDataHub";
        //private string _ServerURL = "http://127.0.0.1:5000/myDataHub";
        //private string _ServerURL = "https://127.0.0.1:5001/myDataHub";
        //private string _ServerURL = "http://localhost:5000/myDataHub";

        public string ServerURL
        {
            get { return _ServerURL; }
        }
    }
}
