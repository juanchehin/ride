using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using ride.Datos;
using ride.Modelo;
using ride.VistaModelo;

using Xamarin.Forms;

namespace ride.VistaModelo
  {
  internal class VMesperarofertas:BaseViewModel
    {
    #region VARIABLES
    ObservableCollection<Mofertasdeconduct> _listaofertas;
    bool _visibleOfertas;
    #endregion
    #region CONSTRUCTOR
    public VMesperarofertas(INavigation navigation)
      {
      Navigation=navigation;
      VisibleOfertas=false;
      Listarofertas();
      Activartimer();
      }
    #endregion
    #region OBJETOS
    public bool VisibleOfertas
      {
      get { return _visibleOfertas; }
      set { SetValue(ref _visibleOfertas,value); }
      }
    public ObservableCollection<Mofertasdeconduct> Listaofertas
      {
      get { return _listaofertas; }
      set { SetValue(ref _listaofertas,value); }
      }
    #endregion
    #region PROCESOS
    private void Activartimer()
      {
      var tiempo = TimeSpan.FromSeconds(1);
      Device.StartTimer(tiempo, () =>
      {
        if(Listaofertas.Count>0)
          {
          VisibleOfertas=true;
          foreach(var item in Listaofertas)
            {
            var timespan = item.Timespan-TimeSpan.FromSeconds(1);
            item.Timespan=timespan;
            String[] cadena = timespan.ToString().Split(':');
            var time = cadena[2];
            item.Progress=Convert.ToDouble(time)*0.05;
            if(Convert.ToDouble(time)==0)
              {
              Eliminarofertas(item);
              }

            }
          }
        else
          {
          VisibleOfertas=false;
          }
        return true;
      });
      }
    private async void Eliminarofertas(Mofertasdeconduct parametros)
      {
      var funcion = new Dofertasdeconduct();
      await funcion.Eliminaroferta(parametros);
      }
    public void Listarofertas()
      {
      var funcion = new Dofertasdeconduct();
      Listaofertas=funcion.Listaofertas();
      }
   
    #endregion
    #region COMANDOS
   // public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
    #endregion
    }
  }
