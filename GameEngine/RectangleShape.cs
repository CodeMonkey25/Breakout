using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMain.GameEngine;

public class RectangleShape
{
    private Texture2D texture;
    public int Width => texture.Width;
    public int Height => texture.Height;

    public RectangleShape(GraphicsDevice graphicsDevice, int size, Color color)
    : this(graphicsDevice, size, size, color)
    {
    }
    
    public RectangleShape(GraphicsDevice graphicsDevice, int width, int height, Color color)
    {
        texture = new Texture2D(graphicsDevice, width, height);
        Color[] data = new Color[width * height];
        for (int i = 0; i < data.Length; ++i)
        {
            data[i] = color;
        }
        texture.SetData(data);
    }
 
    public Texture2D GetTexture()
    {
        return texture;
    }
}