namespace TaskManagement
{
    partial class TasksForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnAdd = new Button();
            btnDelete = new Button();
            pictureBox1 = new PictureBox();
            btnEdit = new Button();
            listView1 = new ListView();
            label1 = new Label();
            txtDescription = new TextBox();
            txtTitle = new TextBox();
            dateTimePickerDeadline = new DateTimePicker();
            label2 = new Label();
            label5 = new Label();
            labelDeadline = new Label();
            labelDescription = new Label();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.ForeColor = Color.SteelBlue;
            btnAdd.Location = new Point(649, 640);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(173, 51);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDelete.ForeColor = Color.SteelBlue;
            btnDelete.Location = new Point(828, 640);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(173, 51);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.up0;
            pictureBox1.Location = new Point(1208, 74);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(262, 268);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // btnEdit
            // 
            btnEdit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEdit.ForeColor = Color.SteelBlue;
            btnEdit.Location = new Point(470, 640);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(173, 51);
            btnEdit.TabIndex = 3;
            btnEdit.Text = "Редактировать";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // listView1
            // 
            listView1.FullRowSelect = true;
            listView1.Location = new Point(311, 74);
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.Size = new Size(872, 544);
            listView1.TabIndex = 4;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.BackColor = SystemColors.Window;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.SteelBlue;
            label1.Location = new Point(642, 9);
            label1.Name = "label1";
            label1.Size = new Size(269, 48);
            label1.TabIndex = 5;
            label1.Text = "Ваши задачи";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(12, 248);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(260, 122);
            txtDescription.TabIndex = 6;
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(12, 160);
            txtTitle.Multiline = true;
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(260, 34);
            txtTitle.TabIndex = 7;
            // 
            // dateTimePickerDeadline
            // 
            dateTimePickerDeadline.Location = new Point(12, 422);
            dateTimePickerDeadline.Name = "dateTimePickerDeadline";
            dateTimePickerDeadline.Size = new Size(260, 27);
            dateTimePickerDeadline.TabIndex = 8;
            // 
            // label2
            // 
            label2.BackColor = SystemColors.Window;
            label2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            label2.ForeColor = Color.SteelBlue;
            label2.Location = new Point(12, 74);
            label2.Name = "label2";
            label2.Size = new Size(260, 35);
            label2.TabIndex = 9;
            label2.Text = "Новая задача";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.BackColor = SystemColors.Window;
            label5.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(152, 180, 212);
            label5.Location = new Point(12, 118);
            label5.Name = "label5";
            label5.Size = new Size(260, 34);
            label5.TabIndex = 12;
            label5.Text = "Название";
            // 
            // labelDeadline
            // 
            labelDeadline.BackColor = SystemColors.Window;
            labelDeadline.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            labelDeadline.ForeColor = Color.FromArgb(152, 180, 212);
            labelDeadline.Location = new Point(12, 378);
            labelDeadline.Name = "labelDeadline";
            labelDeadline.Size = new Size(260, 34);
            labelDeadline.TabIndex = 13;
            labelDeadline.Text = "Срок выполнения";
            // 
            // labelDescription
            // 
            labelDescription.BackColor = SystemColors.Window;
            labelDescription.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            labelDescription.ForeColor = Color.FromArgb(152, 180, 212);
            labelDescription.Location = new Point(12, 202);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(260, 34);
            labelDescription.TabIndex = 14;
            labelDescription.Text = "Описание";
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.SteelBlue;
            btnSave.Location = new Point(12, 567);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(173, 51);
            btnSave.TabIndex = 15;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // TasksForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(1482, 703);
            Controls.Add(btnSave);
            Controls.Add(labelDescription);
            Controls.Add(labelDeadline);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(dateTimePickerDeadline);
            Controls.Add(txtTitle);
            Controls.Add(txtDescription);
            Controls.Add(label1);
            Controls.Add(listView1);
            Controls.Add(btnEdit);
            Controls.Add(pictureBox1);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Name = "TasksForm";
            Text = "Tasks";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAdd;
        private Button btnDelete;
        private PictureBox pictureBox1;
        private Button btnEdit;
        private ListView listView1;
        private Label label1;
        private TextBox txtDescription;
        private TextBox txtTitle;
        private DateTimePicker dateTimePickerDeadline;
        private Label label2;
        private Label label5;
        private Label labelDeadline;
        private Label labelDescription;
        private Button btnSave;
    }
}
