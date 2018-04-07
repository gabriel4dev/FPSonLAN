[System.Serializable]
public class MatchSettings
{

    public float respawnTime { get; set; }

    public MatchSettings()
    {
        this.respawnTime = 3f;
    }
}
