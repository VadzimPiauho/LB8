using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            string site = "http://informer.gismeteo.by/rss/36570.xml";
            try
            {
                    Console.Clear();
                    Console.WriteLine("*******Меню*********");
                    Console.WriteLine("***Выберите город***");
                    Console.WriteLine("1 - Алма-Ата");
                    Console.WriteLine("2 - Астана");
                    Console.WriteLine("3 - Ашхабад");
                    Console.WriteLine("4 - Баку");
                    Console.WriteLine("5 - Москва");
                    Console.WriteLine("6 - Вильнюс");
                    Console.WriteLine("7 - Душанбе");
                    Console.WriteLine("8 - Ереван");
                    Console.WriteLine("9 - Киев");
                    Console.WriteLine("a - Кишинев");
                    Console.WriteLine("b - Минск");
                    Console.WriteLine("c - Рига");
                    Console.WriteLine("f - Таллинн");
                    Console.WriteLine("g - Ташкент");
                    Console.WriteLine("k - Тбилиси");
                    Console.WriteLine("0 - Выход из программы");
                    Console.WriteLine("*******************************");

                    switch (_getch())
                    {
                        case '1':
                            gorod = "36870";
                            
                            break;
                        case '2':
                            gorod = "35188";
                            
                            break;
                        case '3':
                            gorod = "38880";
                           
                            break;
                        case '4':
                            gorod = "37850";
                           
                            break;
                        case '5':
                            gorod = "27612";
                            
                            break;
                        case '6':
                            gorod = "26730";
                            
                            break;
                        case '7':
                            gorod = "38836";
                            
                            break;
                        case '8':
                            gorod = "37789";
                            
                            break;
                        case '9':
                            gorod = "33345";
                            
                            break;
                        case 'a':
                            gorod = "33815";
                            
                            break;
                        case 'b':
                            gorod = "26850";
                            
                            break;
                        case 'c':
                            gorod = "26422";
                            
                            break;
                        case 'f':
                            gorod = "26038";
                            
                            break;
                        case 'g':
                            gorod = "38457";
                            
                            break;
                        case 'k':
                            gorod = "37549";
                            
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
            catch (Exception)
            {
                Console.WriteLine("Неверный ввод Завершение программы");
                Environment.Exit(0);
            }
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
        }
    }
}
