using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMain;

public class Main : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Paddle paddle;
    private MouseState mState;
    private SpriteFont _font;
    
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
        paddle = new Paddle(_graphics.GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        mState = Mouse.GetState();
        paddle.Update(gameTime, mState);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        string text;
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        text = $"screen: {_graphics.GraphicsDevice.Viewport.Width}, {_graphics.GraphicsDevice.Viewport.Height}";
        _spriteBatch.DrawString(_font, text, new Vector2(20, 20), Color.White);
        text = $"mouse: {mState.X}, {mState.Y}";
        _spriteBatch.DrawString(_font, text, new Vector2(20, 40), Color.White);
        text = $"paddle position: ({paddle.X}, {paddle.Y})";
        _spriteBatch.DrawString(_font, text, new Vector2(20, 60), Color.White);
        text = $"paddle size: ({paddle.Width}, {paddle.Height})";
        _spriteBatch.DrawString(_font, text, new Vector2(20, 80), Color.White);
        _spriteBatch.End();
        
        paddle.Draw(_spriteBatch);
        
        base.Draw(gameTime);
    }
}
