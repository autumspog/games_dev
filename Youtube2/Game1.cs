using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace Youtube2;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    //private List<Sprite> sprites;
    //private List<Sprite> floors;
    //private List<Sprite> background;

    //private Texture2D playerTexture;
    private Texture2D textureAtlas;
    //private Texture2D textureFloor;
    //private Texture2D textureTree;
    //private Player player;
    //private YSortCamera camera;
    private Vector2 camera;
    Texture2D spritesheet;
    AnimationManager am;

    //tilemap
    /*
    private Dictionary<Vector2, int> tilemap;
    private List<Rectangle> textureStore;
    */

    //tilemap v2
    private Dictionary<Vector2, int> mg;
    private Dictionary<Vector2, int> fg;
    private Dictionary<Vector2, int> collisions;
    private Texture2D hitboxTexture;

    private int TILESIZE = 32;
    private Texture2D rectangleTexture;

    private Sprite player;
    private List<Rectangle> intersections;
    private KeyboardState prevKeystate;




    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        //sprites = new();
        //background = new();
        //floors = new();
        camera = Vector2.Zero;


         
        /*//TileMap v1
        tilemap = LoadMap("../../../Data/map.csv");
              
        textureStore = new()
        {
            new Rectangle(0,0,8,8),
            new Rectangle(0,8,8,8),
            new Rectangle(8,0,8,8),
            new Rectangle(8,8,8,8),
            
        };
        */

        fg = LoadMap("../../../Data/level1_fg.csv");
        mg = LoadMap("../../../Data/level1_mg.csv");
        collisions = LoadMap("../../../Data/level1_collision.csv");
        intersections = new();

    }
    
    private Dictionary<Vector2, int> LoadMap(string filepath)
    {
        Dictionary<Vector2, int> results = new();

        StreamReader reader = new(filepath);


        //Map build
        int y = 0;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] items = line.Split(',');

            for (int x = 0; x < items.Length; x++)
            {
                if (int.TryParse(items[x], out int value))
                {
                    if (value > -1)
                    {
                        results[new Vector2(x, y)] = value;
                    }
                }
            }
            y++;
        }
        return results;
        

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
        //playerTexture = Content.Load<Texture2D>("Josie_Static");
        //textureTree = Content.Load<Texture2D>("tree");
        textureAtlas = Content.Load<Texture2D>("castle");
        //textureAtlas = Content.Load<Texture2D>("atlas");
        //textureFloor = Content.Load<Texture2D>("grass");
        hitboxTexture = Content.Load<Texture2D>("hitbox");

        rectangleTexture = new Texture2D(GraphicsDevice, 1, 1);
        rectangleTexture.SetData(new Color[] { new(255, 0, 0, 255) });

        am = new(12, 3, new Vector2(256, 512));
        //player = new Sprite(Content.Load<Texture2D>("burt"), new Rectangle(TILESIZE*3,0,TILESIZE,TILESIZE *2 ), new Rectangle(0,0,8,16));
        player = new Sprite(Content.Load<Texture2D>("burt_sprite"), new Rectangle(TILESIZE*3,0,TILESIZE,TILESIZE *2 ), am.GetFrame());

        //spritesheet = Content.Load<Texture2D>("burt_sprite");

        

        //_spriteBatch.Draw(spritesheet, new Rectangle(TILESIZE * 3, 0, TILESIZE, TILESIZE * 2), am.GetFrame(), Color.White);

        //background play   
        /*int yout = -400;
        int xout = 0; 
        //for (int i = 0; i < 32; i++)
        while (xout < 2000)
        {
            while (yout < 2000)
            {
                floors.Add(new Sprite(textureFloor, new(yout, xout, 64, 32), new(0, 0, 8, 4)));
                yout += 64;
                //xout += 0;
            }
            yout = -400;
            xout += 32;
        }
        */

        /*
        foreach (var item in tilemap)
        {
            Rectangle dest = new(
                (int)item.Key.X * 32,
                (int)item.Key.Y * 32,
                32,
                32
             );

            Rectangle src = textureStore[item.Value - 1];

            background.Add(new Sprite(textureAtlas, dest, src));
        }
        */
        //TRee
        /*for (int i = 0; i < 32; i++)
        {
            sprites.Add(new Sprite(textureTree, new(i * 64, i * 32, 64, 128), new(0, 0, 8, 16)));
        
        }
        
        */
        //player = new Player(playerTexture, new(100, 100, 32, 64), new(0, 0, 19, 22));
        //sprites.Add(player);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        /*foreach (Sprite sprite in sprites)
        {
            sprite.Update();
        }*/
        /*foreach (Sprite floor in floors)
        {
            floor.Update();
        }
        */
        //camera.Follow(player.drect, new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));
        /*if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            camera.X -= 5;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            camera.X += 5;
        }
        */
        player.Update(Keyboard.GetState(), prevKeystate, gameTime);
        prevKeystate = Keyboard.GetState();

        player.rect.X += (int)player.velocity.X;
        intersections = getIntersectingTilesHorizontal(player.rect);

        foreach (var rect in intersections)
        {
            if (collisions.TryGetValue(new Vector2(rect.X, rect.Y), out int _val))
            {
                Rectangle collision = new Rectangle(
                    rect.X * TILESIZE,
                    rect.Y * TILESIZE,
                    TILESIZE,
                    TILESIZE);

                if (!player.rect.Intersects(collision))
                {
                    continue;
                }
                if (player.velocity.X > 0.0f)
                {
                    player.rect.X = collision.Left - player.rect.Width;
                } else if (player.velocity.X < 0.0f) {
                    player.rect.X = collision.Right;
                }

            }
        }

        player.rect.Y += (int)player.velocity.Y;
        intersections = getIntersectingTilesVerticle(player.rect);

        player.Grounded = false;
        foreach (var rect in intersections)
        {
            if (collisions.TryGetValue(new Vector2(rect.X, rect.Y), out int _val))
            {
                Rectangle collision = new Rectangle(
                    rect.X * TILESIZE,
                    rect.Y * TILESIZE,
                    TILESIZE,
                    TILESIZE);
                //Colliding with teh top face

                if (!player.rect.Intersects(collision))
                {
                    continue;
                }
                if (player.velocity.Y > 0.0f)
                {
                    player.rect.Y = collision.Top - player.rect.Height;
                    player.velocity.Y = 1.0f;
                    player.Grounded = true;
                    player.jumpCounter = 0;
                }
                else if (player.velocity.Y < 0.0f)
                {
                    player.rect.Y = collision.Bottom;
                }

            }
        

            if (!player.Grounded && player.jumpCounter == 0)
            {
                player.jumpCounter++;
            }
        }
        am.Update();
        base.Update(gameTime);
    }


    public List<Rectangle> getIntersectingTilesHorizontal(Rectangle target)
    {
        List<Rectangle> intersections = new();

        int widthInTiles = (target.Width - (target.Width % TILESIZE)) / TILESIZE;
        int heightInTiles = (target.Height - (target.Height % TILESIZE)) / TILESIZE;

        for (int x = 0; x <= widthInTiles; x++)
        {
            for (int y = 0; y <= heightInTiles; y++)
            {
                intersections.Add(new Rectangle(
                    (target.X + x * TILESIZE) / TILESIZE,
                    (target.Y + y * (TILESIZE - 1)) / TILESIZE,
                    TILESIZE,
                    TILESIZE)
                );
            }
        }

        return intersections;

    }
    public List<Rectangle> getIntersectingTilesVerticle(Rectangle target)
    {
        List<Rectangle> intersections = new();

        int widthInTiles = (target.Width - (target.Width % TILESIZE)) / TILESIZE;
        int heightInTiles = (target.Height - (target.Height % TILESIZE)) / TILESIZE;

        for (int x = 0; x <= widthInTiles; x++)
        {
            for (int y = 0; y <= heightInTiles; y++)
            {
                intersections.Add(new Rectangle(
                    (target.X + x * TILESIZE -1) / TILESIZE,
                    (target.Y + y * (TILESIZE)) / TILESIZE,
                    TILESIZE,
                    TILESIZE)
                );
            }
        }

        return intersections;

    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);


        
        /*foreach (Sprite floor in floors)
        {
            floor.Draw(_spriteBatch, camera.position);
        }
        */


        /*
        foreach (var item in tilemap)
        {
            Rectangle dest = new(
                (int)item.Key.X * 32,
                (int)item.Key.Y * 32,
                32,
                32
             );

            Rectangle src = textureStore[item.Value - 1];

            _spriteBatch.Draw(textureAtlas, dest, src, Color.White);
        }
        */ //replaced with background

        //int display_tilesize = 64;
        int num_tiles_per_row = 8;
        int pixel_tilesize = 8;

        foreach (var item in mg)
        {
            Rectangle drect = new(
                (int)item.Key.X * TILESIZE + (int)camera.X,
                (int)item.Key.Y * TILESIZE + (int)camera.Y,
                TILESIZE,
                TILESIZE
            );


            int x = item.Value % num_tiles_per_row;
            int y = item.Value / num_tiles_per_row;


            Rectangle src = new(
                x * pixel_tilesize,        
                y * pixel_tilesize,
                pixel_tilesize,
                pixel_tilesize
            );

            _spriteBatch.Draw(textureAtlas, drect, src, Color.White);
        };

        foreach (var item in fg)
        {
            Rectangle drect = new(
                (int)item.Key.X * TILESIZE + (int)camera.X,
                (int)item.Key.Y * TILESIZE + (int)camera.Y,
                TILESIZE,
                TILESIZE
            );


            int x = item.Value % num_tiles_per_row;
            int y = item.Value / num_tiles_per_row;


            Rectangle src = new(
                x * pixel_tilesize,
                y * pixel_tilesize,
                pixel_tilesize,
                pixel_tilesize
            );

            _spriteBatch.Draw(textureAtlas, drect, src, Color.White);
        };

        foreach (var item in collisions)
        {
            Rectangle drect = new(
                (int)item.Key.X * TILESIZE + (int)camera.X,
                (int)item.Key.Y * TILESIZE + (int)camera.Y,
                TILESIZE,
                TILESIZE
            );


            int x = item.Value % num_tiles_per_row;
            int y = item.Value / num_tiles_per_row;


            Rectangle src = new(
                x * pixel_tilesize,
                y * pixel_tilesize,
                pixel_tilesize,
                pixel_tilesize
            );
              
            _spriteBatch.Draw(hitboxTexture, drect, src, Color.White);
        };
        //_spriteBatch.Draw(spritesheet, new Rectangle(TILESIZE * 3, 0, TILESIZE, TILESIZE * 2), am.GetFrame(), Color.White);
        /*foreach (var rect in intersections)
        {
            DrawRectHollow(
                _spriteBatch,
                new Rectangle(
                    rect.X * TILESIZE,
                    rect.Y * TILESIZE,
                    TILESIZE,
                    TILESIZE                    
                    ),
                4
            );
        }
        */
        player.Draw(_spriteBatch);

        //DrawRectHollow(_spriteBatch, player.rect, 4);

        /*foreach (Sprite back in background)
        {
            back.Draw(_spriteBatch, camera.position);
        }
        */
        //camera.Draw(_spriteBatch, sprites);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    public void DrawRectHollow(SpriteBatch spritBatch, Rectangle rect, int thickness)
    {
        spritBatch.Draw(
            rectangleTexture,
            new Rectangle(
                rect.X,
                rect.Y,
                rect.Width,
                thickness
            ),
            Color.White
        );
        spritBatch.Draw(
            rectangleTexture,
            new Rectangle(
                rect.X,
                rect.Bottom - thickness,
                rect.Width,
                thickness
            ),
            Color.White
        );
        spritBatch.Draw(
            rectangleTexture,
            new Rectangle(
                rect.X,
                rect.Y,
                thickness,
                rect.Height
            ),
            Color.White
        );
        spritBatch.Draw(
            rectangleTexture,
            new Rectangle(
                rect.Right - thickness,
                rect.Y,
                thickness,
                rect.Height
            ),
            Color.White
        );
    }
}
