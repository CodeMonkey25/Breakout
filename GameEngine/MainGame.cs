using System;
using GameMain.GameEngine.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMain.GameEngine;

public class MainGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private BaseGameState _currentGameState;
    private RenderTarget2D _renderTarget;
    private Rectangle _renderScaleRectangle;
    
    public MainGame(BaseGameState firstGameState, int width, int height)
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = width,
            PreferredBackBufferHeight = height,
            IsFullScreen = false,
            SynchronizeWithVerticalRetrace = false,
        };
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        _currentGameState = firstGameState;
    }

    protected override void Initialize()
    {
        // _graphics.ApplyChanges();

        _renderTarget = new RenderTarget2D(
            _graphics.GraphicsDevice,
            _graphics.PreferredBackBufferWidth,
            _graphics.PreferredBackBufferHeight,
            false,
            SurfaceFormat.Color,
            DepthFormat.None,
            0,
            RenderTargetUsage.DiscardContents
        );

        _renderScaleRectangle = GetScaleRectangle();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        LoadGameState();
    }

    protected override void UnloadContent()
    {
        UnloadGameState();
    }
    
    protected override void Update(GameTime gameTime)
    {
        // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //     Exit();

        _currentGameState.HandleInput(gameTime);
        _currentGameState.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Render to the Render Target
        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _currentGameState.Render(_spriteBatch);
        _spriteBatch.End();

        // Now render the scaled content
        _graphics.GraphicsDevice.SetRenderTarget(null);
        _graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);
        _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
        _spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    #region Events
    private void CurrentGameState_OnStateSwitched(object sender, BaseGameState e)
    {
        SwitchGameState(e);
    }

    private void _currentGameState_OnEventNotification(object sender, BaseGameStateEvent e)
    {
        switch (e)
        {
            case GameQuit _:
                Exit();
                break;
        }
    }
    #endregion
    
    #region Methods
    private Rectangle GetScaleRectangle()
    {
        double designedAspectRatio = _graphics.PreferredBackBufferWidth / (double)_graphics.PreferredBackBufferHeight;
        double actualAspectRatio = Window.ClientBounds.Width / (double)Window.ClientBounds.Height;

        int x = 0;
        int y = 0;
        int width = Window.ClientBounds.Width;
        int height = Window.ClientBounds.Height;

        if (actualAspectRatio <= designedAspectRatio)
        {
            height = (int)Math.Ceiling(Window.ClientBounds.Width / designedAspectRatio);
            y = (Window.ClientBounds.Height - height) / 2;
        }
        else
        {
            width = (int)Math.Ceiling(Window.ClientBounds.Height * designedAspectRatio);
            x = (Window.ClientBounds.Width - width) / 2;
        }

        return new Rectangle(x, y, width, height);
    }
 
    private void SwitchGameState(BaseGameState gameState)
    {
        UnloadGameState();
        _currentGameState = gameState;
        LoadGameState();
    }

    private void UnloadGameState()
    {
        if (_currentGameState == null) return;

        _currentGameState.OnStateSwitched -= CurrentGameState_OnStateSwitched;
        _currentGameState.OnEventNotification -= _currentGameState_OnEventNotification;
        _currentGameState.UnloadContent();
        _currentGameState = null;
    }

    private void LoadGameState()
    {
        _currentGameState.Initialize(Content, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);
        _currentGameState.LoadContent();
        _currentGameState.OnStateSwitched -= CurrentGameState_OnStateSwitched;
        _currentGameState.OnStateSwitched += CurrentGameState_OnStateSwitched;
        _currentGameState.OnEventNotification -= _currentGameState_OnEventNotification;
        _currentGameState.OnEventNotification += _currentGameState_OnEventNotification;
    }
    #endregion
}