using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Viewsonic.Processor;
using static DataStruct;
using Utiilty;

namespace Hsinpa
{
    public class MainEntryApp : MonoBehaviour
    {
    //    [SerializeField]
    //    private DetailPageView detailPageView;
    //    private ExeProcessor _exeProcessor;
    //    private Dictionary<string, FullServerData> server_dict = new Dictionary<string, FullServerData>();
    //    private string test_ip = "127.0.0.1";

    //    void Start()
    //    {
    //        _exeProcessor = new ExeProcessor();
    //        server_dict.Add(test_ip, new FullServerData());

    //        Task.Run(() =>
    //        {
    //            _exeProcessor.Start(
    //"C:\\Users\\hsinp\\AppData\\Local\\Programs\\Python\\Python312\\python.exe", "PY Test",
    //"D:\\CodeStation\\PythonProject\\intel_test_python\\test_python.py", OnExeCallback);
    //        });
    //    }

    //    private void OnExeCallback(object sender, System.Diagnostics.DataReceivedEventArgs e)
    //    {
    //        Debug.Log(e.Data);

    //        ProcessSystemConsumption(test_ip, e.Data);
    //    }

    //    private void ProcessSystemConsumption(string ip_key, string raw_text)
    //    {
    //        if (!server_dict.ContainsKey(ip_key)) return;
    //        FullServerData fullServerData = server_dict[ip_key];

    //        AssignClassValue(raw_text, "Voltage", (int p_value) => fullServerData.CDUSystemConsumption.Voltage = p_value);
    //        AssignClassValue(raw_text, "Electric_current", (int p_value) => fullServerData.CDUSystemConsumption.Electric_current = p_value);
    //        AssignClassValue(raw_text, "Power", (int p_value) => fullServerData.CDUSystemConsumption.Power = p_value);
    //        AssignClassValue(raw_text, "Pump", (int p_value) => fullServerData.CDUSystemConsumption.Pump = p_value);

    //        detailPageView.UpdateConsumptionData(fullServerData.CDUSystemConsumption);
    //    }

    //    private void AssignClassValue(string raw_text, string target_string, System.Action<int> callback)
    //    {
    //        if (raw_text.Contains(target_string))
    //        {
    //            var parse_text = TextCaptUtility.Capture_Bracket_Char(raw_text);
    //            if (parse_text != null && int.TryParse(parse_text, out int parse_int))
    //            {
    //                callback(parse_int);
    //            }
    //        }
    //    }

    //    private void OnDestroy()
    //    {
    //        _exeProcessor.Kill();
    //    }
    }
}
