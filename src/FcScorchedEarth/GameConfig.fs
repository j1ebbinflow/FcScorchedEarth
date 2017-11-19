module GameConfig 

open System

open OpenTK
open OpenTK.Graphics.OpenGL

let onLoadSetup (game : GameWindow) = 
    game.VSync <- VSyncMode.On
    GL.Enable(EnableCap.Blend)
    GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.One)
    GL.Enable(EnableCap.PointSprite)

let onResizeSetup (game : GameWindow) = 
    let aspectRatio = float game.Width / float game.Height
    let fov = Math.PI / 2.
    let mutable projection = Matrix4.CreatePerspectiveFieldOfView(float32 fov, float32 aspectRatio, 1.0f, 100.0f)

    GL.Viewport(game.ClientRectangle.X, game.ClientRectangle.Y, game.ClientRectangle.Width, game.ClientRectangle.Height)
    GL.MatrixMode(MatrixMode.Projection)
    GL.LoadMatrix(&projection)

let preRenderConfigure() = 
    GL.Clear(ClearBufferMask.ColorBufferBit ||| ClearBufferMask.DepthBufferBit)
    let mutable modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY)
    GL.MatrixMode(MatrixMode.Modelview)
    GL.LoadMatrix(&modelview)
