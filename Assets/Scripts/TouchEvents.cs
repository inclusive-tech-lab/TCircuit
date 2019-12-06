using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class TouchEvents : MonoBehaviour {
    private HapticSquare haptic;
    private GameObject touched;
    private Vector3 touchPosWorld; //for the hitInformation object
    private Vector3 touchPosWorldPrev;

    bool touchMoveStarted = false;

    PlayerLog thisPlayerLog;

    // Use this for initialization
    void Start ()
    {
        haptic = GameObject.Find("HapticSquare").GetComponent<HapticSquare>();

        thisPlayerLog = GameObject.Find("EventSystem").GetComponent<PlayerLog>();
    }

    // Update is called once per frame
    void Update()
    {

        // TOUCH BEGIN//

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //transform the touch position into world space from screen space and store it.
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            //raycast with this information. If we have hit something we can process it.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);


            if (hitInformation.collider != null)
            {
                if (hitInformation.collider.name == "Close")
                {
                    GameObject.Find("circuit-outlineS1").GetComponent<Circuit>().HideHint();
                }
                else if (hitInformation.collider.name == "Finish")
                {
                    GameObject.Find("Finish Button").GetComponent<Button>().EndSession();
                }
                else if (hitInformation.collider.name == "Go Back")
                {
                    thisPlayerLog.AddEvent("t = " + Time.time.ToString() + ", Go back to exhibit");

                    GameObject.Find("Confirm Finish").SetActive(false);
                }
                else if (hitInformation.collider.name.StartsWith("Zone"))
                {
                    if (!GlobalVariables.hintOn) thisPlayerLog.AddTouchEvent("Touch Down", touchPosWorld);
                    Debug.Log("touch down circuit");
                }

            }

            touchPosWorldPrev = touchPosWorld;            

            //GlobalVariables.interactionLog.AppendLine(Time.time.ToString() + " touch down");
        }

        // TOUCH MOVE //

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            //transform the touch position into world space from screen space and store it.
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            //raycast with this information. If we have hit something we can process it.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);


            if (hitInformation.collider != null)
            {
                if (!touchMoveStarted)
                {
                    if (!GlobalVariables.hintOn) thisPlayerLog.AddTouchEvent("Touch Move Start ", touchPosWorld);
                    touchMoveStarted = true;
                }
                
                //We should have hit something with a 2D Physics collider, find if the circuit outline is touched
                string thisZone = hitInformation.transform.gameObject.tag;
                string thisZoneName = hitInformation.transform.gameObject.name;

                //determine what texture to feel, if any.
                FlowCurrent(thisZone, thisZoneName);
            }
            else
            {                
                haptic.DeactivateHaptic();
                //Debug.Log("moving out of the outline!");
                if (touchMoveStarted)
                {
                    if (!GlobalVariables.hintOn) thisPlayerLog.AddTouchEvent("Touch Move End ", touchPosWorld);
                    touchMoveStarted = false;

                }
            }
            touchPosWorldPrev = touchPosWorld;
        }

        // TOUCH END //
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            //transform the touch position into world space from screen space and store it.
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            //raycast with this information. If we have hit something we can process it.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hitInformation.collider != null && hitInformation.collider.name.StartsWith("Zone"))
            {
                if (!GlobalVariables.hintOn)
                {
                    if (touchMoveStarted) thisPlayerLog.AddTouchEvent("Touch Move End ", touchPosWorld);
                    thisPlayerLog.AddTouchEvent("Touch Up", touchPosWorldPrev);
                }                
                touchMoveStarted = false;
                //Debug.Log("touch up circuit");
            }

            haptic.DeactivateHaptic();
        }
    }

    private void FlowCurrent(string dir, string zoneName)
    {
        string newCurrentHapticType;
        String thisFeel = FeelStripe(touchPosWorldPrev, touchPosWorld, dir);
        if (thisFeel.StartsWith("vertical"))
        {
            if (GlobalVariables.hapticOn) haptic.ActivateHaptic();
            newCurrentHapticType = GlobalVariables.currentHapticType;
            haptic.UpdateHaptics(newCurrentHapticType);
            if (!GlobalVariables.hintOn)
            {
                if (thisFeel.EndsWith("clockwise")) thisPlayerLog.AddTouchEvent("Touch Move Clockwise " + zoneName + ", " + dir, touchPosWorld);
                else thisPlayerLog.AddTouchEvent("Touch Move Counter-Clockwise " + zoneName + ", " + dir, touchPosWorld);
            }
        }
        else if (thisFeel.StartsWith("horizontal"))
        {
            if (GlobalVariables.hapticOn) haptic.ActivateHaptic();
            newCurrentHapticType = GlobalVariables.currentHapticType + "-horizontal";
            haptic.UpdateHaptics(newCurrentHapticType);
            if (!GlobalVariables.hintOn)
            {
                if (thisFeel.EndsWith("clockwise")) thisPlayerLog.AddTouchEvent("Touch Move Clockwise " + zoneName + ", " + dir, touchPosWorld);
                else thisPlayerLog.AddTouchEvent("Touch Move Counter-Clockwise " + zoneName + ", " + dir, touchPosWorld);
            }
        }
        else //don't feel any texture
        {
            if (!GlobalVariables.formative) haptic.DeactivateHaptic();
            else if (GlobalVariables.hapticOn) haptic.ActivateHaptic();
            if (!GlobalVariables.hintOn) thisPlayerLog.AddTouchEvent("Error Touch Move Clockwise " + zoneName + ", " + dir, touchPosWorld);
            //Debug.Log("moving opposite direction!");
        }

    }

    private string FeelStripe(Vector3 v1, Vector3 v2, string direction)
    {
        switch (direction)
        {
            case "Left":
                if (v1.x > v2.x) return "vertical";
                else return "vertical clockwise";
            case "Right":
                if (v1.x < v2.x) return "vertical";
                else return "vertical clockwise";
            case "Down":
                if (v1.y > v2.y) return "horizontal";
                else return "horizontal clockwise";
            case "Up":
                if (v1.y < v2.y) return "horizontal";
                else return "horizontal clockwise";
            case "Left Down":
                if (Math.Abs(v1.x - v2.x) >= Math.Abs(v1.y - v2.y))
                {
                    if (v1.x > v2.x) return "vertical";
                    else return "vertical clockwise";
                }
                else
                {
                    if (v1.y > v2.y) return "horizontal";
                    else return "horizontal clockwise";
                }
            //if (v1.x > v2.x) return "vertical";
            //else if (v1.y > v2.y) return "horizontal";
            //else return "none";
            case "Down Right":
                if (Math.Abs(v1.x - v2.x) >= Math.Abs(v1.y - v2.y))
                {
                    if (v1.x < v2.x) return "vertical";
                    else return "vertical clockwise";
                }
                else
                {
                    if (v1.y > v2.y) return "horizontal";
                    else return "horizontal clockwise";
                }
            //if (v1.y > v2.y) return "horizontal";
            //else if (v1.x < v2.x) return "vertical";
            //else return "none";
            case "Right Up":
                if (Math.Abs(v1.x - v2.x) >= Math.Abs(v1.y - v2.y))
                {
                    if (v1.x < v2.x) return "vertical";
                    else return "vertical clockwise";
                }
                else
                {
                    if (v1.y < v2.y) return "horizontal";
                    else return "horizontal clockwise";
                }
            //if (v1.x < v2.x) return "vertical";
            //else if (v1.y < v2.y) return "horizontal";
            //else return "none";
            case "Up Left":
                if (Math.Abs(v1.x - v2.x) >= Math.Abs(v1.y - v2.y))
                {
                    if (v1.x > v2.x) return "vertical";
                    else return "vertical clockwise";
                }
                else
                {
                    if (v1.y < v2.y) return "horizontal";
                    else return "horizontal clockwise";
                }
            //if (v1.y < v2.y) return "horizontal";
            //else if (v1.x > v2.x) return "vertical";
            //else return "none";
            default:
                return "none";


        }
    }


    private void FlowCurrentDirectional(string dir, string zoneName)
    {
        /*
        // NON-DIRECTIONAL TEXTURES //
        if (Feel(touchPosWorldPrev, touchPosWorld, dir))
        {
            if (GlobalVariables.hapticOn) haptic.ActivateHaptic();
          //haptic.UpdateHaptics(HapticSquare.HapticType.STRIPEHIGH);
            thisPlayerLog.AddTouchEvent("Touch Move counter-clockwise " + dir + ", " + zoneName , touchPosWorld);
            Debug.Log("moving " + dir);
        }
        else
        {
            if (!GlobalVariables.formative) haptic.DeactivateHaptic();
            else if (GlobalVariables.hapticOn) haptic.ActivateHaptic();
           //haptic.UpdateHaptics(HapticSquare.HapticType.DOTS);
            thisPlayerLog.AddTouchEvent("Touch Move clockwise " + dir + ", " + zoneName, touchPosWorld);
            Debug.Log("moving opposite direction!");
        }
        */

        // DIRECTIONAL TEXTURES, E.G., STRIPES //
        string newCurrentHapticType;
        if (FeelStripe(touchPosWorldPrev, touchPosWorld, dir) == "vertical")
        {
            if (GlobalVariables.hapticOn) haptic.ActivateHaptic();
            newCurrentHapticType = GlobalVariables.currentHapticType;
            Debug.Log(newCurrentHapticType);
            haptic.UpdateHaptics(newCurrentHapticType);
            if (!GlobalVariables.hintOn) thisPlayerLog.AddTouchEvent("Touch Move Counter-Clockwise " + zoneName + ", " + dir, touchPosWorld);
            //Debug.Log("moving " + dir);
        }
        else if (FeelStripe(touchPosWorldPrev, touchPosWorld, dir) == "horizontal")
        {
            if (GlobalVariables.hapticOn) haptic.ActivateHaptic();
            newCurrentHapticType = GlobalVariables.currentHapticType + "-horizontal";
            Debug.Log(newCurrentHapticType);
            haptic.UpdateHaptics(newCurrentHapticType);
            if (!GlobalVariables.hintOn) thisPlayerLog.AddTouchEvent("Touch Move Counter-Clockwise " + zoneName + ", " + dir, touchPosWorld);
            //Debug.Log("moving " + dir);
        }
        else //don't feel any texture
        {
            if (!GlobalVariables.formative) haptic.DeactivateHaptic();
            else if (GlobalVariables.hapticOn) haptic.ActivateHaptic();
            if (!GlobalVariables.hintOn) thisPlayerLog.AddTouchEvent("Touch Move Clockwise " + zoneName + ", " + dir, touchPosWorld);
            //Debug.Log("moving opposite direction!");
        }

    }


    private string FeelStripeDirectional(Vector3 v1, Vector3 v2, string direction)
    {
        switch (direction)
        {
            case "Left":
                if (v1.x > v2.x) return "vertical";
                else return "none";
            case "Right":
                if (v1.x < v2.x) return "vertical";
                else return "none";
            case "Down":
                if (v1.y > v2.y) return "horizontal";
                else return "none";
            case "Up":
                if (v1.y < v2.y) return "horizontal";
                else return "none";
            case "Left Down":
                if (v1.x > v2.x) return "vertical";
                else if (v1.y > v2.y) return "horizontal";
                else return "none";
            case "Down Right":
                if (v1.y > v2.y) return "horizontal";
                else if (v1.x < v2.x) return "vertical";
                else return "none";
            case "Right Up":
                if (v1.x < v2.x) return "vertical";
                else if (v1.y < v2.y) return "horizontal";
                else return "none";
            case "Up Left":
                if (v1.y < v2.y) return "horizontal";
                else if (v1.x > v2.x) return "vertical";
                else return "none";
            default:
                return "none";
            

        }
    }

    private bool Feel(Vector3 v1, Vector3 v2, string direction)
    {
        if (direction == "left")
        {
            if (v1.x > v2.x) return true;
            else return false;
        }
        else if (direction == "right")
        {
            if (v1.x < v2.x) return true;
            else return false;
        }
        else if (direction == "down")
        {
            if (v1.y > v2.y) return true;
            else return false;
        }
        else if (direction == "up")
        {
            if (v1.y < v2.y) return true;
            else return false;
        }
        else return false;
    }
}
