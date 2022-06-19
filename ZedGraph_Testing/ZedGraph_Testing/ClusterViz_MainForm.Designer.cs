namespace ZedGraph_Testing
{
    partial class main_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.zedGraph = new ZedGraph.ZedGraphControl();
            this.button_data_draw_graph = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_data = new System.Windows.Forms.TabPage();
            this.label_file_name = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tabPage_graphics = new System.Windows.Forms.TabPage();
            this.panel_graphics = new System.Windows.Forms.Panel();
            this.listBox_graphics = new System.Windows.Forms.ListBox();
            this.main_menu = new System.Windows.Forms.MenuStrip();
            this.main_menu_file = new System.Windows.Forms.ToolStripMenuItem();
            this.main_menu_file_open = new System.Windows.Forms.ToolStripMenuItem();
            this.main_menu_file_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl.SuspendLayout();
            this.tabPage_data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tabPage_graphics.SuspendLayout();
            this.panel_graphics.SuspendLayout();
            this.main_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // zedGraph
            // 
            this.zedGraph.AutoScroll = true;
            this.zedGraph.AutoSize = true;
            this.zedGraph.Location = new System.Drawing.Point(3, 3);
            this.zedGraph.Name = "zedGraph";
            this.zedGraph.ScrollGrace = 0D;
            this.zedGraph.ScrollMaxX = 0D;
            this.zedGraph.ScrollMaxY = 0D;
            this.zedGraph.ScrollMaxY2 = 0D;
            this.zedGraph.ScrollMinX = 0D;
            this.zedGraph.ScrollMinY = 0D;
            this.zedGraph.ScrollMinY2 = 0D;
            this.zedGraph.Size = new System.Drawing.Size(1149, 500);
            this.zedGraph.TabIndex = 0;
            // 
            // button_data_draw_graph
            // 
            this.button_data_draw_graph.Location = new System.Drawing.Point(6, 394);
            this.button_data_draw_graph.Name = "button_data_draw_graph";
            this.button_data_draw_graph.Size = new System.Drawing.Size(114, 25);
            this.button_data_draw_graph.TabIndex = 1;
            this.button_data_draw_graph.Text = "Построить график";
            this.button_data_draw_graph.UseVisualStyleBackColor = true;
            this.button_data_draw_graph.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_data);
            this.tabControl.Controls.Add(this.tabPage_graphics);
            this.tabControl.Location = new System.Drawing.Point(12, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1215, 539);
            this.tabControl.TabIndex = 2;
            // 
            // tabPage_data
            // 
            this.tabPage_data.Controls.Add(this.label_file_name);
            this.tabPage_data.Controls.Add(this.button_data_draw_graph);
            this.tabPage_data.Controls.Add(this.dataGridView);
            this.tabPage_data.Location = new System.Drawing.Point(4, 22);
            this.tabPage_data.Name = "tabPage_data";
            this.tabPage_data.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_data.Size = new System.Drawing.Size(1207, 513);
            this.tabPage_data.TabIndex = 0;
            this.tabPage_data.Text = "Данные";
            this.tabPage_data.UseVisualStyleBackColor = true;
            // 
            // label_file_name
            // 
            this.label_file_name.AutoSize = true;
            this.label_file_name.Location = new System.Drawing.Point(7, 4);
            this.label_file_name.Name = "label_file_name";
            this.label_file_name.Size = new System.Drawing.Size(0, 13);
            this.label_file_name.TabIndex = 1;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(6, 20);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(724, 368);
            this.dataGridView.TabIndex = 0;
            // 
            // tabPage_graphics
            // 
            this.tabPage_graphics.Controls.Add(this.panel_graphics);
            this.tabPage_graphics.Controls.Add(this.listBox_graphics);
            this.tabPage_graphics.Location = new System.Drawing.Point(4, 22);
            this.tabPage_graphics.Name = "tabPage_graphics";
            this.tabPage_graphics.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_graphics.Size = new System.Drawing.Size(1207, 513);
            this.tabPage_graphics.TabIndex = 1;
            this.tabPage_graphics.Text = "Графики";
            this.tabPage_graphics.UseVisualStyleBackColor = true;
            // 
            // panel_graphics
            // 
            this.panel_graphics.Controls.Add(this.zedGraph);
            this.panel_graphics.Location = new System.Drawing.Point(49, 4);
            this.panel_graphics.Name = "panel_graphics";
            this.panel_graphics.Size = new System.Drawing.Size(1155, 506);
            this.panel_graphics.TabIndex = 3;
            // 
            // listBox_graphics
            // 
            this.listBox_graphics.FormattingEnabled = true;
            this.listBox_graphics.Location = new System.Drawing.Point(-4, 0);
            this.listBox_graphics.Name = "listBox_graphics";
            this.listBox_graphics.Size = new System.Drawing.Size(47, 290);
            this.listBox_graphics.TabIndex = 2;
            // 
            // main_menu
            // 
            this.main_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.main_menu_file});
            this.main_menu.Location = new System.Drawing.Point(0, 0);
            this.main_menu.Name = "main_menu";
            this.main_menu.Size = new System.Drawing.Size(1239, 24);
            this.main_menu.TabIndex = 3;
            this.main_menu.Text = "menuStrip1";
            // 
            // main_menu_file
            // 
            this.main_menu_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.main_menu_file_open,
            this.main_menu_file_exit});
            this.main_menu_file.Name = "main_menu_file";
            this.main_menu_file.Size = new System.Drawing.Size(48, 20);
            this.main_menu_file.Text = "Файл";
            // 
            // main_menu_file_open
            // 
            this.main_menu_file_open.Name = "main_menu_file_open";
            this.main_menu_file_open.Size = new System.Drawing.Size(121, 22);
            this.main_menu_file_open.Text = "Открыть";
            this.main_menu_file_open.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // main_menu_file_exit
            // 
            this.main_menu_file_exit.Name = "main_menu_file_exit";
            this.main_menu_file_exit.Size = new System.Drawing.Size(121, 22);
            this.main_menu_file_exit.Text = "Выход";
            this.main_menu_file_exit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 578);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.main_menu);
            this.MainMenuStrip = this.main_menu;
            this.Name = "main_form";
            this.Text = "ClusterViz";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage_data.ResumeLayout(false);
            this.tabPage_data.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tabPage_graphics.ResumeLayout(false);
            this.panel_graphics.ResumeLayout(false);
            this.panel_graphics.PerformLayout();
            this.main_menu.ResumeLayout(false);
            this.main_menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraph;
        private System.Windows.Forms.Button button_data_draw_graph;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_data;
        private System.Windows.Forms.TabPage tabPage_graphics;
        private System.Windows.Forms.MenuStrip main_menu;
        private System.Windows.Forms.ToolStripMenuItem main_menu_file;
        private System.Windows.Forms.ToolStripMenuItem main_menu_file_open;
        private System.Windows.Forms.ToolStripMenuItem main_menu_file_exit;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label_file_name;
        private System.Windows.Forms.ListBox listBox_graphics;
        private System.Windows.Forms.Panel panel_graphics;
    }
}

