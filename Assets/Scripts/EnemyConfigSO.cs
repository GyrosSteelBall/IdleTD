using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyConfig", menuName = "Enemies/Enemy Config")]
public class EnemyConfigSO : ScriptableObject
{
    public float speed;
    public float maxHealth;
    public int goldValue;
}