using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D boll;
        Texture2D paddel;
        Rectangle paddel1;
        Rectangle paddel2;
        Rectangle bollHitBox;
        Vector2 bollPosition;
        Vector2 bollHastighet;
        Random slump = new Random();
        KeyboardState keyboard = Keyboard.GetState();
        int poängVänster = 0;
        int poängHöger = 0;
        SpriteFont Resultat;
        Vector2 ResultatPosition = new Vector2(350, 50);

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

            boll = Content.Load<Texture2D>("boll");
            paddel = Content.Load<Texture2D>("paddle");
            bollHitBox = new Rectangle (0, 0, boll.Width, boll.Height);
            NyBoll();

            paddel1 = new Rectangle(50, 250, paddel.Width, paddel.Height);
            paddel2 = new Rectangle(750 - paddel.Width, 250, paddel.Width, paddel.Height);

            Resultat = Content.Load<SpriteFont>("Resultat");
        }

        protected override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            bollPosition += bollHastighet;

            FlyttaPaddles();
            KollaKollisioner();
            KollaPoäng();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(Resultat, $"{poängVänster} - {poängHöger}", ResultatPosition, Color.Black);
            _spriteBatch.Draw(boll, bollPosition, Color.White);
            _spriteBatch.Draw(paddel, paddel1, Color.White);
            _spriteBatch.Draw(paddel, paddel2, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        void NyBoll()
        {

            bollPosition.X = 400 - (boll.Width / 2);
            bollPosition.Y = 240 - (boll.Height / 2);
            bollHastighet.X = slump.Next(5, 8);
            bollHastighet.Y = slump.Next(5, 8);
        }

        void FlyttaPaddles()
        {

            if (keyboard.IsKeyDown(Keys.Down))
            {
                paddel2.Y += 5;
            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                paddel2.Y -= 5;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                paddel1.Y += 5;
            }
            if (keyboard.IsKeyDown(Keys.W))
            {
                paddel1.Y -= 5;
            }
        }

        void KollaKollisioner()
        {
            bollHitBox.X = (int)bollPosition.X;
            bollHitBox.Y = (int)bollPosition.Y;

            if (bollHitBox.Intersects(paddel1) || bollHitBox.Intersects(paddel2))
            {
                bollHastighet.X *= -1;
            }

            if (bollPosition.Y < 0 || bollPosition.Y + boll.Height > 480)
            {
                bollHastighet.Y *= -1;
            }
        }

        void KollaPoäng()
        {
            if (bollPosition.X < 0)
            {
                poängHöger++;
                NyBoll();
            }
            if (bollPosition.X > 800)
            {
                poängVänster++;
                NyBoll();
            }
        }
    }
}
