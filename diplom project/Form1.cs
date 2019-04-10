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
        static Label l2;
        static ProgressBar w;

        public countFiles()
        {
            InitializeComponent();
            current = Thread.CurrentThread;
            l = label1;
            l2 = label4;
            w = progressBar1;

            //label1.Text = ccx.ToString();
            
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

        /**
         * 
         * 1 qq 
         * 2 ff 
         * 3 rr
         * 4 hh
         * 5 qq
         * 
         * qq
         *  1
         *  5
         * ff
         *  2
         * rr
         *  3
         * 
         * 
         **/

        static List<Model> sortList(List<Model> ff)
        {
            var elements = ff.GroupBy(f => f.Hash).ToArray();

            List<Model> sorterOne = new List<Model>();
            List<Model> sorterTwo = new List<Model>();

            foreach(var e in elements)
            {
                if (e.Count() > 1)
                    foreach (var rr in e)
                        sorterTwo.Add(rr);
                else
                    sorterOne.Add(e.First());
            }

            List<Model> mm = new List<Model>();
            mm.AddRange(sorterTwo);
            mm.AddRange(sorterOne);

            return mm;

        }
        
        void startThread()
        {
            var d = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);//получили все папки в ProgramFiles
            GetAll(d, files);

            w.BeginInvoke(new setProgressBar(b => { w.Minimum = 0; w.Maximum = b; }), new object[] { files.Count });

            int i = 0;
            foreach (var file in files)
            {
                file.Hash = file.GetHash();
                i++;
                w.BeginInvoke(new setProgressBar(b => { w.Value = b; l2.Text = b.ToString(); }), new object[] { i });
            }

        }

        public delegate void setlabel(int a);
        public delegate void setProgressBar(int b);

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

            //l.BeginInvoke(new setlabel(a => { l.Text = a.ToString(); }), new object[] { ccx });//1 вариант - а потом обернул в блок try catch
            //w.BeginInvoke(new setProgressBar( b => { w.Minimum = 0; w.Maximum = b; w.Value = 0; }), new object[] { ccx });
            try
            {
                l.BeginInvoke(new setlabel(a => { l.Text = a.ToString(); }), new object[] { ccx });
            }
            catch
            {
                return;
            }

            if (folders != null)
                foreach (var f in folders)
                    GetAll(f, data);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string NameOfFind = "";
            NameOfFind = textBox1.Text.ToLower();
            List<Model> finderFiles = new List<Model>();
            //foreach (var file in files)
            //{
            //    if( NameOfFind == file.ShortName)
            //    {
            //        finderFiles.Add(file);
            //    }
            //}

            finderFiles = files.Where(f => f.ShortName.ToLower().Contains(NameOfFind)).ToList();

            dataGridView1.DataSource = sortList(finderFiles);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = "";
            dataGridView1.DataSource = files;
        }
    }
}
