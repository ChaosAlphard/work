using SerialPortDemo.DTO;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPortDemo.Model {
  public delegate void SerialPortEventHandler(object sender, SerialPortEventArgs e);

  public class SerialPortEventArgs : EventArgs {
    public bool isOpened = false;
    public byte[] receivedBytes = null;
  }

  class ComModel {
    private SerialPort port;

    public event SerialPortEventHandler comRecDataEvent = null;
    public event SerialPortEventHandler comOpenEvent = null;
    public event SerialPortEventHandler comCloseEvent = null;

    private Object comLock = new Object();

    public ComModel(SerialPort port) {
      this.port = port;
    }

    public Msg openPort() {
      if(port.IsOpen) {
        return new Msg(false, "Port is already been opened!");
      }
      var evArgs = new SerialPortEventArgs();
      try {
        port.Open();
        port.DataReceived += new SerialDataReceivedEventHandler(onDataReceived);
        evArgs.isOpened = true;
      } catch(Exception e) {
        evArgs.isOpened = false;
        MessageBox.Show(e.ToString());
        return new Msg(false, "Open fail!");
      } finally {
        if(comOpenEvent != null) {
          comOpenEvent.Invoke(this, evArgs);
        }
      }
      return new Msg(true, "Open success!");
    }

    public Msg closePort() {
      try {
        var closeThread = new Thread(new ThreadStart(closePortThread));
        closeThread.Start();
        return new Msg(true, "Close success!");
      } catch (Exception e) {
        MessageBox.Show(e.ToString());
        return new Msg(false, "Close fail!");
      }
    }

    private void closePortThread() {
      SerialPortEventArgs evArgs = new SerialPortEventArgs();
      try {
        port.Close();
        evArgs.isOpened = false;
        port.DataReceived -= new SerialDataReceivedEventHandler(onDataReceived);
      } catch(Exception e) {
        MessageBox.Show(e.ToString());
        throw new Exception("closeThreadErr");
      } finally {
        if(comCloseEvent != null) {
          comCloseEvent.Invoke(this, evArgs);
        }
      }
    }

    public Msg sendData(byte[] data) {
      if (!port.IsOpen) {
        return new Msg(false, "Oprt was not open!");
      }
      try {
        port.Write(data, 0, data.Length);
        return new Msg(true, "Send success");
      } catch(Exception e) {
        MessageBox.Show(e.ToString());
        return new Msg(false, "Send fail");
      }
    }

    public bool isOpen() {
      return port.IsOpen;
    }

////////////////////////////////////////////////////////////////////////////////
////Event///////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
    private void onDataReceived(object sender, SerialDataReceivedEventArgs e) {
      if(port.BytesToRead <= 0) {
        return;
      }
      //Thread Safety explain in MSDN:
      // Any public static (Shared in Visual Basic) members of this type are thread safe. 
      // Any instance members are not guaranteed to be thread safe.
      // So, we need to synchronize I/O
      lock (comLock) {
        int len = port.BytesToRead;
        byte[] data = new byte[len];
        try {
          port.Read(data, 0, len);
        } catch(Exception err) {
          MessageBox.Show(err.ToString());
          return;
        }
      }
    }

////////////////////////////////////////////////////////////////////////////////
////Property////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
    public ComModel setPortName(string portName) {
      port.PortName = portName;
      return this;
    }

    public ComModel setBaudRate(int baudRate) {
      port.BaudRate = baudRate;
      return this;
    }

    public ComModel setDataBits(int dataBits) {
      port.DataBits = dataBits;
      return this;
    }

    public ComModel setStopBits(StopBits stopBits) {
      port.StopBits = stopBits;
      return this;
    }

    public ComModel setParity(Parity parity) {
      port.Parity = parity;
      return this;
    }

    public ComModel setHandshake(Handshake handshake) {
      port.Handshake = handshake;
      if(handshake == Handshake.None) {
        port.RtsEnable = true;
        port.DtrEnable = true;
      }
      return this;
    }

    public ComModel setTimeout(int timeout) {
      port.ReadTimeout = timeout;
      port.WriteTimeout = timeout;
      return this;
    }

    public ComModel setReadTimeout(int timeout) {
      port.ReadTimeout = timeout;
      return this;
    }

    public ComModel setWriteTimeout(int timeout) {
      port.WriteTimeout = timeout;
      return this;
    }
  }// clasee
}
