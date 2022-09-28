using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard Instance;

    [SerializeField] private LeaderboardPlayerView leaderboardPlayerUI;
    [SerializeField] private RectTransform leaderboardContainer;

    private List<LeaderBoardPlacement> leaderBoardPlacements = new List<LeaderBoardPlacement>();
    private List<LeaderboardPlayerView> leaderboardPlayerViews = new List<LeaderboardPlayerView>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void RegisterPlayer(string playerNumber)
    {
        string playerName = "Player " + playerNumber;
        
        Debug.Log($"Incoming player with name: {playerName}");
        
        if (leaderBoardPlacements.Any(p => p.playerName == playerName)) return;
     
        Debug.Log($"Registering player with name: {playerName}");
        
        leaderBoardPlacements.Add(new LeaderBoardPlacement(playerName));

        LeaderboardPlayerView playerView = Instantiate(leaderboardPlayerUI, leaderboardContainer);
        leaderboardPlayerViews.Add(playerView);
        playerView.SetRacePosition(leaderboardPlayerViews.Count);
        
        UpdatePlayerPlacement(playerName, 0, 0);

        UpdateUI();
    }

    public void UpdatePlayerPlacement(string playerName, int labs, int checkPointNumber)
    {
        LeaderBoardPlacement placement = leaderBoardPlacements.FirstOrDefault(p => p.playerName == playerName);

        if (string.IsNullOrEmpty(placement.playerName)) return;

        placement.labsCompleted = labs;
        placement.checkPointNumber = checkPointNumber;

        UpdateUI();
    }

    private void UpdateUI()
    {
        leaderBoardPlacements
            .OrderBy(p => p.labsCompleted)
            .ThenBy(p => p.checkPointNumber);

        for (int i = 0; i < leaderBoardPlacements.Count; i++)
        {
            Debug.Log($"Position {i} {leaderBoardPlacements[i].playerName}");
            leaderboardPlayerViews[i].SetName(leaderBoardPlacements[i].playerName);
        }
    }
    
    private struct LeaderBoardPlacement
    {
        public LeaderBoardPlacement(string playerName)
        {
            this.playerName = playerName;
            labsCompleted = 0;
            checkPointNumber = 0;
        }
        
        public string playerName;
        public int labsCompleted;
        public int checkPointNumber ;
    }
}