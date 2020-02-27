using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class DatasetReader : MonoBehaviour
{
    /*
     - A base de dados a ser utilizada deve respeitar o seguinte padrão:
     - (Id, Latitude, Longitude, Descrição, {...})
     - A Id deve conter até 10 digitos e é a maneira pela qual o poi será referenciado 
     - A descrição será a maneira pela qual o poi será apresentado ao usuário
    */
    
    [SerializeField] private string datasetPath;

    private List<string[]> _poiDataset = new List<string[]>();
    private List<string> _datasetLabel = new List<string>();
    private List<Type> _labelTypes = new List<Type>();

    List<string> _tempLine = new List<string>();
    
    // Start is called before the first frame update
    void Awake()
    {
        ReadTextFile();
    }
    private void ReadTextFile() // atributos separados por ','
    {
        StreamReader file = new StreamReader(datasetPath);
        bool firstLine = true;
        
        while (!file.EndOfStream)
        {
            string line = file.ReadLine();
            
            if(firstLine) // armazenar label do dataset
            {
                string[] lineSplited = line.Split(',');
                foreach (var label in lineSplited)
                {
                    _datasetLabel.Add(label);
                }
                firstLine = false;
            }
            else // armazenar dataset
            {
                _tempLine.Add(line);
            }
        }
        
        file.Close();
        ParseList();
    }

    private void ParseList()
    {
        foreach (var line in _tempLine)
        {
            string[] lineSplited = line.Split(',');
            
            if (_labelTypes.Count < 1) // se primeira linha de atributos, verificar o tipo deles
            {
                DefineLabelTypes(lineSplited);
            }
            
            _poiDataset.Add(lineSplited); 
        }
        _tempLine = null;
    }

    private void DefineLabelTypes(string[] dataExample) // cria uma lista de tipos para cada atributo da database
    {
        foreach (string data in dataExample)
        {
            if (int.TryParse(data, out int _)) { _labelTypes.Add(typeof(int)); }
            else if (double.TryParse(data, NumberStyles.Number,CultureInfo.InvariantCulture, out _)) 
            { _labelTypes.Add(typeof(double)); }
            else if (float.TryParse(data, out _)){ _labelTypes.Add(typeof(float)); }
            else if (bool.TryParse(data, out _)) { _labelTypes.Add(typeof(bool)); }
            else { _labelTypes.Add(typeof(string)); }
        }
    }
    
    public List<string[]> GetPoiList()
    {
        return _poiDataset;
    }

    public List<string> GetDatabaseLabel()
    {
        return _datasetLabel;
    }

    public List<Type> GetLabelTypes()
    {
        return _labelTypes;
    }

}
