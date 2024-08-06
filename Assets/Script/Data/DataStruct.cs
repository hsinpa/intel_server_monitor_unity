using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStruct
{

    public class SingleServerData
    {
        public string _id;
        public string device_id;
        public string device_name;
        public string server_ip;
    }

    public class FullServerData
    {
        public string _id;

        public string cdu_ip;
        public string server_ip;
        public string device_name;
        public string device_id;
        public string timestamp;

        public float pressure;
        public int level;

        public float temperature1_high;
        public float temperature1_low;
        public float temperature2_in;
        public float temperature2_out;
        public float temperature_w_in;
        public float temperature_w_out;

        public int voltage;
        public int electric_current;
        public int power;
        public int pump;

        public int error_code;

        public Status status;
        public CDUInfo cpu_info;

        public FullServerData() {
            status = new Status();
            cpu_info = new CDUInfo();
        }
    }

    public class CDUInfo
    {
        public string CPU0_Vcore_Temp;
        public string CPU1_Vcore_Temp;
        public string CPU0_Vcore_Pwr;
        public string CPU1_Vcore_Pwr;
    }

    public class Status
    {
        public bool stop;
        public bool auto;
        public bool manual;
        public bool error;
    }
}
