using EvilEye.Events;
using EvilEye.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using VRC;
using VRC.Animation;
using VRC.SDKBase;

namespace EvilEye.Module.Movement
{
    class Fly : BaseModule, OnUpdateEvent
    {
        private VRCMotionState vrcMotionState;

        public Fly() : base("Fly", "Fly high", Main.Instance.movementButton, null, true)
        {
        }

        public override void OnEnable()
        {
            vrcMotionState = VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<VRCMotionState>();
            VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<CharacterController>().enabled = false;
            Main.Instance.onUpdateEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.onUpdateEvents.Remove(this);
            vrcMotionState.Method_Public_Void_0();
            VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<CharacterController>().enabled = true;
        }

        public void OnUpdate()
        {
            float right = 0f;
            float up = 0f;
            float forward = 0f;

            if (XRDevice.isPresent)
            {
                right = Input.GetAxis("Horizontal");
                up = Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical");
                forward = Input.GetAxis("Vertical");
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    forward += 1f;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    forward -= 1f;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    right += 1f;
                }

                if (Input.GetKey(KeyCode.A))
                {
                    right -= 1f;
                }

                if (Input.GetKey(KeyCode.E))
                {
                    up += 1f;
                }

                if (Input.GetKey(KeyCode.Q))
                {
                    up -= 1f;
                }
            }

            VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position += VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.right  * Networking.LocalPlayer.GetWalkSpeed() * Time.deltaTime * right * (float)(Input.GetKey(KeyCode.LeftShift) ? 8 : 1);
            VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position += VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.forward  * Networking.LocalPlayer.GetWalkSpeed() * Time.deltaTime * forward * (float)(Input.GetKey(KeyCode.LeftShift) ? 8 : 1);
            VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position += VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.up  * Networking.LocalPlayer.GetWalkSpeed() * Time.deltaTime * up * (float)(Input.GetKey(KeyCode.LeftShift) ? 8 : 1);

            vrcMotionState.Reset();
        }
    }
}
