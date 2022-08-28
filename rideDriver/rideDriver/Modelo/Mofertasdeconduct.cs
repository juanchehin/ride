using System;
using System.Collections.Generic;
using System.Text;

using rideDriver.VistaModelo;

namespace rideDriver.Modelo
  {
  public class Mofertasdeconduct : BaseViewModel
    {
    public string idoferta { get; set; }
    public string idconductor { get; set; }
    public string idpedido { get; set; }
    public string estado { get; set; }
    public string tiempoalorigen { get; set; }
    public string tarifa { get; set; }

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
