using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private GameObject _infoBackground;
    private GameObject _infoText;
    
    // Start is called before the first frame update
    void Start()
    {
        _infoBackground = GameObject.Find("InfoBackground");
        _infoText = GameObject.Find("InfoText");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().localPosition = Vector3.zero;
        
        Vector3 pos = Input.mousePosition;

        pos.x += 80;
        pos.y += 70;

        GetComponent<RectTransform>().position = pos;
        
        Rect rect = _infoText.GetComponent<RectTransform>().rect;
        RectTransform rt = _infoBackground.GetComponent (typeof (RectTransform)) as RectTransform;

        rt.sizeDelta = new Vector2((rect.x * -1) * 2 + 10, (rect.y * -1) * 2 + 10);
    }
}
