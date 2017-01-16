using System;
using System.IO;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES20;

using Hjg.Pngcs;
using System.Collections.Generic;

using Nima.OpenGL;
using Nima.Math2D;
using Nima;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*Nima.Animation.AnimationCurveTest test = new Nima.Animation.AnimationCurveTest(0, 34.77278518676758,  0.8424242403224804, 29.32800508902757,  0.00000999999999995449, 161.2324085038573,  1, 98.13641357421875);
            double[] expected = new double[60] {34.77278518676758, 34.68191960114582, 34.626263045220234, 34.60825406058168, 34.630616793905936, 34.69640927316223, 34.80908258233374, 34.97255407429494, 35.19129888214371, 35.470465595669474, 35.81602431382377, 36.23495877318582, 36.73551956101267, 37.32756369914048, 38.023019154232124, 38.836534778601106, 39.78641381941253, 40.89599639959702, 42.195782600806766, 43.72683875969582, 45.54656481224143, 47.7391451822138, 50.436237728268814, 53.863166555806046, 58.46164806479278, 65.31831409552156, 78.36519515402807, 96.74933053662754, 104.50584942069626, 108.5097884695479, 110.98771097769225, 112.62997351481819, 113.73961779039637, 114.4775418713105, 114.93990868784401, 115.18903138882105, 115.26773413400494, 115.20679337895885, 115.02911889353365, 114.75225556329072, 114.38995778094773, 113.95322199445968, 113.45098765217877, 112.89062715646956, 112.27829704435263, 111.61919524907398, 110.91775319639322, 110.17778168580635, 109.4025833537627, 108.59504055052177, 107.75768484454623, 106.89275260388268, 106.00222989070684, 105.08788905743623, 104.15131882996351, 103.19394922884001, 102.2170723627827, 101.2218598927122, 100.2093777906199, 99.18059888389004};
            double inc = 1.0 / 60.0;
            for (int i = 0; i < 60; i++)
            {
                double _x = i * inc;
                Console.WriteLine(VAL " + i + " " + _x + " " + test.Get(_x) + " " + (expected[i] - test.Get(_x)));
            }
            return;*/
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
        //Texture m_Texture;

        Renderer2D m_Renderer;
        //VertexBuffer m_VertexBuffer;
        //IndexBuffer m_IndexBuffer;

        Actor2D m_Actor;
        ActorInstance2D m_ActorInstance;

        public SimpleES20Window(GraphicsContextFlags flags)
        : base(800, 600, GraphicsMode.Default, "GL", GameWindowFlags.Default, DisplayDevice.Default, 2, 0, flags)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            m_Actor = Actor2D.Load("Assets/Archer.nima");
            m_ActorInstance = new ActorInstance2D(m_Actor);
            m_ActorInstance.Play("Ski", true);
            m_Renderer = new Renderer2D();

            Color4 color = Color4.MidnightBlue;
            GL.ClearColor(color.R, color.G, color.B, color.A);
            /*
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
                        m_IndexBuffer.SetData(indexBufferData);*/
        }

        protected override void OnResize(EventArgs e)
        {
            m_Renderer.Resize(Width, Height);
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (m_ActorInstance != null)
            {
                m_ActorInstance.Advance((float)e.Time);
            }

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
            if (m_ActorInstance != null)
            {
                m_ActorInstance.Draw(view, m_Renderer);
            }
            // e.Time
            /*float[] transform = Mat2D.Create();
            m_Renderer.BlendMode = Renderer2D.BlendModes.Transparent;
            Mat2D.Scale(transform, transform, Vec2D.Create(2048.0f, 256.0f));
            m_Renderer.DrawTextured(view, transform, m_VertexBuffer, m_IndexBuffer, 1.0f, Color.White, m_Texture);*/
            this.SwapBuffers();
        }
    }
}
