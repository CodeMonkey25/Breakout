using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMain;

public class Main : Game
{
    // private const int Width = 1280;
    // private const int Height = 720;
    
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;
    
    private Board Board { get; set; }
    private MouseState MouseState { get; set; }

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // _graphics.PreferredBackBufferWidth = Width;
        // _graphics.PreferredBackBufferHeight = Height;
        // _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("GameFont");
        Board = new Board(
            _graphics.GraphicsDevice,
            _graphics.PreferredBackBufferWidth,
            _graphics.PreferredBackBufferHeight);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        MouseState = Mouse.GetState();
        Board.Update(gameTime, MouseState);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        Board.Draw(_spriteBatch, _font);
        
        // _spriteBatch.Begin();
        // string text;
        // text = $"screen: {_graphics.GraphicsDevice.Viewport.Width}, {_graphics.GraphicsDevice.Viewport.Height}";
        // _spriteBatch.DrawString(_font, text, new Vector2(20, 20), Color.White);
        // text = $"mouse: {MouseState.X}, {MouseState.Y}";
        // _spriteBatch.DrawString(_font, text, new Vector2(20, 40), Color.White);
        // text = $"paddle position: ({Board.Paddle.X}, {Board.Paddle.Y})";
        // _spriteBatch.DrawString(_font, text, new Vector2(20, 60), Color.White);
        // text = $"paddle size: ({Board.Paddle.Width}, {Board.Paddle.Height})";
        // _spriteBatch.DrawString(_font, text, new Vector2(20, 80), Color.White);
        // _spriteBatch.End();

        base.Draw(gameTime);
    }
}