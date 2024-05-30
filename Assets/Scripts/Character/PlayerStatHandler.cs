using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    [SerializeField] private PlayerStats baseStat;
    public PlayerStats CurrentStat { get; private set; }
    public List<PlayerStats> statModifiers = new List<PlayerStats>();

    private void Awake() 
    {
        UpdatePlayerStat();
    }

    public void UpdatePlayerStat() 
    {
        PlayerSO _playerSO = baseStat.playerSO;

        CurrentStat = new PlayerStats
        {
            playerSO = _playerSO
        };

        ApplyModifiers();
    }


    private void ApplyModifiers()
    {
        foreach (var modifier in statModifiers)
        {
            CurrentStat.playerSO.currenthealth += modifier.playerSO.currenthealth;
        }
    }
    
    public void AddModifier(PlayerStats modifier)
    {
        statModifiers.Add(modifier);
        ApplyModifiers();
    }

    public void RemoveModifier(PlayerStats modifier)
    {
        statModifiers.Remove(modifier);
        ApplyModifiers();
    }

    public void AddHealthEffect()
    {
        if (CurrentStat.playerSO.currenthealth >= CurrentStat.playerSO.maxhealth)
            return;

        PlayerStats addStat = new PlayerStats
        {
            statsChangeType = StatsChangeType.Add,
            playerSO = ScriptableObject.CreateInstance<PlayerSO>()
        };

        addStat.playerSO.currenthealth = 1;

        AddModifier(addStat);
    }
}
