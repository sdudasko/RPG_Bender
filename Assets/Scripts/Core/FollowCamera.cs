using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        // Update is called once per frame
        void LateUpdate()
        {
            /**
             * transform.position je pozicia TOHO, co je attachnute na nas transform, cize nasa follow camera je attachnuta
             * Transform je ten nas player, chceme vediet jeho x, y, z - jeho transform
             */
            transform.position = target.position;
        }
    }
}
