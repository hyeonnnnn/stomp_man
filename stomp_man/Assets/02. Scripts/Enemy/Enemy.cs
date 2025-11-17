using UnityEngine;

public enum EnemyType
{
    Ground,
    Sky
}

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;
}
