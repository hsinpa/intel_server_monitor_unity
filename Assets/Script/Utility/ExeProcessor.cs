using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace Viewsonic.Processor {
    public class ExeProcessor
    {
        private Process _process;
        private string _process_name;

        public bool ProcessUpAndRunning => this._process != null;

        public bool Start(string exe_path, string process_name, string argument, DataReceivedEventHandler callback)
        {

#if UNITY_STANDALONE_WIN
            bool fileExist = File.Exists(exe_path);

            if (ProcessUpAndRunning) {
                Kill();
            }

            if (!fileExist) return false;

            _process_name = process_name;

            try
            {
                this._process = new Process();
                _process.StartInfo.FileName = exe_path;

                if (argument != null)
                    _process.StartInfo.Arguments = argument;

                _process.StartInfo.UseShellExecute = false;
                _process.StartInfo.RedirectStandardOutput = true;
                _process.StartInfo.CreateNoWindow = true;

                if (callback != null)
                    _process.OutputDataReceived += new DataReceivedEventHandler(callback);

                _process.Start();
                _process.BeginOutputReadLine();
                _process.WaitForExit();
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError(e.Message);
            }

            return fileExist && ProcessUpAndRunning;

#endif

            return false;
        }

        public void Kill() {

#if UNITY_STANDALONE_WIN
            if (ProcessUpAndRunning)
            {
                //UnityEngine.Debug.Log("Process Killed");

                var process_array = Process.GetProcessesByName(_process_name);

                foreach (var p in process_array)
                    p.Kill();

                this._process = null;
            }
#endif
        }
    }
}
