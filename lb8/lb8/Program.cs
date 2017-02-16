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
            string gorod = null;
            string site = "http://informer.gismeteo.by/rss/";
            try
            {
                Console.Clear();
                Console.WriteLine("****************************Меню******************************"); 

                Console.WriteLine("1 - Создать XML документ исходных городов по заданию");
                Console.WriteLine("2 - Добавить город в XML документ");
                Console.WriteLine("3 - Отобразить города из XML документа");
                Console.WriteLine("4 - Найти самую теплую погоду в представленных городах");
                Console.WriteLine("5 - Выбрать город и просматреть погоду на текущую дату");
                Console.WriteLine("0 - Выход из программы");
                Console.WriteLine("**************************************************************");

                switch (_getch())
                {
                    case '1':
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
                        break;
                    case '2':
                       

                        break;
                    case '3':
                        gorod = "city.xml";
                        readDoc(gorod);
                        _getch();
                        break;
                    case '4':
                        gorod = "";

                        break;
                    case '5':
                        XPathDocument doc = new XPathDocument($"{site}{gorod}.xml");
                        XPathNavigator nav = doc.CreateNavigator();
                        XPathNodeIterator iterator = nav.Select("/rss/channel/item");
                        while (iterator.MoveNext())
                        {
                            XPathNodeIterator it = iterator.Current.Select("title");
                            it.MoveNext();
                            string manufactured = it.Current.Value;
                            it = iterator.Current.Select("description");
                            it.MoveNext();
                            string model = it.Current.Value;
                            string[] newDew = model.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                            for (int i = 0; i < newDew.Length; i++)
                            {
                                if (newDew[i].Contains("температура"))
                                {
                                    Console.WriteLine(newDew[i].Replace(" ", ""));
                                }
                            }
                            //Console.WriteLine("{0} {1}", manufactured, model);
                        }

                        break;
                    case '6':
                        gorod = "";

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
        catch

        (Exception)
            {
                Console.WriteLine("Неверный ввод Завершение программы");
                Environment.Exit(0);
            }
        }

        private static void readDoc(string gorod)
        {
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
                
                Console.WriteLine("Код - {1}  Горо - {0}", name, number);
            }
        }
    }
}
