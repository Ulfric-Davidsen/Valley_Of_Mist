using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSS.UI
{
    public class CameraFacing : MonoBehaviour
    {
        void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
