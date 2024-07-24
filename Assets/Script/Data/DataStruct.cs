using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStruct
{

    public class FullServerData
    {
        public FullServerData() {
            CDUSystemConsumption = new CDUSystemConsumption();
            CDUInfo = new CDUInfo();
        }

        public CDUSystemConsumption CDUSystemConsumption;
        public CDUInfo CDUInfo;
    }

    public class CDUInfo
    {

    }

    public class CDUSystemConsumption
    {
        public int Voltage;
        public int Electric_current;
        public int Power;
        public int Pump;
    }

}
