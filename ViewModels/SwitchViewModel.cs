using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TouringCarHMI.Common;
using TouringCarHMI.Communication;
using TouringCarHMI.Models;

namespace TouringCarHMI.ViewModels
{
    public class SwitchViewModel : BindableBase
    {
        public CancellationTokenSource tokenSource = new CancellationTokenSource();
        private ModbusTCP modbus;
        public DelegateCommand<SwitchBar> SwitchCommand { get; private set; }
        public SwitchViewModel()
        {
             SwitchBars = new ObservableCollection<SwitchBar>();
            CreateSwitcBar();
            SetSubscribe();
            SwitchCommand = new DelegateCommand<SwitchBar>(SwitchChanged);
        }

        public void SwitchChanged(SwitchBar obj)
        {
            
        }

        void CreateSwitcBar()
        {
            SwitchBars.Add(new SwitchBar() { Title = "插座",Tag= "\uec76",State=false,Address=1});
            SwitchBars.Add(new SwitchBar() { Title = "电视", Tag = "\ue600", State = false, Address = 2 });
            SwitchBars.Add(new SwitchBar() { Title = "电热水器", Tag = "\ue63d", State = false, Address = 3 });
            SwitchBars.Add(new SwitchBar() { Title = "冰箱", Tag = "\ue90b" , State = false, Address = 4 });
            SwitchBars.Add(new SwitchBar() { Title = "空调", Tag = "\ue659", State = false, Address = 5 });
            SwitchBars.Add(new SwitchBar() { Title = "换气扇", Tag = "\ued5c" , State = false, Address = 6 });
            SwitchBars.Add(new SwitchBar() { Title = "防冻", Tag = "\ue641", State = false, Address = 7 });
            SwitchBars.Add(new SwitchBar() { Title = "卫星电视", Tag = "\ue691", State = false, Address = 8 });
            SwitchBars.Add(new SwitchBar() { Title = "外侧灯", Tag = "\ue856" , State = false, Address = 9 });
            SwitchBars.Add(new SwitchBar() { Title = "冷水泵", Tag = "\ue657", State = false, Address = 10 });
            SwitchBars.Add(new SwitchBar() { Title = "监控", Tag = "\ue901", State = false, Address = 11 });
            SwitchBars.Add(new SwitchBar() { Title = "射灯", Tag = "\ue60a", State = false, Address = 12 });
            SwitchBars.Add(new SwitchBar() { Title = "顶灯", Tag = "\ue768", State = false, Address = 13 });
            SwitchBars.Add(new SwitchBar() { Title = "充电/逆变", Tag = "\ue63c", State = false, Address = 14 });
            SwitchBars.Add(new SwitchBar() { Title = "备用", Tag = "\ue613", State = true, Address = 15 });
        }

        private ObservableCollection<SwitchBar> switchBars;

        public ObservableCollection<SwitchBar> SwitchBars
        {
            get { return switchBars; }
            set
            {
                switchBars = value;
                this.RaisePropertyChanged();
            }
        }

        public void SetSubscribe()
        {
            EventAggregatorRepository
                .GetInstance()
                .eventAggregator
                .GetEvent<GetModbusClass>()
                .Subscribe(ReceiveModbus, ThreadOption.UIThread, true);
        }
        public void ReceiveModbus(ModbusTCP modbus)
        {
           this.modbus=modbus;
            Task task=Task.Run(() => {
                while (this.modbus.Connected && !tokenSource.IsCancellationRequested)
                {
                    Task.Delay(500).Wait();
                    bool[] temp = modbus.ReadCoils(1, 1, 15);
                    if (temp != null)
                    {
                        App.Current?.Dispatcher.Invoke(new Action(() =>
                        {
                            for (int i = 0; i < temp.Length; i++)
                            {
                                SwitchBars[i]=new SwitchBar() { Address=SwitchBars[i].Address,Title=SwitchBars[i].Title,Tag=SwitchBars[i].Tag,State=temp[i]};
                            }
                           
                        }));
                       
                    }
                }
            }, tokenSource.Token);
        }
    }
}
