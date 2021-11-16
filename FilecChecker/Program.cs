using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace FilecChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            //byte[] hash = CreateMD5();
            Console.WriteLine("1 - зафиксировать | 2 - проверить");
            string numb = Console.ReadLine();

            switch (numb)
            {
                case "1":
                    Fix();
                    break;
                case "2":
                    Check();
                    break;
            }

        }
        public static byte[] CreateMD5(string path)
        {

            using (FileStream fs = File.OpenRead(path))
            using (HashAlgorithm hashAlgorithm = MD5.Create())
            {
                byte[] hash = hashAlgorithm.ComputeHash(fs);
                return hash;
            }
        }

        public static void Fix()
        {
            Console.WriteLine("Путь к файлу(зафиксировать)");
            string path = Console.ReadLine();
            string hashFile = Prepare(path);

            string path2 = Directory.GetCurrentDirectory() + "\\check\\";
            DirectoryInfo di = Directory.CreateDirectory(path2);

            //изменить метод записи текста в файл

            StreamWriter sw = new StreamWriter(path2 + "fixxx.txt");
            sw.WriteLine(hashFile);
            sw.Close();
            Console.WriteLine("Зафиксировано");
            Console.ReadLine();
        }

        public static void Check()
        {
            Console.WriteLine("Путь к файлу(проверить)");
            string path = Console.ReadLine();
            string hashFile = Prepare(path);

            string path2 = Directory.GetCurrentDirectory() + "\\check\\fixxx.txt";
            string readText = File.ReadAllText(path2);

            if (readText == hashFile)
            {
                Console.WriteLine("Файл не изменён");
            }
            else
            {
                Console.WriteLine("Файл изменён");
            }

            Console.ReadLine();
        }

        public static string Prepare(string path)
        {
            byte[] hash = CreateMD5(path);

            string hashFile = "";
            int x = 0;
            hashFile = hashFile + hash[x].ToString();
            x++;
            while (x != 15)
            {
                hashFile += hash[x].ToString();
                x++;
            }

            return hashFile;
        }
    }
}
