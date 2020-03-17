using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePOIBehavior : MonoBehaviour
{
    private GameObject _canvas;

    private void Start()
    {
        _canvas = GameObject.Find("Canvas");
    }

    // Start is called before the first frame update
    private void OnMouseOver()
    {
        if (CompareTag("poi"))
            _canvas.GetComponent<CanvasBehavior>().UpdatePoiDisplayed(name);
        

    }

    private void OnMouseExit()
    {
        if (CompareTag("poi"))
            _canvas.GetComponent<CanvasBehavior>().HidePOIInfo();
    }

    public void ColorSetter(Color newColor)
    {
        GetComponent<Renderer>().material.color = newColor;
    }

    public void SizeSetter(Vector3 newSize)
    {
        GetComponent<Transform>().localScale = newSize;
    }

    public void OnMouseDown()
    {
        if (CompareTag("poi"))
            _canvas.GetComponent<CanvasBehavior>().UpdateHeaderText(name);
    }
}
