using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class PlayerLog : MonoBehaviour {

    //string filePath;

    //StringBuilder interactionLog;

	// Use this for initialization
	void Start () {

    }



    public void CreateLogFile()
    {
        //GlobalVariables.filePath = Application.dataPath + "/../InteractionLogs/session-" + DateTime.Now.ToString("yyyy-MM-dd--h-mm-tt") + ".txt";
        GlobalVariables.filePath = Application.persistentDataPath + "/session-" + DateTime.Now.ToString("yyyy-MM-dd--h-mm-tt") + ".txt";

        if (!File.Exists(GlobalVariables.filePath))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(GlobalVariables.filePath))
            {
                sw.WriteLine("***** Interaction Log File *****");
                sw.WriteLine("Date and time: " + DateTime.Now.ToString());
                if (GlobalVariables.hapticOn) sw.WriteLine("Condition: Haptics");
                else sw.WriteLine("Condition: Control");
            }
        }


    }

    public void AddTouchEvent(string logInfo, Vector3 pos)
    {
        using (StreamWriter sw = File.AppendText(GlobalVariables.filePath))
        {
            sw.WriteLine("t = " + Time.time.ToString() + ", " + logInfo + ", " + 
                "x = " + pos.x + ", " + "y = " + pos.y);

        }
    }
    public void AddEvent(string logInfo)
    {
        using (StreamWriter sw = File.AppendText(GlobalVariables.filePath))
        {
            sw.WriteLine(logInfo);
            //sw.WriteLine("t = " + Time.time.ToString() + ", " + logInfo);
 
        }

    }

    // not using it now
    public void SaveLogData()
    {
        //File.WriteAllText(GlobalVariables.filePath, GlobalVariables.interactionLog.ToString());

    }
    public void EndLog()
    {
        using (StreamWriter sw = File.AppendText(GlobalVariables.filePath))
        {
            sw.WriteLine("t = " + Time.time.ToString() + ", End logging data");

        }

    }
    public static void Log(string logMessage, TextWriter w)
    {
        w.Write("\r\nLog Entry : ");
        w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToLongDateString());
        w.WriteLine("  :");
        w.WriteLine("  :{0}", logMessage);
        w.WriteLine("-------------------------------");
    }
}
