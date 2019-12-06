using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TouchEventsVoltage : MonoBehaviour {
    bool chargeIsTapped;
    GameObject touchedCharge;
    Vector3 touchPosWorld;
    Vector3 offset;
     

    // Use this for initialization
    void Start () {
        chargeIsTapped = false;
    }

    // Update is called once per frame
    void Update() {

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
                //We should have hit something with a 2D Physics collider!
                //find if the object that is touched is a charge
                if (hitInformation.transform.gameObject.tag == "Charge")
                {
                    touchedCharge = hitInformation.transform.gameObject;
                    offset = touchPosWorld - touchedCharge.transform.position;
                    chargeIsTapped = true;
                }
            }

            
        }

        // TOUCH MOVE //

        if (Input.touchCount > 0 && chargeIsTapped && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of the finger since last frame
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector3 newChargePosition = touchPosWorld - offset;
            touchedCharge.transform.position = new Vector3(newChargePosition.x, newChargePosition.y, newChargePosition.z);
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

        }

        // TOUCH END //
        if (Input.touchCount > 0 && chargeIsTapped && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            chargeIsTapped = false; //end the touch

        }
    }
}
