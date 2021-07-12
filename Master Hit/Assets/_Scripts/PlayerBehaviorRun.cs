using UnityEngine;

public class PlayerBehaviorRun : IPlayerBehavior
{
    private float _speed = 5f;
    private float _turnSpeed = 60f;

    private Player _player;

    private Quaternion _targetRotation;
    private Transform _wayPoint;
    private Transform _playerTransform;

    public PlayerBehaviorRun(Transform newTransform, Player player)
    {
        _playerTransform = newTransform;
        _player = player;
    }

    public void Enter()
    {
        _wayPoint = _player.GetCurrentWayPoint();
        SetRotationToPoint();
    }

    public void Exit()
    {

    }

    public void Update()
    {
        TurnToPoint();
        Moving();
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
            _player.SetBehaviorIdle();
    }
}
