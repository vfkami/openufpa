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
}
