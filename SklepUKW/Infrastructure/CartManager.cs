using SklepUKW.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepUKW.Infrastructure
{
    public class CartManager
    {
        //musi korzystac z bazy danych i mechanizmu sieci
        FilmsContext db;
        ISessionManager session;

        public CartManager(FilmsContext db, ISessionManager session)
        {
            this.db = db;
            this.session = session;
        }

        public List<CartItem> GetCartItems()//cartitem - klasa ktorej nie ma. alt enter. tworzy sie plik cartitem. przeniesc do folderu Model
        {
            //klik na cartitem, f12 - przejscie do klasy
            //potrzebujemy klucz sesji - cartsessionkey w consts
            //pobieranie itemow z koszyka
            List<CartItem> items;

            if(session.Get<List<CartItem>>(Consts.CartSessionKey) == null) //obiekt w <> to obiekt wpychany i jest to lista itemow koszyka
            {
                items = new List<CartItem>();
            }
            else
            {
                items = session.Get<List<CartItem>>(Consts.CartSessionKey); //wkladamy to co jest w sesji
            }

            return items; //jak cos w sesji jest, jak cos dodalismy, to wrzucamy do listy produktow w koszyku. a jak nie ma nic, to zwracamy pusta liste produktow/filmow w koszyku
        }


        //dodawanie itemow do koszyka
        public void AddToCart(int filmId)
        {
            var cart = GetCartItems(); //pobranie z sesji co juz jest
            //szukamy w koszyku filmu
            var thisFilm = cart.Find(f => f.Film.FilmId == filmId); //thisfilm - film o tym id//wyszukiwanie filmu za pomoca listy z zgodnym ID

            if(thisFilm != null) //jesli jest film to zwiekszamy ilosc pozycji
            {
                thisFilm.Quantity++;
            }
            else //jak nie ma filmu to tworzymy nowy item na podstawie bazy danych (db)
            {
                //szukamy w bazie danych
                var newCartItem = db.Films.Where(f => f.FilmId == filmId).SingleOrDefault(); //pobieranie filmu o danym id

                if(newCartItem != null)//co jest w cartitem
                {
                    var cartItem = new CartItem() //tworzymy cartitem na podstawie klasy
                    {
                        //dodajemy neizbedne parametry
                        Film = newCartItem,
                        Quantity = 1,
                        Price = newCartItem.Price
                    };

                    cart.Add(cartItem); //dodajemy film do listy GetCartItems
                }
            }

            session.Set(Consts.CartSessionKey, cart); //wrzucamy liste GetCartItems do sesji
        }


        public int RemoveFromCart(int filmId) //usuwamy film o konkretnym id
        {
            //czy jest ta pozycja w koszyku
            var cart = GetCartItems(); //pobranie z sesji co juz jest
            //szukamy w koszyku filmu
            var thisFilm = cart.Find(f => f.Film.FilmId == filmId); //thisfilm - film o tym id//wyszukiwanie filmu za pomoca listy z zgodnym ID

            if(thisFilm != null)
            {
                if(thisFilm.Quantity > 1)
                {
                    thisFilm.Quantity--;
                    return thisFilm.Quantity;
                }
                else
                {
                    cart.Remove(thisFilm);
                }
            }

            return 0;
        }

        public int GetCartQuantity()
        {
            //sumowanie wartosci z danego pola dla wszystkich itemow/pozycji w liscie
            var cart = GetCartItems(); //pobieranie listy produktow z koszyka
            var sum = cart.Sum(i => i.Quantity); //sumowanie w tej liscie

            return sum;
        }

        public decimal GetCartValue()
        {
            var cart = GetCartItems(); //pobieranie zawartosci koszyka
            var sum = cart.Sum(i => (i.Price * i.Quantity)); //cena * ilosc, mnozenie sumowac

            return sum;
        }
    }
}