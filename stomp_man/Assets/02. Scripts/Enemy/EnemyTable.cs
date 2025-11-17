using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTable", menuName = "Game/EnemyTable")]
public class EnemyTable : ScriptableObject
{
    [System.Serializable]
    public struct EnemyData
    {
        public GameObject EnemyPrefab;
        public string EnemyName;
        [Range(0f, 1f)] public float SpawnChance;
    }

    public EnemyData[] enemys;
}