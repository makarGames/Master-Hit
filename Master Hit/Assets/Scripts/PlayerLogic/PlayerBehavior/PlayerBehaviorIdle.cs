using UnityEngine;

namespace PlayerLogic.PlayerBehavior
{
    public class PlayerBehaviorIdle : IPlayerBehavior
    {
        private readonly PlayerData _playerData;

        public PlayerBehaviorIdle(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void Enter()
        {
            _playerData.PlayerAnimator.PlayIdle();
        }

        public void Update()
        {
            if (Player.S.startRound)
                Player.S.CheckForReadyToMove();
            else if (Input.GetMouseButtonDown(0))
                Player.S.startRound = true;
        }

        public void Exit()
        {
        }
    }
}