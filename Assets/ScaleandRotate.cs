using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScaleandRotate : MonoBehaviour
{
    public Slider ScalingScr;
    public Slider RotationScr;
    public float scaleMinValue;
    public float scaleMaxValue;
    public float rotMinValue;
    public float rotMaxValue;
    private readonly float value;



    // Start is called before the first frame update
    void Start()
    {
        // Add the listner 
        ScalingScr = GameObject.Find("Slider 11").GetComponent<Slider>();
        ScalingScr.minValue = scaleMinValue;
        ScalingScr.maxValue = scaleMaxValue;


        ScalingScr.onValueChanged.AddListener(ScaleSliderUpdate);

        RotationScr = GameObject.Find("Slider 12").GetComponent<Slider>();
        RotationScr.minValue = rotMaxValue;
        RotationScr.maxValue = rotMaxValue;

        RotationScr.onValueChanged.AddListener(RotateSliderUpdate);


    }

    private void ScaleSliderUpdate(float arg0)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void ScaleSliderUpdate()
    {
        transform.localScale = new Vector3(value, value, value);


    }
    void RotateSliderUpdate(float value)
    {
        transform.localEulerAngles = new Vector3(transform.rotation.x, value, transform.rotation.z);

    }
}
