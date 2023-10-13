using UnityEngine;

public class EnemiesCounter : MonoBehaviour, IEnemyObserver
{
    private const string ENEMYCOUNTERLABEL = "Enemies: ";

    private TMPro.TextMeshProUGUI _textComponent;
    private int _enemiesCount;

    private void Awake()
    {
        _textComponent = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Start()
    {
        _enemiesCount = 0;
    }

    public void OnNotify(EnemyAction enemyAction)
    {
        switch (enemyAction)
        {
            case EnemyAction.Died:
                {
                    _enemiesCount--;
                    _textComponent.text = ENEMYCOUNTERLABEL + _enemiesCount.ToString();
                    break;
                }
            case EnemyAction.Spawned:
                {
                    _enemiesCount++;
                    _textComponent.text = ENEMYCOUNTERLABEL + _enemiesCount.ToString();
                    break;
                }
        }
    }
}
