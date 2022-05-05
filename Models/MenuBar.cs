using System;
using System.Collections.Generic;
using System.Text;

namespace TouringCarHMI.Models
{
    public class MenuBar
    {
        private string tag;
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Tag
        {
            get { return tag; }
            set { tag= value; }
        }

        private string title;
        /// <summary>
        /// 菜单标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string nameSpace;
        /// <summary>
        /// 菜单命名空间
        /// </summary>
        public string NameSpace
        {
            get { return nameSpace; }
            set { nameSpace = value; }
        }

        //private bool state;
        ///// <summary>
        ///// 菜单图标
        ///// </summary>
        //public bool State
        //{
        //    get { return state; }
        //    set { state = value; }
        //}
    }
}
