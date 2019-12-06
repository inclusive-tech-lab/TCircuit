using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InsideComponent : MonoBehaviour {
    public int spriteVersion = 1;
    public SpriteRenderer spriteR;
    //private Sprite[] sprites;
    public List<Sprite> sprites = new List<Sprite>();
    public Sprite mySprite;
    public int oldR;

    // Use this for initialization
    void Start () {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        mySprite = Resources.Load<Sprite>("Sprites/Insides/R0Text");
        sprites.Add(mySprite);
        mySprite = Resources.Load<Sprite>("Sprites/Insides/R1Text");
        sprites.Add(mySprite);
        mySprite = Resources.Load<Sprite>("Sprites/Insides/R2Text");
        sprites.Add(mySprite);
        mySprite = Resources.Load<Sprite>("Sprites/Insides/R3Text");
        sprites.Add(mySprite);
        //sprites = Resources.LoadAll<Sprite>(spriteNames);
        spriteR.sprite = sprites[GlobalVariables.RGlobal - 1];
        Slider mySlider = GameObject.Find("SliderR").GetComponent<UnityEngine.UI.Slider>();
        mySlider.value = GlobalVariables.RGlobal;

        // activate haptics
        //GameObject haptic = GameObject.Find("HapticMesh1");
        //haptic.SetActive(true);


        //haptic = GameObject.Find("HapticMesh2");
        //haptic.SetActive(true);
        //haptic = GameObject.Find("HapticMesh3");
        //haptic.SetActive(true);
        //haptic = GameObject.Find("HapticMesh4");
        //haptic.SetActive(true);
        /*
        // set haptics
        GameObject haptic;
        for (int i = 0; i < 4; i++)
        {
            haptic = this.transform.GetChild(i).gameObject;
            haptic.SetActive(true);
        }
        haptic = this.transform.GetChild(GlobalVariables.RGlobal - 1).gameObject;
        haptic.SetActive(true);
        */
    }
	
	// Update is called once per frame
	void Update () {
        //spriteR.sprite = sprites[spriteVersion];


    }

    public void UpdateSprite(float R)
    {
        Debug.Log("R value is: " + R);
        spriteVersion = (int) R;
        if (spriteVersion > 4)
            spriteVersion = 1;
        if (sprites.Count != 0) spriteR.sprite = sprites[spriteVersion - 1];

        // Update the haptics: activating the corresponding texture (mesh)
        //Change this code later, not a good idea to call the child by index
        //GameObject haptic;
        //haptic = this.transform.GetChild(GlobalVariables.RGlobal - 1).gameObject;
        //haptic.SetActive(false);
        //haptic = this.transform.GetChild(spriteVersion - 1).gameObject;
        ////haptic.GetComponent<HapticSetting>().InitHaptics2();
        //haptic.SetActive(true);

        GlobalVariables.RGlobal = spriteVersion;

    }

    public void BacktoCircuitScene()
    {
        //GameObject haptic = this.transform.GetChild(GlobalVariables.RGlobal - 1).gameObject;
        // haptic.SetActive(false);
        GameObject haptic;
        for (int i = 0; i < 4; i++)
        {
            haptic = this.transform.GetChild(i).gameObject;
            haptic.SetActive(false);
        }
        SceneManager.LoadScene("Circuit"+ GlobalVariables.thisCircuit);
         
    }


}
