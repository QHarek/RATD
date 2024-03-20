using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _alignmentPathCenter;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _smoothAlignmentDistance;
    [SerializeField] private float _spiralPathRadius;
    [SerializeField] private float _alignmentPathRadius;
    [SerializeField] private float _spiralRadiusReductionPerSecond;
    [SerializeField] private float _spiralMinimumRadius;

    private EnemyBehavior _enemyBehavior;
    private Vector3 _towerPosition;
    private Vector3 _nextPredictedPosition;
    private float _alignmentPathAngle;
    private float _spiralPathAngle;
    private bool _onStartPath;
    private bool _onAlignmentPath;
    private bool _onSpiralPath;

    public Vector3 NextPredictedPosition => _nextPredictedPosition;

    private void Awake()
    {
        _enemyBehavior = GetComponent<EnemyBehavior>();
        _towerPosition = FindObjectOfType<TowerShooter>().gameObject.transform.position;
    }

    private void Start()
    {
        _onStartPath = true;
        _onAlignmentPath = false;
        _onSpiralPath = false;     
    }

    private void Update()
    {
        if(!_enemyBehavior.IsDead)
        {
            if (_onStartPath)
            {
                MoveLinear();
            }
            if (_onAlignmentPath)
            {
                SmoothAlignment();
            }
            if (_onSpiralPath)
            {
                MoveInSpiral();
            }
        }    
    }

    private void MoveLinear()
    {
        if (Vector3.Distance(_towerPosition, transform.position) > _smoothAlignmentDistance)
        {
            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        }
        else
        {
            _onStartPath = false;
            _onAlignmentPath = true;
            _alignmentPathAngle = Mathf.Atan2(transform.position.z - _alignmentPathCenter.z, transform.position.x - _alignmentPathCenter.x);
        }
    }

    private void SmoothAlignment()
    {
        if (Vector3.Distance(_towerPosition, transform.position) > _spiralPathRadius)
        {
            float angularSpeed = _moveSpeed * Time.deltaTime / _alignmentPathRadius;
            transform.position = PositionOnCircle(_alignmentPathCenter, _alignmentPathRadius, _alignmentPathAngle);
            _alignmentPathAngle -= angularSpeed;

            _nextPredictedPosition = PositionOnCircle(_alignmentPathCenter, _alignmentPathRadius, _alignmentPathAngle - _moveSpeed * Mathf.Deg2Rad);
            transform.LookAt(_nextPredictedPosition);
        }
        else
        {
            _onAlignmentPath = false;
            _onSpiralPath = true;
            _spiralPathAngle = Mathf.Atan2(transform.position.z - _towerPosition.z, transform.position.x - _towerPosition.x);
        }
    }

    private void MoveInSpiral()
    {
        if (_spiralPathRadius > _spiralMinimumRadius)
        {
            _spiralPathRadius -= _spiralRadiusReductionPerSecond * Time.deltaTime;
        }

        float angularSpeed = _moveSpeed * Time.deltaTime / _spiralPathRadius;
        transform.position = PositionOnCircle(_towerPosition, _spiralPathRadius, _spiralPathAngle);
        _spiralPathAngle += angularSpeed;

        _nextPredictedPosition = PositionOnCircle(_towerPosition, _spiralPathRadius, _spiralPathAngle + _moveSpeed * Mathf.Deg2Rad);
        transform.LookAt(_nextPredictedPosition);

    }

    public Vector3 PositionOnCircle(Vector3 center, float radius, float angle)
    {
        float x = center.x + radius * Mathf.Cos(angle);
        float y = center.y;
        float z = center.z + radius * Mathf.Sin(angle);
        return new Vector3(x, y, z);
    }
}
