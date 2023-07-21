using System;
using UnityEngine;

namespace PlayerLogic
{
    [Serializable]
    public class PlayerData
    {
        [field:SerializeField] public Transform Transform { get; private set; }
        [field:SerializeField] public PlayerAnimator PlayerAnimator { get; private set; }
    }
}