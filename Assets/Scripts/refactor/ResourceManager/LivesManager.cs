using UnityEngine;

public class LivesManager : Singleton<LivesManager>
{
    [SerializeField]
    private int lives = 50; // Or any initial value

    void OnEnable()
    {
        EventBus.Instance.Publish(new LivesManagerLivesUpdatedEvent(lives));
        EventBus.Instance.Subscribe<EnemyControllerReachedFinalWaypointEvent>(OnEnemyReachedFinalWaypoint);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<EnemyControllerReachedFinalWaypointEvent>(OnEnemyReachedFinalWaypoint);
    }

    private void OnEnemyReachedFinalWaypoint(EnemyControllerReachedFinalWaypointEvent inputEvent)
    {
        lives--;
        EventBus.Instance.Publish(new LivesManagerLivesUpdatedEvent(lives));
        // Check if lives are 0 and end the game if necessary
    }
}