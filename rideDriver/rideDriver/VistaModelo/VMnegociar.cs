using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using rideDriver.Datos;
using rideDriver.Modelo;
using rideDriver.Servicios;
using rideDriver.VistaModelo;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace rideDriver.VistaModelo
{
  internal class VMnegociar : BaseViewModel
    {
    #region VARIABLES 
    List<Mpedidos> _listatiempo;
    bool _visibleListatiempos;
    Map maparuta;
    double _tarifainicial;
    public Mgooglematrix ParamMatrixCliente { get; set; }
    public Mgooglematrix ParamMatrixConductor { get; set; }
    public Mpedidos parametrosRecibe { get; set; }

    private readonly IGoogleMapsApiService _googleMapsApi = new GoogleMapsApiService();
    #endregion 
    #region CONSTRUCTOR
    public VMnegociar(INavigation navigation,Map maparutaRef,Mpedidos parametrosTrae)
      {
      Navigation=navigation;
      maparuta=maparutaRef;
      parametrosRecibe=parametrosTrae;
      _tarifainicial=Convert.ToDouble(parametrosRecibe.tarifa);
      parametrosRecibe.TarifaTextbtn=_tarifainicial.ToString();
      parametrosRecibe.EstadobtnDism=false;
      VisibleListatiempos=false;
      Dibujarruta();

      }
    #endregion
    #region OBJETOS
    public List<Mpedidos> Listatiempo
      {
      get { return _listatiempo; }
      set { SetValue(ref _listatiempo,value); }
      }
    public bool VisibleListatiempos
      {
      get { return _visibleListatiempos; }
      set { SetValue(ref _visibleListatiempos,value); }
      }
    #endregion
    #region PROCESOS
    private void Mostrarlistatiempos()
      {
      VisibleListatiempos=true;
      string tiempoAlpuntoa = ParamMatrixConductor.Tiempo;//4 min
      string[] cadena = tiempoAlpuntoa.Split(' ');
      Listatiempo=new List<Mpedidos>()
        {
        new Mpedidos()
          {
          tiempoproximo=tiempoAlpuntoa
          },
        new Mpedidos()
          {
          tiempoproximo =(Convert.ToDouble( cadena[0])+5) + " " + cadena[1]
          },
          new Mpedidos()
          {
          tiempoproximo =(Convert.ToDouble( cadena[0])+10) + " " + cadena[1]
          },
           new Mpedidos()
          {
          tiempoproximo =(Convert.ToDouble( cadena[0])+20) + " " + cadena[1]
          }

        };
      }
    private void Aumentartarifa()
      {
      _tarifainicial+=0.5;
      parametrosRecibe.TarifaTextbtn=_tarifainicial.ToString();
      ValidarbtnDism();
      }
    private void ValidarbtnDism()
      {
      if (parametrosRecibe.tarifa==_tarifainicial.ToString())
        {
        parametrosRecibe.EstadobtnDism=false;
        }
      else
        {
        parametrosRecibe.EstadobtnDism=true;

        }
      }
    private void Disminuirtarifa()
      {
      if (parametrosRecibe.tarifa!=_tarifainicial.ToString())
        {
        _tarifainicial-=0.5;
        parametrosRecibe.TarifaTextbtn=_tarifainicial.ToString();
        }
      ValidarbtnDism();
      }

    public async void Dibujarruta()
      {

      ParamMatrixCliente=await _googleMapsApi.DibujarRutapuntoB(parametrosRecibe.lt_lg_origen,parametrosRecibe.lt_lg_destino,maparuta);
      ParamMatrixConductor=await _googleMapsApi.DibujarRutapuntoA("-12.787260, -76.574726",parametrosRecibe.lt_lg_origen,maparuta);

      }
    public  async void InsertarOferta()
      {
      var funcion = new Dofertas();
      var parametros = new Mofertasdeconduct();
      parametros.idconductor="Modelo";
      parametros.tarifa=_tarifainicial.ToString();
      parametros.tiempoalorigen="4 min";
      parametros.idpedido=parametrosRecibe.idpedido;
      parametros.estado="PENDIENTE";
      await funcion.Insertarofertas(parametros);
      await Navigation.PopAsync();
      }
    #endregion
    #region COMANDOS
    public ICommand Aceptarcommand => new Command(Mostrarlistatiempos);
    public ICommand Aumentartarifacommand => new Command(Aumentartarifa);
    public ICommand Disminuirtarifacommand => new Command(Disminuirtarifa);

    public ICommand InsertarOfertacommand => new Command(InsertarOferta);
    #endregion
    }
}
