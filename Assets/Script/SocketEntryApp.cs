using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataStruct;
using Viewsonic.Processor;
using System.Threading.Tasks;
using WebSocketSharp;

using System.IO;
using System.Net.Http;
using System;
using Hsinpa.Utility;

namespace Hsinpa
{
    public class SocketEntryApp : MonoBehaviour
    {
        

        [SerializeField]
        private HomePageView homePageView;

        [SerializeField]
        private DetailPageView detailPageView;

        private Dictionary<string, FullServerData> server_dict = new Dictionary<string, FullServerData>();
        private string test_ip = "ws://localhost:5000";

        void Start()
        {
            fetch_all();

            using (var ws = new WebSocket(test_ip))
            {
                ws.OnMessage += on_socket_message;

                ws.Connect();
            }

            detailPageView.SetCallback(back_callback: () =>
            {
                UtilityFunc.CanvasOps(homePageView.canvasGroup, true);
                UtilityFunc.CanvasOps(detailPageView.canvasGroup, false);
            });
        }

        void on_socket_message(object sender, MessageEventArgs e)
        {
            Debug.Log("on_socket_message \n" + e.Data);

            FullServerData server_data = JsonUtility.FromJson<FullServerData>(e.Data);

            if (server_dict.ContainsKey(server_data.server_ip))
            {
                server_dict[server_data.server_ip] = server_data;
            } else
            {
                server_dict.Add(server_data.server_ip, server_data);
            }

            homePageView.PushOrUpdateServer(server_data, on_homepage_server_click);
            detailPageView.UpdateData(server_data);
        }

        void on_homepage_server_click(string server_id)
        {
            Debug.Log("server_id " + server_id);

            detailPageView.SetId(server_id);

            if (server_dict.TryGetValue(server_id, out FullServerData server_data))
            {
                detailPageView.UpdateData(server_data);
                UtilityFunc.CanvasOps(homePageView.canvasGroup, false);
                UtilityFunc.CanvasOps(detailPageView.canvasGroup, true);
            }
        }



        async void fetch_all()
        {
            HttpClient sharedClient = new HttpClient();
            sharedClient.Timeout = TimeSpan.FromSeconds(3);

            try
            {
                var fetch_response = await sharedClient.GetAsync(APIStatic.FetchAllServer);

                Debug.Log("Fetch suscess " + fetch_response.IsSuccessStatusCode);

                if (fetch_response.IsSuccessStatusCode)
                {
                    var json_response = await fetch_response.Content.ReadAsStringAsync();
                    Debug.Log(json_response);

                    SingleServerData[] server_array = JsonHelper.FromJson<SingleServerData>(json_response);

                    foreach (SingleServerData ssd in server_array)
                        fetch_single_detail(ssd._id);
                }

                sharedClient.Dispose();
            }
            catch
            {
                Debug.LogError("fetch_all fail");
            }
        }

        async void fetch_single_detail(string id)
        {
            string url = string.Format(APIStatic.FetchSingleServer, id);
            Debug.Log("fetch_single_detail " + url);

            HttpClient sharedClient = new HttpClient();
            var fetch_response = await sharedClient.GetAsync(url);

            var json_response = await fetch_response.Content.ReadAsStringAsync();
            Debug.Log(json_response);

            FullServerData server_detail = JsonUtility.FromJson<FullServerData>(json_response);

            Utility.UtilityFunc.SetDictionary(server_dict, id, server_detail);

            homePageView.PushOrUpdateServer(server_detail, on_homepage_server_click);

            sharedClient.Dispose();
        }
    }
}