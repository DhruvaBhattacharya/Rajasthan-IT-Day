using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Lean.Touch;

public class ARTapToPlaceObject : MonoBehaviour
{
    /*[SerializeField]
    public GameObject t1;
    public GameObject t2;
    public GameObject t3;
    public GameObject t4;*/

    //public GameObject img;
    public bool enable;
    // private Vector3 mOffset;
    // private float mZCoord;

    //public GameObject UIArrows;
    public GameObject placementIndicator;
    //public GameObject floor;
    //public GameObject player;
    //public GameObject objectToPlace;
    public GameObject DefaultModel;
    public Transform ModelParent;
    //public GameObject spawnedObject;

    /*[Header("Buttons")]
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;
*/
    //private ARSessionOrigin arOrigin;
    private ARRaycastManager arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private float initialDistance;

    private Vector3 initialScale;
    private GameObject spawnedOject;

    private bool isButtonSelected = false;
    private bool isdeletebuttonselected = false;
    private Vector3 mousePosWorld;


    public static GameObject SelectedModel;
    public static bool isGOselected;
    //private object arObjectToSpawn;

    /* private void OnMouseDown()
     {
         mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
         mOffset = gameObject.transform.position - GetMouseWorldPos();

     }*/

    /*private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }*/

    /*private void OnMouseDrag()
    {
        transform.position = mousePosWorld + mOffset;
    }*/

   /* public void DestroyGameObject()
    {
        Destroy(t1);
        Destroy(t2);
        Destroy(t3);
        Destroy(t4);
    }*/

    void Start()
    {

        arOrigin = FindObjectOfType<ARRaycastManager>();
        spawnedOject = DefaultModel;
    }


    void Update()
    {
        CheckModelSelected();
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && isButtonSelected)
            {
                PlaceObject();
                Debug.Log("UI is Clicked");
            }

        }

        if (Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
            {
                return;
            }
            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = spawnedOject.transform.localScale;
                Debug.Log("Initial Distance: " + initialDistance + "GameObject Name:"
                    + spawnedOject.name);

            }

            else
            {
                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                if (Mathf.Approximately(initialDistance, 0))
                {
                    return;

                }
                var factor = currentDistance / initialDistance;
                spawnedOject.transform.localScale = initialScale * factor;
            }
        }
    }



    private void PlaceObject()
    {
        GameObject Spawn = Instantiate(spawnedOject, placementPose.position, placementPose.rotation);
        isButtonSelected = false;
        Spawn.transform.SetParent(ModelParent);
        //AllButtonInteractable();
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    public void ChangeModel(GameObject modelPrefab)
    {
        spawnedOject = modelPrefab;
        isButtonSelected = true;
        isGOselected = false;

    }

   

    /*void AllButtonInteractable()
    {
        Button1.interactable = true;
        Button2.interactable = true;
        Button3.interactable = true;
        Button4.interactable = true;
    }*/

    void CheckModelSelected()
    {
        if (SelectedModel != null)
        {
            SelectedModel.GetComponent<LeanPinchScale>().enabled = true;
            SelectedModel.GetComponent<LeanDragTranslate>().enabled = true;
            SelectedModel.GetComponent<LeanTwistRotateAxis>().enabled = true;
            /*img.gameObject.SetActive(true);*/
        }
    }

    public void selectedModelisNone()
    {
        if (SelectedModel != null)
        {
            SelectedModel.GetComponent<LeanPinchScale>().enabled = false;
            SelectedModel.GetComponent<LeanDragTranslate>().enabled = false;
            SelectedModel.GetComponent<LeanTwistRotateAxis>().enabled = false;
           // img.gameObject.SetActive(false);
        }
        isGOselected = false;
        SelectedModel = null;
    }

    public void DeleteGO()
    {
        if (SelectedModel != null)
        {
            Destroy(SelectedModel);
        }
    }
}