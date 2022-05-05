using System;
using System.Collections.Generic;
using System.Text;

namespace TouringCarHMI.Models
{
    public class SwitchBar
    {
        private string tag;
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public bool State { get; set; }

        public ushort Address { get; set; }
    }
}
