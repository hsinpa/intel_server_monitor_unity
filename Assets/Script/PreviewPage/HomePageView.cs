using Hsinpa.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataStruct;
using Hsinpa.Utility;
using UnityEngine.UI;

public class HomePageView : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    [SerializeField]
    private Transform server_container;

    [SerializeField]
    private ServerDeviceView server_view_prefab;

    [SerializeField]
    private Button quit_button;

    private Dictionary<string, ServerDeviceView> serverViewDict = new Dictionary<string, ServerDeviceView>();

    public void Start()
    {
        UtilityFunc.ClearChildObject(server_container);

        UtilityFunc.SetSimpleBtnEvent(quit_button, () =>
        {
            Application.Quit();
        });
    }

    public void PushOrUpdateServer(FullServerData fullServerData, System.Action<string> device_view_callback)
    {

        try
        {
            if (serverViewDict.ContainsKey(fullServerData.device_id))
            {
                serverViewDict[fullServerData.device_id].Setup(fullServerData._id,
                    fullServerData.device_name, fullServerData.device_id, fullServerData.server_ip,
                                                                (int)fullServerData.temperature_w_in, device_view_callback);
            }
            else
            {
                ServerDeviceView new_device_view = UtilityFunc.CreateObjectToParent<ServerDeviceView>(server_container, server_view_prefab.gameObject);
                new_device_view.Setup(fullServerData._id, fullServerData.device_name, fullServerData.device_id, fullServerData.server_ip, (int)fullServerData.temperature_w_in, device_view_callback);

                serverViewDict.Add(fullServerData.device_id, new_device_view);
            }

        } catch(System.Exception err) { Debug.Log(err.Message); }

    }
}
