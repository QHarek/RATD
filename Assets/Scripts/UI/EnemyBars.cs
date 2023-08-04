using UnityEngine;
using UnityEngine.UI;

public class EnemyBars : MonoBehaviour, IEnemyStatsObserver
{
    [SerializeField] private Slider _barHP;

    private Transform _canvasTransform;
    private EnemyHP _enemyStats;

    private void Awake()
    {
        _enemyStats = GetComponentInParent<EnemyHP>();
        _canvasTransform = GetComponent<Canvas>().transform;
    }

    private void Start()
    {
        FindObjectOfType<EnemyStatsSubject>().AddObserver(this);
    }

    private void LateUpdate()
    {
        _canvasTransform.LookAt(transform.position + Camera.main.transform.forward);
    }

    public void OnNotify(EnemyStatsAction enemyStatsAction)
    {
        if (enemyStatsAction == EnemyStatsAction.UpdateHP)
        {
            _barHP.maxValue = _enemyStats.MaxHP;
            _barHP.value = _enemyStats.CurrentHP;
        }

        if (enemyStatsAction == EnemyStatsAction.UpdateMana)
        {

        }
    }
}
