using System.Collections.Generic;
using GameMain.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMain;

public class Board
{
    public int Width { get; }
    public int Height { get; }
    private RectangleShape Sprite { get; }
    
    private Paddle Paddle { get; }
    private List<Brick> Bricks { get; }

    public Board(GraphicsDevice graphicsDevice)
    {
        Width = graphicsDevice.Viewport.Width;
        Height = graphicsDevice.Viewport.Height;
        Sprite = new RectangleShape(graphicsDevice, Width, Height, Color.Black);
        Paddle = new Paddle(graphicsDevice);

        // create bricks
        int rowCount = Height / 3 / (Brick.RequestedHeight + Brick.Cushion);
        int columnCount = Width / (Brick.RequestedWidth + Brick.Cushion);

        int usedWidth = Brick.RequestedWidth * columnCount
                        + Brick.Cushion * (columnCount - 1);
        int paddingX = (Width - usedWidth) / 2;

        int usedHeight = Brick.RequestedHeight * rowCount
                         + Brick.Cushion * (rowCount - 1);
        int paddingY = ((Height / 2) - usedHeight) / 2;

        Bricks = new List<Brick>(columnCount * rowCount);
        int y = paddingY;
        for (int i = 0; i < rowCount; i++)
        {
            int x = paddingX;
            for (int j = 0; j < columnCount; j++)
            {
                Bricks.Add(new Brick(graphicsDevice, x, y));
                x += Brick.Cushion + Brick.RequestedWidth;
            }

            y += Brick.Cushion + Brick.RequestedHeight;
        }
    }

    public void Update(GameTime gameTime, MouseState mouseState)
    {
        Paddle.Update(gameTime, mouseState);
        foreach (Brick brick in Bricks)
        {
            brick.Update(gameTime);
        }
        // update ball
        // detect collisions
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(Sprite.Texture, Sprite.Rectangle, Color.White);
        spriteBatch.End();
        
        Paddle.Draw(spriteBatch);
        foreach (Brick brick in Bricks)
        {
            brick.Draw(spriteBatch);
        }
    }
}