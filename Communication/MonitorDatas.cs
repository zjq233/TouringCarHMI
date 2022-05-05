using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TouringCarHMI.Communication
{
    public class MonitorDatas: BindableBase
    {
        
        public MonitorDatas()
        {
            Alarms = new ObservableCollection<Alarm>();
        }

        private ObservableCollection<Alarm>alarms;
        public ObservableCollection<Alarm> Alarms
        {
            get { return alarms; }
            set
            {
                alarms = value;
                this.RaisePropertyChanged();
            }
        }
        private int cleanWater;               
        public int CleanWater
        {
            get { return cleanWater; }
            set
            {
                if (cleanWater == value)
                    return;
                cleanWater = value;
                this.RaisePropertyChanged();
                if (value < 20)
                {
                    UpdateException("CleanWater","净水量过低");
                }
                else
                {
                    UpdateException("CleanWater","");
                }
            }
        }

        public void UpdateException(string name, string msg)
        {
            //取序列中满足条件的第一个元素，如果没有元素满足条件，则返回默认值
            //（对于可以为null的对象，默认值为null
            var e = Alarms.FirstOrDefault(e => e.ValueName == name);
            if (e != null)
            {
                if (string.IsNullOrEmpty(msg))
                    Alarms.Remove(e);
                else
                    e.Content = msg;
            }
            else if(!string.IsNullOrEmpty(msg))
            {
                Alarms.Add(new Alarm (name, msg) );
            }
        }

        private int grayWater;

        public int GrayWater
        {
            get { return grayWater; }
            set
            {
                if(grayWater == value)
                    return;
                grayWater = value;
                this.RaisePropertyChanged();
                if (value > 50)
                {
                    UpdateException("GrayWater", "灰水量过高");
                }
                else
                {
                    UpdateException("GrayWater", "");
                }
            }
        }


        private int hotWater;

        public int HotWater
        {
            get { return hotWater; }
            set
            {
                if(hotWater == value)
                    return;
                hotWater = value;
                this.RaisePropertyChanged();
                if (value < 20)
                {
                    UpdateException("HotWater", "热水量过低");
                }
                else
                {
                    UpdateException("HotWater", "");
                }
            }
        }

        private int blackWater;

        public int BlackWater
        {
            get { return blackWater; }
            set
            {
                if(blackWater == value) return;
                blackWater = value;
                this.RaisePropertyChanged();
                if (value > 50)
                {
                    UpdateException("BlackWater", "黑水量过高");
                }
                else
                {
                    UpdateException("BlackWater", "");
                }
            }
        }
        private bool connectState=true;

        public bool ConnectState
        {
            get { return connectState; }
            set
            {
                if (connectState == value) return;
                connectState =value;
                this.RaisePropertyChanged();
                if (value==false)
                {
                    UpdateException("connectState", "连接断开");
                }
                else
                {
                    UpdateException("connectState", "");
                }
            }
        }


        private int battery;
        public int Battery
        {
            get { return battery; }
            set
            {
                if(battery == value) return;    
                battery = value;
                this.RaisePropertyChanged();
                if (value < 20)
                {
                    UpdateException("battery", "电池电量过低");
                }
                else
                {
                    UpdateException("battery", "");
                }
            }
        }

    }
}
