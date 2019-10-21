using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using SerialPortDemo.DTO;
using SerialPortDemo.Model;
using SerialPortDemo.Handler;

namespace SerialPortDemo {
  public partial class BaseForm : Form {

    // 串口对象
    private ComModel port = new ComModel(new SerialPort());
    // msg
    private StringBuilder recStr = new StringBuilder();
    private string recedStr = "";

    private bool __sendIsHex = false;
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
    }

    // 主窗体加载
    public void onLoad(object sender, EventArgs e) {
      SPHandler.updateSerialPortList(_comCbx);
    }

    // 更新
    private void onSPUpdate(object sender, EventArgs e) {
      SPHandler.updateSerialPortList(_comCbx);
    }

    // 切换串口状态
    private void onTogglePort(object sender, EventArgs e) {
      if (port.isOpen()) {
        try {
          port.setPortName(_comCbx.Text.Trim())
            .setBaudRate(Convert.ToInt32(_botCbx.Text.Trim()))
            .setDataBits(Convert.ToInt32(_dataCbx.Text.Trim()))
            .setStopBits(SPHandler.getStopBits(_stopCbx.SelectedIndex))
            .setParity(SPHandler.getParity(_parityCbx.SelectedIndex))
            .setHandshake(SPHandler.getHandshake(_handCbx.SelectedIndex))
            .setTimeout(1000);
        } catch(Exception err) {
          MessageBox.Show(err.ToString());
          return;
        }
        openPort();
      } else {
        closePort();
      }
      
      // if(!port.IsOpen) {
      //   bool openSucc = SPHandler.openSPort(
      //     port, _comCbx.Text.Trim(),  // 串口名
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
      //     port.DataReceived += onDataReceived();
      //   }
      //   
      // } else {
      //   bool closeSucc = SPHandler.closeSPort(port,_tip);
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
      //     port.DataReceived -= onDataReceived();
      //   }
      // }
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
        _send_input.Enabled = _send_submit.Enabled = true;
      } else {
        _tip.Text = openMsg.msg;
      }
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
        _send_input.Enabled = _send_submit.Enabled = false;
      } else {
        _tip.Text = closeMsg.msg;
      }
    }

    // 发送数据
    private void sendData(object sender, EventArgs e) {
      if(!port.isOpen()) {
        MessageBox.Show("串口没有打开");
        return;
      }
      var dataToSend = _send_input.Text.Trim();
      if(dataToSend == "") {
        MessageBox.Show("数据不能为空");
        return;
      }
      port.sendData(StrHandler.str2Byte(dataToSend));
    }

////////////////////////////////////////////////////////////////////////////////
////Event///////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
    private SerialDataReceivedEventHandler onDataReceived() {
      return new SerialDataReceivedEventHandler(
        (sender, e) => {
          var sp = (SerialPort)sender;
          string data = sp.ReadExisting().Trim();
          Console.WriteLine(data);
          if(data != "") {
            receiveArea.Text = data;
            clearRec.Enabled = true;
          } else {
            clearRec.Enabled = false;
          }
          // try {
          //   var strbud = new StringBuilder();
          //   while(port.BytesToRead > 0) {
          //     var sch = (char)port.ReadByte();
          //     Console.WriteLine(sch);
          //     recStr.Append( (char)port.ReadByte() );
          //   }
          //   receiveArea.Text = recStr.ToString();
          // } catch(Exception err) {
          //   MessageBox.Show($"在读取时出现错误:\n{err}");
          // }
        }
      );
    }

    private void sendRadioClick(object sender, EventArgs e) {
      if (_send_hex.Checked) {
        sendIsHex = true;

        _send_input.Text = StrHandler
          .str2Hex(_send_input.Text);

        _send_input.ReadOnly = true;
      } else {
        sendIsHex = false;

        _send_input.Text = StrHandler
          .hex2Str(_send_input.Text);

        _send_input.ReadOnly = false;
      }
    }

    private void recRadioClick(object sender, EventArgs e) {
      if (_rec_hex.Checked) {
        recIsHex = true;
        receiveArea.Text = StrHandler
          .hex2Str(_send_input.Text);
      } else {
        recIsHex = false;
        receiveArea.Text = StrHandler
          .hex2Str(_send_input.Text);
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
