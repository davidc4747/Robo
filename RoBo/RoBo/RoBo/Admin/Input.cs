using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace RoBo
{
    public class Input
    {
        KeyboardState keys, oldKeys;
        MouseState mouse, oldMouse;

        //Mouse Inputs
        public Vector2 MousePos
        {
            get { return new Vector2(mouse.X, mouse.Y); }
        }
        public Vector2 DeltaMousePos
        {
            get { return MousePos - new Vector2(oldMouse.X, oldMouse.Y); }
        }
        public int WheelValue
        {
            get { return mouse.ScrollWheelValue; }
        }
        public int DeltaWheelValue
        {
            get { return WheelValue - oldMouse.ScrollWheelValue; }
        }

        //Mouse Button Input
        public bool LeftClick
        {
            get { return mouse.LeftButton == ButtonState.Pressed; }
        }
        public bool RightClick
        {
            get { return mouse.RightButton == ButtonState.Pressed; }
        }

        //Mouse Button Pressed Inputs
        public bool LeftClickPressed
        {
            get { return mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released; }
        }
        public bool RightClickPressed
        {
            get { return mouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Released; }
        }


        //Movement Inputs
        public bool Up 
        {
            get { return keys.IsKeyDown(Keys.W) || keys.IsKeyDown(Keys.Up); }
        }
        public bool Down
        {
            get { return keys.IsKeyDown(Keys.S) || keys.IsKeyDown(Keys.Down); }
        }
        public bool Left
        {
            get { return keys.IsKeyDown(Keys.A) || keys.IsKeyDown(Keys.Left); }
        }
        public bool Right
        {
            get { return keys.IsKeyDown(Keys.D) || keys.IsKeyDown(Keys.Right); }
        }

        //Movement Pressed Inputs
        public bool UpPressed
        {
            get { return (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W)) || (keys.IsKeyDown(Keys.Up) && oldKeys.IsKeyUp(Keys.Up)); }
        }
        public bool DownPressed
        {
            get { return (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S)) || (keys.IsKeyDown(Keys.Down) && oldKeys.IsKeyUp(Keys.Down)); }
        }
        public bool LeftPressed
        {
            get { return (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A)) || (keys.IsKeyDown(Keys.Left) && oldKeys.IsKeyUp(Keys.Left)); }
        }
        public bool RightPressed
        {
            get { return (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D)) || (keys.IsKeyDown(Keys.Right) && oldKeys.IsKeyUp(Keys.Right)); }
        }
        
        //Action Inputs
        public bool EscapePressed
        {
            get { return keys.IsKeyDown(Keys.Escape) && oldKeys.IsKeyUp(Keys.Escape); }
        }
        public bool SpacePressed
        {
            get { return keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space); }
        }
        public bool EnterPressed
        {
            get { return keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter); }
        }
        

        //Game Specific Inputs
        public bool PausePressed
        {
            get { return isPressed(Keys.Escape); }
        }
        public bool ReloadPressed
        {
            get { return isPressed(Keys.R); }
        }
        public bool SkillPressed
        {
            get { return isPressed(Keys.F); }
        }
        public bool RagePressed
        {
            get { return isPressed(Keys.Space); }
        }

        public bool SwapActUp
        {
            get { return isPressed(Keys.E); }
        }
        public bool SwapActDown
        {
            get { return isPressed(Keys.Q); }
        }        

        public bool FirstWep
        {
            get { return isPressed(Keys.D1); }
        }
        public bool SecondWep
        {
            get { return isPressed(Keys.D2); }
        }
        public bool ThirdWep
        {
            get { return isPressed(Keys.D3); }
        }

        //---Game Controls---
        //Reload        : R
        //Action Skill  : F
        //Swap ActSkill : Q, E
        //Rage Mode     : Space
        //Swap Wep +,-  : 1,2,3 : scroll wheel
        //Pause         : esc

        //~~Grenade     : G
        //~~Melee       : V
        //-------------------

        public void start()
        {
            keys = Keyboard.GetState();
            mouse = Mouse.GetState();
        }

        public void end()
        {
            oldKeys = keys;
            oldMouse = mouse;
        }

        public bool isPressed(Keys key)
        {
            return keys.IsKeyDown(key) && oldKeys.IsKeyUp(key);
        }

        public bool isPressed(ButtonState button)
        {
            return button == ButtonState.Pressed && button == ButtonState.Released;
        }

        public bool isDown(Keys key)
        {
            return keys.IsKeyDown(key);
        }

        public bool isDown(ButtonState button)
        {
            return button == ButtonState.Pressed;
        }

    }
}
