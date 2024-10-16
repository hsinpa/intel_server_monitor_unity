using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ServerDeviceView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Transform heat_tag;

    [SerializeField]
    private TextMeshProUGUI server_name;

    [SerializeField]
    private TextMeshProUGUI server_code;

    private string _id;

    private System.Action<string> _device_view_callback;

    public void OnPointerClick(PointerEventData eventData)
    {
        _device_view_callback?.Invoke(_id);
    }

    public void Setup(string _id,  string p_server_name, string server_serial_code, string server_ip, int temperature, System.Action<string> device_view_callback)
    {
        server_name.text = p_server_name;
        server_code.text = server_serial_code;
        // heat_tag.gameObject.SetActive(temperature >= 70);

        this._id = _id;
        this._device_view_callback = device_view_callback;
    }
}
