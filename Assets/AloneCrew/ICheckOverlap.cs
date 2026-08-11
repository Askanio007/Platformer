using System.Collections.Generic;
using AloneCrew.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace AloneCrew
{
    public abstract class ICheckOverlap : MonoBehaviour
    {
        public abstract GameObject[] GetObjectsInRange();
    }
}