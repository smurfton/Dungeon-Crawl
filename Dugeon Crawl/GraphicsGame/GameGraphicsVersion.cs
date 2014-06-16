using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace Dungeon_Crawl.GraphicsGame
{
    /// <summary>
    /// The main implementation for the console version of the game
    /// </summary>
    class GameGraphicsVersion : GameWindow, IGame
    {
        Color testColor = Color.White;

        /// <summary>
        /// Called on program start- initial loading
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            VSync = VSyncMode.On;

            base.OnLoad(e);
        }

        /// <summary>
        /// Called once per frame before draws- used for updating game logic (such as physics)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Keyboard[Key.Escape])
            {
                Exit();
            }

            base.OnUpdateFrame(e);
        }

        /// <summary>
        /// Called once per frame to draw- used for actual rendering
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.GenBuffer();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(testColor);
            GL.Vertex2(-1.0f, 1.0f);
            GL.Vertex2(0.0f, -1.0f);
            GL.Vertex2(1.0f, 1.0f);

            GL.End();

            SwapBuffers();

            base.OnRenderFrame(e);
        }

        /// <summary>
        /// Called on resizing of  the window
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            base.OnResize(e);
        }

        public void ChangeTriangleColor(Color color)
        {
            testColor = color;
        }


        public void TalkToBob(GameLogic.GameConversation.Conversation whatConversation)
        {
            //@tmp
        }
    }
}
