using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Toggle = UnityEngine.UI.Toggle;

public class CheckGroupEffects : MonoBehaviour
{
    // Esse script dita as regras de cada CheckBox Group para funcionar
    // como regulador do filtro categórico para cada atributo na base
    
    public GameObject[] toggleList;
    
    private List<GameObject> _finalToggleList = new List<GameObject>();
    private int _id;
    private string _label;
    
    // Renomeia o texto dos checkbox para o nome de cada categoria recebida da base de dados 
    public void UpdateCheckBoxes(List<string> categories)
    {
        //print(toggleList.Length);
        for(int i = 0; i < toggleList.Length; i++)
        {
            if (i < categories.Count)
            {
                toggleList[i].SetActive(true);
                toggleList[i].GetComponentInChildren<Text>().text = categories[i];
                _finalToggleList.Add(toggleList[i]);
            }
            else { toggleList[i].Destroy(); }
        }
    }
    
    // chamado em tempo de execução, no Editor, por cada checkbox, quando o seu valor muda.
    // Essa função passa como parâmetro os valores definidos no slider para a função
    // responsável por armazenar a lista de filtros aplicados.
    
    public void OnValueChanged()
    {
        GameObject poiManager = GameObject.Find("POIManager");
        List<String> parameters = new List<string>();
        
        foreach (GameObject toggle in _finalToggleList)
        {
            bool isOff = !toggle.GetComponent<Toggle>().isOn;
            if (isOff){ parameters.Add(toggle.GetComponentInChildren<Text>().text); }
            //se toggle desativado = adiciona a lista de parametros para deixarem de ser exibido
        }
        
        poiManager.GetComponent<PoiManagerBehavior>().UpdateFilterList(_id, parameters);
    }

    // Armazena algumas informações fundamentais
    // para o Groupbox quando ele for instanciado
    public void SetGroupBasics(int index, string attributeName)
    {
        _id = index;
        _label = attributeName;
    }
    
    public int GetId() {return _id;}
}
