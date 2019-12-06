using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using System;

public class Button : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {
    }

    // formative version
    public void NextScene()
    {
        GlobalVariables.thisCircuit += 1;

        switch (GlobalVariables.thisCircuit)

        {
            case 2:
                SceneManager.LoadScene("CircuitA Options");
                break;
            case 3:
                SceneManager.LoadScene("CircuitA Strength");
                break;
            case 4:
                SceneManager.LoadScene("CircuitA Strength V2");
                break;
            case 5:
                SceneManager.LoadScene("CircuitA Texture");
                break;
        }
    }

    //formative version
    public void PrevScene()
    {
        GlobalVariables.thisCircuit -= 1;

        switch (GlobalVariables.thisCircuit)

        {
            case 1:
                SceneManager.LoadScene("CircuitA");
                break;
            case 2:
                SceneManager.LoadScene("CircuitA Options");
                break;
            case 3:
                SceneManager.LoadScene("CircuitA Strength");
                break;
            case 4:
                SceneManager.LoadScene("CircuitA Strength V2");
                break;
        }
    }

    //main version
    public void StartSession()
    {
        //Get the family ID from the input field
        //InputField txt_Input = GameObject.Find("StartInputField").GetComponent<InputField>();
        //if (txt_Input != null) GlobalVariables.familyID = txt_Input.text;

        //Create a new log file
        PlayerLog thisPlayerLog = GameObject.Find("EventSystem").GetComponent<PlayerLog>();
        thisPlayerLog.CreateLogFile();

        //GlobalVariables.interactionLog = new StringBuilder(DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + " Start logging\n");
        //GlobalVariables.interactionLog.AppendLine("Family:" + GlobalVariables.familyID);


        //Load the first task
        SceneManager.LoadScene("Task Question");
    }


    public void EndSession()
    {
        //Create a new log file
        PlayerLog thisPlayerLog = GameObject.Find("EventSystem").GetComponent<PlayerLog>();
        thisPlayerLog.EndLog();
        SceneManager.LoadScene("StartEnd");
        Debug.Log("end session");
    }

    //main version
    public void NextPage(string thisPage)
    {
        //Debug.Log(GlobalVariables.thisCircuit);
        


        if (thisPage == "question")
        {
            SceneManager.LoadScene("Task Explore");
        }
        else
        {
            GlobalVariables.thisTask += 1;
            SceneManager.LoadScene("Task Question");
        }

    }

    //main version
    public void PrevPage(string thisPage)
    {
        //Debug.Log(GlobalVariables.thisCircuit);



        if (thisPage == "question")
        {
            GlobalVariables.thisTask -= 1;
            SceneManager.LoadScene("Task Explore");
        }
        else
        {            
            SceneManager.LoadScene("Task Question");
        }

    }


}
