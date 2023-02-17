using Circles.controls;
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
        Shader shader;
        Circle circle;

        Matrix4 projection = Matrix4.Identity;
        public Game(int width, int heigth) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            Size = new Vector2i(width, heigth);

            shader = new(@"shaders\lines.vert", @"shaders\lines.frag");
            circle = new Circle(200, 4, 1, ((Vector4)Color4.Red).Xyz);
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            shader.Use();
            //projection = Matrix4.Identity;
            Console.WriteLine(projection * new Vector4(0.7f, 0.7f, -1, 1));
            shader.setUniform(projection, "projection");
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Start Code

            shader.Use();
            circle.draw();

            // End Code

            SwapBuffers();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60), e.Width / (float)e.Height, 0.1f, 100);
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
