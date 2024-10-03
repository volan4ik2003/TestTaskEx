
using UnityEngine;

    public abstract class InputService : IInputService
    {
        public abstract Vector2 Axis { get; }

        public static Vector2 UnityInputAxis()
        {
            return new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
        }

        public static Vector2 MobileInputAxis()
        {
            return new Vector2(MyJoystick.Instance.Joystick.Horizontal, MyJoystick.Instance.Joystick.Vertical);
        }
    }



