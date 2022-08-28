using System;
using System.Collections.Generic;
using System.Text;

using ride.VistaModelo;

namespace ride.Modelo
  {
  public class Mofertasdeconduct : BaseViewModel
    {
    public string idoferta { get; set; }
    public string idconductor { get; set; }
    public string idusuario { get; set; }
    public string estado { get; set; }
    public string tiempoalorigen { get; set; }

    //Objetos
    public double _progress;
    public double Progress
      {
      get { return _progress; }
      set { SetValue(ref _progress,value); }
      }
    
  public TimeSpan _timespan;
  public TimeSpan Timespan
    {
    get { return _timespan; }
    set { SetValue(ref _timespan,value); }
    }
  }
  }
