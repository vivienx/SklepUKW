using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace SklepUKW.Infrastructure
{
    public class SessionManager : ISessionManager// : dziedzienie z interfejsu. alt enter po wpisaniu : issessionamanager
    {

        HttpSessionState session; //obiekt naszej sesji
        
        
        public SessionManager() //inicjalizacja w konstruktorze
        {
            session = HttpContext.Current.Session;
        }


        public void Abandon()
        {
            session.Abandon(); //zamykanie sesji
        }

        public T Get<T>(string key)
        {
            return (T)session[key]; //pobieranie sesji. zwrocenie sesji o danym kluczu. T - rzutowanie na typ generyczny
                                    //T -> jakis dowolny obiekt np film
        }

        public void Set<T>(string name, T value)
        {
            session[name] = value;
        }

        public T TryGet<T>(string key)
        {
            try
            {
                return (T)session[key];
            }
            catch (NullReferenceException)
            {
                return default(T); //w nawiasie typ generyczny
            }
        }
    }
}