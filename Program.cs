using System;
using System.IO;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES20;

using Hjg.Pngcs;
using System.Collections.Generic;

using Nima.OpenGL;
using Nima.Math2D;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SimpleES20Window example;
            try
            {
                example = new SimpleES20Window(GraphicsContextFlags.Embedded);
            }
            catch
            {
                example = new SimpleES20Window(GraphicsContextFlags.Default);
            }

            if (example != null)
            {
                using (example)
                {
                    //Utilities.SetWindowTitle(example);
                    example.Run(30.0, 0.0);
                }
            }
        }
    }

    public class SimpleES20Window : GameWindow
    {
        Texture m_Texture;

        Renderer2D m_Renderer;
        VertexBuffer m_VertexBuffer;
        IndexBuffer m_IndexBuffer;

        public SimpleES20Window(GraphicsContextFlags flags)
            : base(800, 600, GraphicsMode.Default, "GL", GameWindowFlags.Default, DisplayDevice.Default, 2, 0, flags)
        { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Color4 color = Color4.MidnightBlue;
            GL.ClearColor(color.R, color.G, color.B, color.A);
            GL.Enable(EnableCap.DepthTest);

            m_Renderer = new Renderer2D();

            m_Texture = new Texture("Assets/Archer0.png", true);
            float[] vertexBufferData = {
                    0.0f, 1.0f,
					0.0f, 1.0f,

					1.0f, 1.0f,
					1.0f, 1.0f,

					1.0f, 0.0f,
					1.0f, 0.0f,

					0.0f, 0.0f,
					0.0f, 0.0f
                };
            ushort[] indexBufferData = {
                0, 1, 2, 2, 3, 0
            };

            m_VertexBuffer = new VertexBuffer();
            m_IndexBuffer = new IndexBuffer();
            m_VertexBuffer.SetData(vertexBufferData);
            m_IndexBuffer.SetData(indexBufferData);
        }

        protected override void OnResize(EventArgs e)
        {
            m_Renderer.Resize(Width, Height);
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var keyboard = OpenTK.Input.Keyboard.GetState();
            if (keyboard[OpenTK.Input.Key.Escape])
            {
                this.Exit();
                return;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            float[] view = Mat2D.Create();
            float[] transform = Mat2D.Create();
            m_Renderer.BlendMode = Renderer2D.BlendModes.Transparent;
            Mat2D.Scale(transform, transform, Vec2D.Create(2048.0f, 256.0f));
            m_Renderer.DrawTextured(view, transform, m_VertexBuffer, m_IndexBuffer, 1.0f, Color.White, m_Texture);
            this.SwapBuffers();
        }
    }
}
