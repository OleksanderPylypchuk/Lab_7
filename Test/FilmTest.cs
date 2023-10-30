namespace Lab_6
{
    [TestClass]
    public class FilmTest
    {
        [TestMethod]
        public void TryParseIfFalse()
        {
            string line = "falsfpas";
            Film film;

            bool actual=Film.TryParse(line, out film);

            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void TryParseIfTrue()
        {
            string line = "BaseFilm,0,0,0,1";
            Film film;

            bool actual = Film.TryParse(line, out film);

            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void ParseWrongValue()
        {
            string line = "akfoskf";
            Film film; Exception exception=null;

            try
            {
                film = Film.Parse(line);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
        }
        [TestMethod]
        public void ParseWrongNumbers()
        {
            string line = "BaseFilm,10,52,0,1";
            Film film; Exception exception = null;

            try
            {
                film = Film.Parse(line);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
        }
        [TestMethod]
        public void Parse4Params()
        {
            string line = "BaseFilm,0,0,0";
            Film film; Exception exception = null;

            try
            {
                film = Film.Parse(line);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNull(exception);
        }
        [TestMethod]
        public void Parse5Params()
        {
            string line = "BaseFilm,0,0,0,10";
            Film film; Exception exception = null;

            try
            {
                film = Film.Parse(line);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNull(exception);
        }
        [TestMethod]
        public void TestToString()
        {
            Film film=new Film();

            string line = film.ToString();

            Assert.AreEqual(line, "BaseFilm,0,0,0,1");
        }
        [TestMethod]
        public void TestView()
        {
            Film film = new Film();
            string expected = ($"Назва: {film.FilmName}\nВид кінематографії: {film.FilmDescription}" +
                $"\nВікова категорія: {film.FilmAgeRating}+\nЖанр фільму: {film.FilmCategory}" +
                $"\nОцінка критиків: {film.FilmRating}/10\nДовжина фільму: {film.Filmlength / 60}" +
                $" годин {film.Filmlength % 60} хвилин\nПереглянуто у кінотеатрі {Film.Cinema}");

            string actual = film.FilmView();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestTooLongName()
        {
            string line = "BaseFilmTooLong,0,0,0,10"; Exception exception = null;

            try
            {
                Film film = Film.Parse(line);
            }
            catch (Exception ex)
            {
                exception= ex;
            }

            Assert.IsNotNull(exception);
        }
        [TestMethod]
        public void TestToShortNameName()
        {
            string line = "Bas,0,0,0,10"; Exception exception = null;

            try
            {
                Film film = Film.Parse(line);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
        }
        [TestMethod]
        public void TestRightName()
        {
            string line = "BaseFilm,0,0,0,10"; Exception exception = null;

            try
            {
                Film film = Film.Parse(line);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNull(exception);
        }

        [TestMethod]
        public void ChangeCinema()
        {
            string OldCinema = Film.Cinema;

            Film.NewCinema(OldCinema+"1");

            Assert.AreNotEqual(OldCinema, Film.Cinema);
        }
    }
}