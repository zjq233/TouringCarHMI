using NModbus;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TouringCarHMI.Common;
using TouringCarHMI.Communication;

namespace TouringCarHMI.ViewModels
{
    public class FirstViewModel : MonitorDatas
    {
       public CancellationTokenSource tokenSource = new CancellationTokenSource();
        private ModbusTCP modbus;

        private DispatcherTimer showtimer;

        private string systemDay;
        public string SystemDay
        {
            get { return systemDay; }
            set
            {
                systemDay = value;
                this.RaisePropertyChanged();
            }
        }


        private string systemTime;
        public string SystemTime
        {
            get { return systemTime; }
            set
            {
                systemTime = value;
                this.RaisePropertyChanged();
            }
        }

        //public bool QuitFlag { get;  set; }

        public void ConnectExecute(string ip,int port)
        {
            App.Current?.Dispatcher.Invoke(new Action(() =>
            ConnectState = false));
            modbus = new ModbusTCP(ip, port);
            if(!modbus.Connected)
            {
                Task task = Task.Run(() => {
                 do  {
                        Task.Delay(200).Wait();
                        modbus = new ModbusTCP(ip, port);
                    }
                    while (!modbus.Connected&& !tokenSource.IsCancellationRequested) ;
                }, tokenSource.Token);
                task.ContinueWith(t => Monitor());
            }
            else
               Monitor();
            //try
            //{
            //    if(modbus == null)
            //         modbus = new ModbusTCP(ip, port);
            //    if (!modbus.Connected)
            //        modbus = new ModbusTCP(ip, port);
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine(ex.Message);
            //}
        }

        public FirstViewModel()
        {
            showtimer = new DispatcherTimer();
            showtimer.Tick += new EventHandler(
                (object sender, EventArgs e) => {
                    SystemDay = DateTime.Now.ToString("yyy-MM-dd   ") + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                    SystemTime = DateTime.Now.ToString("HH:mm:ss");
                }) ;
            showtimer.Interval=new TimeSpan(0, 0, 0, 1, 0);
            showtimer.Start();
            SetSubscribe();
            ConnectExecute("127.0.0.1", 502);
        }

        public void SetSubscribe()
        {
            EventAggregatorRepository
                .GetInstance()
                .eventAggregator
                .GetEvent<GetQuitMessage>()
                .Subscribe(ReceiveQuitMessage, ThreadOption.UIThread, true);
        }
        public void ReceiveQuitMessage(bool Flag)
        {
            //QuitFlag = Flag;
            tokenSource.Cancel();

        }

        private void SendModbusClass(ModbusTCP modbus)
        {
            EventAggregatorRepository.GetInstance().eventAggregator.GetEvent<GetModbusClass>().Publish(modbus);
        }


        public void Monitor()
        {
            //if(!modbus.Connected)
            //{
            //    return;
            //}
            App.Current?.Dispatcher.Invoke(() =>
             ConnectState = true);
            SendModbusClass(modbus);
            Task task = Task.Run(() =>
              {
              try
              {
                //this.ConnectExecute("127.0.0.1", 502);
                      while (modbus.Connected && !tokenSource.IsCancellationRequested)
                      {
                          Task.Delay(200).Wait();
                          ushort[] temp = modbus.ReadHoldingRegisters(1, 0, 5);
                          if (temp!= null)
                          {
                              App.Current?.Dispatcher.Invoke(new Action(() =>
                                              {
                                                  CleanWater = temp[0];
                                                  HotWater = temp[1];
                                                  GrayWater = temp[2];
                                                  BlackWater = temp[3];
                                                  Battery = temp[4];
                                              }));
                           } 
                      }
                      
                  }
                  catch (Exception ex)
                  {
                      System.Diagnostics.Debug.WriteLine(ex.Message);
                  }
              },tokenSource.Token);
            //task.Start();
            task.ContinueWith(t => ConnectExecute("127.0.0.1", 502));
        }
    }
}
