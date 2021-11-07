using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amterp.Utils {
    public class SetActiveOnLoad : MonoBehaviour {
        [SerializeField] private bool _startActive;

        void Start() {
            gameObject.SetActive(_startActive);
        }
    }
}