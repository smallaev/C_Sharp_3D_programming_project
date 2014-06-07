using System;
using Mogre;

namespace RaceGame
{
    class PlayerController : CharacterController
    {
        Vector3 move = Vector3.ZERO;
        


        public PlayerController(Character player)
        {
            speed = 180;
            character = player;
            
        }

        public override void Update(FrameEvent evt)     
        {
            MovementsControl(evt);
            MouseControl();
            ShootingControl();

        }

        private void MovementsControl(FrameEvent evt) 
        {
            if (forward) {
                move += character.Model.Forward;

            }

            if (backward)
            {
                move -= character.Model.Forward;

            }

            if (left)
            {
                move += character.Model.Left;

            }

            if (right)
            {
                move -= character.Model.Left;

            }

            if (up)
            {
                move += character.Model.Up;

            }

            if (down)
            {
                move -= character.Model.Up;

            }

            move = move.NormalisedCopy * speed;

            if (accellerate)
            {
                move = move * 2;
            }

            if (move != Vector3.ZERO)
            {
                move *= evt.timeSinceLastFrame;
                character.Move(move);
                
            }


        }


        private void MouseControl()
        {
            character.Model.GameNode.Yaw(Mogre.Math.AngleUnitsToRadians(angles.y));
        }

        private void ShootingControl()
        {
            if (shoot)
                character.Shoot();
        }
    }
}
