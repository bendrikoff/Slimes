using System;
using System.Timers;
using Code_Base.Mechanics.TimerReward;
using UnityEngine;

public class Timer
{
    public Action OnTimerEnd;
    public int Hours;
    public int Minutes;
    public int Seconds;
    
    public System.Timers.Timer gameTimer;
    private double timeSpentInSeconds;
    private double rewardIntervalInSeconds;
    
    public Timer(int startSeconds)
    {
        timeSpentInSeconds = 0;
        gameTimer = new System.Timers.Timer(1000);
        rewardIntervalInSeconds = startSeconds;
        gameTimer.Elapsed += OnTimerElapsed;
        gameTimer.Start();
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        timeSpentInSeconds += 1;
        CheckRewards();
        DisplayTimeRemaining();
    }

    private void CheckRewards()
    {
        if (timeSpentInSeconds >= rewardIntervalInSeconds)
        {
            GiveReward();
            gameTimer.Stop();
        }
    }

    private void GiveReward()
    {
        OnTimerEnd?.Invoke();
    }

    private void DisplayTimeRemaining()
    {
        double timeRemaining = rewardIntervalInSeconds - timeSpentInSeconds;

        if (timeRemaining > 0)
        {
            Hours = (int)(timeRemaining / 3600);
            Minutes = (int)((timeRemaining % 3600) / 60);
            Seconds = (int)(timeRemaining % 60);
        }
    }
}