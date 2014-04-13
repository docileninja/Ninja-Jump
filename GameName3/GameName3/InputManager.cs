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
        public float swipeDirection()
        {
            /*
            var gesture = default(GestureSample);

            while (TouchPanel.IsGestureAvailable)
            {
                gesture = TouchPanel.ReadGesture();

                if (gesture.GestureType == GestureType.DragComplete)
                {
                    return (float)Math.Atan2(gesture.Delta.Y, gesture.Delta.X);
                }
            }
            return 0.0f;
            */

            
            TouchCollection touchCol = TouchPanel.GetState();

            foreach (TouchLocation touch in touchCol)
            {
                Console.WriteLine("TouchLocationState = {0}", touch.State);
                // You're looking for when they finish a drag, so only check
                // released touches.
                if (touch.State != TouchLocationState.Released)
                    continue;
                

                TouchLocation prevLoc;

                // Sometimes TryGetPreviousLocation can fail. Bail out early if this happened
                // or if the last state didn't move
                if (!touch.TryGetPreviousLocation(out prevLoc) || prevLoc.State != TouchLocationState.Moved)
                    break;


                // get your delta
                var delta = touch.Position - prevLoc.Position;

                // Usually you don't want to do something if the user drags 1 pixel.
                if (delta.LengthSquared() < 100)
                    continue;
                /*
                if (delta.X < 0 || delta.Y < 0)
                    return new RotateLeftCommand(_gameController);

                if (delta.X > 0 || delta.Y > 0)
                    return new RotateRightCommand(_gameController);
                */
                
            }
            
            return 0f;
        }
    }
}
