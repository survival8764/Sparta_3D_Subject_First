using UnityEngine;

[CreateAssetMenuAttribute(fileName = "PlayerSO",menuName = "FactorySO/Character/Player", order = 1)]
public class PlayerSO : CharacterSO
{
    public float maxhealth;
    public float currenthealth;
    public float maxStamina;
    public float currentStamina;
}
