﻿namespace SerialPortDemo {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
      this._receive = new System.Windows.Forms.GroupBox();
      this.clearRec = new System.Windows.Forms.Button();
      this._rec_string = new System.Windows.Forms.RadioButton();
      this._rec_hex = new System.Windows.Forms.RadioButton();
      this._send_input = new System.Windows.Forms.TextBox();
      this._send_submit = new System.Windows.Forms.Button();
      this._openPort = new System.Windows.Forms.Button();
      this.botLab = new System.Windows.Forms.Label();
      this._com = new System.Windows.Forms.GroupBox();
      this._tip = new System.Windows.Forms.Label();
      this.F5 = new System.Windows.Forms.Button();
      this.handLab = new System.Windows.Forms.Label();
      this._handCbx = new System.Windows.Forms.ComboBox();
      this.parityLab = new System.Windows.Forms.Label();
      this._parityCbx = new System.Windows.Forms.ComboBox();
      this.stopBitLab = new System.Windows.Forms.Label();
      this._stopCbx = new System.Windows.Forms.ComboBox();
      this.dataBitLab = new System.Windows.Forms.Label();
      this._dataCbx = new System.Windows.Forms.ComboBox();
      this.comLab = new System.Windows.Forms.Label();
      this._comCbx = new System.Windows.Forms.ComboBox();
      this._botCbx = new System.Windows.Forms.ComboBox();
      this._send = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this._formatShow = new System.Windows.Forms.TextBox();
      this._send_string = new System.Windows.Forms.RadioButton();
      this._send_hex = new System.Windows.Forms.RadioButton();
      this.receiveArea = new System.Windows.Forms.TextBox();
      this._autoSend = new System.Windows.Forms.CheckBox();
      this._receive.SuspendLayout();
      this._com.SuspendLayout();
      this._send.SuspendLayout();
      this.SuspendLayout();
      // 
      // _receive
      // 
      this._receive.Controls.Add(this.clearRec);
      this._receive.Controls.Add(this._rec_string);
      this._receive.Controls.Add(this._rec_hex);
      this._receive.Controls.Add(this.receiveArea);
      this._receive.Location = new System.Drawing.Point(218, 279);
      this._receive.Name = "_receive";
      this._receive.Size = new System.Drawing.Size(520, 270);
      this._receive.TabIndex = 5;
      this._receive.TabStop = false;
      this._receive.Text = "Receive";
      // 
      // clearRec
      // 
      this.clearRec.Enabled = false;
      this.clearRec.Location = new System.Drawing.Point(439, 241);
      this.clearRec.Name = "clearRec";
      this.clearRec.Size = new System.Drawing.Size(75, 23);
      this.clearRec.TabIndex = 6;
      this.clearRec.Text = "Clear";
      this.clearRec.UseVisualStyleBackColor = true;
      this.clearRec.Click += new System.EventHandler(this.clearReceive);
      // 
      // _rec_string
      // 
      this._rec_string.AutoSize = true;
      this._rec_string.Cursor = System.Windows.Forms.Cursors.Hand;
      this._rec_string.Location = new System.Drawing.Point(374, 244);
      this._rec_string.Name = "_rec_string";
      this._rec_string.Size = new System.Drawing.Size(59, 16);
      this._rec_string.TabIndex = 2;
      this._rec_string.Text = "String";
      this._rec_string.UseVisualStyleBackColor = true;
      this._rec_string.CheckedChanged += new System.EventHandler(this.recRadioClick);
      // 
      // _rec_hex
      // 
      this._rec_hex.AutoSize = true;
      this._rec_hex.Checked = true;
      this._rec_hex.Cursor = System.Windows.Forms.Cursors.Hand;
      this._rec_hex.Location = new System.Drawing.Point(327, 244);
      this._rec_hex.Name = "_rec_hex";
      this._rec_hex.Size = new System.Drawing.Size(41, 16);
      this._rec_hex.TabIndex = 1;
      this._rec_hex.TabStop = true;
      this._rec_hex.Text = "Hex";
      this._rec_hex.UseVisualStyleBackColor = true;
      this._rec_hex.CheckedChanged += new System.EventHandler(this.recRadioClick);
      // 
      // _send_input
      // 
      this._send_input.Enabled = false;
      this._send_input.Location = new System.Drawing.Point(6, 20);
      this._send_input.Multiline = true;
      this._send_input.Name = "_send_input";
      this._send_input.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this._send_input.Size = new System.Drawing.Size(251, 206);
      this._send_input.TabIndex = 5;
      this._send_input.TextChanged += new System.EventHandler(this.cacheText);
      this._send_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onKeyDown);
      // 
      // _send_submit
      // 
      this._send_submit.Enabled = false;
      this._send_submit.Location = new System.Drawing.Point(439, 232);
      this._send_submit.Name = "_send_submit";
      this._send_submit.Size = new System.Drawing.Size(75, 23);
      this._send_submit.TabIndex = 4;
      this._send_submit.Text = "Send";
      this._send_submit.UseVisualStyleBackColor = true;
      this._send_submit.Click += new System.EventHandler(this.sendData);
      // 
      // _openPort
      // 
      this._openPort.Location = new System.Drawing.Point(6, 206);
      this._openPort.Name = "_openPort";
      this._openPort.Size = new System.Drawing.Size(188, 24);
      this._openPort.TabIndex = 6;
      this._openPort.Text = "打开串口";
      this._openPort.UseVisualStyleBackColor = true;
      this._openPort.Click += new System.EventHandler(this.onTogglePort);
      // 
      // botLab
      // 
      this.botLab.AutoSize = true;
      this.botLab.Location = new System.Drawing.Point(6, 49);
      this.botLab.Name = "botLab";
      this.botLab.Size = new System.Drawing.Size(41, 12);
      this.botLab.TabIndex = 8;
      this.botLab.Text = "波特率";
      // 
      // _com
      // 
      this._com.Controls.Add(this.F5);
      this._com.Controls.Add(this.handLab);
      this._com.Controls.Add(this._handCbx);
      this._com.Controls.Add(this.parityLab);
      this._com.Controls.Add(this._parityCbx);
      this._com.Controls.Add(this.stopBitLab);
      this._com.Controls.Add(this._stopCbx);
      this._com.Controls.Add(this.dataBitLab);
      this._com.Controls.Add(this._dataCbx);
      this._com.Controls.Add(this.comLab);
      this._com.Controls.Add(this._comCbx);
      this._com.Controls.Add(this._botCbx);
      this._com.Controls.Add(this.botLab);
      this._com.Controls.Add(this._openPort);
      this._com.Location = new System.Drawing.Point(12, 12);
      this._com.Name = "_com";
      this._com.Size = new System.Drawing.Size(200, 537);
      this._com.TabIndex = 4;
      this._com.TabStop = false;
      this._com.Text = "COM";
      // 
      // _tip
      // 
      this._tip.AutoSize = true;
      this._tip.Location = new System.Drawing.Point(6, 237);
      this._tip.Name = "_tip";
      this._tip.Size = new System.Drawing.Size(0, 12);
      this._tip.TabIndex = 23;
      // 
      // F5
      // 
      this.F5.Location = new System.Drawing.Point(6, 176);
      this.F5.Name = "F5";
      this.F5.Size = new System.Drawing.Size(188, 24);
      this.F5.TabIndex = 22;
      this.F5.Text = "刷新串口";
      this.F5.UseVisualStyleBackColor = true;
      this.F5.Click += new System.EventHandler(this.onSPUpdate);
      // 
      // handLab
      // 
      this.handLab.AutoSize = true;
      this.handLab.Location = new System.Drawing.Point(6, 153);
      this.handLab.Name = "handLab";
      this.handLab.Size = new System.Drawing.Size(59, 12);
      this.handLab.TabIndex = 21;
      this.handLab.Text = "Handshake";
      // 
      // _handCbx
      // 
      this._handCbx.FormattingEnabled = true;
      this._handCbx.Items.AddRange(new object[] {
            "None",
            "XOnXOff"});
      this._handCbx.Location = new System.Drawing.Point(71, 150);
      this._handCbx.Name = "_handCbx";
      this._handCbx.Size = new System.Drawing.Size(123, 20);
      this._handCbx.TabIndex = 20;
      this._handCbx.Text = "None";
      // 
      // parityLab
      // 
      this.parityLab.AutoSize = true;
      this.parityLab.Location = new System.Drawing.Point(6, 127);
      this.parityLab.Name = "parityLab";
      this.parityLab.Size = new System.Drawing.Size(41, 12);
      this.parityLab.TabIndex = 19;
      this.parityLab.Text = "Parity";
      // 
      // _parityCbx
      // 
      this._parityCbx.FormattingEnabled = true;
      this._parityCbx.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
      this._parityCbx.Location = new System.Drawing.Point(71, 124);
      this._parityCbx.Name = "_parityCbx";
      this._parityCbx.Size = new System.Drawing.Size(123, 20);
      this._parityCbx.TabIndex = 18;
      this._parityCbx.Text = "None";
      // 
      // stopBitLab
      // 
      this.stopBitLab.AutoSize = true;
      this.stopBitLab.Location = new System.Drawing.Point(6, 101);
      this.stopBitLab.Name = "stopBitLab";
      this.stopBitLab.Size = new System.Drawing.Size(41, 12);
      this.stopBitLab.TabIndex = 17;
      this.stopBitLab.Text = "停止位";
      // 
      // _stopCbx
      // 
      this._stopCbx.FormattingEnabled = true;
      this._stopCbx.Items.AddRange(new object[] {
            "1",
            "2"});
      this._stopCbx.Location = new System.Drawing.Point(71, 98);
      this._stopCbx.Name = "_stopCbx";
      this._stopCbx.Size = new System.Drawing.Size(123, 20);
      this._stopCbx.TabIndex = 16;
      this._stopCbx.Text = "1";
      // 
      // dataBitLab
      // 
      this.dataBitLab.AutoSize = true;
      this.dataBitLab.Location = new System.Drawing.Point(6, 75);
      this.dataBitLab.Name = "dataBitLab";
      this.dataBitLab.Size = new System.Drawing.Size(41, 12);
      this.dataBitLab.TabIndex = 15;
      this.dataBitLab.Text = "数据位";
      // 
      // _dataCbx
      // 
      this._dataCbx.FormattingEnabled = true;
      this._dataCbx.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
      this._dataCbx.Location = new System.Drawing.Point(71, 72);
      this._dataCbx.Name = "_dataCbx";
      this._dataCbx.Size = new System.Drawing.Size(123, 20);
      this._dataCbx.TabIndex = 14;
      this._dataCbx.Text = "8";
      // 
      // comLab
      // 
      this.comLab.AutoSize = true;
      this.comLab.Location = new System.Drawing.Point(6, 23);
      this.comLab.Name = "comLab";
      this.comLab.Size = new System.Drawing.Size(41, 12);
      this.comLab.TabIndex = 13;
      this.comLab.Text = "通讯口";
      // 
      // _comCbx
      // 
      this._comCbx.FormattingEnabled = true;
      this._comCbx.Items.AddRange(new object[] {
            "Fail"});
      this._comCbx.Location = new System.Drawing.Point(71, 20);
      this._comCbx.Name = "_comCbx";
      this._comCbx.Size = new System.Drawing.Size(123, 20);
      this._comCbx.TabIndex = 11;
      this._comCbx.Text = "NotFound";
      // 
      // _botCbx
      // 
      this._botCbx.FormattingEnabled = true;
      this._botCbx.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "115200"});
      this._botCbx.Location = new System.Drawing.Point(71, 46);
      this._botCbx.Name = "_botCbx";
      this._botCbx.Size = new System.Drawing.Size(123, 20);
      this._botCbx.TabIndex = 10;
      this._botCbx.Text = "9600";
      // 
      // _send
      // 
      this._send.BackColor = System.Drawing.SystemColors.Control;
      this._send.Controls.Add(this._tip);
      this._send.Controls.Add(this._autoSend);
      this._send.Controls.Add(this.label1);
      this._send.Controls.Add(this._formatShow);
      this._send.Controls.Add(this._send_string);
      this._send.Controls.Add(this._send_hex);
      this._send.Controls.Add(this._send_input);
      this._send.Controls.Add(this._send_submit);
      this._send.Location = new System.Drawing.Point(218, 12);
      this._send.Name = "_send";
      this._send.Size = new System.Drawing.Size(520, 261);
      this._send.TabIndex = 6;
      this._send.TabStop = false;
      this._send.Text = "Send";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(263, 20);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(41, 12);
      this.label1.TabIndex = 9;
      this.label1.Text = "Format";
      // 
      // _formatShow
      // 
      this._formatShow.Location = new System.Drawing.Point(263, 35);
      this._formatShow.Multiline = true;
      this._formatShow.Name = "_formatShow";
      this._formatShow.ReadOnly = true;
      this._formatShow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this._formatShow.Size = new System.Drawing.Size(251, 191);
      this._formatShow.TabIndex = 8;
      // 
      // _send_string
      // 
      this._send_string.AutoSize = true;
      this._send_string.Checked = true;
      this._send_string.Cursor = System.Windows.Forms.Cursors.Hand;
      this._send_string.Location = new System.Drawing.Point(374, 235);
      this._send_string.Name = "_send_string";
      this._send_string.Size = new System.Drawing.Size(59, 16);
      this._send_string.TabIndex = 7;
      this._send_string.TabStop = true;
      this._send_string.Text = "String";
      this._send_string.UseVisualStyleBackColor = true;
      this._send_string.CheckedChanged += new System.EventHandler(this.sendRadioClick);
      // 
      // _send_hex
      // 
      this._send_hex.AutoSize = true;
      this._send_hex.Cursor = System.Windows.Forms.Cursors.Hand;
      this._send_hex.Location = new System.Drawing.Point(327, 235);
      this._send_hex.Name = "_send_hex";
      this._send_hex.Size = new System.Drawing.Size(41, 16);
      this._send_hex.TabIndex = 6;
      this._send_hex.Text = "Hex";
      this._send_hex.UseVisualStyleBackColor = true;
      this._send_hex.CheckedChanged += new System.EventHandler(this.sendRadioClick);
      // 
      // receiveArea
      // 
      this.receiveArea.Location = new System.Drawing.Point(7, 20);
      this.receiveArea.Multiline = true;
      this.receiveArea.Name = "receiveArea";
      this.receiveArea.ReadOnly = true;
      this.receiveArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.receiveArea.Size = new System.Drawing.Size(507, 215);
      this.receiveArea.TabIndex = 0;
      // 
      // _autoSend
      // 
      this._autoSend.AutoSize = true;
      this._autoSend.Enabled = false;
      this._autoSend.Location = new System.Drawing.Point(243, 236);
      this._autoSend.Name = "_autoSend";
      this._autoSend.Size = new System.Drawing.Size(78, 16);
      this._autoSend.TabIndex = 10;
      this._autoSend.Text = "Auto Send";
      this._autoSend.UseVisualStyleBackColor = true;
      this._autoSend.CheckedChanged += new System.EventHandler(this.toggleAutoSend);
      // 
      // BaseForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(750, 561);
      this.Controls.Add(this._send);
      this.Controls.Add(this._receive);
      this.Controls.Add(this._com);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "BaseForm";
      this.ShowIcon = false;
      this.Text = "Title";
      this.Load += new System.EventHandler(this.onLoad);
      this._receive.ResumeLayout(false);
      this._receive.PerformLayout();
      this._com.ResumeLayout(false);
      this._com.PerformLayout();
      this._send.ResumeLayout(false);
      this._send.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.GroupBox _receive;
    private System.Windows.Forms.RadioButton _rec_string;
    private System.Windows.Forms.RadioButton _rec_hex;
    private System.Windows.Forms.TextBox _send_input;
    private System.Windows.Forms.Button _openPort;
    private System.Windows.Forms.Label botLab;
    private System.Windows.Forms.GroupBox _com;
    private System.Windows.Forms.ComboBox _botCbx;
    private System.Windows.Forms.Label comLab;
    private System.Windows.Forms.ComboBox _comCbx;
    private System.Windows.Forms.Label stopBitLab;
    private System.Windows.Forms.ComboBox _stopCbx;
    private System.Windows.Forms.Label dataBitLab;
    private System.Windows.Forms.ComboBox _dataCbx;
    private System.Windows.Forms.Label handLab;
    private System.Windows.Forms.ComboBox _handCbx;
    private System.Windows.Forms.Label parityLab;
    private System.Windows.Forms.ComboBox _parityCbx;
    private System.Windows.Forms.GroupBox _send;
    private System.Windows.Forms.Button clearRec;
    private System.Windows.Forms.RadioButton _send_string;
    private System.Windows.Forms.RadioButton _send_hex;
    private System.Windows.Forms.Button F5;
    private System.Windows.Forms.Button _send_submit;
    private System.Windows.Forms.Label _tip;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox _formatShow;
    private System.Windows.Forms.TextBox receiveArea;
    private System.Windows.Forms.CheckBox _autoSend;
  }
}

