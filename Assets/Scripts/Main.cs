using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public static class GlobalVariables
{
    public static int thisCircuit = 1; //not really using it
    public static int thisTask = 1;
    public static int RGlobal = 1;
    public static bool formative = false; // this is for the code for the formative study
    public static bool hapticOn = true;

    public static string currentHapticType;
    public static bool hintOn = false;

    // logging data
    //public static StringBuilder interactionLog;
    public static string filePath;
    public static string familyID;
}

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject thank = GameObject.Find("ThankYou");

        if (GlobalVariables.thisTask == 5)
        {
            thank.SetActive(true);
            GameObject startB = GameObject.Find("Start Button");
            startB.SetActive(false);
        }
        else
        {
            thank.SetActive(false);
        }
            
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
