using UnityEngine;

namespace PlayerLogic.PlayerBehavior
{
    public class PlayerBehaviorRun : IPlayerBehavior
    {
        private readonly PlayerData _playerData;
        private float _speed = 5f;
        private float _turnSpeed = 60f;

        private Quaternion _targetRotation;
        private Transform _wayPoint;
        private Transform _playerTransform;

        public PlayerBehaviorRun(PlayerData playerData)
        {
            _playerData = playerData;
            _playerTransform = _playerData.Transform;
        }

        public void Enter()
        {
            _wayPoint = Player.S.GetCurrentWayPoint();
            SetRotationToPoint();
            _playerData.PlayerAnimator.PlayRun();
        }

        public void Update()
        {
            TurnToPoint();
            Moving();
        }

        public void Exit()
        {
        }

        private void SetRotationToPoint()
        {
            Vector3 targetDir = _wayPoint.position - _playerTransform.position;
            targetDir.y = 0.0f;
            _targetRotation = Quaternion.LookRotation(targetDir);
        }

        private void Moving()
        {
            float step = _speed * Time.deltaTime;

            _playerTransform.position = Vector3.MoveTowards(_playerTransform.position, _wayPoint.position, step);
            CheckForDistance(_playerTransform.position, _wayPoint.position);
        }

        private void TurnToPoint()
        {
            float step = _turnSpeed * Time.deltaTime;

            _playerTransform.rotation = Quaternion.RotateTowards(_playerTransform.rotation, _targetRotation, step);
        }

        private void CheckForDistance(Vector3 start, Vector3 end)
        {
            float distance = Vector3.Distance(start, end);

            if (distance < 0.5f)
                Player.S.SetBehaviorIdle();
        }
    }
}