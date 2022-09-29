using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPlayerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI racePosition;

    [SerializeField] private Image background;
    
    [SerializeField] private Sprite localPlayerBackground;
    [SerializeField] private Sprite otherPlayerBackground;


    public void SetLocalPlayerIndicator(bool isLocalPlayer)
    {
        background.sprite = isLocalPlayer ? localPlayerBackground : otherPlayerBackground;
    }
    
    public void SetName(string playerName)
    {
        this.playerName.text = playerName;
    }

    public void SetRacePosition(int racePosition)
    {
        this.racePosition.text = racePosition.ToString();
    }
}