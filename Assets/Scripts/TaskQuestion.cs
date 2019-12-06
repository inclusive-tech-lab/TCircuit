using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskQuestion : MonoBehaviour {

    private Sprite taskSprite;
    SpriteRenderer taskSpriteR;

    PlayerLog thisPlayerLog;

    // Use this for initialization
    void Start () {

        thisPlayerLog = GameObject.Find("EventSystem").GetComponent<PlayerLog>();

        GameObject backButton = GameObject.Find("Back Button");

        switch (GlobalVariables.thisTask)
        {
            case 1:
                taskSprite = Resources.Load<Sprite>("Sprites/Tasks/task1Q");
                thisPlayerLog.AddEvent("=============================================\n"
                    + "t = " + Time.time.ToString() + ", Start Question 1"
                    + "\n=============================================");
                
                //deactivate the back button
                backButton.SetActive(false);
                break;
            case 2:
                taskSprite = Resources.Load<Sprite>("Sprites/Tasks/task2Q");
                thisPlayerLog.AddEvent("=============================================\n"
                    + "t = " + Time.time.ToString() + ", Start Question 2"
                    + "\n=============================================");
                break;
            case 3:
                taskSprite = Resources.Load<Sprite>("Sprites/Tasks/task3Q");
                thisPlayerLog.AddEvent("=============================================\n"
                    + "t = " + Time.time.ToString() + ", Start Question 3"
                    + "\n=============================================");
                
                break;
            case 4:
                taskSprite = Resources.Load<Sprite>("Sprites/Tasks/task4Q");
                thisPlayerLog.AddEvent("=============================================\n"
                    + "t = " + Time.time.ToString() + ", Start Question 4"
                    + "\n=============================================");
                break;
            case 5:
                taskSprite = Resources.Load<Sprite>("Sprites/Tasks/task5Q");
                thisPlayerLog.AddEvent("=============================================\n"
                    + "t = " + Time.time.ToString() + ", Start Question 5"
                    + "\n=============================================");
                break;
        }

        GameObject taskQ = GameObject.Find("Task Question");
        taskSpriteR = taskQ.GetComponent<SpriteRenderer>();
        taskSpriteR.sprite = taskSprite;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
