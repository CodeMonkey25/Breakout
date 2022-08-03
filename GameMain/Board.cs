using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace GameMain;

public class Board
{
    private Paddle Paddle { get; }
    private Ball Ball { get; }
    private List<Brick> Bricks { get; }
    private RectangleF Rectangle { get; }
    public float Width => Rectangle.Width;
    public float Height => Rectangle.Height;
    
    public Board(GraphicsDevice graphicsDevice, int width, int height)
    {
        // create board
        Rectangle = new RectangleF(0f,0f, width, height);

        // create paddle
        Paddle = new Paddle(width, height);

        // create bricks
        int rowCount = height / 3 / (Brick.Height + Brick.Cushion);
        int columnCount = width / (Brick.Width + Brick.Cushion);

        int usedWidth = Brick.Width * columnCount
                        + Brick.Cushion * (columnCount - 1);
        int paddingX = (width - usedWidth) / 2;

        int usedHeight = Brick.Height * rowCount
                         + Brick.Cushion * (rowCount - 1);
        int paddingY = (height / 2 - usedHeight) / 2;

        Bricks = new List<Brick>(columnCount * rowCount);
        int y = paddingY;
        for (int i = 0; i < rowCount; i++)
        {
            int x = paddingX;
            for (int j = 0; j < columnCount; j++)
            {
                Bricks.Add(new Brick(x, y));
                x += Brick.Cushion + Brick.Width;
            }

            y += Brick.Cushion + Brick.Height;
        }

        // create ball
        Ball = new Ball(graphicsDevice, this, paddingX, y + paddingX);
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
            // check for game over
        }
        else if (Ball.DetectCollision(Paddle))
        {
            // do nothing?
        }
        else
        {
            Bricks.RemoveAll(Ball.DetectCollision);
            // check for game win
        }
    }

    public void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        spriteBatch.Begin();
        spriteBatch.FillRectangle(Rectangle, Color.Black);
        spriteBatch.DrawString(font, $"BRICKS: {Bricks.Count}", new Vector2(20, 20), Color.White);
        spriteBatch.End();

        Paddle.Draw(spriteBatch);
        foreach (Brick brick in Bricks)
        {
            brick.Draw(spriteBatch);
        }
        Ball.Draw(spriteBatch);
    }
}