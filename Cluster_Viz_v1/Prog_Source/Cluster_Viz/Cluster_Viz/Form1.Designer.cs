namespace Cluster_Viz
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
            this.main_menu = new System.Windows.Forms.MenuStrip();
            this.main_menu_file = new System.Windows.Forms.ToolStripMenuItem();
            this.main_menu_file_open = new System.Windows.Forms.ToolStripMenuItem();
            this.main_menu_file_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tab_control = new System.Windows.Forms.TabControl();
            this.tab_page_data = new System.Windows.Forms.TabPage();
            this.button_build_matrix = new System.Windows.Forms.Button();
            this.excel_dataGridView = new System.Windows.Forms.DataGridView();
            this.tab_page_2D = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.button_plot_clear_current = new System.Windows.Forms.Button();
            this.button_add_cluster = new System.Windows.Forms.Button();
            this.button_show_labels = new System.Windows.Forms.Button();
            this.button_plot_clear_all = new System.Windows.Forms.Button();
            this.work_area = new System.Windows.Forms.Panel();
            this.panel_2D = new System.Windows.Forms.Panel();
            this.main_menu.SuspendLayout();
            this.tab_control.SuspendLayout();
            this.tab_page_data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.excel_dataGridView)).BeginInit();
            this.tab_page_2D.SuspendLayout();
            this.SuspendLayout();
            // 
            // main_menu
            // 
            this.main_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.main_menu_file});
            this.main_menu.Location = new System.Drawing.Point(0, 0);
            this.main_menu.Name = "main_menu";
            this.main_menu.Size = new System.Drawing.Size(1643, 24);
            this.main_menu.TabIndex = 0;
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
            // 
            // main_menu_file_exit
            // 
            this.main_menu_file_exit.Name = "main_menu_file_exit";
            this.main_menu_file_exit.Size = new System.Drawing.Size(121, 22);
            this.main_menu_file_exit.Text = "Выход";
            // 
            // tab_control
            // 
            this.tab_control.Controls.Add(this.tab_page_data);
            this.tab_control.Controls.Add(this.tab_page_2D);
            this.tab_control.Location = new System.Drawing.Point(13, 28);
            this.tab_control.Name = "tab_control";
            this.tab_control.SelectedIndex = 0;
            this.tab_control.Size = new System.Drawing.Size(1630, 730);
            this.tab_control.TabIndex = 1;
            // 
            // tab_page_data
            // 
            this.tab_page_data.Controls.Add(this.button_build_matrix);
            this.tab_page_data.Controls.Add(this.excel_dataGridView);
            this.tab_page_data.Location = new System.Drawing.Point(4, 22);
            this.tab_page_data.Name = "tab_page_data";
            this.tab_page_data.Padding = new System.Windows.Forms.Padding(3);
            this.tab_page_data.Size = new System.Drawing.Size(1622, 704);
            this.tab_page_data.TabIndex = 0;
            this.tab_page_data.Text = "Данные";
            this.tab_page_data.UseVisualStyleBackColor = true;
            // 
            // button_build_matrix
            // 
            this.button_build_matrix.Location = new System.Drawing.Point(3, 446);
            this.button_build_matrix.Name = "button_build_matrix";
            this.button_build_matrix.Size = new System.Drawing.Size(251, 38);
            this.button_build_matrix.TabIndex = 0;
            this.button_build_matrix.Text = "Построить матрицу графиков";
            this.button_build_matrix.UseVisualStyleBackColor = true;
            this.button_build_matrix.Click += new System.EventHandler(this.button_build_matrix_Click);
            // 
            // excel_dataGridView
            // 
            this.excel_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.excel_dataGridView.Location = new System.Drawing.Point(3, 3);
            this.excel_dataGridView.Name = "excel_dataGridView";
            this.excel_dataGridView.Size = new System.Drawing.Size(860, 437);
            this.excel_dataGridView.TabIndex = 0;
            // 
            // tab_page_2D
            // 
            this.tab_page_2D.AutoScroll = true;
            this.tab_page_2D.Controls.Add(this.label1);
            this.tab_page_2D.Controls.Add(this.button_plot_clear_current);
            this.tab_page_2D.Controls.Add(this.button_add_cluster);
            this.tab_page_2D.Controls.Add(this.button_show_labels);
            this.tab_page_2D.Controls.Add(this.button_plot_clear_all);
            this.tab_page_2D.Controls.Add(this.work_area);
            this.tab_page_2D.Controls.Add(this.panel_2D);
            this.tab_page_2D.Location = new System.Drawing.Point(4, 22);
            this.tab_page_2D.Name = "tab_page_2D";
            this.tab_page_2D.Padding = new System.Windows.Forms.Padding(3);
            this.tab_page_2D.Size = new System.Drawing.Size(1622, 704);
            this.tab_page_2D.TabIndex = 1;
            this.tab_page_2D.Text = "Графики 2D";
            this.tab_page_2D.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1313, 446);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // button_plot_clear_current
            // 
            this.button_plot_clear_current.Location = new System.Drawing.Point(1087, 417);
            this.button_plot_clear_current.Name = "button_plot_clear_current";
            this.button_plot_clear_current.Size = new System.Drawing.Size(160, 36);
            this.button_plot_clear_current.TabIndex = 7;
            this.button_plot_clear_current.Text = "Снять выделение с графика";
            this.button_plot_clear_current.UseVisualStyleBackColor = true;
            this.button_plot_clear_current.Click += new System.EventHandler(this.button_plot_clear_current_Click);
            // 
            // button_add_cluster
            // 
            this.button_add_cluster.Location = new System.Drawing.Point(1313, 374);
            this.button_add_cluster.Name = "button_add_cluster";
            this.button_add_cluster.Size = new System.Drawing.Size(96, 37);
            this.button_add_cluster.TabIndex = 6;
            this.button_add_cluster.Text = "Создать кластер";
            this.button_add_cluster.UseVisualStyleBackColor = true;
            this.button_add_cluster.Click += new System.EventHandler(this.button_add_cluster_Click);
            // 
            // button_show_labels
            // 
            this.button_show_labels.Location = new System.Drawing.Point(1504, 374);
            this.button_show_labels.Name = "button_show_labels";
            this.button_show_labels.Size = new System.Drawing.Size(118, 37);
            this.button_show_labels.TabIndex = 5;
            this.button_show_labels.Text = "Показать координаты точек";
            this.button_show_labels.UseVisualStyleBackColor = true;
            this.button_show_labels.Click += new System.EventHandler(this.button_show_labels_Click);
            // 
            // button_plot_clear_all
            // 
            this.button_plot_clear_all.Location = new System.Drawing.Point(1087, 374);
            this.button_plot_clear_all.Name = "button_plot_clear_all";
            this.button_plot_clear_all.Size = new System.Drawing.Size(161, 37);
            this.button_plot_clear_all.TabIndex = 4;
            this.button_plot_clear_all.Text = "Снять выделение со всех графиков";
            this.button_plot_clear_all.UseVisualStyleBackColor = true;
            this.button_plot_clear_all.Click += new System.EventHandler(this.button_plot_clear_all_Click);
            // 
            // work_area
            // 
            this.work_area.Location = new System.Drawing.Point(1088, 0);
            this.work_area.Name = "work_area";
            this.work_area.Size = new System.Drawing.Size(534, 368);
            this.work_area.TabIndex = 1;
            // 
            // panel_2D
            // 
            this.panel_2D.Location = new System.Drawing.Point(7, 7);
            this.panel_2D.Name = "panel_2D";
            this.panel_2D.Size = new System.Drawing.Size(1075, 691);
            this.panel_2D.TabIndex = 0;
            // 
            // main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1643, 758);
            this.Controls.Add(this.tab_control);
            this.Controls.Add(this.main_menu);
            this.MainMenuStrip = this.main_menu;
            this.Name = "main_form";
            this.Text = "Cluster Viz";
            this.Load += new System.EventHandler(this.Main_Form_Load);
            this.main_menu.ResumeLayout(false);
            this.main_menu.PerformLayout();
            this.tab_control.ResumeLayout(false);
            this.tab_page_data.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.excel_dataGridView)).EndInit();
            this.tab_page_2D.ResumeLayout(false);
            this.tab_page_2D.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private OxyPlot.WindowsForms.PlotView plot_2D;
        private OxyPlot.WindowsForms.PlotView work_plot;

        private System.Windows.Forms.MenuStrip main_menu;
        private System.Windows.Forms.ToolStripMenuItem main_menu_file;
        private System.Windows.Forms.ToolStripMenuItem main_menu_file_open;
        private System.Windows.Forms.ToolStripMenuItem main_menu_file_exit;
        private System.Windows.Forms.TabControl tab_control;
        private System.Windows.Forms.TabPage tab_page_data;
        private System.Windows.Forms.TabPage tab_page_2D;
        private System.Windows.Forms.DataGridView excel_dataGridView;
        private System.Windows.Forms.Button button_build_matrix;
        private System.Windows.Forms.Panel panel_2D;
        private System.Windows.Forms.Panel work_area;
        private System.Windows.Forms.Button button_plot_clear_all;
        private System.Windows.Forms.Button button_show_labels;
        private System.Windows.Forms.Button button_add_cluster;
        private System.Windows.Forms.Button button_plot_clear_current;
        private System.Windows.Forms.Label label1;
    }
}

