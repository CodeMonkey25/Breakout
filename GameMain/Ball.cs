using GameMain.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.VectorDraw;

namespace GameMain;

public class Ball
{
    public const int RequestedDiameter = 15;

    private Matrix _localProjection;
    private Matrix _localView;
    private readonly PrimitiveDrawing _primitiveDrawing;
    private readonly PrimitiveBatch _primitiveBatch;
    
    private Vector2 _location;
    private const float _radius = RequestedDiameter / 2F;

    public Ball(GraphicsDevice graphicsDevice, int x, int y)
    {
        _location = new Vector2(x, y);
        _localProjection = Matrix.CreateOrthographicOffCenter(0f, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, 0f, 0f, 1f);
        _localView = Matrix.Identity;
        _primitiveBatch = new PrimitiveBatch(graphicsDevice);
        _primitiveDrawing = new PrimitiveDrawing(_primitiveBatch);
    }

    public void Update(GameTime gameTime)
    {
        
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        _primitiveBatch.Begin(ref _localProjection, ref _localView);
        _primitiveDrawing.DrawSolidCircle(_location, _radius, Color.White);
        _primitiveBatch.End();
    }
}