using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using rideDriver.Conexiones;
using rideDriver.Modelo;

namespace rideDriver.Datos
  {
  public class Dpedidos
    {
        public ObservableCollection<Mpedidos> Listarpedidos()
        {
            var lista = new ObservableCollection<Mpedidos>();
            var data = Constantes.firebase
              .Child("Pedidos")
              .AsObservable<Mpedidos>()
              .Subscribe((item) =>
              {
                  if (item.Object != null && item.Object.estado != "CONFIRMADO" && item.Key != "Modelo")
                  {
                      if (item.Key != item.Object.idpedido)
                      {
                          item.Object.idpedido = item.Key;
                          item.Object.origen_lugar = item.Object.origen_lugar;
                          item.Object.estado = item.Object.estado;
                          item.Object.destino_lugar = item.Object.destino_lugar;
                          item.Object.tarifa = item.Object.tarifa;
                          item.Object.tiempo = item.Object.tiempo;
                          item.Object.lt_lg_origen = item.Object.lt_lg_origen;
                          item.Object.lt_lg_destino = item.Object.lt_lg_destino;
                          lista.Add(item.Object);
                      }
                      else
                      {
                          lista.Remove(item.Object);
                      }
                  }
              }
              );
            return lista;
        }
    public async Task <List<Mpedidos>> ListarPedidosConfirmados(Mpedidos parametros)
      {
    
      return (await Constantes.firebase
        .Child("Pedidos")
        .OnceAsync<Mpedidos>())
        .Where(a => a.Key==parametros.idpedido)
        .Where(b => b.Object.estado=="CONFIRMADO")
         .Where(c => c.Object.idchofer==parametros.idchofer)
        .Select(item => new Mpedidos
          {
          idpedido=item.Key
          }).ToList();
      }
    public async Task<Mpedidos> ObtenerPedidosConfirmados(Mpedidos parametros)
      {
      var mpedidos = new Mpedidos();
      var data = (await Constantes.firebase
         .Child("Pedidos")
         .OnceAsync<Mpedidos>())
         .Where(a => a.Key==parametros.idpedido)
         .Where(b => b.Object.estado=="CONFIRMADO")
         .Where(c=>c.Object.idchofer==parametros.idchofer)
         .FirstOrDefault();

      mpedidos.lt_lg_origen=data.Object.lt_lg_origen;
      mpedidos.lt_lg_destino=data.Object.lt_lg_destino;
      return mpedidos;

      }
    }
}
