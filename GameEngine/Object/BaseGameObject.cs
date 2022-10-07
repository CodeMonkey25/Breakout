using System;
using System.Collections.Generic;
using GameMain.GameEngine.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameMain.GameEngine.Object;

public class BaseGameObject
{
    public event EventHandler<BaseGameStateEvent> OnObjectChanged;
    public virtual int Width => Texture.Width;
    public virtual int Height => Texture.Height;
    public int ZIndex { get; set; }
    public bool Destroyed { get; private set; }

    protected Texture2D Texture { get; set; }
    protected Texture2D BoundingBoxTexture { get; set; }
    protected float Angle { get; set; }
    protected Vector2 Direction { get; set; }
    private readonly IList<Rectangle> _boundingBoxes = new List<Rectangle>();
    public IEnumerable<Rectangle> BoundingBoxes => _boundingBoxes;

    private Vector2 _position = Vector2.One;

    public virtual Vector2 Position
    {
        get => _position;
        set
        {
            Vector2 offset = value - _position;
            foreach (Rectangle bb in _boundingBoxes)
            {
                bb.Offset(offset);
            }

            _position = value;
        }
    }

    public void RenderBoundingBoxes(SpriteBatch spriteBatch)
    {
        if (Destroyed) return;

        if (BoundingBoxTexture == null)
        {
            BoundingBoxTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            BoundingBoxTexture.SetData(new[] { Color.White });
        }

        foreach (Rectangle bb in _boundingBoxes)
        {
            spriteBatch.Draw(BoundingBoxTexture, bb, Color.Red);
        }
    }

    public virtual void OnNotify(BaseGameStateEvent gameEvent)
    {
    }

    public virtual void Render(SpriteBatch spriteBatch)
    {
        if (Destroyed) return;
        spriteBatch.Draw(Texture, Position, Color.White);
    }

    public void Destroy()
    {
        Destroyed = true;
    }

    public void SendEvent(BaseGameStateEvent e)
    {
        OnObjectChanged?.Invoke(this, e);
    }

    public void AddBoundingBox(Rectangle bb)
    {
        _boundingBoxes.Add(bb);
    }

    protected Vector2 CalculateDirection(float angleOffset = 0.0f)
    {
        // Direction = new Vector2((float)Math.Cos(Angle - angleOffset), (float)Math.Sin(Angle - angleOffset));
        Direction = Direction.Rotate(angleOffset);
        Direction.Normalize();
        return Direction;
    }
}