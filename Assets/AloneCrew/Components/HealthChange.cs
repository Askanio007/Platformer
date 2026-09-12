using System;
using UnityEngine.Events;

namespace AloneCrew.Components
{
    [Serializable]
    public class HealthChange : UnityEvent<int>
    {}
}