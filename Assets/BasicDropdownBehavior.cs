using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicDropdownBehavior : MonoBehaviour
{
    private GameObject POIManager;

    public void onValueChanged(int value)
    {
        //1 = largura
        //0 = altura
        POIManager = GameObject.Find("POIManager");
        int index = GetComponent<Dropdown>().value;
        string label = GetComponent<Dropdown>().options[index].text;

        List<string> databaseLabels = new List<string>(POIManager.GetComponent<DatasetReader>().GetDatabaseLabel());

        for (int i = 0; i < databaseLabels.Count; i++)
        {
            if (databaseLabels[i] == label)
            {
                index = i;
                break;
            }
        }

        if (value == 1)
        {
            POIManager.GetComponent<PoiManagerBehavior>().updateLargura(index, label);
        }
        else if (value == 0)
        {
            POIManager.GetComponent<PoiManagerBehavior>().updateAltura(index, label);
        }
    }


}
