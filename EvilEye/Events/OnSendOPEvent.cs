using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Events
{
    public interface OnSendOPEvent
    {
        bool OnSendOP(byte opCode, Il2CppSystem.Object parameters, RaiseEventOptions raiseEventOptions);
    }
}
