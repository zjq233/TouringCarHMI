using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using TouringCarHMI.Communication;

namespace TouringCarHMI.Common
{
    public class GetModbusClass:PubSubEvent<ModbusTCP>
    {

    }
}
