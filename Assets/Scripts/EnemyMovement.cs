using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _timeForSingleCircle;

    private const float DistanceToStartCurve = 7.5f;
    private const float StartCurveRotationSpeedModifier = 2.5f;
    private const int StartCurveBeginAngle = 265;
    private const int StartCurveEndAngle = 335;
    private const int StartPathAngle = 40;
    private const int StartCorridorSpeedModifier = 2;

    public const float PathRadius = 5.5f;

    private EnemyBehavior _enemyBehavior;
    private Vector3 _startCurveCircleCenter = new Vector3(-9.4f, 0.1f, -5.1f);
    private Vector3 _pathCircleCenter = new Vector3(0, 0.1f, 0);
    private float _nextPathAngle;
    private float _angleVelocity;
    private bool _onSpawnCorridor;
    private bool _onStartCurve;
    private bool _onPath;

    public float AngleVelocity => _angleVelocity;
    public float NextPathAngle => _nextPathAngle;
    public bool OnPath => _onPath;

    private void Awake()
    {
        _enemyBehavior = GetComponent<EnemyBehavior>();
    }

    private void Start()
    {
        _nextPathAngle = 0;
        _angleVelocity = (Mathf.PI * 2) / _timeForSingleCircle;
        _onSpawnCorridor = true;
        _onStartCurve = false;
        _onPath = false;
    }

    private void Update()
    {
        if(_enemyBehavior.IsDead == false)
        {
            if (_onSpawnCorridor)
                MoveOnStartCorridor();
            if (_onStartCurve)
                MoveOnStartCurve();
            if (_onPath)
                MoveOnPath();
        }
    }

    private void MoveOnStartCorridor()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > DistanceToStartCurve)
            transform.Translate(Vector3.forward * Time.deltaTime * StartCorridorSpeedModifier);
        else
        {
            _onSpawnCorridor = false;
            _onStartCurve = true;
            _nextPathAngle = StartCurveBeginAngle * Mathf.Deg2Rad * _angleVelocity;
        }
    }

    private void MoveOnStartCurve()
    {
        _nextPathAngle += Time.deltaTime * _angleVelocity;
        float newEnemyX = _startCurveCircleCenter.x - Mathf.Cos(_nextPathAngle) * PathRadius;
        float newEnemyY = _startCurveCircleCenter.y;
        float newEnemyZ = _startCurveCircleCenter.z + Mathf.Sin(_nextPathAngle) * PathRadius;
        transform.position = new Vector3(newEnemyX, newEnemyY, newEnemyZ);
        transform.Rotate(0, 360 / _timeForSingleCircle * Time.deltaTime * StartCurveRotationSpeedModifier, 0);

        if (_nextPathAngle > StartCurveEndAngle * Mathf.Deg2Rad * _angleVelocity)
        {
            _onPath = true;
            _onStartCurve = false;
            _nextPathAngle = StartPathAngle * Mathf.Deg2Rad * _angleVelocity;
        }
    }

    private void MoveOnPath()
    {
        _nextPathAngle += Time.deltaTime * _angleVelocity;
        float newEnemyX = _pathCircleCenter.x - Mathf.Cos(_nextPathAngle) * PathRadius;
        float newEnemyY = _pathCircleCenter.y;
        float newEnemyZ = _pathCircleCenter.z - Mathf.Sin(_nextPathAngle) * PathRadius;
        transform.position = new Vector3(newEnemyX, newEnemyY, newEnemyZ);
        transform.Rotate(0, -360 / _timeForSingleCircle * Time.deltaTime, 0);
    }
}
