using System.Text.Json.Serialization;

namespace Lab_7
{
    public enum FilmDescription
    {
        Фільм=2,
        Мультфільм=0,
        Аніме=1
    }
    public class Film
    {
        [JsonIgnore]
        public static string Cinema
        { 
            get;
            set;
		}="BaseCinema";
        public static void NewCinema(string cinema)
        {
            Cinema = cinema;
        }
        [JsonIgnore]
        private static int count = 0;
        [JsonIgnore]
        public static int Count
        {
            get; set;
        }
        [JsonPropertyName("Name")]
        private string _filmName;
        public string FilmName
        {
            get { return _filmName; }
            set
            {
                if (value.Length <= 10 && value.Length >= 4)
                {
                    _filmName = value;
                    return;
                }
                else throw new Exception("Ім'я не підходить");
            }
        }
        [JsonPropertyName("Kind")]
        private FilmDescription _filmDescription;
		public FilmDescription FilmDescription
        {
            get { return _filmDescription; }
            private set { _filmDescription = value; }
        }

        public void SetFilmDescription(string value)
        {
            FilmDescription description;

            switch (value)
            {
                case "0":
                    description = FilmDescription.Мультфільм;
                    break;
                case "1":
                    description = FilmDescription.Аніме;
                    break;
                case "2":
                    description = FilmDescription.Фільм;
                    break;
                default:
                    throw new Exception("піська");
            }

            FilmDescription = description;
        }
        [JsonPropertyName("Genre")]
        private string _filmCategory;
		public string FilmCategory
        {
            get { return _filmCategory; }
            set
            {
                string[] rightnumbers = {"0", "1", "2", "3", "4", "5" };
                foreach (string rightnumber in rightnumbers)
                {
                    if (value == rightnumber)
                    {
                        _filmCategory = rightnumber;
                        return;
                    }
                }
                throw new Exception("Не можливе значення");
            }
        }
        [JsonPropertyName("Age rating")]
        private string _filmAgeRating;
		public string FilmAgeRating { get { return _filmAgeRating; } set
            {
                switch (value)
                {
                    case "0":
						_filmAgeRating = "0";
                        break;
                    case "16":
						_filmAgeRating = "16";
                        break;
                    case "18":
						_filmAgeRating = "18";
                        break;
                        default : throw new Exception("Гівно");
                }
            }
        }
		[JsonPropertyName("Rating")]
		private int _filmRating=5;
        public int FilmRating{
            get { return _filmRating; }
            set
            {
                if (value >= 1 && value <= 10)
                {
                    _filmRating = value;
                }
                else throw new Exception("пісюн");
            }
        }
		[JsonIgnore]
		private int _filmlength;
		[JsonIgnore]
		public int Filmlength { get { return _filmlength; } set { _filmlength = value; } }
        public Film() : this("BaseFilm", "0", "0", "0", 1) { }

        public Film(string filmNameParam, string filmDescrParam, string filmCategParam, string filmAgeRatingParam, int filmRatingParam )
        {
            FilmName = filmNameParam;
            SetFilmDescription(filmDescrParam);
            FilmCategory = filmCategParam;
            FilmAgeRating = filmAgeRatingParam;
            FilmRating = filmRatingParam;
            Filmlength = FilmRating * 10 + FilmName.Length * 3+10;
            Count++;
        }
        public Film(string filmNameParam, string filmDescrParam, string filmCategParam, string filmAgeRatingParam)
        {
            FilmName = filmNameParam;
            SetFilmDescription(filmDescrParam);
            FilmCategory = filmCategParam;
            FilmAgeRating = filmAgeRatingParam;
            Filmlength = FilmRating * 10 + FilmName.Length * 3 + 10;
            Count++;
        }
        public string FilmView()
        {
            string category;
			switch (FilmCategory)
			{
				case "0":
					category = "Хоррор";
					break;
				case "1":
					category = "Бойовик";
					break;
				case "2":
					category = "Романтика";
					break;
				case "3":
					category = "Комедія";
					break;
				case "4":
					category = "Наукова фантастика";
					break;
				case "5":
					category = "Фентезі";
					break;
                default:throw new Exception("Щось не так");
			};
			return ($"Назва: {FilmName}\nВид кінематографії: {FilmDescription}" +
                $"\nВікова категорія: {FilmAgeRating}+\nЖанр фільму: {category}"+
                $"\nОцінка критиків: {FilmRating}/10\nДовжина фільму: {Filmlength/60}"+
                $" годин {Filmlength%60} хвилин\nПереглянуто у кінотеатрі {Cinema}");
        }
        public static Film Parse(string line)
        {
            string[] split = line.Split(',');
            switch(split.Length)
            {
                case 4:
                    string tempname = split[0];
                    string tempDescription = split[1];
                    string tempCateg = split[2];
                    string tempAge = split[3];
                    return new Film(tempname, tempDescription, tempCateg, tempAge);
                case 5:
                    tempname = split[0];
                    tempDescription = split[1];
                    tempCateg = split[2];
                    tempAge = split[3];
                    string tempRating = split[4];
                    return new Film(tempname, tempDescription, tempCateg, tempAge, int.Parse(tempRating));
                default:
                    throw new Exception("Неможливі параметри");
            }
        }
        public static bool TryParse(string line, out Film film)
        {
            string[] split = line.Split(',');
            if(split.Length == 4 || split.Length==5)
            {
                film = Parse(line);
                return true;
            }
            else
            {
                film = null;
                return false;
            }
        }
        public override string ToString()
        {
            return $"{FilmName},{(int)FilmDescription},{FilmCategory},{FilmAgeRating},{FilmRating}";
        }
    }
}
