using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circles
{
    public class Game : GameWindow
    {
        public Game(int width, int heigth) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            Size = new Vector2i(width, heigth);
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Start Code

            //GL.DrawArrays(PrimitiveType.LineLoop, )
            
            // End Code

            SwapBuffers();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }
        public void Run(double UpdateCap)
        {
            base.Run();

            UpdateFrequency = UpdateCap;
            RenderFrequency = UpdateCap;
        }
    }
}
