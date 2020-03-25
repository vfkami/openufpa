using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePOIBehavior : MonoBehaviour
{
    // Esse script contem alguns comportamentos que os objetos instanciados
    // terão na cena ao serem instanciados
    
    private GameObject _canvas;

    private void Start()
    {
        _canvas = GameObject.Find("Canvas");
    }

    private void OnMouseOver()
    { 
        _canvas.GetComponent<CanvasBehavior>().UpdatePoiDisplayed(name, transform.parent);
    }

    private void OnMouseExit()
    { 
        _canvas.GetComponent<CanvasBehavior>().HidePoiInfo();
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
