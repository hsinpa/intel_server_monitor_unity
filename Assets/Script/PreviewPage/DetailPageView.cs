using Hsinpa.Utility;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using static DataStruct;


namespace Hsinpa
{
    public class DetailPageView : MonoBehaviour
    {
        public CanvasGroup canvasGroup;

        [Header("Left Panel")]
        [Header("Status")]
        [SerializeField]
        private StateSlotVIew stop_status;

        [SerializeField]
        private StateSlotVIew auto_status;

        [SerializeField]
        private StateSlotVIew manual_status;

        [SerializeField]
        private StateSlotVIew error_status;

        [Header("Consumption")]
        [SerializeField]
        private StateSlotVIew power_slot;

        [SerializeField]
        private StateSlotVIew voltage_slot;

        [SerializeField]
        private StateSlotVIew electricity_slot;


        [Header("CPU Temp")]
        [SerializeField]
        private TextMeshProUGUI CPU_0_Temp;

        [SerializeField]
        private TextMeshProUGUI CPU_0_Power;

        [SerializeField]
        private TextMeshProUGUI CPU_1_Temp;

        [SerializeField]
        private TextMeshProUGUI CPU_1_Power;

        [Header("Details")]
        [SerializeField]
        private TextMeshProUGUI TH_Text;

        [SerializeField]
        private TextMeshProUGUI TL_Text;

        [SerializeField]
        private TextMeshProUGUI LS_Text;

        [SerializeField]
        private TextMeshProUGUI Ti_Text;

        [SerializeField]
        private TextMeshProUGUI To_Text;

        [SerializeField]
        private TextMeshProUGUI Pi_Text;

        [SerializeField]
        private TextMeshProUGUI Wi_Text;

        [SerializeField]
        private TextMeshProUGUI Wo_Text;

        [SerializeField]
        private TextMeshProUGUI PUMPw_Text;

        [Header("Error")]
        [SerializeField]
        private TextMeshProUGUI ErrorCode;

        [Header("Right Panel")]
        [Header("Server 3D")]
        [SerializeField]
        private TextMeshProUGUI device_name;

        [SerializeField]
        private TextMeshProUGUI device_id;

        [SerializeField]
        private Button back_btn;

        private string _server_ip;

        public void SetCallback(System.Action back_callback)
        {
            UtilityFunc.SetSimpleBtnEvent(back_btn, back_callback);
        }

        public void SetId(string server_ip)
        {
            _server_ip = server_ip;
        }

        public void UpdateData(FullServerData cduSystemConsumption)
        {
            if (cduSystemConsumption == null || cduSystemConsumption.server_ip != _server_ip) return;

            device_name.text = cduSystemConsumption.device_name;
            device_id.text = cduSystemConsumption.device_id;

            ErrorCode.text = cduSystemConsumption.error_code + "";

            stop_status.SetHighlight(cduSystemConsumption.status.stop);
            auto_status.SetHighlight(cduSystemConsumption.status.auto);
            manual_status.SetHighlight(cduSystemConsumption.status.manual);
            error_status.SetHighlight(cduSystemConsumption.status.error);

            power_slot.SetText(cduSystemConsumption.power + "W");
            voltage_slot.SetText(cduSystemConsumption.voltage + "V");
            electricity_slot.SetText(cduSystemConsumption.electric_current + "A");

            CPU_0_Temp.text = cduSystemConsumption.cpu_info.CPU0_Vcore_Temp;
            CPU_0_Power.text = cduSystemConsumption.cpu_info.CPU0_Vcore_Pwr;
            CPU_1_Temp.text = cduSystemConsumption.cpu_info.CPU1_Vcore_Temp;
            CPU_1_Power.text = cduSystemConsumption.cpu_info.CPU1_Vcore_Pwr;

            TH_Text.text = cduSystemConsumption.temperature1_high + "";
            TL_Text.text = cduSystemConsumption.temperature1_low + "";
            Ti_Text.text = cduSystemConsumption.temperature2_in + "";
            To_Text.text = cduSystemConsumption.temperature2_out + "";
            Wi_Text.text = cduSystemConsumption.temperature_w_in + "";
            Wo_Text.text = cduSystemConsumption.temperature_w_out + "";
            PUMPw_Text.text = (cduSystemConsumption.pump + "Hz");
        }
    }
}
