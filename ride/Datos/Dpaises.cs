using PhoneNumbers;
using ride.Modelo;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ride.Datos
{
    public class Dpaises
    {
        public static List<RegionInfo> PaisesIso3166()
        {
            var paises = new List<RegionInfo>();
            foreach(var cultura in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                var info = new RegionInfo(cultura.LCID);
                if(paises.All(p => p.Name != info.Name))   // Funcion lambda
                
                    paises.Add(info);
                }
                return paises.OrderBy(p => p.EnglishName).ToList();
            }
        
        public List<Mpaises> Mostrarpaises()
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var listapaises = new List<Mpaises>();
            var isopaises = PaisesIso3166();
            listapaises.AddRange(isopaises.Select(p => new Mpaises
            {
                Codigopais = phoneNumberUtil.GetCountryCodeForRegion
                (p.TwoLetterISORegionName).ToString(),
                Pais = p.EnglishName,
                Iconourl = $"https://hatscripts.github.io/circle-flags/flags/{p.TwoLetterISORegionName.ToLower()}.svg"
            }));
            return listapaises;
        }

    }
}
