using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class PoiClass : MonoBehaviour
{
    public Poi myPoi = new Poi();

    public class Poi
    {
        //Atributos Fixos lidos da base de dados
        private string _id, _descricao;
        private double _latitude, _longitude;

        //Atributos variáveis que podem ser alterados via filtro
        private int _altura, _largura;
        private Material _cor;
        private bool _visivel = true;
        

        public void CreatePoi(string cod, double lat, double lon, string desc)
        {
            _id = cod;
            _latitude = lat;
            _longitude = lon;
            _descricao = desc;
        }
        
        //getter and setter function lists
        public string GetPoiName(){ return _descricao; }
        public string GetPoiId(){ return _id; }
        public double GetPoiLat(){ return _latitude; }
        public double GetPoiLong(){ return _longitude; }
        public void SetMaterial(Material material){ _cor = material; }
        public void SetAltura(int value) { _altura = value; }
        public void SetLargura(int value) { _largura = value; }
        public bool IsVisivel(){ return _visivel; }
        public void SetVisivel(bool estado){ _visivel = estado; }

    }
}
