using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicDropdownEffects : MonoBehaviour
{
    public GameObject POIManager;
    public Canvas MainCanvas;

    private List<Type> newTypeList;
    List<string> newOptions = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    {
        newOptions = PopulateDropdown(POIManager.GetComponent<DatasetReader>().GetDatabaseLabel());
        newTypeList = new List<Type>(POIManager.GetComponent<DatasetReader>().GetLabelTypes()) {[0] = null};

        newTypeList.RemoveRange(1, 3);
    }
    
    List<String> PopulateDropdown (List<string> options) {
        GetComponent<Dropdown>().ClearOptions();
        
        List<string> tempOptions = new List<string>(options);
        
        // Remove Exceptions
        for (int i = 0; i <= 3; i++)
            tempOptions.RemoveAt(0);
        
        
        //Add Default Item
        tempOptions.Insert(0, "- selecione -");

        
        // Finalize
        GetComponent<Dropdown>().AddOptions(tempOptions);
        
        return tempOptions;
    }
    
    public void ReturnOptionIndex()
    {
        int index = GetComponent<Dropdown>().value;
        if (index != 0)
        {
            MainCanvas.GetComponent<CanvasBehavior>().DropdownOptionReturn(
                index + 3, 
                newOptions[index], 
                newTypeList[index]);
        }
    }
}
