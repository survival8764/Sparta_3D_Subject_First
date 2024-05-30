public enum StatsChangeType
{
    Add,
    Multiple,
    Override
}

[System.Serializable]
public class CharacterStats
{
    public StatsChangeType statsChangeType;
    public CharacterSO characterSO;
}
