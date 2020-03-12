using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BasicDropdownEffects : MonoBehaviour
{
    ///  Esse script é utilizado por todos os dropdowns que necessitam
    ///  mostrar todos os atributos da base. Além disso, ele precisa do
    ///  PoiManager para funcionar (onde está o script que lê e armazena
    ///  a base de dados) e o Canvas para exibir as opções no dropdown em
    ///  questão


    public GameObject poiManager;
    public Canvas mainCanvas;

    private List<Type> _newTypeList;
    List<string> _newOptions = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    {
        _newOptions = PopulateDropdown(poiManager.GetComponent<DatasetReader>().GetDatabaseLabel());
        _newTypeList = new List<Type>(poiManager.GetComponent<DatasetReader>().GetLabelTypes()) {[0] = null};
        _newTypeList.RemoveRange(1, 3);
    }
    
    // Limpa a lista de opções e insere os
    // atributos da base de dados como
    // opções para seleção do usuário
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
    
    // retorna a opção selecionada pelo usuário
    public void ReturnOptionIndex()
    {
        int index = GetComponent<Dropdown>().value;
        if (index != 0)
        {
            mainCanvas.GetComponent<CanvasBehavior>().GetDropdownOption(
                index + 3, 
                _newOptions[index], 
                _newTypeList[index]);
        }
    }
    
    
    public void ColorChanger()
    {
        int index = GetComponent<Dropdown>().value;
        if (index != 0)
        {
            poiManager.GetComponent<PoiManagerBehavior>().UpdatePoiColorByAttribute(index + 3);
            return;
        }
        poiManager.GetComponent<PoiManagerBehavior>().UpdatePoiColorByAttribute(0);
        
    }
}
