using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpriteClasses{
    public class Sprite{

        public Vector2 originalVelocity, position, spriteOrigin, velocity;

        public Vector2 Velocity {
            get {
                return velocity;
            }
            set {
                velocity = value;
            }
        }

        public Vector2 OriginalVelocity {
            get {
                return originalVelocity;
            }
            set {
                originalVelocity = value;
            }
        }

        public Vector2 Position {
            get {
                return position;
            }
            set {
                position = value;
            }
        }

        public Vector2 SpriteOrigin {
            get {
                return spriteOrigin;
            }
            set {
                spriteOrigin = value;
            }
        }

        public bool Alive { get; set; }
        public float Rotation { get; set; }
        public float RotationSpeed { get; set; }
        public float Scale { get; set; }
        public SpriteEffects SpriteEffect { get; set; }
        public Texture2D TextureImage { get; set; }

        public Rectangle CollisionRectangle {
            get {
                if (SpriteOrigin != Vector2.Zero) {
                    return new Rectangle((int)Position.X, (int)Position.Y, (int)(TextureImage.Width * Scale), (int)(TextureImage.Height * Scale));
                } else {
                    return new Rectangle((int)SpriteOrigin.X, (int)SpriteOrigin.Y, (int)(TextureImage.Width * Scale), (int)(TextureImage.Height * Scale));
                }
            }
        }


        // my vars
        const byte IDLE = 0, LEFT = 1, UP = 2, RIGHT = 3, DOWN = 4;


        public Sprite() {

        }

        public Sprite(Texture2D textureImage, Vector2 position, Vector2 velocity, bool setOrigin, float rotationSpeed, float scale, SpriteEffects spriteEffect) {
            TextureImage = textureImage;
            Position = position;
            OriginalVelocity = velocity;
            Velocity = velocity;
            RotationSpeed = rotationSpeed;
            Scale = scale;
            SpriteEffect = spriteEffect;

            if (setOrigin) {
                SpriteOrigin = new Vector2(TextureImage.Width / 2, TextureImage.Height / 2);
            }
            else {
                SpriteOrigin = Vector2.Zero;
            }

            // Renamed 'active' to 'alive' as it made more sense
            Alive = true;

        }

        public virtual void Update(GameTime gameTime) {
            if (Alive) {
                
                if (Rotation < 360) {
                    Rotation += RotationSpeed;
                }
                else if (Rotation >= 360) {
                    Rotation = 0;
                }

                position.X += (velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds);
                position.Y += (velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        public bool CollisionMouse(int x, int y) {
            if (CollisionRectangle.Contains(x, y) && Alive){
                Kill();
                return true;
            } else {
                return false;
            }
        }

        public bool CollisionSprite(Sprite sprite) {
            return true;
        }

        public void Kill() {
            Alive = false;
            velocity = Vector2.Zero;
            position = new Vector2(-100, -100);

        }

        public void Controls(byte direction) {
            if (direction == IDLE) {
                velocity.X = 0;
                velocity.Y = 0;
            } else {
                if (direction == RIGHT) {
                    velocity.X += originalVelocity.X;
                } else if (direction == LEFT) {
                    velocity.X -= originalVelocity.X;
                }

                if (direction == UP) {
                    velocity.Y -= originalVelocity.Y;
                } else if (direction == DOWN) {
                    velocity.Y += originalVelocity.Y;
                }
            }
        }



        public virtual void Update(GameTime gameTime, GraphicsDevice Device) {
            if (Alive) {
                if ((position.X + SpriteOrigin.X * Scale) >= Device.Viewport.Width || position.X - SpriteOrigin.X * Scale <= 0) {
                    velocity.X *= -1;
                }

                if ((position.Y + SpriteOrigin.Y * Scale) >= Device.Viewport.Height || position.Y - SpriteOrigin.Y * Scale <= 0) {
                    velocity.Y *= -1;
                }

                Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            if (Alive) {
                spriteBatch.Draw(TextureImage, position, new Rectangle(0, 0, TextureImage.Width, TextureImage.Height), Color.White, MathHelper.ToRadians(Rotation), SpriteOrigin, Scale, SpriteEffect, 0);
            }
        }

    }
}
