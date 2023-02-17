using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Reflection.Metadata;

namespace Circles.controls
{
    public class Shader
    {
        public int Handle;
        private bool handleDisposed = false;

        public Shader(string vertShaderPath, string fragShaderPath)
        {
            Handle = GL.CreateProgram();

            string curPath = Directory.GetCurrentDirectory();
            string vertShaderSource = File.ReadAllText(Path.Join(curPath, vertShaderPath));
            string fragShaderSource = File.ReadAllText(Path.Join(curPath, fragShaderPath));

            int vertShader = GL.CreateShader(ShaderType.VertexShader);
            int fragShader = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(vertShader, vertShaderSource);
            GL.ShaderSource(fragShader, fragShaderSource);

            GL.CompileShader(vertShader);
            GL.CompileShader(fragShader);

            GL.AttachShader(Handle, vertShader);
            GL.AttachShader(Handle, fragShader);

            GL.LinkProgram(Handle);

            GL.GetShader(vertShader, ShaderParameter.CompileStatus, out int vertStatus);
            GL.GetShader(fragShader, ShaderParameter.CompileStatus, out int fragStatus);
            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int linkStatus);
            if (vertStatus + fragStatus + linkStatus < 3)
            {
                string vertInfoLog = GL.GetShaderInfoLog(vertShader);
                string fragInfoLog = GL.GetShaderInfoLog(fragShader);
                string linkInfoLog = GL.GetProgramInfoLog(Handle);

                Console.WriteLine($"Vertice Shader Log:\n" +
                                  $"{vertInfoLog}\n\n" +
                                  $"Fragment Shader Log:\n" +
                                  $"{fragInfoLog}\n\n" +
                                  $"Program Link Log:\n" +
                                  $"{linkInfoLog}\n\n");
            }

            // Clean Up

            GL.DetachShader(Handle, vertStatus);
            GL.DetachShader(Handle, fragStatus);

            GL.DeleteShader(vertShader);
            GL.DeleteShader(fragShader);
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }
        public void setUniform(Matrix4 data, string uniformName)
        {
            GL.UniformMatrix4(GL.GetUniformLocation(Handle, uniformName), false, ref data);
        }
        #region IDisposable
        ~Shader()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!handleDisposed)
            {
                GL.DeleteProgram(Handle);
                handleDisposed = true;
            }
            if (disposing)
            {
                Handle = 0;
            }
        }
        #endregion
    }
}
