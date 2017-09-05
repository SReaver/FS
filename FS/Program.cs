using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS
{
    class Program
    {
        public static int Fibbonachi(int n)
        {
          return n>1?Fibbonachi(n-1)+Fibbonachi(n-2):n;
        }
        static void Main(string[] args)
        {
            #region
            Console.Write("Введите путь к файлу: ");
            string path = Console.ReadLine();
            NewFile(path);
            IEnumerable<int> mas = ReadFile();

            var t = mas.Count();
            string text = "";
            foreach (var item in mas)
            {
                text += item + " ";
            }
            for (int i = t; i < t * 2; i++)
            {
                text = text + Fibbonachi(i) + " ";
            }

            if (string.IsNullOrEmpty(path))
                path = Directory.GetCurrentDirectory();

            WriteFile(path + @"\OUTPUT.TXT", text);
            #endregion
            ReadFileByte(path + @"\OUTPUT.TXT");
        }
        static void NewFile(string pathString = "")
        {
            if (string.IsNullOrEmpty(pathString))
            {
                pathString = Directory.GetCurrentDirectory();
            }
            // Создаем новый файл
            //FileInfo f = new FileInfo(pathString + @"\INPUT.TXT");
            FileStream fs = File.Create(pathString + @"\INPUT.TXT");
            fs.Close();

            string text = "";
            for (int i = 0; i < 6; i++)
            {
                text = text + Fibbonachi(i) + " ";
            }
            WriteFile(pathString + @"\INPUT.TXT", text);

        }
        static void WriteFile(string pathString, string text)
        {
         
            using (StreamWriter sw = new StreamWriter(pathString))
            {
                sw.Write(text);

            }
        }
        static IEnumerable<int> ReadFile(string pathString = "")
        {
            IEnumerable<int> mas = null;
            if (string.IsNullOrEmpty(pathString))
                pathString = Directory.GetCurrentDirectory();

            using (StreamReader SR = new StreamReader(pathString + @"\INPUT.TXT"))
            {
                string sb="";
                //sb = SR.ReadToEnd();
                //Console.WriteLine(sb);
               
                while ((sb=SR.ReadLine())!=null)
                {
                    mas = sb.Trim().Split(' ').Select(s => Int32.Parse(s.ToString()));//.ToArray();
                }
            }
            return mas;
        }
        static void ReadFileByte(string path)
        {
            if (string.IsNullOrEmpty(path))
                path = Directory.GetCurrentDirectory();
            // создаем объект BinaryReader
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                List<byte> lb = new List<byte>();
                Dictionary<byte, int> jack = new Dictionary<byte, int>();
                int count = 0;
                
              
                // пока не достигнут конец файла
                // считываем каждое значение из файла
                while (reader.PeekChar() > -1)
                {
                    lb.Add(reader.ReadByte());
                }

                foreach (var item in lb.GroupBy(g => g))
                {
                    jack.Add(item.Key, lb.Count(w => w == item.Key));
                }
                Console.WriteLine("\t" + "Слово\t" + "Количество\t");
                int c = 1;
                foreach (var item in jack)
                {
                    Console.WriteLine($"{c++},\t {item.Key}, \t {item.Value} \t");

                }
            }
        }
    }
}
