using TMPro;
using UnityEngine;

public class LeaderboardPlayerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI racePosition;
    
    public void SetName(string playerName)
    {
        this.playerName.text = playerName;
    }

    public void SetRacePosition(int racePosition)
    {
        this.racePosition.text = racePosition.ToString();
    }
}