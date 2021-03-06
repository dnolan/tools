﻿using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("Please specify a directory");
                return;
            }

            var dir = new DirectoryInfo(path);

            if (!dir.Exists)
            {
                MessageBox.Show(string.Format("Directory {0} not found", path));
                return;
            }

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
            var path = ScanPath.Text;

            FileListView.Items.Clear();

            ScanDir(path);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (FileListView.Items.Count == 0)
            {
                MessageBox.Show("No empty folders found");
                return;
            }

            if (FileListView.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select some directories to delete.");
                return;
            }

            var prompt = MessageBox.Show("Are you sure?", "No return...", MessageBoxButtons.YesNo);

            if (prompt == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            foreach (ListViewItem lvi in FileListView.CheckedItems)
            {
                if (Directory.Exists(lvi.Text))
                {
                    Directory.Delete(lvi.Text);
                }
            }

            FileListView.Items.Clear();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            FileListView.Items.Clear();
        }
    }
}
