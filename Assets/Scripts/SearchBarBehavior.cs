using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchBarBehavior : MonoBehaviour
{
    public Text search;
    public InputField searchbar;
    public Canvas mainCanvas;

    public Text AAAAAAAA;
    
    public Font usedFont;
    
    private string[] termos =
    {
        "reitoria", "restaurante", "instituto", "biblioteca",
        "hangarzinho", "veropesinho", "restaurante II", "biblioteca II",
        "auditorio", "lanchonete", "terminal", "centro"
    };
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        string digitado = search.text;
        if (digitado.Length >= 3) //Quantidade minima de termos para iniciar a busca
        {
            int contador = 1;
            foreach (string palavra in termos)
            {
                //print(palavra + " " + digitado);
                if (palavra.Contains(digitado))
                {
                    //Debug.LogError(contador + " " + palavra);
                    contador++;

                    if (Input.GetKey(KeyCode.Return))
                    {
                        //Debug.LogAssertion("enter apertadoooo");
                        updateSearchBox(palavra);
                    }
                }
            }
        }
    }

    void updateSearchBox(string text)
    {
        print(text);
        print(searchbar.GetComponentInChildren<Text>().text);
        searchbar.GetComponentInChildren<Text>().text = text; 
    }
    
}
