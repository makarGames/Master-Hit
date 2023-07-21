using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField, GetComponent] private Animator _animator;

        private static readonly int Run = Animator.StringToHash("Run");

        public void PlayIdle()
        {
            _animator.SetBool(Run, false);
        }

        public void PlayRun()
        {
            _animator.SetBool(Run, true);
        }
    }
}