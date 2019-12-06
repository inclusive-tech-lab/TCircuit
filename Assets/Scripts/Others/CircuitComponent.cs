using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CircuitComponent : MonoBehaviour {

    Sprite mySprite;
    SpriteRenderer spriteR;
    int brightness;

    // Use this for initialization
    void Start () {

        //TestFunction();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        Text myText = GameObject.Find("Text1").GetComponent<UnityEngine.UI.Text>();
        myText.text = "R = " + GlobalVariables.RGlobal;
        brightness = 5 - GlobalVariables.RGlobal;
        mySprite = Resources.Load<Sprite>("Sprites/Components/bulb" + brightness);
        if (this.name == "Bulb") spriteR.sprite = mySprite;

    }
	
	// Update is called once per frame
	void Update () {   
        
    }

    public void TestFunction()
    {
        GameObject test = GameObject.Find("CircuitComponent");
        
        GameObject go = Instantiate(test);
        test.transform.position += new Vector3(1.0f, 1.0f, 0.0f);
        //go.SetActive(true);
    }

    public void NextScene()
    {
        GlobalVariables.thisCircuit = 2;
        SceneManager.LoadScene("Circuit2");
        
    }

    public void PrevScene()
    {
        GlobalVariables.thisCircuit = 1;
        SceneManager.LoadScene("Circuit1");
    }


}
