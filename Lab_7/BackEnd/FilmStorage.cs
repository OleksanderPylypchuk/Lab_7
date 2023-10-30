using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab_7
{
    public class FilmStorage
    {
        List<Film> _listOfFilms = new List<Film>();
        public void AddFilm(Film film)
        {
            _listOfFilms.Add(film);
        }
        public string FilmList()
        {
            if (_listOfFilms.Count > 0)
            {
                string filmlist = "Список фільмів: ";
                foreach (Film film in _listOfFilms)
                {
                    filmlist += (film.FilmName + "; ");
                }
                return (filmlist+"\nКількість фільмів: "+Film.Count);
            }
            throw new Exception ("Список пустий");
        }
        public string SearchFilm()
        {
            if (_listOfFilms.Count < 1)
                throw new Exception("Не додано фільми");
            return $"{_listOfFilms[_listOfFilms.Count - 1].FilmView()}\nКодування фільму:\n{_listOfFilms[_listOfFilms.Count - 1].ToString()}";
        }
        public string SearchFilm(string filmname)
        {
            List<Film> foundFilms = new List<Film>(_listOfFilms.FindAll(
                delegate
                {
                    foreach (Film film in _listOfFilms)
                        if (film.FilmName == filmname)
                            return true;
                    return false;
				}));
            if (foundFilms.Count > 0)
            {
                string filmPosters = "Список знайдених кінокартин:\n";
                foreach (Film film in foundFilms)
                {
                    filmPosters += (film.FilmView()+"\nКодування фільму:\n" + film.ToString() + "\n\n");
                }
                return filmPosters;
            }
            throw new Exception("Не знайдено таких фільмів");
        }
        public string SearchFilm(int filmrating)
        {
            List<Film> foundFilms = new List<Film>(_listOfFilms.FindAll(
				delegate
				{
					foreach (Film film in _listOfFilms)
						if (film.FilmRating == filmrating)
							return true;
					return false;
				}));
            if (foundFilms.Count > 0)
            {
                string filmPosters = "Список знайдених кінокартин:\n";
                foreach (Film film in foundFilms)
                {
                    filmPosters += (film.FilmView() + "\nКодування фільму:\n" + film.ToString() + "\n\n");
                }
                return filmPosters;
            }
            throw new Exception("Не знайдено таких фільмів");
        }
        public void FilmDeleter()
        {
            if (_listOfFilms.Count == 0)
                throw new Exception("Немає фільмів для видалення");
            _listOfFilms.RemoveAt(_listOfFilms.Count - 1);
            Film.Count--;
        }
        public void FilmDeleter(bool i)
        {
            _listOfFilms.Clear();
        }
        public bool FilmDeleter(string filmname)
        {
			bool result = false;
            int count=_listOfFilms.Count;
			for (int i = 0; i < count; i++)
			{
				if (_listOfFilms[i].FilmName == filmname)
				{
					if (AskToDelete(_listOfFilms[i]))
					{
						Film.Count--; count--; result = true;
						_listOfFilms.RemoveAt(i);
						i--; 
					}
				}
			}
			return result;
		}
        public bool FilmDeleter(int filmrating)
        {
            bool result=false; 
            for (int i=0; i< _listOfFilms.Count;i++)
            {
                if (_listOfFilms[i].FilmRating == filmrating)
                {
                    if (AskToDelete(_listOfFilms[i]))
                    {
						Film.Count--;
						result = true;
						_listOfFilms.RemoveAt(i);
						i--;
					}
				}
            }
            return result;
        }

        public bool AskToDelete(Film film)
        {
            Console.WriteLine(film.FilmView());
            Console.WriteLine("\nВидалити цей фільм? 1 - так, 2 - ні");
            switch(Console.ReadLine())
            {
                case "1": return true;
                    case "2": return false;
                    default: throw new Exception("Немає такої опції");
            }
        }
        public void CreateJson(string filepath)
        {
            string json="";
            foreach(Film film in _listOfFilms)
            {
                json += JsonSerializer.Serialize(film);
                json += "\n";
            }
			if (json=="")
				throw new Exception("Немає доданих фільмів");
			File.WriteAllText(filepath, json);
		}
        public void ReadJson(string filepath)
        {
            List<string> lines= new List<string>();
            lines=File.ReadAllLines(filepath).ToList();
            int count=_listOfFilms.Count;
            foreach (string line in lines)
            {
                if (line=="")
                    continue;
                Film? film= JsonSerializer.Deserialize<Film>(line);
                if (film != null)
                {
                    _listOfFilms.Add(film);
                }
            }
            if (count==_listOfFilms.Count)
                throw new Exception("Жодного фільму не додано");
        }
        public void CreateCSV(string filepath)
        {
            string lines = "";
            foreach(Film film in _listOfFilms)
            {
                lines += (film.ToString() + "\n");
            }
            if(lines=="")
                throw new Exception("Немає доданих фільмів");
			File.WriteAllText(filepath, lines);
		}
		public void ReadCSV(string filepath)
		{
			string[] lines = File.ReadAllLines(filepath);
			int count = _listOfFilms.Count;
			foreach (string line in lines)
			{
				Film? film = Film.Parse(line);
				if (film != null)
				{
					_listOfFilms.Add(film);
				}
			}
			if (count == _listOfFilms.Count)
				throw new Exception("Жодного фільму не додано");
		}
	}
}
