﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EmptyFolder
{
    public partial class FileForm : Form
    {
        public FileForm()
        {
            InitializeComponent();


        }

        public void ScanDir(string path)
        {
            var dir = new DirectoryInfo(path);

            if (dir.Name.ToLower() == "_lightroom")
            {
                return;
            }

            var empty = dir.GetFileSystemInfos().Count() == 0;


            if (!empty)
            {
                foreach (var f in dir.GetDirectories())
                {
                    ScanDir(f.FullName);
                }
            }

            if (empty)
            {
                FileListView.Items.Add(path);
            }
        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            var path = "D:\\PhotosTest";

            FileListView.Items.Clear();

            ScanDir(path);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in FileListView.Items)
            {
                if (Directory.Exists(lvi.Text))
                {
                    Directory.Delete(lvi.Text);
                }
            }

            FileListView.Items.Clear();
        }
    }
}