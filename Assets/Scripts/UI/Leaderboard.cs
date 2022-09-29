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
        
        if (leaderBoardPlacements.Any(p => p.PlayerName == playerName)) return;
     
        Debug.Log($"Registering player with name: {playerName}");
        
        leaderBoardPlacements.Add(new LeaderBoardPlacement(playerName));

        LeaderboardPlayerView playerView = Instantiate(leaderboardPlayerUI, leaderboardContainer);
        leaderboardPlayerViews.Add(playerView);
        playerView.SetRacePosition(leaderboardPlayerViews.Count);
        
        UpdatePlayerPlacement(playerName, 0, 0);

        UpdateUI();
    }

    public void UpdatePlayerPlacement(string playerName, int laps, int checkPointNumber)
    {
        Debug.Log($"{playerName} has completed {laps} laps and is at checkpoint {checkPointNumber}");
        
        LeaderBoardPlacement placement = leaderBoardPlacements.FirstOrDefault(p => p.PlayerName == playerName);

        if (string.IsNullOrEmpty(placement.PlayerName)) return;

        placement.LapsCompleted = laps;
        placement.CheckPointNumber = checkPointNumber;

        UpdateUI();
    }

    private void UpdateUI()
    {
        leaderBoardPlacements = leaderBoardPlacements
            .OrderByDescending(p => p.LapsCompleted)
            .ThenByDescending(p => p.CheckPointNumber).ToList();

        for (int i = 0; i < leaderBoardPlacements.Count; i++)
        {
            Debug.Log($"Position {i+1} {leaderBoardPlacements[i].PlayerName}");
            leaderboardPlayerViews[i].SetName(leaderBoardPlacements[i].PlayerName);
        }
    }
    
    private class LeaderBoardPlacement
    {
        public LeaderBoardPlacement(string playerName)
        {
            PlayerName = playerName;
            LapsCompleted = 0;
            CheckPointNumber = 0;
        }
        
        public readonly string PlayerName;
        public int LapsCompleted;
        public int CheckPointNumber ;
    }
}