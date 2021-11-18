using Photon.Realtime;

namespace EvilEye.Events
{
    public interface OnSendOPEvent
    {
        bool OnSendOP(byte opCode, Il2CppSystem.Object parameters, RaiseEventOptions raiseEventOptions);
    }
}
