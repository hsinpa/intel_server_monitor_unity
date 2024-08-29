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
using Unity.VisualScripting;

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
        private WebSocket webSocket;

        private string threadServerDataID = null;
        private int incremental_id = 1;

        void Start()
        {
            fetch_all();

            webSocket = new WebSocket(test_ip);

            webSocket.OnMessage += on_socket_message;

            webSocket.OnOpen += (sender, obj) =>
                {
                    Debug.Log("Websocket OPEN");
                };

            webSocket.Connect();


            detailPageView.SetCallback(back_callback: () =>
            {
                UtilityFunc.CanvasOps(homePageView.canvasGroup, true);
                UtilityFunc.CanvasOps(detailPageView.canvasGroup, false);
            });

            for (int i = 1; i <= 5; i++)
            {
                string test_text = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "single_data_" + i +".json"));
                process_single_detail(test_text);
            }
        }

        private void Update()
        {
            if (threadServerDataID != null) {
                fetch_single_detail(threadServerDataID);
                threadServerDataID = null;
            } 
        }

        void on_socket_message(object sender, MessageEventArgs e)
        {
            Debug.Log("on_socket_message \n" + e.Data);

            try
            {
                string server_id = SimpleJSON.JSON.Parse(e.Data)["_id"].Value;
                threadServerDataID = server_id;
            }
            catch (Exception err)
            {
                Debug.LogError("on_socket_message \n" + err.Message);

            }


            //FullServerData server_data = JsonUtility.FromJson<FullServerData>(e.Data);

            //if (server_dict.ContainsKey(server_data.server_ip))
            //{
            //    server_dict[server_data.server_ip] = server_data;
            //} else
            //{
            //    server_dict.Add(server_data.server_ip, server_data);
            //}

            //homePageView.PushOrUpdateServer(server_data, on_homepage_server_click);
            //detailPageView.UpdateData(server_data);
        }

        void on_homepage_server_click(string server_id)
        {
            Debug.Log("server_id " + server_id);

            if (server_dict.TryGetValue(server_id, out FullServerData server_data))
            {
                detailPageView.SetId(server_data.server_ip);

                detailPageView.UpdateData(server_data);
                UtilityFunc.CanvasOps(homePageView.canvasGroup, false);
                UtilityFunc.CanvasOps(detailPageView.canvasGroup, true);
            }
        }



        async void fetch_all()
        {
            HttpClient sharedClient = new HttpClient();
            sharedClient.Timeout = TimeSpan.FromSeconds(3);
            //try
            //{
            var fetch_response = await sharedClient.GetAsync(APIStatic.FetchAllServer);

            Debug.Log("Fetch suscess " + fetch_response.IsSuccessStatusCode);

            if (fetch_response.IsSuccessStatusCode)
            {
                var json_response = await fetch_response.Content.ReadAsStringAsync();
                Debug.Log(json_response);

                process_fetch_all(json_response);
            }

            sharedClient.Dispose();
            //}
            //catch(Exception e)
            //{
            //    Debug.LogError(e.Message);
            //    Debug.LogError("fetch_all fail");
            //}
        }

        void process_fetch_all(string raw_fetch_all_text)
        {
            var simple_json = SimpleJSON.JSON.Parse(raw_fetch_all_text).AsArray;
            for (int i = 0; i < simple_json.Count; i++)
            {
                Debug.Log(simple_json[i]["_id"].Value);
                fetch_single_detail(simple_json[i]["_id"].Value);

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

            //FullServerData server_detail = JsonUtility.FromJson<FullServerData>(json_response);

            //server_dict = Utility.UtilityFunc.SetDictionary(server_dict, id, server_detail);

            //homePageView.PushOrUpdateServer(server_detail, on_homepage_server_click);

            process_single_detail(json_response);

            sharedClient.Dispose();
        }

        void process_single_detail(string fetch_single_text)
        {
            FullServerData server_detail = JsonUtility.FromJson<FullServerData>(fetch_single_text);

            server_detail.device_name = "Intel 4U 1P-0"+incremental_id;
            server_dict = Utility.UtilityFunc.SetDictionary(server_dict, server_detail._id, server_detail);;

            homePageView.PushOrUpdateServer(server_detail, on_homepage_server_click);

            detailPageView.SetId(server_detail.server_ip);
            detailPageView.UpdateData(server_detail);

            incremental_id++;
        }

        private void OnDestroy()
        {
            if (webSocket != null)
            {
                webSocket = null;
            }
        }
    }
}