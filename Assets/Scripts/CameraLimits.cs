using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class CameraLimits : MonoBehaviour
{
    public GameObject controladorAltura;
    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
        
        if (!Input.GetMouseButton(0)) return;
 
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);
 
        transform.Translate(move, Space.World);
        var position = transform.position;
        position = new Vector3(
            Mathf.Clamp(position.x, -60, 90),
            Mathf.Clamp(position.y, 60, 150),
            Mathf.Clamp(position.z, -100, 130));
        
        transform.position = position;
        transform.localPosition = new Vector3(position.x, controladorAltura.GetComponent<Slider>().value, position.z);
    }
    
    

    
}