using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using diplom_project.DB;

namespace diplom_project
{
    public partial class countFiles : Form
    {

        public countFiles()
        {
            InitializeComponent();           label1.Text = "0";

            //var d = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);//получили все папки в ProgramFiles


            //var files = Directory.GetFiles(d);//получили все файлы в ProgramFiles
            //var folders = Directory.GetDirectories(d);//получили все папки в ProgramFiles


            //foreach (var i in files)
            //{
            //    listBox1.Items.Add(i);//вывели их в ListBox
            //}



            //foreach (var i in files)
            //    listBox1.Items.Add(i);

            //listBox1.DataSource = files;


            //List<Model> files = new List<Model>();
            //var d = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);//получили все папки в ProgramFiles

            //GetAll(d, files);

            //dataGridView1.DataSource = files;


            Connection c = new Connection();
            var v = c.Files.ToArray();
        }



        void GetAll(string dir, List<Model> data)
        {
            string[] files = null;
            try
            {
                files = Directory.GetFiles(dir); //получили все файлы в ProgramFiles
            } catch (Exception ex) { }
            if (files != null)
                foreach(var e in files)
                data.Add(new Model { Filename = e });

            string[] folders = null;
            try
            {
                folders = Directory.GetDirectories(dir);//получили все папки в ProgramFiles
            }
            catch
            {
                return;
            }

            if (folders != null)
                foreach (var f in folders)
                    GetAll(f, data);
        }

    }
}
