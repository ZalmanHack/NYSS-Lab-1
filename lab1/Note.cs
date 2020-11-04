using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab1
{
    public class Note
    {
        private List<Record> _records = new List<Record>();
        
        public List<Record> Records { get { return _records; } set { _records = value; } }
        
        public Note()
        {

        }
        
        public bool SearchFromId(int id, out int index)
        {
            int left = 0;
            int right = Records.Count - 1;
            bool flag = false;
            int mid = 0;
            while (left <= right && flag != true)
            {
                mid = (left + right) / 2;
                if (Records[mid].Id == id) 
                    flag = true;
                if (Records[mid].Id > id) 
                    right = mid - 1; 
                else 
                    left = mid + 1;
            }
            index = mid;
            return flag;
        }
        
        private void GenericMenu(Dictionary<string, Action> menuPuncts, Action header)
        {
            Dictionary<string, Action>.Enumerator it;
            int index;
            bool IsDigit;
            do // работает до тех пора, пока пользователь не выберет существующий пункт меню
            {
                index = 0; // номер пункта меню (с 1 по N)
                header();
                Console.WriteLine();
                foreach (var item in menuPuncts) // выводим названия пуктов меню, которые являются ключами словаря menuPuncts
                    Console.WriteLine($" {++index} | {item.Key}");
                Console.WriteLine($" 0 | Выход");
                ShowAs.Improve("\n>> ");
                IsDigit = int.TryParse(Console.ReadLine(), out index); // считываем номер меню с клавиатуры
                Console.Clear();
                if (IsDigit && index <= menuPuncts.Count)
                {
                    if (index == 0)
                        return;
                    it = menuPuncts.GetEnumerator();
                    for (int i = 0; i < index; i++) // перемещаем энумератор на нужную позицию пункта меню
                        it.MoveNext();
                    it.Current.Value(); // вызываем метод для этого пункта
                }
                else
                    ShowAs.Error("Команда не найдена!\nПопробуйте еще раз.\n");
            } while (true);
        }
        
        public void ShowMenu()
        {
            Dictionary<string, Action> menu = new Dictionary<string, Action>();
            menu.Add("Добавить запись", delegate { Add(); });
            menu.Add("Подробнее", delegate { ShowCanvas(); });
            menu.Add("Редактировать", delegate { Edit(); });
            menu.Add("Удалить", delegate { Delete(); });
            // генерируем таблицу с записями
            Action header = delegate {
                Console.WriteLine(Record.ShowHeader());
                foreach (var row in Records)
                    Console.WriteLine(row.ShowInfo());
            };
            GenericMenu(menu, header);
        }
        
        public void Add() 
        {
            Record record = new Record();
            Console.WriteLine("Создание новой записи №{0}", record.Id);
            record.SetFirstName();
            record.SetLastName();
            record.SetPhone();
            record.SetCountry();
            Records.Add(record);
        }
        
        public void Edit()
        {
            int index, id;
            Console.Write("Введите id записи: ");
            if (int.TryParse(Console.ReadLine(), out id) && SearchFromId(id, out index))
            {
                Dictionary<string, Action> menu = new Dictionary<string, Action>();
                menu.Add("Фамилия", delegate { Records[index].SetLastName(); });
                menu.Add("Имя", delegate { Records[index].SetFirstName(); });
                menu.Add("Отчество", delegate { Records[index].SetMiddName(); });
                menu.Add("Дата рождения", delegate { Records[index].SetBornDate(); });
                menu.Add("Страна", delegate { Records[index].SetCountry(); });
                menu.Add("Телефон", delegate { Records[index].SetPhone(); });
                menu.Add("Должность", delegate { Records[index].SetPost(); });
                menu.Add("Организация", delegate { Records[index].SetClub(); });
                menu.Add("Прочее", delegate { Records[index].SetOther(); });
                Action header = delegate { Console.WriteLine(Records[index].ShowInfo(Record.ShowType.Canvas)); };
                GenericMenu(menu, header);
            }
            else
            {
                Console.Clear();
                ShowAs.Error("Id не найден.\n");
            }
        }
        
        public void Delete()
        {
            int index, id, symbol;
            Console.Write("Введите id записи: ");
            if (int.TryParse(Console.ReadLine(), out id) && SearchFromId(id, out index))
            {
                Console.WriteLine("Вы уверены, что хотите удалить запись {0}?\n 1 | Да.\n 2 | Нет.\n", Records[index].Id);
                if (int.TryParse(Console.ReadLine(), out symbol) && symbol == 1)
                    Records.RemoveAt(index);
                Console.Clear();
            }
            else
            {
                Console.Clear();
                ShowAs.Error("Id не найден.\n");
            }
        }
        
        public void ShowTable()
        {
            if (Records.Count < 1)
                return;
            Dictionary<string, Action> menu = new Dictionary<string, Action>();
            menu.Add("Подробная информация о записи", delegate { ShowCanvas(); });
            menu.Add("Назад", delegate { return; });
            // генерируем таблицу с записями
            Action header = delegate {
                Console.WriteLine(Record.ShowHeader());
                foreach (var row in Records)
                    Console.WriteLine(row.ShowInfo());
            };
            GenericMenu(menu, header);
        }

        public void ShowCanvas()
        {
            int index, id;
            Console.Write("Введите id записи: ");
            if (int.TryParse(Console.ReadLine(), out id) && SearchFromId(id, out index))
            {
                Dictionary<string, Action> menu = new Dictionary<string, Action>();
                // генерируем таблицу с записями
                Action header = delegate { Console.WriteLine(Records[index].ShowInfo(Record.ShowType.Canvas)); };
                GenericMenu(menu, header);
            }
            else
            {
                Console.Clear();
                ShowAs.Error("Id не найден.\n");
            }
        }
    }
}