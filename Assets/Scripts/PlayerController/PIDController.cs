using UnityEngine;

//Proportional Integral Derivative Controller
    namespace PlayerController
    {
        [System.Serializable]
        public class PIDController
        {
            //Our PID coefficients for tuning the controller
            public float pCoeff = .8f;
            public float iCoeff = .0002f;
            public float dCoeff = .2f;
            public float minimum = -1;
            public float maximum = 1;

            //Variables to store values between calculations
            float _integral;
            float _lastProportional;

            //We pass in the value we want and the value we currently have, the code
            //returns a number that moves us towards our goal
            public float Seek(float seekValue, float currentValue)
            {
                var deltaTime = Time.fixedDeltaTime;
                var proportional = seekValue - currentValue;

                var derivative = (proportional - _lastProportional) / deltaTime;
                _integral += proportional * deltaTime;
                _lastProportional = proportional;

                //The actual PID formula
                var value = pCoeff * proportional + iCoeff * _integral + dCoeff * derivative;
                value = Mathf.Clamp(value, minimum, maximum);

                return value;
            }
        }
    }