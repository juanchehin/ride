using System;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using rideDriver.Modelo;
using rideDriver.Conexiones;
using System.Collections.ObjectModel;

namespace rideDriver.Datos
  {
  public class Dofertas
    {
    public async Task Insertarofertas(Mofertasdeconduct parametros)
      {
      await Constantes.firebase
        .Child("Ofertasdeconduct")
        .PostAsync(new Mofertasdeconduct()
          {
          tiempoalorigen=parametros.tiempoalorigen,
          idconductor=parametros.idconductor,
          tarifa=parametros.tarifa,
          idpedido=parametros.idpedido,
          estado=parametros.estado
          });
      }
    public ObservableCollection<Mofertasdeconduct>ListarofertasXidconductor(string idconductor)
      {
      var lista = new ObservableCollection<Mofertasdeconduct>();
      var data = Constantes.firebase
        .Child("Ofertasdeconduct")
        .AsObservable<Mofertasdeconduct>()
        .Subscribe((item) =>
        {
          if (item.Object.idconductor==idconductor)
            {
            if (item.Key!=item.Object.idoferta)
              {
              item.Object.Timespan=TimeSpan.FromSeconds(20);
              item.Object.idoferta=item.Key;
              lista.Add(item.Object);
              }
            else
              {
              lista.Remove(item.Object);
              }
            }
        });
      return lista;

      }
    public async Task EliminarOfertas(Mofertasdeconduct parametros)
      {
      var data = (await Constantes.firebase
        .Child("Ofertasdeconduct")
        .OnceAsync<Mofertasdeconduct>()).Where(a => a.Key==parametros.idoferta).FirstOrDefault();
      await Constantes.firebase.Child("Ofertasdeconduct").Child(data.Key).DeleteAsync();
      }
    }
  }
