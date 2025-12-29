using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// InGame Timer으로 시간 변화에 따른 이벤트
/// </summary>
public class InGameTimer
{
    public Action<int> minuteUpdate = null;
    public Action timerUpdate = null;

    private Coroutine _inGameTimer;
    private int _seconds = 0;
    private int _minutes = 0;
    private int _hours = 0;

    public int GetSeconds { get { return _seconds; } }
    public int GetMinutes { get { return _minutes; } }
    public int GetHours { get { return _hours; } }
    public int GetTotalMinutes { get { return GetMinutes + GetHours * 60; } }
    public InGameTimer()
    {
        Managers.Game.inGameTimer = this;
        minuteUpdate += Managers.Game.IsStageCleared;
    }
    public void StartTimer()
    {
        _inGameTimer = CoroutineHelper.Start(Timer(), CoroutineType.InGameSystem);
    }
    public void StopTimer()
    {
        CoroutineHelper.Stop(_inGameTimer, CoroutineType.InGameSystem);
    }
    public void ReStart()
    {
        _seconds = _minutes = _hours = 0;

        StopTimer();

        _inGameTimer = CoroutineHelper.Start(Timer(), CoroutineType.InGameSystem);
    }
    private IEnumerator Timer()
    {
        while(!Managers.Game.GameOver)
        {
            _seconds++;

            if(_seconds == 60)
            {
                _seconds = 0;
                _minutes++;

                minuteUpdate?.Invoke(_minutes);

                if(_minutes == 60)
                {
                    _minutes = 0;
                    _hours++;
                }
            }

            if(Managers.Game.Playing)
            {
                timerUpdate?.Invoke();
            }

            yield return new WaitForSeconds(1);
        }
    }
}