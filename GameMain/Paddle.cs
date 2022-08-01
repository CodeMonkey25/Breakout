using System;
using GameMain.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMain;

public class Paddle
{
    private const int RequestedWidth = 100;
    private const int RequestedHeight = 20;
    private const float Speed = 0.3f;
    
    private int MaxX { get; }
    private RectangleShape Sprite { get; }
    
    public Paddle(GraphicsDevice graphicsDevice)
    {
        Sprite = new RectangleShape(graphicsDevice, RequestedWidth, RequestedHeight, Color.Blue);
        MaxX = graphicsDevice.Viewport.Width - Sprite.Width;
        int maxY = graphicsDevice.Viewport.Height - Sprite.Height;
        Sprite.UpdatePosition(
            MaxX / 2,
            maxY - 25
        );
    }

    public void Update(GameTime gameTime, MouseState mouseState)
    {
        int startingX = Sprite.X + Sprite.Width / 2;
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
        
        int newX = Sprite.X + delta;
        newX = Math.Min(newX, MaxX);
        newX = Math.Max(newX, 0);
        Sprite.UpdateX(newX);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(Sprite.Texture, Sprite.Rectangle, Color.White);
        spriteBatch.End();
    }
}