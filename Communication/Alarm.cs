using System;
using System.Collections.Generic;
using System.Text;

namespace TouringCarHMI.Communication
{
    public class Alarm
    { 
        public Alarm(string name,string msg)
        {
            this.Content = msg;
            this.ValueName = name;
        }
        public string ValueName { get; set; }
        
        public string Content { get; set; }
    }
}
