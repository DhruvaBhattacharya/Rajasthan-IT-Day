using UnityEngine;
using Lean.Touch;
public class DragObjects : MonoBehaviour
{
    public Camera myCam;

    private float startXPos;
    private float startYPos;

    private bool isThisGOselected = false;

    private void Start()
    {
        myCam = Camera.main;
    }
    private void Update()
    {
        if (isThisGOselected)
        {
            SelectGameobject();
        }

        CompareslectedModel();
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;

        if (!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);

        startXPos = mousePos.x - transform.localPosition.x;
        startYPos = mousePos.y - transform.localPosition.y;

        isThisGOselected = true;
    }

    private void OnMouseUp()
    {
        isThisGOselected = false;
    }

    public void SelectGameobject()
    {
        Vector3 mousePos = Input.mousePosition;

        if (!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);
        //transform.localPosition = new Vector3(mousePos.x - startXPos, mousePos.y - startYPos, transform.localPosition.z);
        ARTapToPlaceObject.SelectedModel = transform.gameObject;
        Debug.Log(transform.gameObject.name);
    }

    void CompareslectedModel()
    {
        GameObject selectM = ARTapToPlaceObject.SelectedModel;
        if (selectM != this)
        {
            GetComponent<LeanPinchScale>().enable = false;
            GetComponent<LeanDragTranslate>().enable = false;
            GetComponent<LeanTwistRotateAxis>().enable = false;
        }
    }
}