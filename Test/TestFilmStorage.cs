namespace Lab_6
{
    [TestClass]
    public class TestFilmStorage
    {
        [TestMethod]
        public void TestAddingElement()
        {
            FilmStorage storage = new FilmStorage();
            Exception excep = null;

            try
            {
                storage.AddFilm(new Film());
            }
            catch (Exception ex)
            { excep = ex; }

            Assert.IsNull(excep);
        }
        [TestMethod]
        public void TestRemoveNotAddedElement()
        {
            var storage = new FilmStorage();
            Exception excep = null;

            try
            {
                storage.FilmDeleter();
            }
            catch (Exception ex)
            { excep = ex; }

            Assert.IsNotNull(excep);
        }
        [TestMethod]
        public void TestRemoveElement()
        {
            var storage = new FilmStorage();
            Exception exception = null;
            storage.AddFilm(new Film());

            try
            {
                storage.FilmDeleter();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNull(exception);
        }
        [TestMethod]
        public void SearchingEmptyStorage()
        {
            var storage = new FilmStorage();
            Exception exception = null;

            try
            {
                storage.SearchFilm();
            }
            catch(Exception ex)
            { exception = ex; }

            Assert.IsNotNull(exception);
        }
        [TestMethod]
        public void SearchingNonEmptyStorage()
        {
            var storage = new FilmStorage();
            storage.AddFilm(new Film());

            string? line=storage.SearchFilm();

            Assert.IsNotNull(line);
        }
        [TestMethod]
        public void SearchingNonExistingName()
        {
            var storage = new FilmStorage();
            storage.AddFilm(new Film());
            Exception exception = null;

            try
            {
                storage.SearchFilm("SomeName");
            }
            catch (Exception ex)
            { exception = ex; }

            Assert.IsNotNull(exception);
        }
        [TestMethod]
        public void SearchingExistingName()
        {
            var storage = new FilmStorage();
            storage.AddFilm(new Film());
            Exception exception = null;

            try
            {
                storage.SearchFilm("BaseFilm");
            }
            catch (Exception ex)
            { exception = ex; }

            Assert.IsNull(exception);
        }
        [TestMethod]
        public void SearchingNonExistingRating()
        {
            var storage = new FilmStorage();
            storage.AddFilm(new Film());
            Exception exception = null;

            try
            {
                storage.SearchFilm(6);
            }
            catch (Exception ex)
            { exception = ex; }

            Assert.IsNotNull(exception);
        }
        [TestMethod]
        public void SearchingExistingRating()
        {
            var storage = new FilmStorage();
            storage.AddFilm(new Film());
            Exception exception = null;

            try
            {
                storage.SearchFilm(1);
            }
            catch (Exception ex)
            { exception = ex; }

            Assert.IsNull(exception);
        }
        [TestMethod]
        public void EmptyFilmList()
        {
            var storage = new FilmStorage();
            Exception exception = null;

            try
            {
                storage.FilmList();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
        }
        [TestMethod]
        public void NotEmptyList()
        {
            var storage = new FilmStorage();
            storage.AddFilm(new Film());
            Exception exception = null;

            try
            {
                storage.FilmList();
            }
            catch(Exception ex)
            {
                exception = ex;
            }

            Assert.IsNull(exception);
        }
    }
}
