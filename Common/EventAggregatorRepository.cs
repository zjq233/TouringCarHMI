using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace TouringCarHMI.Common
{
    public class EventAggregatorRepository
    {
        public EventAggregator eventAggregator;
        private static EventAggregatorRepository eventAggregatorRepository;
        private static object _lock =new object();

        public EventAggregatorRepository()
        {
            eventAggregator = new EventAggregator();
        }

        public static EventAggregatorRepository GetInstance()
        {
            if (eventAggregatorRepository == null)
            {
                lock (_lock)
                {
                    if(eventAggregatorRepository == null)
                            eventAggregatorRepository = new EventAggregatorRepository();
                }
            }
           
            return eventAggregatorRepository;
        }

    }
}
