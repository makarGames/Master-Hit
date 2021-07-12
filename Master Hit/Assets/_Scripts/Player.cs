using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private ShootController _weapon;

    private WayPoint _wayPointScript;

    private Dictionary<Type, IPlayerBehavior> _behaviorsMap;
    private IPlayerBehavior _behaviorCurrent;

    private int _amountPoints;
    private int _currentPoint = 0;

    public bool startRound { get; set; }

    public static Player S;

    private void Awake()
    {
        if (S == null)
            S = this;
    }

    private void Start()
    {
        startRound = false;
        _amountPoints = _wayPoints.Count;
        _wayPointScript = GetCurrentWayPoint().GetComponent<WayPoint>();
        InitBehaviors();
        SetBehaviorByDefault();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && startRound)
            _weapon.Shoot();
        if (_behaviorCurrent != null)
            _behaviorCurrent.Update();
    }

    public Transform GetCurrentWayPoint()
    {
        return _wayPoints[_currentPoint];
    }

    public void CheckForReadyToMove()
    {
        if (GetCurrentWayPoint() == _wayPoints[_amountPoints - 1])
        {
            GameOver.S.Finish();
            return;
        }

        if (_wayPointScript.HasEnemies())
        {
            _currentPoint++;
            _wayPointScript = GetCurrentWayPoint().GetComponent<WayPoint>();
            SetBehaviorRun();
        }
    }

    private void InitBehaviors()
    {
        _behaviorsMap = new Dictionary<Type, IPlayerBehavior>();

        _behaviorsMap[typeof(PlayerBehaviorIdle)] = new PlayerBehaviorIdle(_animator);
        _behaviorsMap[typeof(PlayerBehaviorRun)] = new PlayerBehaviorRun(_playerTransform, this);
    }

    private void SetBehavior(IPlayerBehavior newBehavior)
    {
        if (_behaviorCurrent != null)
            _behaviorCurrent.Exit();

        _behaviorCurrent = newBehavior;
        _behaviorCurrent.Enter();
    }

    private void SetBehaviorByDefault()
    {
        SetBehaviorIdle();
    }

    private IPlayerBehavior GetBehavior<T>() where T : IPlayerBehavior
    {
        var type = typeof(T);
        return _behaviorsMap[type];
    }

    public void SetBehaviorIdle()
    {
        var behavior = GetBehavior<PlayerBehaviorIdle>();
        SetBehavior(behavior);
    }

    public void SetBehaviorRun()
    {
        var behavior = GetBehavior<PlayerBehaviorRun>();
        SetBehavior(behavior);
    }

    public bool CurrentBehaviorIdle()
    {
        return _behaviorCurrent == GetBehavior<PlayerBehaviorIdle>();
    }
}
