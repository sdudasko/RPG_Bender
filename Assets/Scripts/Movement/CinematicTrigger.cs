﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics // Test comment:)
{
    public class CinematicTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GetComponent<PlayableDirector>().Play();
        }
    }
}