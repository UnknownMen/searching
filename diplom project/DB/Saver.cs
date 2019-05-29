using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace diplom_project.DB
{    
    class Saver
    {
        static public string pathWithEnv = @"%USERPROFILE%\mydata.json";
        static public string filePath = Environment.ExpandEnvironmentVariables(pathWithEnv);
        //BinaryWriter bw;
        //BinaryReader br;

        //public void Save(List<Model> files)
        //{
        //    try
        //    {
        //        bw = new BinaryWriter(new FileStream(@"C:\Temp2\mydata.txt", FileMode.Append));
        //    }
        //    catch (IOException e)
        //    {
        //        Console.WriteLine(e.Message + "\n Cannot create file.");
        //        return;
        //    }
        //    try
        //    {
        //        bw.Write(DateTime.Now.ToString());

        //        foreach (var file in files)
        //        {
        //            bw.Write(file.Id);
        //            //bw.Write(file.Filename);
        //            //bw.Write(file.Hash);
        //            bw.Write(file.ShortName);
        //        }
        //    }
        //    catch (IOException e)
        //    {
        //        Console.WriteLine(e.Message + "\n Cannot write to file.");
        //        return;
        //    }
        //    bw.Close();

        //}
        DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Model>));

        public void Save(List<Model> files)
        {           
            //to do проверку, если нет такого файла - создать, иначе добавить
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {                
                jsonFormatter.WriteObject(fs, files);
            }

        }

        public List<Model> Write()
        {
            List < Model > rew = new List<Model>();
            //сделать проверку на существование
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                rew = (List<Model>)jsonFormatter.ReadObject(fs);
                
            }
            return rew;

        }

    }
}
