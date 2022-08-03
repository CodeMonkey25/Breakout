using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMain;

public class Main : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteFont _font;
    private SpriteBatch _spriteBatch;

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
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _font = Content.Load<SpriteFont>("GameFont");
        Board = new Board(_graphics.GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        // TODO: Add your update logic here
        MouseState = Mouse.GetState();
        Board.Update(gameTime, MouseState);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        Board.Draw(_spriteBatch);

        _spriteBatch.Begin();
        string text;
        text = $"screen: {_graphics.GraphicsDevice.Viewport.Width}, {_graphics.GraphicsDevice.Viewport.Height}";
        _spriteBatch.DrawString(_font, text, new Vector2(20, 20), Color.White);
        // text = $"mouse: {MouseState.X}, {MouseState.Y}";
        // _spriteBatch.DrawString(_font, text, new Vector2(20, 40), Color.White);
        // text = $"paddle position: ({Board.Paddle.X}, {Board.Paddle.Y})";
        // _spriteBatch.DrawString(_font, text, new Vector2(20, 60), Color.White);
        // text = $"paddle size: ({Board.Paddle.Width}, {Board.Paddle.Height})";
        // _spriteBatch.DrawString(_font, text, new Vector2(20, 80), Color.White);
        _spriteBatch.End();


        base.Draw(gameTime);
    }
}