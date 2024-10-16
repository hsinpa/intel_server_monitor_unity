using Hsinpa.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        private Hsinpa.UIStyle.UIStylesheet stop_status;

        [SerializeField]
        private Hsinpa.UIStyle.UIStylesheet auto_status;

        [SerializeField]
        private Hsinpa.UIStyle.UIStylesheet manual_status;

        [SerializeField]
        private Hsinpa.UIStyle.UIStylesheet error_status;

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
        private PreviewItemHandler preview_ui;

        [SerializeField]
        private TextMeshProUGUI device_name;

        [SerializeField]
        private TextMeshProUGUI device_id;

        [SerializeField]
        private Image overheat;

        [SerializeField]
        private Button back_btn;

        private string _server_ip;

        public void SetCallback(System.Action back_callback)
        {
            UtilityFunc.SetSimpleBtnEvent(back_btn, () => {

                back_callback?.Invoke();
                preview_ui.ResetRotation();
            });
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

            ErrorCode.text = "錯誤碼 " + cduSystemConsumption.error_code + "";

            stop_status.SetCharacteristic(cduSystemConsumption.status.stop ? 1 : 0);
            auto_status.SetCharacteristic(cduSystemConsumption.status.auto ? 1 : 0);
            manual_status.SetCharacteristic(cduSystemConsumption.status.manual ? 1 : 0);
            error_status.SetCharacteristic(cduSystemConsumption.status.error ? 1 : 0);

            power_slot.SetText("CDU功率 " + Math.Round(cduSystemConsumption.power / 10f, 1) + "W");
            voltage_slot.SetText("CDU電壓 " + Math.Round(cduSystemConsumption.voltage / 10f, 1) + "V");
            electricity_slot.SetText("CDU電流 " + cduSystemConsumption.electric_current + "A");

            CPU_0_Temp.text = $"晶片溫度 {cduSystemConsumption.cpu_info.CPU0_Vcore_Temp}C";
            CPU_0_Power.text = $"耗能 {cduSystemConsumption.cpu_info.CPU0_Vcore_Pwr}W";
            CPU_1_Temp.text = $"晶片溫度 {cduSystemConsumption.cpu_info.CPU1_Vcore_Temp}C";
            CPU_1_Power.text = $"耗能 {cduSystemConsumption.cpu_info.CPU1_Vcore_Pwr}W";

            TH_Text.text = "TH " + Math.Round(cduSystemConsumption.temperature1_high / 10f, 1) + "C";
            TL_Text.text = "TL " + Math.Round(cduSystemConsumption.temperature1_low / 10f, 1) + "C";
            Ti_Text.text = "Ti " + Math.Round(cduSystemConsumption.temperature2_in / 10f, 1) + "C";
            To_Text.text = "To " + Math.Round(cduSystemConsumption.temperature2_out / 10f, 1)+ "C";
            Wi_Text.text = "Wi " + Math.Round(cduSystemConsumption.temperature_w_in / 10f, 1) + "C";
            Wo_Text.text = "Wo "+ Math.Round(cduSystemConsumption.temperature_w_out/10, 1) + "C";

            LS_Text.text = "液位高度 " + cduSystemConsumption.level + "mm";
            PUMPw_Text.text = "泵轉速 " + Math.Round(cduSystemConsumption.pump/100f, 0) + "Hz";
            Pi_Text.text = "泵出口壓力 " + Math.Round(cduSystemConsumption.pressure/100f, 2) + " bar";

            // overheat.enabled = float.Parse(cduSystemConsumption.cpu_info.CPU0_Vcore_Temp) >= 70;
        }

        private float str_to_num(string n_string, float divident, int round_to_digit) {
            if (float.TryParse(n_string, out float parse_float)) {
                return (float) Math.Round(parse_float/divident, round_to_digit);
            }

            return 0;  
        }
    }
}
