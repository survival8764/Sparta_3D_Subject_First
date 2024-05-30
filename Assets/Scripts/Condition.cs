using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    PlayerStatHandler handler;
    
    public float curValue;

    public Image uiBar;

    private void Awake() 
    {
        handler = GetComponent<PlayerStatHandler>();
    }

    private void Start()
    {
        curValue = handler.CurrentStat.playerSO.currenthealth;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }
 
    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, handler.CurrentStat.playerSO.maxhealth);
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return curValue / handler.CurrentStat.playerSO.maxhealth;
    }
}