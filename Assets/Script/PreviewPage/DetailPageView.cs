using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataStruct;


namespace Hsinpa
{
    public class DetailPageView : MonoBehaviour
    {
        [Header("Consumption")]
        [SerializeField]
        private StateSlotVIew power_slot;

        [SerializeField]
        private StateSlotVIew voltage_slot;

        [SerializeField]
        private StateSlotVIew electricity_slot;

        [SerializeField]
        private StateSlotVIew pump_slot;

        public void UpdateConsumptionData(CDUSystemConsumption cduSystemConsumption)
        {
            power_slot.SetText(cduSystemConsumption.Power + "W");
            voltage_slot.SetText(cduSystemConsumption.Voltage + "V");
            electricity_slot.SetText(cduSystemConsumption.Electric_current + "A");
            pump_slot.SetText(cduSystemConsumption.Pump + "Hz");
        }
    }
}
