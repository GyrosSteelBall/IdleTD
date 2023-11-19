using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageFeedbackConfig", menuName = "Game Config/Damage Feedback Config")]
public class DamageFeedbackConfigSO : ScriptableObject
{
    public Material blinkMaterial;
    public float blinkDuration = 0.1f;
}