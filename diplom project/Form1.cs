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
using System.Threading;

namespace diplom_project
{
    public partial class countFiles : Form
    {
        List<Model> files = new List<Model>();
        static int ccx;
        static Thread current;

        static Label l;

        public countFiles()
        {
            InitializeComponent();
            current = Thread.CurrentThread;
            l = label1;

            label1.Text = ccx.ToString();


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

            Thread t = new Thread(startThread);
            t.Start();                
                
            //GetAll(d, files);

            dataGridView1.DataSource = files;

            

            //Connection c = new Connection();
            //var v = c.Files.ToArray();
        }
        
        void startThread()
        {
            var d = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);//получили все папки в ProgramFiles
            GetAll(d, files);
           
        }

        public delegate void setlabel(int a);

       static void GetAll(string dir, List<Model> data)
        {
           
            string[] files = null;
            try
            {
                files = Directory.GetFiles(dir); //получили все файлы в ProgramFiles
            } catch (Exception ex) { }
            if (files != null)
                foreach (var e in files)
                {
                    var m = new Model { Filename = e };
                    //var d = m.GetHash();
                    int v = m.Id;
                    data.Add(m);                    
                }

            string[] folders = null;
            try
            {
                folders = Directory.GetDirectories(dir);//получили все папки в ProgramFiles
            }
            catch
            {
                return;
            }

            ccx =+ data.Count();

            l.BeginInvoke(new setlabel(a => { l.Text = a.ToString(); }), new object[] { ccx });

            if (folders != null)
                foreach (var f in folders)
                    GetAll(f, data);
            
        }



    }
}
