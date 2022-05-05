using System;
using System.Collections.Generic;
using System.Text;
using NModbus;
using System.Net.Sockets;
using System.Threading;
using System.IO.Ports;
namespace TouringCarHMI.Communication
{
        public  class ModbusTCP
        {
            private ModbusFactory modbusFactory;
            private IModbusMaster master;
            private TcpClient tcpClient;

            public string IPAdress { get; set; }
            public int Port { get; set; }

            public bool Connected
            {
            get
            {
                if (tcpClient == null)
                    return false;
                else 
                    return tcpClient.Connected;
            }  
            }

            public ModbusTCP(string ip, int port)
            {
            try
            {
                IPAdress = ip;
                Port = port;
                modbusFactory = new ModbusFactory();
                tcpClient = new TcpClient(IPAdress, Port);
                if(tcpClient!=null)
                {
                    master = modbusFactory.CreateMaster(tcpClient);
                    master.Transport.ReadTimeout = 2000;
                    master.Transport.Retries = 10;
                }     
            }
            catch
            {

            }
            }


            public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort num)
            {
                return master.ReadCoils(slaveAddress, startAddress, num);
            }

            public bool[] ReadInputs(byte slaveAddress, ushort startAddress, ushort num)
            {
                return master.ReadInputs(slaveAddress, startAddress, num);
            }

            public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort num)
            {
                return master.ReadHoldingRegisters(slaveAddress, startAddress, num);
            }

            public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort num)
            {
                return master.ReadInputRegisters(slaveAddress, startAddress, num);
            }

            public void WriteSingleCoil(byte slaveAddress, ushort startAddress, bool value)
            {
                master.WriteSingleCoil(slaveAddress, startAddress, value);
            }

            public void WriteSingleRegister(byte slaveAddress, ushort startAddress, ushort value)
            {
                master.WriteSingleRegister(slaveAddress, startAddress, value);
            }

            public void WriteMultipleCoils(byte slaveAddress, ushort startAddress, bool[] value)
            {
                master.WriteMultipleCoils(slaveAddress, startAddress, value);
            }

            public void WriteMultipleRegisters(byte slaveAddress, ushort startAddress, ushort[] value)
            {
                master.WriteMultipleRegisters(slaveAddress, startAddress, value);
            }

        }

    }

