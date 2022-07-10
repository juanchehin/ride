using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ride.Conexiones
    {
    public class ConstantesExample  // <-- Cambiar por 'Constantes'
        {
        public const string GoogleMapsApiKey = "tuKey";
        public static FirebaseClient firebase = new FirebaseClient("https://ride-aba50-default-rtdb.firebaseio.com/");
    }
    }
