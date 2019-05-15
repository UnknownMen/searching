using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace diplom_project.DB
{
    class Saver
    {
        BinaryWriter bw;
        BinaryReader br;

        public void Save (List<Model> files)
        {
            try
            {
                bw = new BinaryWriter(new FileStream(@"C:\Temp2\mydata.txt", FileMode.Append));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot create file.");
                return;
            }
            try
            {
                foreach(var file in files)
                {
                    bw.Write(file.Id);
                    bw.Write(file.Filename);
                    bw.Write(file.Hash);
                    bw.Write(file.ShortName);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot write to file.");
                return;
            }
            bw.Close();

        }
    }
}
