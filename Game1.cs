using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Name;

namespace Assignment_01;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Vector2 minecraftPosition;
    private Vector2 minecraftDimensions;
    private Vector2 tepigPos;
    private readonly float tepigSpeed = 100f;

    Texture2D cattexture;
    Texture2D minecrafttexture;
    Texture2D background;

    Texture2D[] minecraftrunningTextures;
    Texture2D[] tepigrunningTextures;
    private int counter;
    private int TepigActiveFrame;
    private int minecraftActiveFrame;
    bool movingLeft;
    bool movingRight;
    bool movingUp;
    bool movingDown;
    private Vector2 tepigVelocity;
    int tepigDirectionFrame;
    private SpriteFont font;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }


    protected override void Initialize()
    {
        // TODO: Add your initialization logic here 
        minecraftPosition = new Vector2(50f, 30f);
        minecraftDimensions = new Vector2(150f, 150f);

        tepigPos = new Vector2(400f, 200f);

        tepigVelocity = new Vector2(tepigSpeed, 0f);

        int screenWidth = _graphics.PreferredBackBufferWidth;
        int screenHeight = _graphics.PreferredBackBufferHeight;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        minecraftrunningTextures = new Texture2D[5];
        tepigrunningTextures = new Texture2D[2];
        
        minecraftrunningTextures[0] = Content.Load<Texture2D>("minecrafttextfront");
        minecraftrunningTextures[1] = Content.Load<Texture2D>("minecrafttextureleft");
        minecraftrunningTextures[2] = Content.Load<Texture2D>("minecrafttextureright");
        minecraftrunningTextures[3] = Content.Load<Texture2D>("minecrafttexturejump");
        minecraftrunningTextures[4] = Content.Load<Texture2D>("minecrafttexturesneak");

        tepigrunningTextures[0] = Content.Load<Texture2D>("tepigright");
        tepigrunningTextures[1] = Content.Load<Texture2D>("tepigleft");

        background = Content.Load<Texture2D>("background");
        cattexture = Content.Load<Texture2D>("cat-standing");
        minecrafttexture = Content.Load<Texture2D>("idkk");

        font = Content.Load<SpriteFont>("Fonts/fontt");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        movingLeft = false;
        movingRight = false;
        movingDown = false;
        movingUp = false;

        if (tepigVelocity.X > 0)
        {
            tepigDirectionFrame = 0;
        } 
        else
        {
            tepigDirectionFrame = 1;
        }
            
        if (tepigPos.X > 630)
        {
            tepigVelocity.X = -tepigSpeed;
        }
        if (tepigPos.X < 340)
        {
            tepigVelocity.X = tepigSpeed;
        }

        float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        

        tepigPos += tepigVelocity * seconds;

        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            minecraftPosition.Y -= 1;
            movingUp = true;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            minecraftPosition.Y += 1;
            movingDown = true;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            minecraftPosition.X += 1;
            movingRight = true;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            minecraftPosition.X -= 1;
            movingLeft = true;
        }

        minecraftPosition.X = MathHelper.Clamp(
            minecraftPosition.X,
            0,
            _graphics.PreferredBackBufferWidth - minecraftDimensions.X
        );
        minecraftPosition.Y = MathHelper.Clamp(
            minecraftPosition.Y,
            0,
            _graphics.PreferredBackBufferHeight
        );

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        Rectangle rect = new Rectangle(
            (int)minecraftPosition.X,
            (int)minecraftPosition.Y,
            (int)minecraftDimensions.X,
            (int)minecraftDimensions.Y
        );

        _spriteBatch.Draw(background, new Rectangle(0, 0, 1000, 500), Color.White);

        _spriteBatch.Draw(tepigrunningTextures[tepigDirectionFrame], tepigPos, Color.White);

        int frameToDraw = minecraftActiveFrame;

        if (movingLeft)
            frameToDraw = 1;  
        else if (movingRight)
            frameToDraw = 2;  
        else if (movingUp)
            frameToDraw = 3; 
        else if (movingDown)
            frameToDraw = 4;
        else 
            frameToDraw = 0;

        _spriteBatch.Draw(minecraftrunningTextures[frameToDraw], rect, Color.White);

        _spriteBatch.Draw(cattexture, new Vector2(-300, 0), Color.White);

        _spriteBatch.DrawString(font, "Assigntment 1!", new Vector2(225, 0), color: Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);

    }

}
