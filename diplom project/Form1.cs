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

namespace diplom_project
{
    public partial class countFiles : Form
    {
        public countFiles()
        {
            InitializeComponent();
            label1.Text = "0";

            var d = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);//получили все папки в ProgramFiles

            var files = Directory.GetFiles(d);//получили все файлы в ProgramFiles
            var folders = Directory.GetDirectories(d);

            foreach (var i in files)
            {
                listBox1.Items.Add(i);//вывели их в ListBox
            }
        }
    }
}
