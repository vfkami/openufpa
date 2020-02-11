using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class CheckGroupEffects : MonoBehaviour
{
    public GameObject[] toggleList;

    // verificar se há a necessidade de transformar isso em uma classe
    int _id;
    string _label;

    public void setGroupBasics(int index, string attributeName)
    {
        _id = index;
        _label = attributeName;
    }
    
    public void UpdateCheckBoxes(List<string> categories)
    {
        for(int i = 0; i < toggleList.Length; i++)
        {
            if (i < categories.Count)
            {
                toggleList[i].SetActive(true);
                toggleList[i].GetComponentInChildren<Text>().text = categories[i];
            }
            else
                toggleList[i].Destroy();
        }
    }
    
    public int getID() {return _id;}
}
