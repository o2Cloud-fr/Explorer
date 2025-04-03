using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Explorer
{
    public partial class Form1 : Form
    {
        private ImageList imageList;

        public Form1()
        {
            InitializeComponent();
            InitializeExplorer();
        }

        private void InitializeExplorer()
        {
            TreeView treeView = new TreeView();
            ListView listView = new ListView();
            SplitContainer splitContainer = new SplitContainer();

            InitializeImageList();

            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Panel1.Controls.Add(treeView);
            splitContainer.Panel2.Controls.Add(listView);
            this.Controls.Add(splitContainer);

            treeView.Dock = DockStyle.Fill;
            treeView.ImageList = imageList;
            treeView.BeforeExpand += TreeView_BeforeExpand;
            PopulateTreeView(treeView);
            treeView.AfterSelect += TreeView_AfterSelect;

            listView.Dock = DockStyle.Fill;
            listView.View = View.Details;
            listView.SmallImageList = imageList;
            listView.Columns.Add("Nom", 250);
            listView.Columns.Add("Type", 100);
            listView.Columns.Add("Taille", 100);
            listView.Columns.Add("Date de modification", 150);
            listView.DoubleClick += ListView_DoubleClick;
        }

        private void InitializeImageList()
        {
            imageList = new ImageList { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new Size(16, 16) };
            imageList.Images.Add(GetSystemIcon(Environment.GetFolderPath(Environment.SpecialFolder.Windows))); // Dossier
            imageList.Images.Add(SystemIcons.Application); // Fichier par défaut
        }

        private void PopulateTreeView(TreeView treeView)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    TreeNode driveNode = new TreeNode(drive.Name, 0, 0) { Tag = drive.RootDirectory.FullName };
                    driveNode.Nodes.Add("..."); // Ajoute un noeud temporaire
                    treeView.Nodes.Add(driveNode);
                }
            }
        }

        private void TreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            node.Nodes.Clear();

            try
            {
                foreach (string dir in Directory.GetDirectories(node.Tag.ToString()))
                {
                    TreeNode subNode = new TreeNode(Path.GetFileName(dir), 0, 0) { Tag = dir };
                    if (Directory.GetDirectories(dir).Length > 0)
                        subNode.Nodes.Add("..."); // Ajoute un noeud temporaire si des sous-dossiers existent
                    node.Nodes.Add(subNode);
                }
            }
            catch (Exception) { }
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            string path = selectedNode.Tag.ToString();
            ListView listView = (ListView)((SplitContainer)this.Controls[0]).Panel2.Controls[0];
            listView.Items.Clear();

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    ListViewItem item = new ListViewItem(dir.Name, 0) { Tag = dir.FullName };
                    item.SubItems.Add("Dossier");
                    item.SubItems.Add("");
                    item.SubItems.Add(dir.LastWriteTime.ToString());
                    listView.Items.Add(item);
                }

                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    int iconIndex = GetFileIconIndex(file.FullName);
                    ListViewItem item = new ListViewItem(file.Name, iconIndex) { Tag = file.FullName };
                    item.SubItems.Add(file.Extension);
                    item.SubItems.Add(FormatFileSize(file.Length));
                    item.SubItems.Add(file.LastWriteTime.ToString());
                    listView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private int GetFileIconIndex(string filePath)
        {
            Icon icon = GetSystemIcon(filePath);
            imageList.Images.Add(icon);
            return imageList.Images.Count - 1;
        }

        private Icon GetSystemIcon(string path)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            SHGetFileInfo(path, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON);
            return Icon.FromHandle(shinfo.hIcon);
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        private const uint SHGFI_ICON = 0x000000100;
        private const uint SHGFI_SMALLICON = 0x000000001;

        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public string szTypeName;
        }

        private string FormatFileSize(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int counter = 0;
            decimal number = bytes;

            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }

            return string.Format("{0:n1} {1}", number, suffixes[counter]);
        }

        private void ListView_DoubleClick(object sender, EventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView.SelectedItems.Count > 0)
            {
                string filePath = listView.SelectedItems[0].Tag.ToString();

                try
                {
                    if (Directory.Exists(filePath))
                    {
                        Process.Start(new ProcessStartInfo()
                        {
                            FileName = filePath,
                            UseShellExecute = true
                        });
                    }
                    else if (File.Exists(filePath))
                    {
                        Process.Start(new ProcessStartInfo()
                        {
                            FileName = filePath,
                            UseShellExecute = true
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Impossible d'ouvrir l'élément : " + ex.Message);
                }
            }
        }
    }
}