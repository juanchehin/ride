using System;
using System.Collections.Generic;
using System.Text;

using rideDriver.VistaModelo;
namespace rideDriver.Modelo
  {
  public class Mpedidos:BaseViewModel
    {
    public string idpedido { get; set; }
    public string origen_lugar { get; set; }
    public string destino_lugar { get; set; }
    public string estado { get; set; }
    public string idchofer { get; set; }
    public string iduser { get; set; }
    public string lt_lg_destino { get; set; }
    public string lt_lg_origen { get; set; }
    public string tiempo { get; set; }
    public string tarifa { get; set; }
    public string distancia { get; set; }
    public string tiempoproximo { get; set; }
    //Objetos
    string _tarifaTextbtn;
    public string TarifaTextbtn
      {
      get { return _tarifaTextbtn; }
      set { SetValue(ref _tarifaTextbtn,value); }
      }
    bool _estadobtnDism;
    public bool EstadobtnDism
      {
      get { return _estadobtnDism; }
      set { SetValue(ref _estadobtnDism,value); }
      }
    
    }
  }
