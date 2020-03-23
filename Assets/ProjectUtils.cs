using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System.Linq;

public class ProjectUtils : MonoBehaviour
{
    // Start is called before the first frame update
    public List<float> NormalizeValues(int index, List<string[]> DatabaseInfo)
    {
        List<float> tList = new List<float>();
        foreach (string[] info in DatabaseInfo)        
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
    
}
