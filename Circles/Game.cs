using Circles.controls;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
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

        List<Circle> circles;
        List<Ball> balls;

        Matrix4 projection = Matrix4.Identity;
        public Game(int width, int heigth) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            Size = new Vector2i(width, heigth);

            Vector2 vez = new(1, 1);
            shader = new(@"shaders\lines.vert", @"shaders\lines.frag");

            circles = new();
            balls = new();
        }
        protected override void OnLoad()
        {
            base.OnLoad();

            for(int i = 1; i <= 10; i++)
            {
                for(int j = 1; j <= 10; j++)
                {
                    Vector3 pos = new(i * 2 + ((i - 1) * 0.6f), j * 2 + ((j - 1) * 0.5f), -1);
                    circles.Add(new(200, i, j, Vector3.One, pos));
                    balls.Add(new(20, i, j, pos));
                }
            }

            GL.ClearColor(0.1f, 0.1f, 0.1f, 1);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            shader.Use();
            balls.ForEach(ball => {
                ball.Update((float)GLFW.GetTime());
            });

            shader.setUniform(projection, "projection");
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Start Code

            shader.Use();

            circles.ForEach(circle =>
            {
                circle.Draw(shader);
            });
            balls.ForEach(ball =>
            {
                ball.Draw(shader);
            });
            // End Code

            SwapBuffers();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            //projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), e.Width / e.Height, 0.1f, 5);
            //projection = Matrix4.CreateOrthographic(e.Width, e.Height, 0.1f, 5);
            projection = Matrix4.CreateOrthographicOffCenter(0, e.Width, e.Height, 0, 0.1f, 5);
            projection = Matrix4.CreateScale(e.Height / 26, e.Height / 26, 1) * projection;

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
