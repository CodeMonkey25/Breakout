using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace GameMain;

public class Paddle
{
    private const float Width = 100f;
    private const float Height = 20f;
    private const float Speed = 0.5f;

    private readonly float _maxX;

    private RectangleF _rectangle;
    public RectangleF Rectangle => _rectangle;
    
    public Paddle(GraphicsDevice graphicsDevice)
    {
        _maxX = graphicsDevice.Viewport.Width - Width;
        float maxY = graphicsDevice.Viewport.Height - Height;
        _rectangle = new RectangleF(
            _maxX / 2f,
            maxY - 25f,
            Width,
            Height
        );
    }

    public void Update(GameTime gameTime, MouseState mouseState)
    {
        float startingX = _rectangle.X + _rectangle.Width / 2f;
        int targetX = mouseState.X;

        int multiplier = 0;
        if (targetX < startingX)
        {
            multiplier = -1;
        }
        else if (targetX > startingX)
        {
            multiplier = 1;
        }

        if (multiplier == 0)
        {
            return;
        }

        float delta = (float)(Speed * gameTime.ElapsedGameTime.TotalMilliseconds);
        delta = Math.Min(delta, Math.Abs(startingX - targetX));
        delta *= multiplier;
        if (delta == 0)
        {
            return;
        }

        float newX = _rectangle.X + delta;
        newX = Math.Min(newX, _maxX);
        newX = Math.Max(newX, 0);
        _rectangle.X = newX;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.FillRectangle(_rectangle, Color.Blue);
        spriteBatch.End();
    }
}