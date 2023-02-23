using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Circles.controls
{
    public class Ball : Circle
    {
        float xLineSpeed, yLineSpeed;
        public Ball(int detail, float xSpeed, float ySpeed) : base(detail, 1, 1, Vector3.One)
        {
            xLineSpeed = xSpeed;
            yLineSpeed = ySpeed;
            model = Matrix4.CreateScale(0.5f, 0.5f, 1);
        }

        public void Update(float totalTime)
        {
            totalTime = totalTime % MathHelper.TwoPi;

            model = Matrix4.CreateTranslation(new Vector3((float) MathHelper.Cos(totalTime * xLineSpeed),
                                                          (float)MathHelper.Sin(totalTime * yLineSpeed),
                                                          -1));
        }
        new public void Draw(Shader shader)
        {
            GL.BindVertexArray(VAO);

            shader.setUniform(Matrix4.CreateScale(0.08f, 0.08f, 1) * model, "model");
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, details);

            GL.BindVertexArray(0);
        }
    }
}
