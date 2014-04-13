using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameName3
{
    public class InputManager
    {
        Vector2 totalSwipe;

        public InputManager()
        {
            totalSwipe = new Vector2();
        }

        public float swipeDirection()
        {
            
            TouchCollection touchCol = TouchPanel.GetState();
            float valueForReturn 0f;

            if (touchCol.Count > 0)
            {
                TouchLocation touch = touchCol[0];
                if (touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved)
                {
                    TouchLocation prevLoc;
                    if (!touch.TryGetPreviousLocation(out prevLoc) || prevLoc.State != TouchLocationState.Moved) ;//break;
                    Vector2 delta = touch.Position - prevLoc.Position;
                    totalSwipe += delta;
                }
                if (touch.State == TouchLocationState.Released)
                {
                    valueForReturn = (float)Math.Atan2(totalSwipe.Y, totalSwipe.X);
                    totalSwipe = new Vector2();
                }
            }

            return valueForReturn;
        }
    }
}
