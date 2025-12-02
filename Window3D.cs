using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace Tema_EGC_Slots
{
    internal class Window3D : GameWindow 
    {
        private int size = 6;
        private int offset = 8;
        private string[] textures = { "C:\\Users\\dxvk\\source\\Tema_EGC_Slots\\numbers\\zero.png", "C:\\Users\\dxvk\\source\\Tema_EGC_Slots\\numbers\\one.png", "C:\\Users\\dxvk\\source\\Tema_EGC_Slots\\numbers\\two.png", "C:\\Users\\dxvk\\source\\Tema_EGC_Slots\\numbers\\three.png", "C:\\Users\\dxvk\\source\\Tema_EGC_Slots\\numbers\\four.png", "C:\\Users\\dxvk\\source\\Tema_EGC_Slots\\numbers\\five.png", "C:\\Users\\dxvk\\source\\Tema_EGC_Slots\\numbers\\six.png", "C:\\Users\\dxvk\\source\\Tema_EGC_Slots\\numbers\\seven.png", "C:\\Users\\dxvk\\source\\Tema_EGC_Slots\\numbers\\eight.png", "C:\\Users\\dxvk\\source\\Tema_EGC_Slots\\numbers\\nine.png" };
        private int[] textureIds = new int[10];
        public Window3D(): base(800, 600, new GraphicsMode(32, 24, 0, 8), "3D Slots")
        {
            VSync = VSyncMode.On;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.Black); //culoare fundal
            // pentru randare 3d
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            // setari AA (antialiasing)
            GL.Enable(EnableCap.Texture2D);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.GenTextures(textures.Length, textureIds);
            for (int i = 0; i < textures.Length; i++)
            {
                LoadTexture(textures[i], textureIds[i]);
                Console.WriteLine("Loaded texture: " + textures[i]+"-" + textureIds[i]);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // perspectiva
            GL.Viewport(0, 0, Width, Height);
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)Width / (float)Height, 1, 256);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            // camera
            Matrix4 lookat = Matrix4.LookAt(new Vector3(50, 30, 0), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
            // Actualizare pozitie obiecte
        }

        private void LoadTexture(string path,int id)
        {
            Bitmap bmp = new Bitmap(path);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                                    ImageLockMode.ReadOnly,
                                                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindTexture(TextureTarget.Texture2D, id);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                          bmp.Width, bmp.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                          PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            // logica joc
            KeyboardState input = Keyboard.GetState();
            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.CornflowerBlue);

            //Draw 3 planes next to each other
            GL.Color3(Color.White);
            for (int i=0; i<3; i++)
            {
                GL.BindTexture(TextureTarget.Texture2D ,textureIds[i]);
                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0, 0);
                GL.Vertex3(-size, 0, i * (size+offset) - size);
                GL.TexCoord2(0, 1);
                GL.Vertex3(- size, 0, i * (size + offset) + size);
                GL.TexCoord2(1, 1);
                GL.Vertex3(size, 0, i * (size + offset) +size);
                GL.TexCoord2(1, 0);
                GL.Vertex3(size, 0, i * (size + offset) - size);
                GL.End();
            }


            SwapBuffers();
        }
    }
}
