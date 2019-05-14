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
using System.Timers;
using System.Diagnostics;


namespace diplom_project
{
    public partial class countFiles : Form
    {
        List<Model> files = new List<Model>();
        static int ccx;
        static Thread current;
        static string pathFinder;

        static Label l;
        static Label l2;
        static ProgressBar w;
        static string NameOfHash;
        
        Random rnd = new Random();

        private System.Timers.Timer aTimer;

        public countFiles()
        {
            InitializeComponent();
            current = Thread.CurrentThread;
            l = label1;
            l2 = label4;
            w = progressBar1;
            NameOfHash = "";
            comboBox1.Text = comboBox1.Items[1].ToString();
            
            SetTimer();

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

            //MainStart();

            // Thread t = new Thread(startThread);
            //t.Start();

            //GetAll(d, files);

            //dataGridView1.DataSource = "";           

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

        private void SetTimer()
        {
            aTimer = new System.Timers.Timer(100);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

        }

        private void OnTimedEvent(Object source, EventArgs e)
        {
            //countFiles s = (countFiles)source;
            btnOpen.BackColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
        }
       
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

        void MainStart()
        {
            Thread t = new Thread(startThread);
            t.Start();
            //dataGridView1.DataSource = files; в данном варианте были ошибки с запонением и была добавлено условие в лямбду функцию стр158
        }
        
        void startThread()
        {
            string pa = pathFinder;
            GetAll(pa, files);

            try
            {
                w.BeginInvoke(new setProgressBar(b => { w.Minimum = 0; w.Maximum = b; }), new object[] { files.Count });
            }
            catch
            {
                return;
            }

            int i = 0;
            foreach (var file in files)
            {
                if (NameOfHash == "MD5") file.Hash = file.GetHashMD5();
                else { file.Hash = file.GetHash32(); }
                i++;
                w.BeginInvoke(new setProgressBar(b => { w.Value = b; l2.Text = b.ToString(); if (b == w.Maximum) dataGridView1.DataSource = files; }), new object[] { i });
            }
        }

        public delegate void setlabel(int a);
        public delegate void setProgressBar(int b);

        static void GetAll(string dir, List<Model> data)
        {
           
            string[] files = null;
            try
            {
                files = Directory.GetFiles(dir); //получили все файлы в папке
            } catch (Exception ex) { }
            if (files != null)
                foreach (var e in files)
                {
                    var m = new Model { Filename = e };
                    //var d = m.GetHash();первоначальный вариант когда хеш считался сразу
                    int v = m.Id;
                    data.Add(m);                    
                }

            string[] folders = null;
            try
            {
                folders = Directory.GetDirectories(dir);//получили все папки в выбранной директории
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
            if (NameOfFind == "")
            {
                MessageBox.Show("Выберите место для поиска");
                return;
            }
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
            //dataGridView1.DataSource = null;
            dataGridView1.DataSource = files;
            textBox1.Text = "";
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {            
            if (dataGridView1.DataSource != null)
            {
                List<Model> temp = new List<Model>();
                Model.next = 0;
                dataGridView1.DataSource = temp;
                ccx = 0;
                files.Clear();
            }
            aTimer.Stop();
            aTimer.Dispose();

            folderBrowserDialog1.ShowDialog();
                        
            Object selectedItem = comboBox1.SelectedItem;
            NameOfHash = selectedItem.ToString();           

            pathFinder = folderBrowserDialog1.SelectedPath;
            label6.Text = pathFinder;

            MainStart();
        }
               
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var element = files[e.RowIndex];

            ProcessStartInfo pi = new ProcessStartInfo(element.Filename);
            pi.Arguments = Path.GetFileName(element.Filename);
            pi.UseShellExecute = true;
            pi.WorkingDirectory = Path.GetDirectoryName(element.Filename);
            pi.FileName = element.Filename;
            pi.Verb = "OPEN";
            Process.Start(pi);

            //MessageBox.Show(element.ShortName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Saver sa = new Saver();
            sa.Save(files);
        }
    }
}
