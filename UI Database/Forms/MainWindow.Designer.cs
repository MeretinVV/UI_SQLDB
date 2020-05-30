namespace UI_Database
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFilterBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.clearBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadXmlBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearDatabaseBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.peopleDataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peopleDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterToolStripMenuItem,
            this.LoadXmlBtn,
            this.ClearDatabaseBtn});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(484, 27);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setFilterBtn,
            this.clearBtn});
            this.filterToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(51, 23);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // setFilterBtn
            // 
            this.setFilterBtn.Name = "setFilterBtn";
            this.setFilterBtn.Size = new System.Drawing.Size(128, 24);
            this.setFilterBtn.Text = "Set filter";
            this.setFilterBtn.Click += new System.EventHandler(this.setFilterBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(128, 24);
            this.clearBtn.Text = "Clear";
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // LoadXmlBtn
            // 
            this.LoadXmlBtn.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LoadXmlBtn.Name = "LoadXmlBtn";
            this.LoadXmlBtn.Size = new System.Drawing.Size(83, 23);
            this.LoadXmlBtn.Text = "Load XML";
            this.LoadXmlBtn.Click += new System.EventHandler(this.LoadXmlBtn_Click);
            // 
            // ClearDatabaseBtn
            // 
            this.ClearDatabaseBtn.Name = "ClearDatabaseBtn";
            this.ClearDatabaseBtn.Size = new System.Drawing.Size(97, 23);
            this.ClearDatabaseBtn.Text = "Clear Database";
            this.ClearDatabaseBtn.Click += new System.EventHandler(this.ClearDatabaseBtn_Click);
            // 
            // peopleDataGridView
            // 
            this.peopleDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.peopleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.peopleDataGridView.Location = new System.Drawing.Point(12, 30);
            this.peopleDataGridView.Name = "peopleDataGridView";
            this.peopleDataGridView.Size = new System.Drawing.Size(460, 319);
            this.peopleDataGridView.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.peopleDataGridView);
            this.Controls.Add(this.menuStrip1);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "MainForm";
            this.Text = "MainWindow";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peopleDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setFilterBtn;
        private System.Windows.Forms.ToolStripMenuItem clearBtn;
        private System.Windows.Forms.ToolStripMenuItem LoadXmlBtn;
        private System.Windows.Forms.ToolStripMenuItem ClearDatabaseBtn;
        private System.Windows.Forms.DataGridView peopleDataGridView;
    }
}

