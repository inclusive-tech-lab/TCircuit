using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circuit : MonoBehaviour {

    private HapticSquare haptic;
    private string currentHapticType;

    GameObject hint;
    GameObject confirmPage;

    private Sprite circuitSprite1;
    private Sprite circuitSprite2;
    private Sprite circuitSprite3;

    Sprite buttonC1Sprite;
    Sprite buttonC2Sprite;
    Sprite buttonC3Sprite;
    Sprite buttonC1ClickedSprite;
    Sprite buttonC2ClickedSprite;
    Sprite buttonC3ClickedSprite;

    //Sprite buttonC1Sprite = Resources.Load<Sprite>("Sprites/Buttons/circuit1");
    //Sprite buttonC1ClickedSprite = Resources.Load<Sprite>("Sprites/Buttons/circuit1clicked");
    //Sprite buttonC2Sprite = Resources.Load<Sprite>("Sprites/Buttons/circuit2");
    //Sprite buttonC2ClickedSprite = Resources.Load<Sprite>("Sprites/Buttons/circuit2clicked");
    //Sprite buttonC3Sprite = Resources.Load<Sprite>("Sprites/Buttons/circuit3");
    //Sprite buttonC3ClickedSprite = Resources.Load<Sprite>("Sprites/Buttons/circuit3clicked");

    Sprite buttonReadSprite;
    Sprite buttonReadClickedSprite;

    //Sprite buttonReadSprite = Resources.Load<Sprite>("Sprites/Buttons/read");
    //Sprite buttonReadClickedSprite = Resources.Load<Sprite>("Sprites/Buttons/readclicked");

    private Sprite taskSprite;

    GameObject nextButton;
    GameObject finishButton;

    GameObject buttonC1;
    GameObject buttonC2;
    GameObject buttonC3;
    GameObject sliderR;

    SpriteRenderer spriteR;

    PlayerLog thisPlayerLog;
    
    // Use this for initialization
    void Start () {

        if (GlobalVariables.formative) StartFormative();
        else StartTasks();

    }

        private void StartTasks()
    {
        thisPlayerLog = GameObject.Find("EventSystem").GetComponent<PlayerLog>();

        hint = GameObject.Find("Hint");
        hint.SetActive(false);

        confirmPage = GameObject.Find("Confirm Finish");
        confirmPage.SetActive(false);

        Debug.Log("this circuit: " + GlobalVariables.thisTask);
        haptic = GameObject.Find("HapticSquare").GetComponent<HapticSquare>();

        nextButton = GameObject.Find("Next Button");
        finishButton = GameObject.Find("Finish Button");

        buttonC1 = GameObject.Find("Circuit1 Button");
        buttonC2 = GameObject.Find("Circuit2 Button");
        buttonC3 = GameObject.Find("Circuit3 Button");

        sliderR = GameObject.Find("SliderR");

        finishButton.SetActive(false);
        buttonC1.SetActive(false);
        buttonC2.SetActive(false);
        buttonC3.SetActive(false);
        sliderR.SetActive(false);

        //set button sprites
        buttonC1Sprite = Resources.Load<Sprite>("Sprites/Buttons/circuit1");
        buttonC1ClickedSprite = Resources.Load<Sprite>("Sprites/Buttons/circuit1clicked");
        buttonC2Sprite = Resources.Load<Sprite>("Sprites/Buttons/circuit2");
        buttonC2ClickedSprite = Resources.Load<Sprite>("Sprites/Buttons/circuit2clicked");
        buttonC3Sprite = Resources.Load<Sprite>("Sprites/Buttons/circuit3");
        buttonC3ClickedSprite = Resources.Load<Sprite>("Sprites/Buttons/circuit3clicked");

        buttonReadSprite = Resources.Load<Sprite>("Sprites/Buttons/read");
        buttonReadClickedSprite = Resources.Load<Sprite>("Sprites/Buttons/readclicked");

        GlobalVariables.hintOn = false;

        switch (GlobalVariables.thisTask)
        {
            case 1:
                circuitSprite1 = Resources.Load<Sprite>("Sprites/Circuits/bg-circuit1");
                taskSprite = Resources.Load<Sprite>("Sprites/Buttons/task1");
                currentHapticType = HapticSquare.HapticType.STRIPEHIGHH; //quick solution for fixing a bug
                haptic.UpdateHaptics(currentHapticType);
                currentHapticType = HapticSquare.HapticType.STRIPEHIGHV;
                haptic.UpdateHaptics(currentHapticType);
                thisPlayerLog.AddEvent("=============================================\n"
                    + "t = " + Time.time.ToString() + ", Start Task 1"
                    + "\n=============================================");
                break;
            case 2:
                circuitSprite1 = Resources.Load<Sprite>("Sprites/Circuits/bg-circuit2");
                taskSprite = Resources.Load<Sprite>("Sprites/Buttons/task2");
                currentHapticType = HapticSquare.HapticType.STRIPEMEDH; //quick solution for fixing a bug
                haptic.UpdateHaptics(currentHapticType);
                currentHapticType = HapticSquare.HapticType.STRIPEMEDV;
                haptic.UpdateHaptics(currentHapticType);
                thisPlayerLog.AddEvent("=============================================\n"
                    + "t = " + Time.time.ToString() + ", Start Task 2"
                    + "\n=============================================");
                break;
            case 3:
                circuitSprite1 = Resources.Load<Sprite>("Sprites/Circuits/bg-circuit1");
                taskSprite = Resources.Load<Sprite>("Sprites/Buttons/task3");
                currentHapticType = HapticSquare.HapticType.STRIPEHIGHV;
                haptic.UpdateHaptics(currentHapticType);

                //activate C1 and C2 buttons
                buttonC1.SetActive(true);
                buttonC2.SetActive(true);

                thisPlayerLog.AddEvent("=============================================\n"
                    + "t = " + Time.time.ToString() + ", Start Task 3"
                    + "\n=============================================");
                break;
            case 4:
                circuitSprite1 = Resources.Load<Sprite>("Sprites/Circuits/bg-circuit1");
                taskSprite = Resources.Load<Sprite>("Sprites/Buttons/task4");
                currentHapticType = HapticSquare.HapticType.STRIPELOWH; //quick solution for fixing a bug
                haptic.UpdateHaptics(currentHapticType);
                currentHapticType = HapticSquare.HapticType.STRIPEHIGHV;
                haptic.UpdateHaptics(currentHapticType);
                //activate C1 and C2 buttons
                buttonC1.SetActive(true);
                buttonC2.SetActive(true);
                buttonC3.SetActive(true);

                thisPlayerLog.AddEvent("=============================================\n"
                    + "t = " + Time.time.ToString() + ", Start Task 4"
                    + "\n=============================================");                
                break;
            case 5:
                circuitSprite1 = Resources.Load<Sprite>("Sprites/Circuits/bg-circuit4R1");
                taskSprite = Resources.Load<Sprite>("Sprites/Buttons/task5");
                currentHapticType = HapticSquare.HapticType.STRIPEMEDV;
                haptic.UpdateHaptics(currentHapticType);

                //activate slider
                sliderR.SetActive(true);

                //deactivate the back button
                nextButton.SetActive(false);
                finishButton.SetActive(true);
                

                thisPlayerLog.AddEvent("=============================================\n"
                    + "t = " + Time.time.ToString() + ", Start Task 5"
                    + "\n=============================================");
                break;
        }

        GlobalVariables.currentHapticType = currentHapticType;

        haptic.DeactivateHaptic();

        GameObject bg = GameObject.Find("Background");
        bg.GetComponent<Image>().sprite = circuitSprite1;
        //spriteR = bg.GetComponent<SpriteRenderer>();
        //spriteR.sprite = circuitSprite1;

        GameObject taskLabel = GameObject.Find("Task Label");
        taskLabel.GetComponent<Image>().sprite = taskSprite;
        //spriteR = taskLabel.GetComponent<SpriteRenderer>();
        //spriteR.sprite = taskSprite;

    }

    private void StartFormative()
    {
        Debug.Log("start circuit.cs for formative");
        // circuits for testing different circuits with high, med, low current
        circuitSprite1 = Resources.Load<Sprite>("Sprites/Circuits/circuitA");
        circuitSprite2 = Resources.Load<Sprite>("Sprites/Circuits/circuitA2");
        circuitSprite3 = Resources.Load<Sprite>("Sprites/Circuits/circuitA3");

        haptic = GameObject.Find("HapticSquare").GetComponent<HapticSquare>();

        currentHapticType = HapticSquare.HapticType.NOISEHIGH;
        haptic.UpdateHaptics(currentHapticType);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // formative function
    public void ChangeHaptic(int option)
    {
        //thisHaptic.DeactivateHaptic();
        //haptic.DeactivateHaptic();
        //haptic.SetEnabled(false);

        switch (option)
        {
            case 1:
                currentHapticType = HapticSquare.HapticType.NOISEHIGH;
                haptic.UpdateHaptics(currentHapticType);
                //haptic.SetEnabled(true);
                Debug.Log(currentHapticType);
                //haptic.ActivateHaptic();
                break;
            case 2:
                currentHapticType = HapticSquare.HapticType.DOTS;
                haptic.UpdateHaptics(currentHapticType);
                //haptic.SetEnabled(true);
                Debug.Log(currentHapticType);
                //haptic.ActivateHaptic();
                break;
            case 3:
                currentHapticType = HapticSquare.HapticType.CHECKERS;
                haptic.UpdateHaptics(currentHapticType);
                //haptic.SetEnabled(true);
                Debug.Log(currentHapticType);
                //haptic.ActivateHaptic();
                break;
            case 4:
                currentHapticType = HapticSquare.HapticType.STRIPEHIGH;
                haptic.UpdateHaptics(currentHapticType);
                //haptic.SetEnabled(true);
                Debug.Log(currentHapticType);
                //haptic.ActivateHaptic();
                break;
        }

    }


    // formative function
    public void ChangeCircuit(int C)
    {
        //haptic.DeactivateHaptic();

        GameObject circuit = GameObject.Find("Circuit");
        SpriteRenderer circuitSpriteR = circuit.GetComponent<SpriteRenderer>();

        switch (C)
        {
            case 1:
                if (this.name == "Circuit Canvas")
                {
                    circuitSpriteR.sprite = circuitSprite1;
                    
                }
                else { circuitSpriteR.sprite = circuitSprite3; Debug.Log("testing circuit change code"); }
                haptic.UpdateHaptics(HapticSquare.HapticType.STRIPEHIGH);
                //haptic.ActivateHaptic();
                break;
            case 2:
                circuitSpriteR.sprite = circuitSprite2;
                haptic.UpdateHaptics(HapticSquare.HapticType.STRIPEMED);
                //haptic.ActivateHaptic();
                break;
            case 3:
                if (this.name == "Circuit Canvas")
                {
                    circuitSpriteR.sprite = circuitSprite3;
                    
                }
                else { circuitSpriteR.sprite = circuitSprite1; Debug.Log("testing circuit change code"); }
                haptic.UpdateHaptics(HapticSquare.HapticType.STRIPELOW);
                //haptic.ActivateHaptic();
                break;

        }
    }


    public void SwitchCircuit(int C)
    {
        GameObject circuit = GameObject.Find("Background");
        
        //SpriteRenderer circuitSpriteR = circuit.GetComponent<SpriteRenderer>();

        switch (C)
        {
            case 1:
                circuitSprite1 = Resources.Load<Sprite>("Sprites/Circuits/bg-circuit1");
                //circuitSpriteR.sprite = circuitSprite1;
                circuit.GetComponent<Image>().sprite = circuitSprite1;
                currentHapticType = HapticSquare.HapticType.STRIPEHIGHV;
                haptic.UpdateHaptics(currentHapticType);

                buttonC1.GetComponent<Image>().sprite = buttonC1ClickedSprite;
                buttonC2.GetComponent<Image>().sprite = buttonC2Sprite;
                buttonC3.GetComponent<Image>().sprite = buttonC3Sprite;

                thisPlayerLog.AddEvent("t = " + Time.time.ToString() + ", Clicked Circuit 1");
                break;
            case 2:
                circuitSprite1 = Resources.Load<Sprite>("Sprites/Circuits/bg-circuit2");
                //circuitSpriteR.sprite = circuitSprite1;
                circuit.GetComponent<Image>().sprite = circuitSprite1;
                currentHapticType = HapticSquare.HapticType.STRIPEMEDV;
                haptic.UpdateHaptics(currentHapticType);

                buttonC1.GetComponent<Image>().sprite = buttonC1Sprite;
                buttonC2.GetComponent<Image>().sprite = buttonC2ClickedSprite;
                buttonC3.GetComponent<Image>().sprite = buttonC3Sprite;

                thisPlayerLog.AddEvent("t = " + Time.time.ToString() + ", Clicked Circuit 2");
                break;
            case 3:
                circuitSprite1 = Resources.Load<Sprite>("Sprites/Circuits/bg-circuit3");
                //circuitSpriteR.sprite = circuitSprite1;
                circuit.GetComponent<Image>().sprite = circuitSprite1;
                currentHapticType = HapticSquare.HapticType.STRIPELOWV;
                haptic.UpdateHaptics(currentHapticType);

                buttonC1.GetComponent<Image>().sprite = buttonC1Sprite;
                buttonC2.GetComponent<Image>().sprite = buttonC2Sprite;
                buttonC3.GetComponent<Image>().sprite = buttonC3ClickedSprite;

                thisPlayerLog.AddEvent("t = " + Time.time.ToString() + ", Clicked Circuit 3");
                break;

        }
        GlobalVariables.currentHapticType = currentHapticType;
    }

    public void ChangeResistance()
    {
        GameObject circuit = GameObject.Find("Background");
        //SpriteRenderer circuitSpriteR = circuit.GetComponent<SpriteRenderer>();
        

        Slider mySlider = GameObject.Find("SliderR").GetComponent<UnityEngine.UI.Slider>();
        
        int R = (int) mySlider.value;
        switch (R)
        {
            case 1:
                circuitSprite1 = Resources.Load<Sprite>("Sprites/Circuits/bg-circuit4R1");
                //circuitSpriteR.sprite = circuitSprite1;
                circuit.GetComponent<Image>().sprite = circuitSprite1;
                currentHapticType = HapticSquare.HapticType.STRIPEMEDV;
                haptic.UpdateHaptics(currentHapticType);
                thisPlayerLog.AddEvent("t = " + Time.time.ToString() + ", Changed resistance to R=1");
                break;
            case 2:
                circuitSprite1 = Resources.Load<Sprite>("Sprites/Circuits/bg-circuit4R2");
                //circuitSpriteR.sprite = circuitSprite1;
                circuit.GetComponent<Image>().sprite = circuitSprite1;
                currentHapticType = HapticSquare.HapticType.STRIPELOWV;
                haptic.UpdateHaptics(currentHapticType);
                thisPlayerLog.AddEvent("t = " + Time.time.ToString() + ", Changed resistance to R=2");
                break;
        }
        GlobalVariables.currentHapticType = currentHapticType;
    }

    public void ShowHint()
    {
        GameObject readB = GameObject.Find("Hint Button");
        if (hint.activeSelf)
        {
            // do nothing

        }
        else
        {
            thisPlayerLog.AddEvent("t = " + Time.time.ToString() + ", Show Hint");
            //readB.GetComponent<Image>().sprite = buttonReadClickedSprite;
            hint.SetActive(true);
            GlobalVariables.hintOn = true;
        }

    }

    public void HideHint()
    {
        thisPlayerLog.AddEvent("t = " + Time.time.ToString() + ", Hide Hint");
        //readB.GetComponent<Image>().sprite = buttonReadSprite;
        hint.SetActive(false);
        GlobalVariables.hintOn = false;
    }

    public void ConfirmEndSession()
    {
        thisPlayerLog.AddEvent("t = " + Time.time.ToString() + ", Clicked on Finish");
        confirmPage.SetActive(true);
    }
}
