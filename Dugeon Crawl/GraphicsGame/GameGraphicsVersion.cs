using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace Dugeon_Crawl.GraphicsGame
{
    class GameGraphicsVersion : GameWindow, IGame
    {
        Color testColor = Color.White;

        protected override void OnLoad(EventArgs e)
        {
            
            VSync = VSyncMode.On;

            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Keyboard[Key.Escape])
            {
                Exit();
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
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
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            base.OnResize(e);
        }

        public void ChangeTriangleColor(Color color)
        {
            testColor = color;
        }
    }
}
