﻿using System.Timers;
using Timer = System.Timers.Timer;

namespace BlazorToastNotifications.Services;

public class ToastService : IDisposable
{
    public event Action<string, ToastLevel>? OnShow;
    public event Action? OnHide;
    private Timer? Countdown;

    public void ShowToast(string message, ToastLevel level)
    {
        OnShow?.Invoke(message, level);
        StartCountdown();
    }

    private void StartCountdown()
    {
        SetCountdown();

        if (Countdown!.Enabled)
        {
            Countdown.Stop();
            Countdown.Start();
        }
        else
        {
            Countdown!.Start();
        }
    }

    private void SetCountdown()
    {
        if (Countdown != null) return;
        
        Countdown = new Timer(5000);
        Countdown.Elapsed += HideToast;
        Countdown.AutoReset = false;
    }

    private void HideToast(object? source, ElapsedEventArgs args) 
        => OnHide?.Invoke();

    public void Dispose() 
        => Countdown?.Dispose();
}