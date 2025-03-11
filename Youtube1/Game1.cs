using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Youtube1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    //private ScaleSprite sprite;
    //private ColoredSprite sprite;
    //private MovingSprite sprite;
    //private Player toast;
    //List<MovingSprite> sprites;
    //List<MovingSprite> spoons;
    List<Sprite> sprites;

    /* Basic Animation
    Texture2D[] runningTextures;
    int counter;
    int activeFrame;
    */

    /*2Frame
    Texture2D spritesheet;
    int counter;
    int activeFrame;
    int numFrames;
    */

    Texture2D spritesheet;
    AnimationManager animationManager;
    AnimationManager willowManager;
    AnimationManager oakleyManager;
    Texture2D willowsheet;
    Texture2D oakleysheet;

    //Player player;
    //Texture2D texture;
    private SpriteFont font;
    private int score = 0;

    //Basic Scoring
    bool is_space_pressed = false;

    Song song;
    public Game1()
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
        Texture2D badtexture = Content.Load<Texture2D>("badbutterfly");
        Texture2D plat1 = Content.Load<Texture2D>("Platform_1");
        Texture2D plat2 = Content.Load<Texture2D>("Platform_2");
        Texture2D plat3 = Content.Load<Texture2D>("Platform_3");
        Texture2D texture = Content.Load<Texture2D>("butterfly");
        Texture2D heart = Content.Load<Texture2D>("Heart");
        spritesheet = Content.Load<Texture2D>("Frog");
        willowsheet = Content.Load<Texture2D>("willow");
        oakleysheet = Content.Load<Texture2D>("Oakley");
        font = Content.Load<SpriteFont>("Fonts/8bitfont");
        /* basic Animation
        runningTextures = new Texture2D[2];

        runningTextures[0] = Content.Load<Texture2D>("frame_0");
        runningTextures[1] = Content.Load<Texture2D>("frame_1");
        */

        /* 2 Frame
        spritesheet = Content.Load<Texture2D>("Frog");
        activeFrame = 0;
        numFrames = 3;
        counter = 0;
        */
        //sprite = new ScaleSprite(texture, Vector2.Zero);
        //sprite = new ColoredSprite(texture, Vector2.Zero, Color.Red);
        //sprite = new MovingSprite(texture, Vector2.Zero, 1f);
        //sprites = new List<MovingSprite>();
        //toast = new Player(texture, Vector2.Zero);
        sprites = new();

        /*for (int i = 0; i < 10; i++)
        {
            sprites.Add(new MovingSprite(texture, new Vector2(0, 10 * i), i));
        }

        spoons = new List<MovingSprite>();
        for (int j = 0; j < 10; j++)
        {
            spoons.Add(new MovingSprite(badtexture, new Vector2(0, 20 * j), j));
        }
        cakes.Add(new MovingSprite(badtexture, new Vector2(100, 100), 0f));
        cakes.Add(new MovingSprite(badtexture, new Vector2(300, 100), 0f));
        cakes.Add(new MovingSprite(badtexture, new Vector2(500, 100), 0f));
        */
        //Sprite sprite = new Sprite(badtexture, new Vector2(100, 100));

        //sprites.Add(sprite);

        //need video
        /*sprites.Add(new Sprite(plat1, new Vector2(100, 350)));
        sprites.Add(new Sprite(plat2, new Vector2(300, 350)));
        sprites.Add(new Sprite(plat2, new Vector2(500, 350)));
        */
        //play loop build floor
        float floor = 0;
        for (int i = 0; i < 20; i++)
        {

            sprites.Add(new Sprite(plat1, new Vector2(floor, 420)));
            sprites.Add(new Sprite(plat2, new Vector2(floor, 440)));
            sprites.Add(new Sprite(plat3, new Vector2(floor, 460)));
            floor += 50;
        }
        //cakes.Add(new Sprite(heart, new Vector2(600, 300)));
        //cakes.Add(new Player(texture, new Vector2(0, 0)));
        

        /*
        player = new Player(texture, new Vector2(100, 100), sprites);
        sprites.Add(player);
        */

        //sprites.Remove(sprite);
        animationManager = new(11, 4, new Vector2(19, 22));
        willowManager = new (5, 2, new Vector2(19, 22));
        oakleyManager = new (5, 2, new Vector2(19, 22));

        //Song 

        song = Content.Load<Song>("Audio/background_music");

        MediaPlayer.Play(song);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        //sprite.Update();
        /*foreach (MovingSprite sprite in sprites)
        {
            sprite.Update();
        }

        foreach (MovingSprite spoon in spoons)
        {
            spoon.Update();
        }*/
        //toast.Update();
        /*
        List<Sprite> killList = new();
        foreach (var sprite in sprites)
        {
            sprite.Update();

            if (sprite != player && sprite.Rect.Intersects(player.Rect))
            {
                killList.Add(sprite);
            }
        }

        foreach (var sprite in killList)
        {
            sprites.Remove(sprite);
        }*/

        
        foreach (var sprite in sprites)
        {
            sprite.Update(gameTime);
        }
        
        //player.Update(gameTime);

        /* Basic Animaition
        counter++;

        if (counter > 29)
        {
            counter = 0;
            activeFrame++;
        
            if (activeFrame > runningTextures.Length - 1)
            {

                activeFrame = 0;
            }
        }
        */

        /* 2 frame 
        counter++;
        if (counter > 29)
        {
            counter = 0;
            activeFrame++;

            if (activeFrame == numFrames)
            {
                activeFrame = 0;
            }
        }
        */

        animationManager.Update();
        willowManager.Update();
        oakleyManager.Update();

        //Font setup
        if (Keyboard.GetState().IsKeyDown(Keys.Space) && !is_space_pressed)
        {
            score++;
            is_space_pressed = true;
        }

        if (Keyboard.GetState().IsKeyUp(Keys.Space))
        {
            is_space_pressed = false;
        }
        
        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        //Three butterfly to screen manually
        //_spriteBatch.Draw(texture, new Rectangle(100,100,100,100), Color.White);
        //_spriteBatch.Draw(texture, new Rectangle(100, 200, 100, 100), Color.Gray);
        //_spriteBatch.Draw(texture, new Rectangle(100, 300, 100, 100), Color.MediumVioletRed);
        //Vid2 - sprite load
        //_spriteBatch.Draw(sprite.texture, sprite.Rect, Color.White);
        //_spriteBatch.Draw(sprite.texture, sprite.Rect, sprite.color);
        /*v2
        foreach (MovingSprite sprite in sprites)
            {
            /_sprite draw _spriteBatch.Draw(sprite.texture, sprite.Rect, Color.White);
            sprite.Draw(_spriteBatch);
            }
        foreach (MovingSprite spoon in spoons)
        {
            // _spriteBatch.Draw(spoon.texture, spoon.Rect, Color.White);
            spoon.Draw(_spriteBatch);
        }*/


        foreach (var sprite in sprites)
        {
            //_spriteBatch.Draw(cake.texture, cake.Rect, Color.White);
            sprite.Draw(_spriteBatch);
        }
        //player.Draw(_spriteBatch);
        //toast.Draw(_spriteBatch);

        /* Basic animation
        _spriteBatch.Draw(runningTextures[activeFrame], new Rectangle(100, 100, 100, 200), Color.White);
        */
        /* Animation 2 frame
        _spriteBatch.Draw(
            spritesheet, 
            new Rectangle(100,100,100,200), 
            new Rectangle(activeFrame * 32, 0, 32, 32),
            Color.White);
        */
        _spriteBatch.Draw(spritesheet, new Rectangle(100, 100, 100, 200), animationManager.GetFrame(), Color.White);
        _spriteBatch.Draw(willowsheet, new Rectangle(300, 100, 100, 200), willowManager.GetFrame(), Color.White);
        _spriteBatch.Draw(oakleysheet, new Rectangle(500, 100, 100, 200), oakleyManager.GetFrame(), Color.White);
        _spriteBatch.DrawString(font, "Score: " + score, Vector2.Zero, Color.White);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
