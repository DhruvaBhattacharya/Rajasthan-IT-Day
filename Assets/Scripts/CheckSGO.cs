using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using UnityEngine.UI;
public class CheckSGO : MonoBehaviour
{
    //private Image img;

    void Start()
    {
        //image = gameObject; //This works if you attach THIS script to the actual image.
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localPosition = new Vector3(transform.localPosition.x, 0.0f, transform.localPosition.z);
        if (!ARTapToPlaceObject.isGOselected || ARTapToPlaceObject.SelectedModel != gameObject)
        { 
            gameObject.GetComponent<LeanPinchScale>().enabled = false;
            gameObject.GetComponent<LeanTwistRotateAxis>().enabled = false;
            gameObject.GetComponent<LeanDragTranslate>().enabled = false;
/*            img.gameObject.SetActive(false);*/
        }

    }
}
