using System.IO.Ports;

Console.WriteLine("Hello, World!");
var serialPorts = SerialPort.GetPortNames();
SerialPort serialPort = new SerialPort();
serialPort.PortName = "COM6";
serialPort.BaudRate = 9600;
serialPort.DataBits = 8;
serialPort.StopBits = StopBits.One;
serialPort.Parity = Parity.None;
serialPort.Open();

serialPort.Write("Hello World");
byte[] bytes = new byte[] { 0x01, 0x02, 0x03 };
serialPort.Write(bytes, 0, bytes.Length);
//serialPort.Read();

Console.WriteLine("等待数据，按任意键退出...");
Console.ReadKey();



serialPort.Close();