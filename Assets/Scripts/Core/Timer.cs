using System;
using UnityEngine;

namespace Core
{
    public class Timer : MonoBehaviour
    {
        private Action timerCallback;
        private Action<GameObject> timerCallbackGameObject;
        private float timer;
        
        public void CreateTimer(float timer, Action timerCallback)
        {
            this.timer = timer;
            this.timerCallback = timerCallback;
        }
        
        public void CreateTimer(float timer, Action<GameObject> timerCallbackGameObject)
        {
            this.timer = timer;
            this.timerCallbackGameObject = timerCallbackGameObject;
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