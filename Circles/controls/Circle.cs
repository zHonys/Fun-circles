using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circles.controls
{
    public class Circle
    {
        protected int details;

        float xSpeed;
        float ySpeed;

        Vector3 color;

        protected int VAO, VBO;

        Vector3[] lines;
        protected Matrix4 model = Matrix4.Identity;

        public Circle(int detail, float xSpeed, float ySpeed, Vector3 linesColor)
        {
            details = detail;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            color = linesColor;

            lines = genPoints();

            setUp();
        }
        private Vector3[] genPoints()
        {
            double pi = MathHelper.Pi;
            List<double> dist = Enumerable.Range(0, details).Select(x => pi*x/((double)details/2)).ToList();

            Vector2d[] pos = dist.Select(n => new Vector2d(MathHelper.Cos(n*xSpeed), MathHelper.Sin(n*ySpeed))).ToArray();

            return pos.Select(vec => new Vector3((float)vec.X, (float)vec.Y, -1)).ToArray();
        }
        private unsafe void setUp()
        {
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(Vector3)*lines.Length, lines, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(Vector3), 0);
            GL.VertexAttrib3(1, color);

            GL.BindVertexArray(0);
        }
        public void Draw(Shader shader)
        {
            GL.BindVertexArray(VAO);

            shader.setUniform(model, "model");
            GL.DrawArrays(PrimitiveType.LineLoop, 0, details);

            GL.BindVertexArray(0);
        }

    }
}
