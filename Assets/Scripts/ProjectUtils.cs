using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System.Linq;
using UnityEngine.UI;

public class ProjectUtils : MonoBehaviour
{
    // Start is called before the first frame update
    public List<float> NormalizeValues(int index, List<string[]> databaseInfo)
    {
        List<float> tList = new List<float>();
        foreach (string[] info in databaseInfo)        
        {
            tList.Add(float.Parse(info[index], CultureInfo.InvariantCulture.NumberFormat));
        }
        
        float minValue = tList.Min();
        float maxValue = tList.Max();
        
        List<float> normalizedValues = new List<float>();
        foreach (var value in tList)
        {
            float tempValue = (value - minValue)/(maxValue - minValue);
            normalizedValues.Add(tempValue);
        }
        return normalizedValues;
    }
    
    public Vector2 GetMinMaxValueFromAttribute(int index, List<string[]> Infos)
    {
        Vector2 minMaxValue; // MinMaxValue[0] = Min, MinMaxValue[1] = Max
        
        int min = 99999, 
            max = 0;
        
        foreach (string[] line in Infos)
        {
            int.TryParse(line[index], out var value);
            if (min >= value)
                min = value;
            
            if (max <= value)
                max = value;
        }

        minMaxValue.x = min;
        minMaxValue.y = max;
        return minMaxValue;
    }
    
    public List<string> GetAllCategoriesFromAttribute(int index, List<string[]> Infos)
    {
        List<string> categories = new List<string>();
        foreach (string[] line in Infos)
        {
            string tempCategory = line[index];
            if (!categories.Contains(tempCategory))
            {
                categories.Add(tempCategory);
            }
        }
        return categories;
    }

    public void DropdownOptions (Dropdown reference, GameObject goManager, bool isHeightDropdown)
    {
        print("calls");
        List<string> options = new List<string>(goManager.GetComponent<DatasetReader>().GetDatabaseLabel());
        List<Type> types = new List<Type>(goManager.GetComponent<DatasetReader>().GetLabelTypes());
        
        // Remove Exceptions
        for (int i = 0; i < 4; i++)
        {
            options.RemoveAt(0);
            types.RemoveAt(0);
        }

        List<string> tempOptions = new List<string> {"- selecione -"};

        
        if (isHeightDropdown)
        {
            for (int i = 0; i < types.Count; i++)
            {
                if (types[i] != typeof(String)) { tempOptions.Add(options[i]); }
            }
        }
        else
        {
            tempOptions.AddRange(options);
        }
        // Finalize
        reference.GetComponent<Dropdown>().ClearOptions();
        reference.GetComponent<Dropdown>().AddOptions(tempOptions);
    }
}
