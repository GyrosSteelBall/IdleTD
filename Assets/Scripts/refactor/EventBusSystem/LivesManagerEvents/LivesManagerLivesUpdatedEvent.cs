public class LivesManagerLivesUpdatedEvent
{
    public int Lives { get; private set; }

    public LivesManagerLivesUpdatedEvent(int lives)
    {
        Lives = lives;
    }
}