using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Toggle = UnityEngine.UI.Toggle;

public class CheckGroupEffects : MonoBehaviour
{
    public GameObject[] toggleList;
    private List<GameObject> finalToggleList = new List<GameObject>();
    int _id;
    string _label;

    public void setGroupBasics(int index, string attributeName)
    {
        _id = index;
        _label = attributeName;
    }
    
    public void UpdateCheckBoxes(List<string> categories)
    {
        //print(toggleList.Length);
        for(int i = 0; i < toggleList.Length; i++)
        {
            if (i < categories.Count)
            {
                toggleList[i].SetActive(true);
                toggleList[i].GetComponentInChildren<Text>().text = categories[i];
                finalToggleList.Add(toggleList[i]);
            }
            else
            {
                toggleList[i].Destroy();
            }
        }
    }
    
    public void OnValueChanged()
    {
        GameObject poiManager = GameObject.Find("POIManager");
        List<String> parameters = new List<string>();
        
        foreach (GameObject toggle in finalToggleList)
        {
            if (!toggle.GetComponent<Toggle>().isOn)
            { //se toggle desativado = adiciona a lista de parametros para deixarem de ser exibidos
                parameters.Add(toggle.GetComponentInChildren<Text>().text);
            }
        }
        poiManager.GetComponent<PoiManagerBehavior>().UpdateVisualization(_id, parameters, false);
    }

    
    public int getID() {return _id;}
}
