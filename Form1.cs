using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _09
{
    public partial class Form1 : Form
    {
        ImageList largeGalery = new ImageList();
        ImageList smalGalery = new ImageList();

        public string opendFile = "";
        string text,text2;
        ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
        
        public Form1()
        {
            InitializeComponent();
            textBox1.MouseClick += new MouseEventHandler(textBox1_MouseClick);

            listView1.Columns.Add("Ім'я", 300);
            listView1.Columns.Add("Формат", 300);
            listView1.Columns.Add("Дата", 300);
            listView1.Columns.Add("Розмір", 300);
            listView1.Columns.Add("Атрибут", 300);
            listView1.Columns.Add("Кількість файлів", 300);


            largeGalery.ImageSize = new Size(150, 150);
            smalGalery.ImageSize = new Size(50, 50);
            

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.UtcNow;
            DateTime date3 = DateTime.Today;
            string[] date = { $"{date1},{date2},{date3}" };

            

            listView1.View = View.Details;


            textBox1.AllowDrop = false;
            //textBox1.DragDrop += RichTextBox1_DragDrop;

            //textBox1.DragEnter += RichTextBox1_DragEnter;

            treeView1.AfterSelect += TreeView1_AfterSelect;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string drive in Directory.GetLogicalDrives())
            {
                TreeNode rootNode = new TreeNode(drive);
                rootNode.Tag = drive;
                treeView1.Nodes.Add(rootNode);

                try
                {
                    foreach (string directory in Directory.GetDirectories(drive))
                    {
                        TreeNode childNode = new TreeNode(Path.GetFileName(directory));
                        childNode.Tag = directory;
                        rootNode.Nodes.Add(childNode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }            
        }

        

    private void TreeView1_AfterSelect(object? sender, TreeViewEventArgs e)
    {
        listView1.Items.Clear();
        if (e.Node.Tag is string)
        {
            string path = (string)e.Node.Tag;
            try
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    FileInfo fileInfo = new FileInfo(file);

                    ListViewItem item = new ListViewItem(fileInfo.Name);
                    item.SubItems.Add(Path.GetExtension(fileInfo.Name));
                    item.SubItems.Add(fileInfo.CreationTime.ToString());
                    item.SubItems.Add(fileInfo.Length.ToString());
                    item.SubItems.Add(fileInfo.Attributes.ToString());
                    item.SubItems.Add(fileInfo.Attributes.ToString());

                    item.Tag = file;

                    listView1.Items.Add(item);
                }
                int numFiles = Directory.GetFiles(path).Length;
                foreach (string directory in Directory.GetDirectories(path))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(directory);

                    ListViewItem item = new ListViewItem(directoryInfo.Name);
                    item.SubItems.Add("");
                    item.SubItems.Add(directoryInfo.CreationTime.ToString());
                    item.SubItems.Add(directoryInfo.Attributes.ToString() + " ");
                    item.SubItems.Add("");
                    item.SubItems.Add(numFiles.ToString() + " files");

                    item.Tag = directory;

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

    }

    //private void RichTextBox1_DragEnter(object? sender, DragEventArgs e)
    //{
    //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
    //    {
    //        e.Effect = DragDropEffects.Copy;
    //    }
    //}
        //private void RichTextBox1_DragDrop(object? sender, DragEventArgs e)
        //{

        //    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        //    if (files.Length > 0 && File.Exists(files[0]))
        //    {
        //        textBox1.Text = File.ReadAllText(files[0]);
        //    }
        //}



        public void seveFilesAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                opendFile = sfd.FileName;
                seveFile(sfd.FileName);
            }
        }

        private void seveFile(string path)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.Write(textBox1.Text);
            writer.Close();
        }

        private void seveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (opendFile == "")
            {
                seveFilesAs();
            }
            else seveFile(opendFile);
        }

        private void seveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            seveFilesAs();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files(*.*)|*.*|Text Files(*.txt)|*.txt";
            openFileDialog.FilterIndex = 2;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                opendFile = openFileDialog.FileName;
                StreamReader rider = new StreamReader(openFileDialog.FileName);
                textBox1.Text = rider.ReadToEnd();
                rider.Close();
            }
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = textBox1.Font;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {

                textBox1.Font = fontDialog.Font;

            }
        }

        private void колірШрифтуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = textBox1.ForeColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {

                textBox1.ForeColor = cd.Color;

            }
        }

        private void колірФонуToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ColorDialog cd = new ColorDialog();
            cd.Color = textBox1.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {

                textBox1.BackColor = cd.Color;

            }
        }

        private void новийДокументToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void копіюватиToolStripMenuItem_Click(object sender, EventArgs e)
        {

            textBox1.Copy();
        }

        private void вставитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cursorPosition = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(cursorPosition, Clipboard.GetText());
            textBox1.SelectionStart = cursorPosition + Clipboard.GetText().Length;
        }

        private void вирізатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }

        private void відмінитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void contextMenuStrip1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(textBox1, e.Location);
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(textBox1, e.Location);
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
             
            MenuStrip menuStrip = new MenuStrip();
            toolStripMenuItem= new ToolStripMenuItem(text);
            menuStrip2.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            
            menuStrip.Items.Add(toolStripMenuItem);
            this.Controls.Add(menuStrip);
            toolStripMenuItem.MergeAction = MergeAction.Insert;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            text2= textBox3.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem(text2);

            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            text = textBox2.Text;
        }
    }
}