using System;
using FishNet.Object;
using Movement.InputControllers;
using Unity.VisualScripting;
using UnityEngine;

namespace Networking
{
     public class DeactivateComponentsForNpc : NetworkBehaviour
     {
          [SerializeField] private MonoBehaviour[] components;
          public override void OnStartClient()
          {
               base.OnStartClient();
               if (!IsOwner)
               {
                    foreach (var component in components)
                    {
                         component.enabled = false;
                    }
               }
          }
     }
}
