using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SlXnaApp2
{
    public class InputManager
    {
        Vector2 startTouchLocation;
        float valueForReturn;

        public InputManager()
        {
            valueForReturn = 0f;
        }

        public float swipeDirection(float trajectory)
        {
            valueForReturn = trajectory;
            TouchCollection touchCol = TouchPanel.GetState();

            foreach (TouchLocation touch in touchCol)
            {
                
                if (touch.State == TouchLocationState.Pressed)
                {
                    //Debug.WriteLine("start x: {0} y: {1}", touch.Position.X, touch.Position.Y);
                    startTouchLocation = touch.Position;
                }
                if (touch.State == TouchLocationState.Released)
                {
                    //Debug.WriteLine("release x: {0} y: {1}", touch.Position.X, touch.Position.Y);
                    Vector2 delta = touch.Position - startTouchLocation;
                    //Debug.WriteLine("delta x: {0} y: {1}", delta.X, delta.Y);
                    valueForReturn = (float)Math.Atan2(delta.Y, delta.X);
                    Debug.WriteLine("trajectory: {0}", valueForReturn);
                }
            }

            return valueForReturn;
        }
    }
}
