using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private Transform _selection;

    // Start is called before the first frame 

    // Update is called once per frame
    private void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;

        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                var selectionRenderer = hit.transform.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                    
                }
                _selection = selection;
                GetComponent<LeanPinchScale>().enable = true;
                GetComponent<LeanDragTranslate>().enable = true;
                GetComponent<LeanTwistRotateAxis>().enable = true;
            }
            
            //GetComponent<ScaleandRotate>().enable = true;


        }

    }
}
