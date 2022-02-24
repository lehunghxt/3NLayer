using Shop.Domain;
using Shop.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer
{
    class Program
    {
        static void Main(string[] args)
        {

            bool isKeep = true;
            while (isKeep)
            {
                PrintMenu();
                string value = Console.ReadLine();
                Console.Clear();
                switch (value)
                {
                    case "1":
                        ListUser();
                        break;
                    case "2":
                        AddUser();
                        break;
                    case "END":
                        isKeep = false;
                        break;
                }
                Console.WriteLine("Tiep tuc ?");

            }

        }
        public static void PrintMenu()
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("Menu:");
            Console.WriteLine("\t 1: Danh sach User");
            Console.WriteLine("\t 2: Add User");
            Console.WriteLine("\t 3: Edit User");
            Console.WriteLine("\t 4: Delete User");
            Console.WriteLine("==========================================================");
        }
        public static void ListUser()
        {
            UserBLL bll = new UserBLL();
            var users = bll.GetListUser();
            Console.WriteLine("Danh sach "+ users.Count +" User");
            foreach (var item in users)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Id {0} \t Username {1} \t Password {2} \t Address {3} \t Age \t {4}", item.Id, item.Username, item.Password, item.Address, item.Age);
            }
            Console.ResetColor();
        }
        public static void AddUser()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            UserBLL bll = new UserBLL();
            UserModel user = new UserModel();
            Console.WriteLine("Username:");
            user.Username = Console.ReadLine();
            Console.WriteLine("Password:");
            user.Password = Console.ReadLine();
            Console.WriteLine("Address:");
            user.Address = Console.ReadLine();
            Console.WriteLine("Age:");
            user.Age = int.Parse(Console.ReadLine());
            bll.AddUser(user);
            Console.WriteLine("Them thanh cong.");
            Console.ResetColor();
        }
    }
}
