using UnityEngine;

    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis {
            get
            {
               Vector2 axis =  MobileInputAxis();

                if(axis == Vector2.zero)
                {
                    axis = UnityInputAxis();
                }
                return axis;
            }
        } 
    }


