using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "EnemyManager/Enemy Data", order = 51)]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private string enemyName;

    [SerializeField]
    private float health;

    [SerializeField]
    private float attackPower;

    [SerializeField]
    private float movementSpeed;

    // You can add more fields as needed

    // Public getters
    public GameObject EnemyPrefab { get { return enemyPrefab; } }
    public string EnemyName { get { return enemyName; } }
    public float Health { get { return health; } }
    public float AttackPower { get { return attackPower; } }
    public float MovementSpeed { get { return movementSpeed; } }
}
