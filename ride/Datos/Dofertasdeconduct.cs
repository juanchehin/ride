using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Firebase.Database.Query;

using ride.Conexiones;
using ride.Modelo;

namespace ride.Datos
  {
  public class Dofertasdeconduct
    {
    public ObservableCollection<Mofertasdeconduct> Listaofertas()
      {
      var data= new ObservableCollection<Mofertasdeconduct>();
      var collection = Constantes.firebase
        .Child("Ofertasdeconduct")
        .AsObservable<Mofertasdeconduct>()
        .Subscribe((item) =>
        {
          if (item.Object.idusuario=="Modelo")
            {
            if (item.Key!=item.Object.idoferta)
              {
              item.Object.Timespan=TimeSpan.FromSeconds(20);
              item.Object.idoferta=item.Key;
              data.Add(item.Object);
              }
            else
              {
              data.Remove(item.Object);
              }

            }
        });
      return data;
      }
    public async Task Eliminaroferta(Mofertasdeconduct parametros)
      {
      var data= (await Constantes.firebase
        .Child("Ofertasdeconduct")
        .OnceAsync<Mofertasdeconduct>()).Where(a=>a.Object.idusuario==parametros.idusuario).FirstOrDefault();

      await Constantes.firebase
        .Child("Ofertasdeconduct")
        .Child(data.Key)
        .DeleteAsync();

      }
    }
  }
