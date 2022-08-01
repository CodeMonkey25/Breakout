using System;
using GameMain.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace GameMain;

public class Paddle
{
    private const int RequestedWidth = 100;
    private const int RequestedHeight = 20;
    private const float Speed = 0.3f;
    
    private int MaxX { get; }
    
    private Vector2 _location;
    private readonly Size2 _size;
    
    public Paddle(GraphicsDevice graphicsDevice)
    {
        _size = new Size2(RequestedWidth, RequestedHeight);
        
        MaxX = graphicsDevice.Viewport.Width - (int)_size.Width;
        int maxY = graphicsDevice.Viewport.Height - (int)_size.Height;
        _location = new Vector2(
            ((float)MaxX / 2),
            maxY - 25
        );
    }

    public void Update(GameTime gameTime, MouseState mouseState)
    {
        int startingX = (int)_location.X + (int)_size.Width / 2;
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

        int delta = (int)(Speed * gameTime.ElapsedGameTime.TotalMilliseconds);
        delta = Math.Min(delta, Math.Abs(startingX - targetX));
        delta *= multiplier;
        if (delta == 0)
        {
            return;
        }
        
        int newX = (int)_location.X + delta;
        newX = Math.Min(newX, MaxX);
        newX = Math.Max(newX, 0);
        _location.X = newX;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.FillRectangle(_location, _size, Color.Blue);
        spriteBatch.End();
    }
}