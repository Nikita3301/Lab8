using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab81
{
    class SPerform
    {
        public List<SPerform> Student = new List<SPerform>();
        public string Pib { get; set; }
        public string Year { get; set; }
        public double Id { get; set; }
        public int Rating { get; set; }
        public string Wishes { get; set; }
        private string Checkdate(string date)
        {
            DateTime dateTime;
            while (!DateTime.TryParse(date, out dateTime))
            {
                Console.WriteLine("Невiрне значення");
                Console.Write("Дата(дд.мм.рр): ");
                date = Console.ReadLine();
            }

            dateTime = Convert.ToDateTime(date);
            date = dateTime.ToShortDateString();
            return date;
        }
        private double Check_num(string number)
        {
            double result;
            while (!double.TryParse(number, out result))
            {
                Console.Write("Введіть номер ще раз:");
                number = Console.ReadLine();
            }

            result = Convert.ToDouble(number);
            return result;
        }
        private int Check_num1(string number)
        {
            int result;
            while (!int.TryParse(number, out result))
            {
                Console.Write("Введіть номер ще раз:");
                number = Console.ReadLine();
            }

            result = Convert.ToInt32(number);
            return result;
        }

        public void Sync()
        {
            int length = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab81\\Lab81\\StudentPerformance.txt").Length;
            for (int i = 0; i < length; i++)
            {
                string[] str = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab81\\Lab81\\StudentPerformance.txt");
                string line = str[i];
                var Pib = line.Substring(0, 30);
                var Year = line.Substring(31, 14);
                var Id = Convert.ToDouble(line.Substring(46, 14));
                var Rating = Convert.ToInt32(line.Substring(61, 6));
                var Wishes = line.Substring(68, 40);
                Student.Insert(i,new SPerform(){Pib=Pib,Year =Year,Id = Id,Rating = Rating,Wishes = Wishes});
            }
        }
        public void Add()
        {
            SPerform add= new SPerform();
            Console.InputEncoding = Encoding.Unicode;
            Console.WriteLine("Введіть ПІБ:");
            string pib = Console.ReadLine();
            while (string.IsNullOrEmpty(pib))
            {
                Console.WriteLine("Введіть ПІБ ще раз:");
                pib = Console.ReadLine();
            }
            add.Pib = pib;
            Console.WriteLine("Введіть дату народження:");
            string date = Console.ReadLine();
            add.Year = Checkdate(date);
            Console.WriteLine("Введіть номер залікової книжки(8 цифр):");
            string id = Console.ReadLine();
            add.Id = Check_num(id);
            while (add.Id.ToString(CultureInfo.CurrentCulture).Length != 8)
            {
                Console.WriteLine("Введіть номер ще раз:");
                id = Console.ReadLine();
                add.Id = Check_num(id);
            }
            Console.WriteLine("Введіть рейтинг студента(0-100):");
            string rating= Console.ReadLine();
            add.Rating = Check_num1(rating);
            while (add.Rating<0 || add.Rating>100)
            {
                Console.WriteLine("Введіть рейтинг ще раз");
                rating= Console.ReadLine();
                add.Rating = Check_num1(rating);
            }
            if (add.Rating >= 90)
            {
                add.Wishes = "Вiтаємо вiдмiнника";
            }

            else if (add.Rating >= 75 && add.Rating < 90)
            {
                add.Wishes = "Mожна вчитися краще";
            }

            else if (add.Rating < 75)
            {
                add.Wishes = "Варто більше уваги приділяти навчанню";
            }
            Console.WriteLine("\nЯкщо ви бажаєте зберегти змiни то натиснiть Enter, якщо нi, то будь-яку iншу клавiшу.");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Student.Add(new SPerform(){Pib = add.Pib,Year = add.Year,Id = add.Id, Rating = add.Rating,Wishes = add.Wishes});
                using (StreamWriter f = new StreamWriter("C:\\Users\\s\\RiderProjects\\Lab81\\Lab81\\StudentPerformance.txt",
                    true))
                    f.WriteLine("{0,-31}{1,-15}{2,-15}{3,-7}{4,-40}", add.Pib, add.Year, add.Id, add.Rating, add.Wishes);
                Console.WriteLine("Змiни збережено\n");
            }
            else
            {
                Console.WriteLine("\nЗмiни не збережено\n");
            }
            
            
        }

        public void Edit()
        {
           SPerform edit = new SPerform();
           int length = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab81\\Lab81\\StudentPerformance.txt").Length;
           Console.WriteLine("Номер рядку:");
           int number = Check_num1(Console.ReadLine());
           while (number > length || number <= 0)
           {
               Console.WriteLine("Номер рядку не може бути менше нуля або більше " + length );
               number = Check_num1(Console.ReadLine());
           }
           
           string[] str = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab81\\Lab81\\StudentPerformance.txt");
           string line = str[number - 1];
           edit.Pib = line.Substring(0, 30);
           edit.Year = line.Substring(31, 14);
           edit.Id = Convert.ToDouble(line.Substring(46, 14));
           edit.Rating = Convert.ToInt32(line.Substring(61, 6));
           edit.Wishes = line.Substring(68, 40);
           Console.WriteLine("Введiть номер елементу стовпчика, який потрібно змінити: ");
           int number1 = Check_num1(Console.ReadLine());
           while (number1 > 4 || number1 <= 0)
           {
               Console.WriteLine("Номер стовпчика не може бути менше нуля або більше чотирьох");
               number1 = Check_num1(Console.ReadLine());
           }
           if (number1 == 1)
           {
               Console.WriteLine("Введіть ПІБ:");
               string pib = Console.ReadLine();
               while (string.IsNullOrEmpty(pib))
               {
                   Console.WriteLine("Введіть ПІБ ще раз:");
                   pib = Console.ReadLine();
               }
               edit.Pib = pib;
           }

           if (number1 == 2)
           {
               Console.WriteLine("Введіть дату народження:");
               string date = Console.ReadLine();
               edit.Year = Checkdate(date);
           }

           if (number1 == 3)
           {
               Console.WriteLine("Введіть номер залікової книжки(8 цифр):");
               string id = Console.ReadLine();
               edit.Id = Check_num(id);
               while (edit.Id.ToString(CultureInfo.CurrentCulture).Length != 8)
               {
                   Console.WriteLine("Введіть номер ще раз:");
                   id = Console.ReadLine();
                   edit.Id = Check_num(id);
               }
           }

           if (number1 == 4)
           {
               Console.WriteLine("Введіть рейтинг студента(0-100):");
               string rating= Console.ReadLine();
               edit.Rating = Check_num1(rating);
               while (edit.Rating<0 || edit.Rating>100)
               {
                   Console.WriteLine("Введіть рейтинг ще раз");
                   rating= Console.ReadLine();
                   edit.Rating = Check_num1(rating);
               }
               if (edit.Rating >= 90)
               {
                   edit.Wishes = "Вiтаємо вiдмiнника";
               }

               else if (edit.Rating >= 75 && edit.Rating < 90)
               {
                   edit.Wishes = "Mожна вчитися краще";
               }

               else if (edit.Rating < 75)
               {
                   edit.Wishes = "Варто більше уваги приділяти навчанню";
               }
           }
           Console.WriteLine("\nЗбереження змін - Enter, відміна - будь-яка інша клавіша.");
           if (Console.ReadKey().Key == ConsoleKey.Enter)
           {
               Student.Insert((number-1),new SPerform(){Pib = edit.Pib,Year = edit.Year,Id = edit.Id, Rating = edit.Rating,Wishes = edit.Wishes});
               Student.RemoveAt(number);
               using (StreamWriter f = new StreamWriter("C:\\Users\\s\\RiderProjects\\Lab81\\Lab81\\StudentPerformance.txt",false))
                   for (int i = 0; i < length; i++)
                   {
                       if(i != (number - 1))f.WriteLine(str[i]);
                       else f.WriteLine("{0,-31}{1,-15}{2,-15}{3,-7}{4,-40}", edit.Pib, edit.Year, edit.Id, edit.Rating, edit.Wishes);
                        
                   }
               Console.WriteLine("Змiни збережено\n");
           }
           else
           {
               Console.WriteLine("\nЗмiни не збережено\n");
           }

        }

        public void Delete()
        {
            int length = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab81\\Lab81\\StudentPerformance.txt").Length;
            Console.WriteLine("Номер рядку:");
            int number = Check_num1(Console.ReadLine());
            while (number > length || number <= 0)
            {
                Console.WriteLine("Номер рядку не може бути менше нуля або більше " + (length));
                number = Check_num1(Console.ReadLine());
            }

            string[] str = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab81\\Lab81\\StudentPerformance.txt");
            Console.WriteLine("\nЗбереження змін - Enter, відміна - будь-яка інша клавіша.");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Student.RemoveAt(number-1);
                using (StreamWriter f = new StreamWriter("C:\\Users\\s\\RiderProjects\\Lab81\\Lab81\\StudentPerformance.txt"))
                    for (int i = 0; i < length; i++)
                    {
                        if (i != number - 1)
                        {
                            
                            f.WriteLine(str[i]);
                        }
                    }

                Console.WriteLine("Змiни збережено\n");
            } 
        }

        public void Output()
        {
            int length = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab81\\Lab81\\StudentPerformance.txt").Length;
            string[] str = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab81\\Lab81\\StudentPerformance.txt");
            Console.WriteLine("{0,-31}{1,-15}{2,-15}{3,-7}{4,-40}", "ПІБ", "Дата нар.", "Зал. кн. №","Рейтинг","  Повідомлення");
            foreach (var variable in Student)
            {
                Console.WriteLine("{0,-31}{1,-15}{2,-15}{3,-7}{4,-40}",variable.Pib ,variable.Year ,variable.Id ,variable.Rating ,variable.Wishes);
            }
        }

        public void Sorted()
        {
            List<SPerform> sorted = Student.OrderBy(one => one.Rating).ToList();
            Console.WriteLine("{0,-31}{1,-15}{2,-15}{3,-7}{4,-40}", "ПІБ", "Дата нар.", "Зал. кн. №","Рейтинг","  Повідомлення");
            sorted.Reverse();
            foreach (var variable in sorted)
            {
                Console.WriteLine("{0,-31}{1,-15}{2,-15}{3,-7}{4,-40}",variable.Pib ,variable.Year ,variable.Id ,variable.Rating ,variable.Wishes);
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding=Encoding.Unicode;
            SPerform sp = new SPerform();
            sp.Sync();
            while (true)
            {
                Console.OutputEncoding = Encoding.Unicode;
                Console.WriteLine("\nВибір режиму роботи: ");
                Console.WriteLine("Додавання записiв - Enter");
                Console.WriteLine("Редагування записiв - E");
                Console.WriteLine("Знищення записiв - Delete");
                Console.WriteLine("Виведення iнформацiї з файла на екран - Tab");
                Console.WriteLine("Сортування за рейтингом - S");
                ConsoleKeyInfo choice;
                choice = Console.ReadKey(true);
                if (choice.Key == ConsoleKey.Enter)
                    sp.Add();
                if (choice.Key == ConsoleKey.E)
                {
                    sp.Edit();
                }

                if (choice.Key == ConsoleKey.Delete)
                {
                    sp.Delete();
                }

                if (choice.Key == ConsoleKey.Tab)
                {
                    sp.Output();
                }

                if (choice.Key == ConsoleKey.S)
                {
                    sp.Sorted();
                }
            }
        }
    }
}
