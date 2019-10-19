namespace SerialPortDemo {
  partial class BaseForm {
    /// <summary>
    /// 必需的设计器变量。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 清理所有正在使用的资源。
    /// </summary>
    /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows 窗体设计器生成的代码

    /// <summary>
    /// 设计器支持所需的方法 - 不要修改
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent() {
      this.DataRecive = new System.Windows.Forms.GroupBox();
      this.Unicode = new System.Windows.Forms.RadioButton();
      this.UTF8 = new System.Windows.Forms.RadioButton();
      this.Hex = new System.Windows.Forms.RadioButton();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.Submit = new System.Windows.Forms.Button();
      this.Open = new System.Windows.Forms.Button();
      this.botLabel = new System.Windows.Forms.Label();
      this.DataSend = new System.Windows.Forms.GroupBox();
      this.stopBit = new System.Windows.Forms.Label();
      this.stopBox = new System.Windows.Forms.ComboBox();
      this.dataBit = new System.Windows.Forms.Label();
      this.dataBox = new System.Windows.Forms.ComboBox();
      this.sportLabel = new System.Windows.Forms.Label();
      this.sPortComboBox = new System.Windows.Forms.ComboBox();
      this.bot = new System.Windows.Forms.ComboBox();
      this.DataRecive.SuspendLayout();
      this.DataSend.SuspendLayout();
      this.SuspendLayout();
      // 
      // DataRecive
      // 
      this.DataRecive.Controls.Add(this.Unicode);
      this.DataRecive.Controls.Add(this.UTF8);
      this.DataRecive.Controls.Add(this.Hex);
      this.DataRecive.Controls.Add(this.textBox2);
      this.DataRecive.Location = new System.Drawing.Point(12, 12);
      this.DataRecive.Name = "DataRecive";
      this.DataRecive.Size = new System.Drawing.Size(400, 260);
      this.DataRecive.TabIndex = 5;
      this.DataRecive.TabStop = false;
      this.DataRecive.Text = "DataRecive";
      // 
      // Unicode
      // 
      this.Unicode.AutoSize = true;
      this.Unicode.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Unicode.Location = new System.Drawing.Point(113, 238);
      this.Unicode.Name = "Unicode";
      this.Unicode.Size = new System.Drawing.Size(65, 16);
      this.Unicode.TabIndex = 3;
      this.Unicode.Text = "Unicode";
      this.Unicode.UseVisualStyleBackColor = true;
      // 
      // UTF8
      // 
      this.UTF8.AutoSize = true;
      this.UTF8.Checked = true;
      this.UTF8.Cursor = System.Windows.Forms.Cursors.Hand;
      this.UTF8.Location = new System.Drawing.Point(54, 238);
      this.UTF8.Name = "UTF8";
      this.UTF8.Size = new System.Drawing.Size(53, 16);
      this.UTF8.TabIndex = 2;
      this.UTF8.TabStop = true;
      this.UTF8.Text = "UTF-8";
      this.UTF8.UseVisualStyleBackColor = true;
      // 
      // Hex
      // 
      this.Hex.AutoSize = true;
      this.Hex.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Hex.Location = new System.Drawing.Point(7, 238);
      this.Hex.Name = "Hex";
      this.Hex.Size = new System.Drawing.Size(41, 16);
      this.Hex.TabIndex = 1;
      this.Hex.Text = "Hex";
      this.Hex.UseVisualStyleBackColor = true;
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(7, 20);
      this.textBox2.Multiline = true;
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = true;
      this.textBox2.Size = new System.Drawing.Size(387, 212);
      this.textBox2.TabIndex = 0;
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(22, 36);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(153, 21);
      this.textBox1.TabIndex = 5;
      // 
      // Submit
      // 
      this.Submit.Enabled = false;
      this.Submit.Location = new System.Drawing.Point(181, 34);
      this.Submit.Name = "Submit";
      this.Submit.Size = new System.Drawing.Size(75, 23);
      this.Submit.TabIndex = 4;
      this.Submit.Text = "Submit";
      this.Submit.UseVisualStyleBackColor = true;
      this.Submit.Click += new System.EventHandler(this.SubmitButtonClick);
      // 
      // Open
      // 
      this.Open.Location = new System.Drawing.Point(6, 230);
      this.Open.Name = "Open";
      this.Open.Size = new System.Drawing.Size(258, 24);
      this.Open.TabIndex = 6;
      this.Open.Text = "打开串口";
      this.Open.UseVisualStyleBackColor = true;
      this.Open.Click += new System.EventHandler(this.OpenSPort);
      // 
      // botLabel
      // 
      this.botLabel.AutoSize = true;
      this.botLabel.Location = new System.Drawing.Point(20, 66);
      this.botLabel.Name = "botLabel";
      this.botLabel.Size = new System.Drawing.Size(41, 12);
      this.botLabel.TabIndex = 8;
      this.botLabel.Text = "波特率";
      // 
      // DataSend
      // 
      this.DataSend.Controls.Add(this.stopBit);
      this.DataSend.Controls.Add(this.stopBox);
      this.DataSend.Controls.Add(this.dataBit);
      this.DataSend.Controls.Add(this.dataBox);
      this.DataSend.Controls.Add(this.sportLabel);
      this.DataSend.Controls.Add(this.sPortComboBox);
      this.DataSend.Controls.Add(this.bot);
      this.DataSend.Controls.Add(this.botLabel);
      this.DataSend.Controls.Add(this.Open);
      this.DataSend.Controls.Add(this.Submit);
      this.DataSend.Controls.Add(this.textBox1);
      this.DataSend.Location = new System.Drawing.Point(422, 12);
      this.DataSend.Name = "DataSend";
      this.DataSend.Size = new System.Drawing.Size(270, 260);
      this.DataSend.TabIndex = 4;
      this.DataSend.TabStop = false;
      this.DataSend.Text = "DataSend";
      // 
      // stopBit
      // 
      this.stopBit.AutoSize = true;
      this.stopBit.Location = new System.Drawing.Point(20, 146);
      this.stopBit.Name = "stopBit";
      this.stopBit.Size = new System.Drawing.Size(41, 12);
      this.stopBit.TabIndex = 17;
      this.stopBit.Text = "停止位";
      // 
      // stopBox
      // 
      this.stopBox.FormattingEnabled = true;
      this.stopBox.Items.AddRange(new object[] {
            "0",
            "1",
            "1.5",
            "2"});
      this.stopBox.Location = new System.Drawing.Point(135, 143);
      this.stopBox.Name = "stopBox";
      this.stopBox.Size = new System.Drawing.Size(121, 20);
      this.stopBox.TabIndex = 16;
      this.stopBox.Text = "1";
      // 
      // dataBit
      // 
      this.dataBit.AutoSize = true;
      this.dataBit.Location = new System.Drawing.Point(20, 120);
      this.dataBit.Name = "dataBit";
      this.dataBit.Size = new System.Drawing.Size(41, 12);
      this.dataBit.TabIndex = 15;
      this.dataBit.Text = "数据位";
      // 
      // dataBox
      // 
      this.dataBox.FormattingEnabled = true;
      this.dataBox.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
      this.dataBox.Location = new System.Drawing.Point(135, 117);
      this.dataBox.Name = "dataBox";
      this.dataBox.Size = new System.Drawing.Size(121, 20);
      this.dataBox.TabIndex = 14;
      this.dataBox.Text = "8";
      // 
      // sportLabel
      // 
      this.sportLabel.AutoSize = true;
      this.sportLabel.Location = new System.Drawing.Point(20, 93);
      this.sportLabel.Name = "sportLabel";
      this.sportLabel.Size = new System.Drawing.Size(41, 12);
      this.sportLabel.TabIndex = 13;
      this.sportLabel.Text = "通讯口";
      // 
      // sPortComboBox
      // 
      this.sPortComboBox.FormattingEnabled = true;
      this.sPortComboBox.Items.AddRange(new object[] {
            "Fail"});
      this.sPortComboBox.Location = new System.Drawing.Point(135, 90);
      this.sPortComboBox.Name = "sPortComboBox";
      this.sPortComboBox.Size = new System.Drawing.Size(121, 20);
      this.sPortComboBox.TabIndex = 11;
      this.sPortComboBox.Text = "Fail";
      // 
      // bot
      // 
      this.bot.FormattingEnabled = true;
      this.bot.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600"});
      this.bot.Location = new System.Drawing.Point(135, 63);
      this.bot.Name = "bot";
      this.bot.Size = new System.Drawing.Size(121, 20);
      this.bot.TabIndex = 10;
      this.bot.Text = "2400";
      // 
      // BaseForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(704, 442);
      this.Controls.Add(this.DataRecive);
      this.Controls.Add(this.DataSend);
      this.Name = "BaseForm";
      this.Text = "Title";
      this.Load += new System.EventHandler(this.onLoad);
      this.DataRecive.ResumeLayout(false);
      this.DataRecive.PerformLayout();
      this.DataSend.ResumeLayout(false);
      this.DataSend.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.GroupBox DataRecive;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.RadioButton Unicode;
    private System.Windows.Forms.RadioButton UTF8;
    private System.Windows.Forms.RadioButton Hex;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button Submit;
    private System.Windows.Forms.Button Open;
    private System.Windows.Forms.Label botLabel;
    private System.Windows.Forms.GroupBox DataSend;
    private System.Windows.Forms.ComboBox bot;
    private System.Windows.Forms.Label sportLabel;
    private System.Windows.Forms.ComboBox sPortComboBox;
    private System.Windows.Forms.Label stopBit;
    private System.Windows.Forms.ComboBox stopBox;
    private System.Windows.Forms.Label dataBit;
    private System.Windows.Forms.ComboBox dataBox;
  }
}

