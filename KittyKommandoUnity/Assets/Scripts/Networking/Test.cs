using System;
using FishNet.Object;
using Movement.InputControllers;
using Unity.VisualScripting;
using UnityEngine;

namespace Networking
{
     public class Test : NetworkBehaviour
     {
          [SerializeField] private InputController controller;
          public override void OnStartClient()
          {
               base.OnStartClient();
               if (!IsOwner)
               {
                    controller.enabled = false;
               }
          }
     }
}
