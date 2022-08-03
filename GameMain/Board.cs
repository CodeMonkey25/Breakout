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
    private Ball Ball { get; }
    private List<Brick> Bricks { get; }

    public Board(GraphicsDevice graphicsDevice)
    {
        Width = graphicsDevice.Viewport.Width;
        Height = graphicsDevice.Viewport.Height;

        // create board
        Sprite = new RectangleShape(graphicsDevice, Width, Height, Color.Black);

        // create paddle
        Paddle = new Paddle(graphicsDevice);

        // create bricks
        int rowCount = Height / 3 / (Brick.Height + Brick.Cushion);
        int columnCount = Width / (Brick.Width + Brick.Cushion);

        int usedWidth = Brick.Width * columnCount
                        + Brick.Cushion * (columnCount - 1);
        int paddingX = (Width - usedWidth) / 2;

        int usedHeight = Brick.Height * rowCount
                         + Brick.Cushion * (rowCount - 1);
        int paddingY = (Height / 2 - usedHeight) / 2;

        Bricks = new List<Brick>(columnCount * rowCount);
        int y = paddingY;
        for (int i = 0; i < rowCount; i++)
        {
            int x = paddingX;
            for (int j = 0; j < columnCount; j++)
            {
                Bricks.Add(new Brick(graphicsDevice, x, y));
                x += Brick.Cushion + Brick.Width;
            }

            y += Brick.Cushion + Brick.Height;
        }

        // create ball
        Ball = new Ball(graphicsDevice, paddingX, y + paddingX);
    }

    public void Update(GameTime gameTime, MouseState mouseState)
    {
        Paddle.Update(gameTime, mouseState);
        foreach (Brick brick in Bricks)
        {
            brick.Update(gameTime);
        }

        Ball.Update(gameTime);

        // detect collisions
        if (Ball.DetectCollision(this))
        {
            // check for end game
        }
        else if (Ball.DetectCollision(Paddle))
        {
            // do nothing?
        }
        else
        {
            Bricks.RemoveAll(Ball.DetectCollision);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(Sprite.Texture, Sprite.Bounds, Color.White);
        spriteBatch.End();

        Paddle.Draw(spriteBatch);
        foreach (Brick brick in Bricks)
        {
            brick.Draw(spriteBatch);
        }

        Ball.Draw(spriteBatch);
    }
}