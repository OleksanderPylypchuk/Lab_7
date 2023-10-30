using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class UIFilms
    {
        FilmStorage listofFilms = new FilmStorage();
        public void UserOptionsShow()//початковий екран
        {
            Console.WriteLine("Виберіть дію:\n1 - Створити фільм\n2 - Виведення списку фільмів" +
                "\n3 - Видалення останнього фільму\n4 - Перегляд останнього доданого фільму\n5 - Пошук фільмів" +
				"\n6 - Видалення за категорією\n7 - Змінити кінотеатр\n8 - Створити файл\n9 - Зчитати файл\n10 - Очистити список\n0 - Вихід");
            switch (Console.ReadLine())
            {
                case "1":
                    FilmCreator();
                    break;
                case "2":
                    FilmLister();
                    break;
                case "3":
                    FilmDeleter();
                    break;
                case "4":
                    FilmLastAddedChecker();
                    break;
                case "5":
                    FilmSearchEngine();
                    break;
                case "6":
                    FilmDeleteEngine();
                    break;
                case "7":
                    ChangeCinema();
                    break;
                case "8":
                    CreateFile();
                    break;
                case "9":
                    ReadFile();
                    break;
                case "10":
                    ClearList();
                    break;
                case "0":
                    Exit();
                    break;
                default:
                    Console.WriteLine("Немає такої опції, натисніть будь яку клавішу");
                    Console.ReadKey();
                    Console.Clear();
                    UserOptionsShow();
                    break;
            }
        }
        public void ReturnToOptions(Exception ex)//Повертає корситувача на початковий екран після виключення
        {
            Console.WriteLine(ex.Message);
            ReturnToOptions();
        }
        public void FilmCreator()//Додає фільм до лісту
        {
            Console.Clear();
            try
            {
                Console.WriteLine("Виберіть спосіб:\n1 - Створення фільму\n2 - Базовий фільм\n3 - Введення даних рядком");
                switch(Console.ReadLine())
                {
                    case "1":
                        FilmCreator(1);
                        break;
                    case "2":
                        BasicFilmCreator();
                        break;
                    case "3":
                        Console.WriteLine("Введіть через кому:\nНазву (4-10 літер)\nКатегорію (0-мультфільм, 1-аніме, 2-фільм)" +
                            "\nЖанр (0-хоррор, 1-бойовик, 2-романтика, 3-комедія, 4-наукова фантастика, 5-фентезі)" +
                            "\nВікову категорію (0, 16, 18)\nОцінку 1-10 (не обов'язково)");
                        string tempLine=Console.ReadLine();
                        Film temp;
                        if(Film.TryParse(tempLine,out temp))
                        {
                            listofFilms.AddFilm(temp);
                            Console.WriteLine("Фільм успішно створено!");
                            ReturnToOptions();
                            break;
                        }
                        else
                        {
                            throw new Exception("Неможливі параметри");
                        }
                    default:
                        throw new Exception("Немає такої опції.");
                }
            }
            catch (Exception ex)
            {
                ReturnToOptions(ex);
            }
        }
        public void BasicFilmCreator()
        {
            listofFilms.AddFilm(new Film());
            Console.WriteLine("Фільм успішно створено!");
            ReturnToOptions();
        }
        public void FilmCreator(int i)//створює фільм при вибраній опції 1
        {
            try
            {
                Console.WriteLine("Введіть назву фільму");
                string newname = Console.ReadLine();
                Console.WriteLine("Введіть категорію кінокартини (0-мультфільм, 1-аніме, 2-фільм)");
                string newdescr = Console.ReadLine(); int tempint;
                Console.WriteLine("Введіть категорію кінокартини (0-хоррор, 1-бойовик, 2-романтика, 3-комедія, 4-наукова фантастика, 5-фентезі)");
                string newcategory = Console.ReadLine();
                Console.WriteLine("Введіть вікову категорію кінокартини (0, 16, 18)");
                string newagerating = Console.ReadLine();
                Console.WriteLine("Виберіть спосіб задання оцінки: 1 - вручну, 2 - автовластивість");
                switch (Console.ReadLine())
                {
                    case "1":
                        int newrating;
                        Console.WriteLine("Введіть оцінку від 1 до 10");
                        string temprating = Console.ReadLine();
                        if (!int.TryParse(temprating, out tempint))//перевірка на інтовість
                            throw new Exception("Не правильний тип");
                        newrating = int.Parse(temprating);
                        listofFilms.AddFilm(new Film(newname, newdescr, newcategory, newagerating, newrating));
                        break;
                    case "2":
                        listofFilms.AddFilm(new Film(newname, newdescr, newcategory, newagerating));
                        break;
                    default:
                        throw new Exception("Немає такої опції");
                }
                Console.WriteLine("Фільм успішно створено! Натисніть будь яку клавішу");
                Console.ReadKey();
                Console.Clear();
                UserOptionsShow();
            }
            catch (Exception ex)
            {
                ReturnToOptions(ex);
            }
        }
        public void FilmLister()//виводить назви доданих фільмів
        {
            try
            {
                Console.Clear();
                Console.WriteLine(listofFilms.FilmList());
                ReturnToOptions();
            }
            catch(Exception ex)
            {
                ReturnToOptions(ex);
            }
        }
        public void FilmDeleter()//видаляє останній доданий фільм
        {
            try
            {
                Console.Clear();
                listofFilms.FilmDeleter();
                Console.WriteLine("Видалено останній доданий фільм");
                ReturnToOptions();
            }
            catch (Exception ex)
            {
                ReturnToOptions(ex);
            }
        }
        public void FilmLastAddedChecker()//Видає інформацію про останній доданий фільм
        {
            try
            {
                Console.Clear();
                Console.WriteLine(listofFilms.SearchFilm());
                ReturnToOptions();
            }
            catch(Exception ex)
            {
                ReturnToOptions(ex);
            }
        }
        public void FilmSearchEngine()//пошукова система
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Введіть категорію пошуку: 1 - за назвою, 2 - за оцінкою");
                string temp = Console.ReadLine();
                switch(temp)
                {
                    case "1":
                        Console.WriteLine("Введіть назву фільму");
                        string searchedName = Console.ReadLine();
                        Console.WriteLine(listofFilms.SearchFilm(searchedName));
                        break;
                    case "2":
                        int tempint;
                        Console.WriteLine("Введіть оцінку фільму");
                        string searchedGrade = Console.ReadLine();
                        if (!int.TryParse(searchedGrade, out tempint))//перевірка на інтовість
                            throw new Exception("Не той тип");
                        Console.WriteLine(listofFilms.SearchFilm(int.Parse(searchedGrade)));
                        break;
                    default: throw new Exception("Не існуюча опція");
                }
                ReturnToOptions();
            }
            catch(Exception ex)
            {
                ReturnToOptions(ex);
            }
        }
        public void FilmDeleteEngine()//Видяляє фільми за категоріями
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Введіть категорію пошуку: 1 - за назвою, 2 - за оцінкою");
                string temp = Console.ReadLine(); 
                switch (temp)
                {
                    case "1":
                        Console.WriteLine("Введіть назву фільму");
                        string searchedName = Console.ReadLine();
                        if (listofFilms.FilmDeleter(searchedName))
                        {
                            Console.WriteLine("Видалено усі фільми з даною назвою");
                            break;
                        }
                        Console.WriteLine("Не знайдено фільмів з такою назвою");
                        break;
                    case "2":
                        int tempint;
                        Console.WriteLine("Введіть оцінку фільму");
                        string searchedGrade = Console.ReadLine();
                        if (!int.TryParse(searchedGrade, out tempint))//перевірка на інтовість
                            throw new Exception("Не той тип");
                        if(listofFilms.FilmDeleter(int.Parse(searchedGrade)))
                        {
                            Console.WriteLine("Видалено усі фільми з даною оцінкою");
                            break;
                        }
                        Console.WriteLine("Не знайдено фільмів з такою оцінкою");
                        break;
                    default: throw new Exception("Не існуюча опція");
                }
                ReturnToOptions();
            }
            catch (Exception ex)
            {
                ReturnToOptions(ex);
            }
        }
        public void ChangeCinema()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Введіть назву кінотеатру");
                string newcinema = Console.ReadLine();
                if (newcinema != "")
                {
                    Console.WriteLine("Змінено кінотеатр");
                    Film.NewCinema(newcinema);
                    ReturnToOptions() ;
                }
                throw new Exception("Не підходяща назва");
            }
            catch (Exception ex)
            {
                ReturnToOptions(ex);
            }
        }
        public void ReadFile()
        {
            try
            {
				Console.WriteLine("1 - зчитати Json, 2 - зчитати CSV");
                string line;
				switch (Console.ReadLine())
				{
					case "1":
						Console.WriteLine("Вкажіть шлях");
						line = Console.ReadLine();
						listofFilms.ReadJson(line);
						break;
					case "2":
						Console.WriteLine("Вкажіть шлях");
					    line = Console.ReadLine();
						listofFilms.ReadCSV(line);
						break;
					default:
						throw new Exception("Неіснуюча опція");
				}
                Console.WriteLine("Додано фільми до списку");
				ReturnToOptions();
			}
            catch(Exception ex)
            {
                ReturnToOptions(ex);
            }
		}
        public void CreateFile()
        {
            try
            {
				Console.WriteLine("1 - створити Json, 2 - Створити CSV");
				string line;
				switch (Console.ReadLine())
				{
					case "1":
						Console.WriteLine("Вкажіть шлях");
						line = Console.ReadLine();
						listofFilms.CreateJson(line);
						break;
					case "2":
						Console.WriteLine("Вкажіть шлях");
						line = Console.ReadLine();
						listofFilms.CreateCSV(line);
						break;
					default:
						throw new Exception("Неіснуюча опція");
				}
			}
            catch(Exception ex)
            {
                ReturnToOptions(ex);
            }
		}
        public void ClearList()
        {
            listofFilms.FilmDeleter(true);
        }
        public void ReturnToOptions()
        {
            Console.WriteLine("\nНатисніть будь яку кнопку");
            Console.ReadLine();
            Console.Clear();
            UserOptionsShow();
        }
        public void Exit()//Вихід
        {
            Environment.Exit(1);
        }
    }
}
