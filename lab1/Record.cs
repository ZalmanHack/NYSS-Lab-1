using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab1
{
    public class Record
    {
        private static int _count = 0;
        private int _id;
        private DateTime _bornDate;
        private string _country;
        private string _firstName;
        private string _lastName;
        private string _middName;
        private string _phone;
        private string _post;
        private string _club;
        private string _other;
        
        public enum ShowType { Table, Canvas }
        
        public Record()
        {
            Record._count += 1;
            this._id = _count;
        }
        
        public Record(string lastName, string firstName, string phone, string country) : this()
        {
            LastName = lastName;
            FirstName = firstName;
            Phone = phone;
            Country = country;
        }
        
        public static int Count { get { return _count; } }
        
        public int Id { get { return _id; } }
        
        public DateTime BornDate { get { return _bornDate; } set { _bornDate = value; } }
        
        public string Country
        {
            get { return string.IsNullOrEmpty(_country) == true ? "" : _country; }
            set { _country = value.Length >= 1 ? value.Substring(0, 1).ToUpper() + value.Remove(0, 1) : value; }
        }
        
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value.Length >= 1 ? value.Substring(0, 1).ToUpper() + value.Remove(0, 1) : value; }
        }
        
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value.Length >= 1 ? value.Substring(0, 1).ToUpper() + value.Remove(0, 1) : value; }
        }
        
        public string MiddName
        {
            get { return _middName; }
            set { _middName = value.Length >= 1 ? value.Substring(0, 1).ToUpper() + value.Remove(0, 1) : value; }
        }
        
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = "";
                foreach (var symbol in value)
                    if (char.IsDigit(symbol))
                        _phone += symbol;
            }
        }
        
        public string Post
        {
            get { return _post; }
            set { _post = value.Length >= 1 ? value.Substring(0, 1).ToUpper() + value.Remove(0, 1) : value; }
        }
        
        public string Club
        {
            get { return _club; }
            set { _club = value.Length >= 1 ? value.Substring(0, 1).ToUpper() + value.Remove(0, 1) : value; }
        }
        
        public string Other
        {
            get { return _other; }
            set { _other = value; }
        }
        
        public string ShowInfo(ShowType type = ShowType.Table)
        {
            switch (type)
            {
                case ShowType.Table : 
                    return string.Format("|{0,6} |{1,15} |{2,15} |{3,15} |", Id, LastName, FirstName, Phone);
                case ShowType.Canvas:
                    string result = string.Format("{0,-15} {1}\n", "Фамилия:", LastName);
                    result += string.Format("{0,-15} {1}\n", "Имя:", FirstName);
                    result += string.Format("{0,-15} {1}\n", "Отчество:", MiddName);
                    result += string.Format("{0,-15} {1}\n", "Дата рождения:", BornDate.ToString("dd.MM.yyyy"));
                    result += string.Format("{0,-15} {1}\n", "Страна:", Country);
                    result += string.Format("{0,-15} {1}\n", "Телефон:", Phone);
                    result += string.Format("{0,-15} {1}\n", "Должность:", Post);
                    result += string.Format("{0,-15} {1}\n", "Организация:", Club);
                    result += string.Format("{0,-15} {1}\n", "Описание:", Other);
                    return result;
                default: return ToString();
            }
        }
        
        public static string ShowHeader()
        {
            return string.Format("|{0,6} |{1,15} |{2,15} |{3,15} |", "Id", "Фамилия", "Имя", "Телефон");
        }
        
        public override string ToString()
        {
            return this.ShowInfo();
        }
        
        private static string SetStrParam(string name = "параметр", bool IsImprove = false)
        {
            do
            {
                Console.Write("Введите {0}: ", name);
                string result = Console.ReadLine();
                Console.Clear();
                if (string.IsNullOrEmpty(result) && IsImprove)
                    ShowAs.Error("Некорректно введены данные.\nПовторите попытку.\n");
                else
                    return result;
            } while (true);
        }
        
        private static int SetIntParam(string name = "параметр", int min = 0, int max = 0)
        {
            int result;
            do
            {
                Console.Write("Введите {0}: ", name);
                if (!int.TryParse(Console.ReadLine(), out result) || result < min || result > max)
                    ShowAs.Error("Некорректно введены данные.\nПовторите попытку.\n");
                else return result;
            } while (true);
        }

        public void SetBornDate()
        {
            int year, month, day;
            year = SetIntParam("год", 1, DateTime.Now.Year);
            month = SetIntParam("месяц", 1, 12);
            day = SetIntParam("день", 1, DateTime.DaysInMonth(year, month));
            this.BornDate = new DateTime(year, month, day);
        }
        
        public void SetFirstName()
        {
            this.FirstName = SetStrParam("имя", true);
        }
        
        public void SetLastName()
        {
            this.LastName = SetStrParam("фамилия", true);
        }
        
        public void SetMiddName()
        {
            this.MiddName = SetStrParam("отчество");
        }
        
        public void SetCountry()
        {
            this.Country = SetStrParam("страну", true);
        }
        
        public void SetPost()
        {
            this.Post = SetStrParam("должность");
        }
        
        public void SetPhone()
        {
            do
            {
                this.Phone = SetStrParam("телефон", true);
                if(string.IsNullOrEmpty(this.Phone))
                    ShowAs.Error("Некорректно введены данные.\nПовторите попытку.\n");
            } while (string.IsNullOrEmpty(this.Phone));
        }
        
        public void SetOther()
        {
            this.Other = SetStrParam("прочее описание");
        }
        
        public void SetClub()
        {
            this.Other = SetStrParam("организацию");
        }
    }
}
