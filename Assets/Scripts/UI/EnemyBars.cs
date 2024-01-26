using UnityEngine;
using UnityEngine.UI;

public class EnemyBars : MonoBehaviour, IEnemyStatsObserver
{
    [SerializeField] private Slider _barHP;

    private Transform _canvasTransform;
    private EnemyStats _enemyStats;

    private void Awake()
    {
        _enemyStats = GetComponentInParent<EnemyStats>();
        _canvasTransform = GetComponent<Canvas>().transform;
    }

    private void Start()
    {
        FindObjectOfType<EnemyStatsSubject>().AddObserver(this);
        _barHP.maxValue = _enemyStats.MaxHP;
        _barHP.value += _barHP.maxValue;
    }

    private void LateUpdate()
    {
        _canvasTransform.LookAt(transform.position + Camera.main.transform.forward);
    }

    public void OnNotify(EnemyStatsAction enemyStatsAction, float value)
    {
        if (enemyStatsAction == EnemyStatsAction.UpdateHP)
        {
            _barHP.maxValue = _enemyStats.MaxHP;
            _barHP.value += value;
        }

        if (enemyStatsAction == EnemyStatsAction.UpdateMP)
        {

        }
    }
}
