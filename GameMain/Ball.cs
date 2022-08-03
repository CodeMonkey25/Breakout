using System;
using GameMain.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.VectorDraw;

namespace GameMain;

public class Ball
{
    private const int Radius = 7;
    private const float Speed = 0.15f;

    private readonly PrimitiveDrawing _primitiveDrawing;
    private readonly PrimitiveBatch _primitiveBatch;
    
    private Matrix _localProjection;
    private Matrix _localView;
    private CircleF _circle;
    private Vector2 _motionVector = Vector2.One;

    public Ball(GraphicsDevice graphicsDevice, int x, int y)
    {
        _circle = new CircleF(new Point2(x, y), Radius);
        _localProjection = Matrix.CreateOrthographicOffCenter(0f, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, 0f, 0f, 1f);
        _localView = Matrix.Identity;
        _primitiveBatch = new PrimitiveBatch(graphicsDevice);
        _primitiveDrawing = new PrimitiveDrawing(_primitiveBatch);
    }

    public void Update(GameTime gameTime)
    {
        float delta = (float)(Speed * gameTime.ElapsedGameTime.TotalMilliseconds);
        _circle.Position += new Vector2(_motionVector.X, _motionVector.Y)
                            * delta;
    }
    
    private void BounceX() { _motionVector.X *= -1; }
    private void BounceY() { _motionVector.Y *= -1; }

    public bool DetectCollision(Board board)
    {
        bool collision = false;
        
        float maxX = board.Width - _circle.Radius;
        float maxY = board.Height - _circle.Radius;
        
        // check for wall collisions
        if (_circle.Center.X < _circle.Radius)
        {
            _circle.Center.X = _circle.Radius;
            BounceX();
            collision = true;
        }
        else if (_circle.Center.X > maxX) 
        {
            _circle.Center.X = maxX;
            BounceX();
            collision = true;
        }
		
        // check for ceiling/floor collisions
        if (_circle.Center.Y < _circle.Radius)
        {
            _circle.Center.Y = _circle.Radius;
            BounceY();
            collision = true;
        }
        else if (_circle.Center.Y > maxY)
        {
            _circle.Center.Y = maxY;
            BounceY();
            collision = true;
        }

        return collision;
    }
    
    public bool DetectCollision(Paddle paddle)
    {
        if (!DetectCollision(paddle.Rectangle))
        {
            return false;
        }
        
        // don't let the ball get trapped inside the paddle
        BoundingRectangle rectangle = paddle.Rectangle;
        do
        {
            _circle.Position += _motionVector;
        } while (_circle.Intersects(rectangle));
        
        return true;
    }

    public bool DetectCollision(Brick brick)
    {
        return DetectCollision(brick.Rectangle);
    }
    
    private bool DetectCollision(RectangleF rectangle)
    {
        if (!_circle.Intersects((BoundingRectangle)rectangle))
        {
            return false;
        }
        
        if (_circle.Center.X > rectangle.Left && rectangle.Right > _circle.Center.X)
        {
            // solid hit on the top/bottom of the brick
            BounceY();
        }
        else if (_circle.Center.Y > rectangle.Bottom && rectangle.Top > _circle.Center.Y)
        {
            // solid hit on the left/right of the brick
            BounceX();
        }
        else
        {
            // hit a corner
            BounceX();
            BounceY();
        }
        
        return true;
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        _primitiveBatch.Begin(ref _localProjection, ref _localView);
        // _primitiveDrawing.DrawSolidCircle(_location, _radius, Color.White);
        FillCircle(_circle, Color.White);
        _primitiveBatch.End();
    }
    
    // This is the same method as PrimitiveDrawing DrawSolidCircle() without the lighter fill color 
    private void FillCircle(CircleF circleF, Color color)
    {
        Vector2 center = circleF.Center;
        float radius = circleF.Radius;
        const double increment = Math.PI * 2.0 / PrimitiveDrawing.CircleSegments;
        double theta = 0.0;

        Vector2 v0 = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
        theta += increment;

        for (int i = 1; i < PrimitiveDrawing.CircleSegments - 1; i++)
        {
            Vector2 v1 = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
            Vector2 v2 = center + radius * new Vector2((float)Math.Cos(theta + increment), (float)Math.Sin(theta + increment));

            _primitiveBatch.AddVertex(v0, color, PrimitiveType.TriangleList);
            _primitiveBatch.AddVertex(v1, color, PrimitiveType.TriangleList);
            _primitiveBatch.AddVertex(v2, color, PrimitiveType.TriangleList);

            theta += increment;
        }

        _primitiveDrawing.DrawCircle(center, radius, color);            
    }
}