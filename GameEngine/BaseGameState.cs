using System;
using System.Collections.Generic;
using System.Linq;
using GameMain.GameEngine.Events;
using GameMain.GameEngine.Input;
using GameMain.GameEngine.Object;
using GameMain.GameEngine.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameMain.GameEngine;

public abstract class BaseGameState
{
    public event EventHandler<BaseGameState> OnStateSwitched;
    public event EventHandler<BaseGameStateEvent> OnEventNotification;

    protected bool Debug { get; set; }
    protected int ViewportWidth { get; set; }
    protected int ViewportHeight { get; set; }
    protected SoundManager SoundManager { get; }
    protected InputManager InputManager { get; }

    private readonly IList<IList<BaseGameObject>> _gameObjects = new List<IList<BaseGameObject>>();
    private ContentManager _contentManager;
    
    public BaseGameState(BaseInputMapper inputMapper)
    {
        SoundManager = new SoundManager();
        InputManager = new InputManager(inputMapper);    
    }
    
    public void Initialize(ContentManager contentManager, int viewportWidth, int viewportHeight)
    {
        _contentManager = contentManager;
        ViewportHeight = viewportHeight;
        ViewportWidth = viewportWidth;
    }

    public abstract void HandleInput(GameTime gameTime);
    public abstract void UpdateGameState(GameTime gameTime);
    public abstract void LoadContent();

    public void UnloadContent()
    {
        _contentManager.Unload();
    }
    
    public void Update(GameTime gameTime) 
    {
        UpdateGameState(gameTime);
        SoundManager.PlaySoundtrack();
    }
    
    protected Texture2D LoadTexture(string textureName)
    {
        return _contentManager.Load<Texture2D>(textureName);
    }

    protected SpriteFont LoadFont(string fontName)
    {
        return _contentManager.Load<SpriteFont>(fontName);
    }

    protected SoundEffect LoadSound(string soundName)
    {
        return _contentManager.Load<SoundEffect>(soundName);
    }
 
    protected void NotifyEvent(BaseGameStateEvent gameEvent)
    {
        OnEventNotification?.Invoke(this, gameEvent);

        foreach (IList<BaseGameObject> gameObjects in _gameObjects)
        {
            foreach (BaseGameObject gameObject in gameObjects)
            {
                gameObject.OnNotify(gameEvent);
            }
        }

        SoundManager.OnNotify(gameEvent);
    }

    protected void SwitchState(BaseGameState gameState)
    {
        OnStateSwitched?.Invoke(this, gameState);
    }

    protected void AddGameObject(BaseGameObject gameObject)
    {
        if (gameObject == null) return;
        
        while (_gameObjects.Count <= gameObject.ZIndex)
        {
            _gameObjects.Add(new List<BaseGameObject>());
        }
        
        _gameObjects[gameObject.ZIndex].Add(gameObject);
    }

    protected void RemoveGameObject(BaseGameObject gameObject)
    {
        _gameObjects[gameObject.ZIndex].Remove(gameObject);
    }

    public virtual void Render(SpriteBatch spriteBatch)
    {
        foreach (IList<BaseGameObject> gameObjects in _gameObjects)
        {
            foreach (BaseGameObject gameObject in gameObjects)
            {
                if (Debug)
                {
                    gameObject.RenderBoundingBoxes(spriteBatch);
                }
                gameObject.Render(spriteBatch);
            }
        }
    }
}