using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

// using Plugin.ExternalMaps;

using rideDriver.Datos;
using rideDriver.Modelo;
using rideDriver.VistaModelo;
using rideDriver.Vistas.Navegacion;

using Xamarin.Forms;
namespace rideDriver.VistaModelo
{
    internal class VMpedidos:BaseViewModel
    {
    #region VARIABLES
    ObservableCollection<Mpedidos> _listapedidos;
    ObservableCollection<Mofertasdeconduct> _listaofertas;
    List<Mpedidos> _listapedidosConfirmados;
    bool _visibleOfertas;
    bool _visibleNavegar;
    bool _visiblePedidos;

    #endregion
    #region CONSTRUCTOR
    public VMpedidos(INavigation navigation)
      {
      Navigation=navigation;

      Listarpedidos();
      Mostrarofertas();
      VisibleOfertas=false;
      VisiblePedidos=true;
      VisibleNavegar=false;
      ValidarOfertas();

      }
    #endregion
    #region OBJETOS
    public bool VisiblePedidos
      {
      get { return _visiblePedidos; }
      set { SetValue(ref _visiblePedidos,value); }
      }
    public List<Mpedidos> ListapedidosConfirmados
      {
      get { return _listapedidosConfirmados; }
      set { SetValue(ref _listapedidosConfirmados,value); }
      }
    public bool VisibleNavegar
      {
      get { return _visibleNavegar; }
      set { SetValue(ref _visibleNavegar,value); }
      }
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
    public ObservableCollection<Mpedidos> Listapedidos
      {
      get { return _listapedidos; }
      set { SetValue(ref _listapedidos,value); }
      }
    #endregion
    #region PROCESOS
    public void Listarpedidos()
      {
      var funcion = new Dpedidos();
            Listapedidos = funcion.Listarpedidos();
        }
    public async void Seleccionar(Mpedidos parametros)
      {
            await Navigation.PushAsync(new Negociar(parametros));
        }
    private void Mostrarofertas()
      {
            var funcion = new Dofertas();
            Listaofertas = funcion.ListarofertasXidconductor("Modelo");
        }
    private async void EliminarOfertas( Mofertasdeconduct parametros)
      {
            var funcion = new Dofertas();
            await funcion.EliminarOfertas(parametros);

        }
    private void ValidarOfertas()
      {
            var tiempo = TimeSpan.FromSeconds(1);
            Device.StartTimer(tiempo, () =>
            {
                if (Listaofertas.Count > 0)
                {
                    VisibleOfertas = true;
                    foreach (var item in Listaofertas)
                    {
                        var timespan = item.Timespan - TimeSpan.FromSeconds(1);
                        item.Timespan = timespan;
                        String[] cadena = timespan.ToString().Split(':');
                        var time = cadena[2];
                        item.Progress = Convert.ToDouble(time) * 0.05;
                        if (Convert.ToDouble(time) == 0)
                        {
                            EliminarOfertas(item);
                        }
                  //00:00:20
                    }
                    return true;
                }
                else
                {
                    Pedidoconfirmado();
                    VisibleOfertas = false;
                    return true;
                }
            });
        }
    private async void Pedidoconfirmado()
      {
      var funcion = new Dpedidos();
      var parametros = new Mpedidos();
      parametros.idpedido="Modelo";
            ListapedidosConfirmados = await funcion.ListarPedidosConfirmados(parametros);
            if (ListapedidosConfirmados.Count>0)
        {
        VisiblePedidos=false;
        VisibleNavegar=true;
        }
      else
        {
        VisiblePedidos=true;
        VisibleNavegar=false;
        }
      }
    private async void NavegaralpuntoA()
      {
      var funcion = new Dpedidos();
      var parametros =new Mpedidos();
      parametros.idpedido="-N1qq0gIZzawDS3KimMA";
      parametros.idchofer="Modelo";
            var data = await funcion.ObtenerPedidosConfirmados(parametros);
            string cadena = data.lt_lg_origen;//18.4545,45.4575
            string[] separadas = cadena.Split(',');

            var lt = separadas[0];
            var lg = separadas[1];
            lt = lt.Replace(".", ",");
            lg = lg.Replace(".", ",");
            var ltdouble = Convert.ToDouble(lt);
            var lgdouble = Convert.ToDouble(lg);
            // await CrossExternalMaps.Current.NavigateTo("Navegar",ltdouble,lgdouble);

        }
    #endregion
    #region COMANDOS
    public ICommand NavegaralpuntoAcommand => new Command(NavegaralpuntoA);
    public ICommand Seleccionarcommand => new Command<Mpedidos>(Seleccionar);
    #endregion
    }
}
