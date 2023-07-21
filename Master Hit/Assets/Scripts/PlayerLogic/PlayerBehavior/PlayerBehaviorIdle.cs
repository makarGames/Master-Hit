using UnityEngine;

namespace PlayerLogic.PlayerBehavior
{
    public class PlayerBehaviorIdle : IPlayerBehavior
    {
        private Animator _animator;
        private Player _player;

        public PlayerBehaviorIdle(Animator newAnimator)
        {
            _animator = newAnimator;
            _player = Player.S;
        }

        public void Enter()
        {
            _animator.SetBool("Idle", true);
        }

        public void Exit()
        {
            _animator.SetBool("Idle", false);
        }

        public void Update()
        {
            if (_player.startRound)
                _player.CheckForReadyToMove();
            else if (Input.GetMouseButtonDown(0))
                _player.startRound = true;
        }
    }
}