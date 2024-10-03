using UnityEngine;

    class MyJoystick : MonoBehaviour
    {
        public static MyJoystick Instance { get; private set; }
        public FloatingJoystick Joystick;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
    }

