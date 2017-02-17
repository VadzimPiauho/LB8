using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace lb8
{
    class Program
    {
        [DllImport("msvcrt")]
        static extern int _getch();

        static void Main(string[] args)
        {
            bool go_on = true;
            string gorod = "38836";
            string site = "http://informer.gismeteo.by/rss/";
            try
            {
                while (go_on)
                {                
                Console.Clear();
                Console.WriteLine("****************************Меню*******************************"); 

                Console.WriteLine("1 - Создать XML документ исходных городов по заданию");
                Console.WriteLine("2 - Добавить город в XML документ");
                Console.WriteLine("3 - Отобразить города из XML документа");
                //Console.WriteLine("4 - Найти самую теплую погоду в представленных городах");
                Console.WriteLine("5 - Выбрать город и просматреть погоду на текущую дату");                
                Console.WriteLine("0 - Выход из программы");
                Console.WriteLine("**************************************************************");

                switch (_getch())
                {
                    case '1':
                            defaultCity();
                        break;
                    case '2':
                            addCity();
                        break;
                    case '3':
                        readDoc();
                            endCase();
                        break;
                    case '4':
                            selectMaxTemp();
                        break;
                    case '5':
                            readDoc();
                            selectCity(gorod,site);
                            endCase();
                            break;
                    case '0':
                        go_on = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор");
                        Thread.Sleep(1000);
                        break;
                }
                }
            }
        catch(Exception)
            {
                Console.WriteLine("Неверный ввод Завершение программы");
                Environment.Exit(0);
            }
        }

        private static void selectMaxTemp()
        {
            throw new NotImplementedException();
        }

        private static void defaultCity()
        {
            XmlTextWriter city = null;
            try
            {
                city = new XmlTextWriter("city.xml", System.Text.Encoding.Unicode);
                city.Formatting = Formatting.Indented;
                city.WriteStartDocument();
                city.WriteStartElement("CITY");
                city.WriteStartElement("city");
                city.WriteElementString("name", "Алмата");
                city.WriteElementString("number", "36870");
                city.WriteEndElement();
                city.WriteStartElement("city");
                city.WriteElementString("name", "Астана");
                city.WriteElementString("number", "35188");
                city.WriteEndElement();
                //city.WriteAttributeString("Image", "MyCar.jpeg");
                city.WriteStartElement("city");
                city.WriteElementString("name", "Ашхабад");
                city.WriteElementString("number", "38880");
                city.WriteEndElement();
                city.WriteStartElement("city");
                city.WriteElementString("name", "Баку");
                city.WriteElementString("number", "37850");
                city.WriteEndElement();
                city.WriteStartElement("city");
                city.WriteElementString("name", "Москва");
                city.WriteElementString("number", "27612");
                city.WriteEndElement();
                city.WriteStartElement("city");
                city.WriteElementString("name", "Вильнюс");
                city.WriteElementString("number", "26730");
                city.WriteEndElement();
                city.WriteStartElement("city");
                city.WriteElementString("name", "Душанбе");
                city.WriteElementString("number", "38836");
                city.WriteEndElement();
                city.WriteStartElement("city");
                city.WriteElementString("name", "Ереван");
                city.WriteElementString("number", "37789");
                city.WriteEndElement();
                city.WriteStartElement("city");
                city.WriteElementString("name", "Киев");
                city.WriteElementString("number", "33345");
                city.WriteEndElement();
                city.WriteStartElement("city");
                city.WriteElementString("name", "Кишинев");
                city.WriteElementString("number", "33815");
                city.WriteEndElement();
                city.WriteStartElement("city");
                city.WriteElementString("name", "Минск");
                city.WriteElementString("number", "26850");
                city.WriteEndElement();
                city.WriteStartElement("city");
                city.WriteElementString("name", "Рига");
                city.WriteElementString("number", "26422");
                city.WriteEndElement();
                city.WriteStartElement("city");
                city.WriteElementString("name", "Таллинн");
                city.WriteElementString("number", "26038");
                city.WriteEndElement();
                city.WriteStartElement("city");
                city.WriteElementString("name", "Ташкент");
                city.WriteElementString("number", "38457");
                city.WriteEndElement();
                city.WriteStartElement("city");
                city.WriteElementString("name", "Тбилиси");
                city.WriteElementString("number", "37549");
                city.WriteEndElement();
                city.WriteEndElement();
            }
            finally
            {
                if (city != null)
                    city.Close();
            }
        }

        private static void endCase()
        {
            Console.WriteLine("Нажмите любую клавишу...");
            _getch();
        }

        private static void selectCity(string gorod, string site)
        {
            //Console.WriteLine("Выберите город и введите его номер попорядку");
            //int j = Convert.ToInt32(Console.ReadLine());
            //XPathDocument doc2 = new XPathDocument($"city.xml");
            //XPathNavigator nav2 = doc2.CreateNavigator();
            //XPathNodeIterator iterator2 = nav2.Select("/CITY/city");
            //while (iterator2.MoveNext())
            //{
            //    if (j-1>0)
            //    {
            //        j--;
            //        continue;
            //    }
            //    XPathNodeIterator it = iterator2.Current.Select("name");
            //    it.MoveNext();
            //    string name = it.Current.Value;
            //    it = iterator2.Current.Select("number");
            //    it.MoveNext();
            //    string number = it.Current.Value;
            //    Console.WriteLine("Вы выбрали = Код - {1}  Город - {0}", name, number);
            //    gorod = number;
            //    break;
            //}
            Console.WriteLine("Выберите городи введите его код ");
            gorod = Console.ReadLine();
            XPathDocument doc = new XPathDocument($"{site}{gorod}.xml");
            XPathNavigator nav = doc.CreateNavigator();
            XPathNodeIterator iterator = nav.Select("/rss/channel/item");
            while (iterator.MoveNext())
            {
                XPathNodeIterator it = iterator.Current.Select("title");
                it.MoveNext();
                string name = it.Current.Value;
                it = iterator.Current.Select("description");
                it.MoveNext();
                string number = it.Current.Value;
                Console.WriteLine("Вы выбрали = Код - {1}  Город - {0}", name, number);
                string[] newDew = number.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < newDew.Length; i++)
                {
                    if (newDew[i].Contains("температура"))
                    {
                        Console.WriteLine(newDew[i].Replace(" ", ""));
                    }
                }
                break;
                //Console.WriteLine("{0} {1}", manufactured, model);
            }
        }

        private static void addCity()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load($"city.xml");
            XmlNode root = doc.DocumentElement;
            XmlNode city = doc.CreateElement("city");
            XmlNode elem1 = doc.CreateElement("name");
            XmlNode elem2 = doc.CreateElement("number");
            Console.WriteLine("Введите название города");
            XmlNode text1 = doc.CreateTextNode(Console.ReadLine());
            Console.WriteLine("Введите код города");
            XmlNode text2 = doc.CreateTextNode(Console.ReadLine());
            elem1.AppendChild(text1);
            elem2.AppendChild(text2);
            city.AppendChild(elem1);
            city.AppendChild(elem2);
            root.AppendChild(city);
            doc.Save($"city.xml");
            Console.WriteLine("Город добавлен");
            Thread.Sleep(1000);
        }

        private static void readDoc()
        {
            int i = 1;
            XPathDocument doc = new XPathDocument($"city.xml");
            XPathNavigator nav = doc.CreateNavigator();
            XPathNodeIterator iterator = nav.Select("/CITY/city");
            while (iterator.MoveNext())
            {
                XPathNodeIterator it = iterator.Current.Select("name");
                it.MoveNext();
                string name = it.Current.Value;
                it = iterator.Current.Select("number");
                it.MoveNext();
                string number = it.Current.Value;
                
                Console.WriteLine("{2} --- Код - {1}  Город - {0}", name, number,i);
                i++;
            }
        }
    }
}
