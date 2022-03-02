using System;
using UnityEngine;

namespace Core
{
    public class Timer : MonoBehaviour
    {
        private Action timerCallback;
        private float timer;
        
        public void CreateTimer(float timer, Action timerCallback)
        {
            this.timer = timer;
            this.timerCallback = timerCallback;
        }

        private void Update()
        {
            if (timer >= 0f)
            {
                timer -= Time.deltaTime;
                if (IsTimerComplete())
                {
                    timerCallback();
                }
            }
        }

        public bool IsTimerComplete()
        {
            return timer <= 0f;
        }
    }
}