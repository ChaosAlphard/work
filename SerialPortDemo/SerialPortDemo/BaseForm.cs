using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using SerialPortDemo.DTO;
using SerialPortDemo.Model;
using SerialPortDemo.Handler;
using System.Threading;

namespace SerialPortDemo {
  public partial class BaseForm : Form {

    // 串口对象
    private ComModel port = new ComModel(new SerialPort());
    // msg
    private byte[] dataToSend = new byte[0];
    private string dataThatReced = "";
    // private StringBuilder dataThatReced = new StringBuilder();

    //public Thread readThread = new Thread()

    private System.Timers.Timer timer = new System.Timers.Timer();

    private bool __sendIsHex = true;
    private bool __recIsHex = true;

    private bool sendIsHex {
      get { return __sendIsHex; }
      set {
        __sendIsHex = value;
        _send_hex.Checked = value;
        _send_string.Checked = !value;
      }
    }
    private bool recIsHex {
      get { return __recIsHex; }
      set {
        __recIsHex = value;
        _rec_hex.Checked = value;
        _rec_string.Checked = !value;
      }
    }

    public BaseForm() {
      InitializeComponent();
      CheckForIllegalCrossThreadCalls = false;
    }

    // 主窗体加载
    public void onLoad(object sender, EventArgs e) {
      SPHandler.updateSerialPortList(_comCbx);
      timer.Enabled = true;
      timer.Interval = 1000;
    }

    // 更新
    private void onSPUpdate(object sender, EventArgs e) {
      SPHandler.updateSerialPortList(_comCbx);
    }

    // 切换串口状态
    private void onTogglePort(object sender, EventArgs e) {
      if (!port.isOpen()) {
        try {
          port.setPortName(_comCbx.Text.Trim())
            .setBaudRate(Convert.ToInt32(_botCbx.Text.Trim()))
            .setDataBits(Convert.ToInt32(_dataCbx.Text.Trim()))
            .setStopBits(SPHandler.getStopBits(_stopCbx.SelectedIndex))
            .setParity(SPHandler.getParity(_parityCbx.SelectedIndex))
            .setHandshake(SPHandler.getHandshake(_handCbx.SelectedIndex))
            .setTimeout(2000);
        } catch(Exception err) {
          MessageBox.Show(err.ToString());
          return;
        }
        openPort();
      } else {
        closePort();
      }
      
      /**
      // if(!port.isOpen()) {
      //   bool openSucc = SPHandler.openSPort(
      //     port.getPort(), _comCbx.Text.Trim(),  // 串口名
      //     Convert.ToInt32(_botCbx.Text.Trim()), // 波特率
      //     Convert.ToInt32(_dataCbx.Text.Trim()),  // 数据位
      //     SPHandler.getStopBits(_stopCbx.SelectedIndex), // 停止位                    
      //     SPHandler.getParity(_parityCbx.SelectedIndex),  // 奇偶效验
      //     SPHandler.getHandshake(_handCbx.SelectedIndex),  // 协议
      //     500, // 超时时间
      //     _tip
      //   );
      // 
      //   if(openSucc) {
      //     _openPort.Text = "关闭串口";
      //     F5.Enabled = _botCbx.Enabled
      //               = _comCbx.Enabled
      //               = _dataCbx.Enabled
      //               = _stopCbx.Enabled
      //               = _parityCbx.Enabled
      //               = _handCbx.Enabled
      //               = false;
      //     _send_input.Enabled = _send_submit.Enabled = true;
      //     port.getPort().DataReceived += onDataReceived();
      //   }
      //   
      // } else {
      //   bool closeSucc = SPHandler.closeSPort(port.getPort(),_tip);
      //   if(closeSucc) {
      //     _openPort.Text = "打开串口";
      //     F5.Enabled = _botCbx.Enabled
      //               = _comCbx.Enabled
      //               = _dataCbx.Enabled
      //               = _stopCbx.Enabled
      //               = _parityCbx.Enabled
      //               = _handCbx.Enabled
      //               = true;
      //     _send_input.Enabled = _send_submit.Enabled = false;
      //     port.getPort().DataReceived -= onDataReceived();
      //   }
      // }
      */
    }

    private void openPort() {
      Msg openMsg = port.openPort();
      if (openMsg.flag) {
        _openPort.Text = "关闭串口";
        F5.Enabled = _botCbx.Enabled
                  = _comCbx.Enabled
                  = _dataCbx.Enabled
                  = _stopCbx.Enabled
                  = _parityCbx.Enabled
                  = _handCbx.Enabled
                  = false;
        _send_input.Enabled = _send_submit.Enabled
                    = _autoSend.Enabled = true;
        port.getPort().DataReceived += onDataReceived();
      }
      _tip.Text = openMsg.msg;
    }

    private void closePort() {
      Msg closeMsg = port.closePort();
      if (closeMsg.flag) {
        _openPort.Text = "打开串口";
        F5.Enabled = _botCbx.Enabled
                  = _comCbx.Enabled
                  = _dataCbx.Enabled
                  = _stopCbx.Enabled
                  = _parityCbx.Enabled
                  = _handCbx.Enabled
                  = true;
        _send_input.Enabled = _send_submit.Enabled
                    = _autoSend.Checked 
                    = _autoSend.Enabled 
                    = false;
        timer.Close();
        port.getPort().DataReceived -= onDataReceived();
      }
      _tip.Text = closeMsg.msg;
    }

    // 发送数据
    private void sendData(object sender, EventArgs e) {
      if(!port.isOpen()) {
        MessageBox.Show("串口没有打开");
        return;
      }
      //port.sendData(dataToSend);
      _tip.Text = port.sendDataWithLine(dataToSend).msg;
    }

////////////////////////////////////////////////////////////////////////////////
////Event///////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
    private SerialDataReceivedEventHandler onDataReceived() {
      return new SerialDataReceivedEventHandler(
        (sender, e) => {
          /** // var sp = (SerialPort)sender;
          // dataThatReced = sp.ReadExisting().Trim();
          // if (dataThatReced != "") {
          //   if (_rec_hex.Checked) {
          //     receiveArea.Text = StrHandler.str2Hex(dataThatReced);
          //   } else {
          //     receiveArea.Text = dataThatReced;
          //   }
          //   clearRec.Enabled = true;
          // } else {
          //   clearRec.Enabled = false;
          // } */

          //var recStr = "";
          try {
            var sp = port.getPort();
            dataThatReced += sp.ReadLine();
            // while(sp.BytesToRead > 0) {
            //   var rb = (char)sp.ReadByte();
            //   recStr.Append( rb );
            // }
            // receiveArea.Text = recStr.ToString();
          } catch(Exception err) {
            MessageBox.Show($"在读取时出现错误:\n{err}");
          }

          if (dataThatReced.Length > 0) {
            if (_rec_hex.Checked) {
              receiveArea.Text = StrHandler.str2HexV2(dataThatReced.ToString());
            } else {
              receiveArea.Text = dataThatReced;
            }
            clearRec.Enabled = true;
          } else {
            clearRec.Enabled = false;
          }
        }
      );
    }

    private void cacheText(object sender, EventArgs e) {
      var str = _send_input.Text.Trim();
      if (str == "") {
        _tip.Text = "内容不能为纯空格";
      }
      if (_send_string.Checked) {
        dataToSend = null;
        dataToSend = StrHandler.str2Byte(str);
        _formatShow.Text = str;
      } else {
        dataToSend = null;
        dataToSend = StrHandler.hex2Bytes(str);
        _formatShow.Text = StrHandler.byte2HexV2(dataToSend);
      }
    }

    private void clearReceive(object sender, EventArgs e) {
      dataThatReced = "";
      //dataThatReced.Clear();
      receiveArea.Text = "";
    }

    private void sendRadioClick(object sender, EventArgs e) {
      if (_send_hex.Checked) {
        sendIsHex = true;

        _send_input.Text = StrHandler
          .byte2HexV2(dataToSend);

        //_send_input.ReadOnly = true;
      } else {
        sendIsHex = false;

        _send_input.Text = StrHandler
          .byte2Str(dataToSend);

        //_send_input.ReadOnly = false;
      }
    }

    private void recRadioClick(object sender, EventArgs e) {
      if (_rec_hex.Checked) {
        recIsHex = true;
        receiveArea.Text = StrHandler
          .str2HexV2(dataThatReced.ToString());
      } else {
        recIsHex = false;
        receiveArea.Text = dataThatReced.ToString();
      }
    }

    private void toggleAutoSend(object sender, EventArgs e) {
      if(_autoSend.Checked) {
        timer.Start();
        timer.Elapsed += new System.Timers
          .ElapsedEventHandler(this.sendData);
      } else {
        timer.Close();
        timer.Elapsed -= new System.Timers
          .ElapsedEventHandler(this.sendData);
      }
    }

    private void onKeyDown(object sender, KeyEventArgs e) {
      if (e.KeyCode == Keys.Enter) {
        return;
      }
    }

////////////////////////////////////////////////////////////////////////////////
////Handler/////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
    private string toggleDataFormat() {
      return "";
    }
  }// class
}
