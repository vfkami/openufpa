using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePOIBehavior : MonoBehaviour
{
    private GameObject _canvas;
    
    // Start is called before the first frame update
    private void OnMouseOver()
    {
        _canvas = GameObject.Find("Canvas");
        _canvas.GetComponent<CanvasBehavior>().UpdatePoiDisplayed(name);
    }

    private void OnMouseExit()
    {
        _canvas = GameObject.Find("Canvas");
        _canvas.GetComponent<CanvasBehavior>().HidePOIInfo();
    }

    public void ColorSetter(Color newColor)
    {
        GetComponent<Renderer>().material.color = newColor;
    }

    public void HeightSetter(Vector3 newSize)
    {
        GetComponent<Transform>().localScale = newSize;
    }

    public void OnMouseDown()
    {
        _canvas.GetComponent<CanvasBehavior>().UpdateHeaderText(name);
    }
}
