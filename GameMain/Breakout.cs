using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.VectorDraw;

namespace GameMain;

public class Breakout : Game
{
    // private const int Width = 1280;
    // private const int Height = 720;

    internal static GameServiceContainer BreakoutServices { get; private set; } = new();
    
    private Board Board { get; set; }

    public Breakout()
    {
        Disposed += (_, _) =>
        {
            Board = null;
            BreakoutServices = null;
        };
        BreakoutServices.AddService(new GraphicsDeviceManager(this));
        
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        BreakoutServices.AddService(new SpriteBatch(GraphicsDevice));
        BreakoutServices.AddService(new PrimitiveBatch(GraphicsDevice));
        BreakoutServices.AddService(new PrimitiveDrawing(BreakoutServices.GetService<PrimitiveBatch>()));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        BreakoutServices.AddService(Content.Load<SpriteFont>("GameFont"));
        Board = new Board(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        Board.Update(gameTime, Mouse.GetState());

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        Board.Draw();

        // SpriteBatch spriteBatch = BreakoutServices.GetService<SpriteBatch>();
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