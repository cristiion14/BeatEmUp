using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    Action timerCallBack;
    float timer;

   public void SetTimer(float timer, Action timerCallback)
    {
        this.timer = timer;
        this.timerCallBack = timerCallback;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer >0)
        {
            timer -= Time.deltaTime;
            if (ElapsedTimer())
                timerCallBack();
        }
        
    }

    public bool ElapsedTimer()
    {
        return timer <= 0f;
    }

}
