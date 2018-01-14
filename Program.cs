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
                    example.Run(60.0, 0.0);
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

        GameActor m_GameActor;
        GameActorInstance m_GameActorInstance;
        Nima.Animation.ActorAnimationInstance m_Animation;

        public SimpleES20Window(GraphicsContextFlags flags)
        : base(800, 600, GraphicsMode.Default, "GL", GameWindowFlags.Default, DisplayDevice.Default, 2, 0, flags)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //m_GameActor = GameActor.Load("Assets/Archer.nima");
            //m_GameActor = GameActor.Load("Assets/Jelly Bow/Jelly Bow.nima");
            m_GameActor = GameActor.Load("Assets/Transform/Transform.nima");
            m_Renderer = new Renderer2D();

            m_GameActor.InitializeGraphics(m_Renderer);

            m_GameActorInstance = m_GameActor.makeInstance();
            //m_Animation = m_GameActorInstance.GetAnimationInstance("Walk");
            m_Animation = m_GameActorInstance.GetAnimationInstance("Untitled");
            int ct = 0;
            m_Animation.AnimationEvent += delegate(object animation, Nima.Animation.AnimationEventArgs args)
            {
                Console.WriteLine("TRIGGER " + args.Name + " " + ct + " " + m_Animation.Time + " " + args.KeyFrameTime);
                ct++;
            };
            m_GameActorInstance.InitializeGraphics(m_Renderer);
            Color4 color = Color4.MidnightBlue;
            GL.ClearColor(color.R, color.G, color.B, color.A);
        }

        protected override void OnResize(EventArgs e)
        {
            m_Renderer.Resize(Width, Height);
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (m_GameActorInstance != null)
            {
                //m_AnimationTime = (m_AnimationTime + (float)e.Time) % m_Animation.Duration;
                m_Animation.Advance((float)e.Time);
                m_Animation.Apply(1.0f);
                m_GameActorInstance.Advance((float)e.Time);
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

            Mat2D view = new Mat2D();
            m_Renderer.SetView(view);
            if (m_GameActorInstance != null)
            {
                m_GameActorInstance.Render(m_Renderer);
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
