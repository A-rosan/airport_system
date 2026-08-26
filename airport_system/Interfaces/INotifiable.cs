using System;
using System.Collections.Generic;
using System.Text;

namespace airport_system.Interfaces
{
    public interface INotifiable
    {
        void SendNotification(string message);
    }
}
