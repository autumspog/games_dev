using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Youtube2;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<Sprite> sprites;

    private Texture2D playerTexture;
    private Texture2D textureAtlas;
    private Player player;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        sprites = new();
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
        playerTexture = Content.Load<Texture2D>("Josie_Static");
        textureAtlas = Content.Load<Texture2D>("grass");

        for (int i = 0; i < 15; i++)
        {
            sprites.Add(new Sprite(textureAtlas, new(i * 64, i * 32, 64, 32), new(0, 0, 8, 4)));
        }
        player = new Player(playerTexture, new(100, 100, 64, 128), new(0, 0, 19, 22));
        sprites.Add(player);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        foreach (Sprite sprite in sprites)
        {
            sprite.Update();
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        foreach (Sprite sprite in sprites)
        {
            sprite.Draw(_spriteBatch, new Vector2(0, 0));
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
