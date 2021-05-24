using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepUKW.Infrastructure
{
    public interface ISessionManager
    {
        //typy generyczne zastepuja dowolny obiekt
        void Set<T>(string name, T value);//dwa agumenty: te sesje oraz nazwe

        T Get<T>(string key); //w nawiaskie klucz/identyfikator dla tej sesji

        T TryGet<T>(string key); //trycatch

        void Abandon(); //usuwanie sesji
    }
}
