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
        newTypeList = POIManager.GetComponent<DatasetReader>().GetLabelTypes();
        
        newTypeList[0] = null;
        newTypeList.RemoveRange(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    List<String> PopulateDropdown (List<string> options) {
        GetComponent<Dropdown>().ClearOptions();
        
        // Add Default Button
        options.Insert(0, "- selecione -");
        
        // Remove Exceptions
        options.Remove("sigla");
        options.Remove("lat");
        options.Remove("long");
        options.Remove("nome");
        
        // Finalize
        GetComponent<Dropdown>().AddOptions(options);
        return options;
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
