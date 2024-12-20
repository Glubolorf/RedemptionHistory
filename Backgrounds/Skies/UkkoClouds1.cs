using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Redemption.Backgrounds.Skies
{
	public class UkkoClouds1 : CustomSky
	{
		public override void Update(GameTime gameTime)
		{
			if (this.Active)
			{
				this.Intensity = Math.Min(1f, 0.01f + this.Intensity);
			}
			else
			{
				this.Intensity = Math.Max(0f, this.Intensity - 0.01f);
			}
			if (this.ticksUntilNextBolt <= 0)
			{
				this.ticksUntilNextBolt = this.random.Next(5, 20);
				int num = 0;
				while (this.bolts[num].IsAlive && num != this.bolts.Length - 1)
				{
					num++;
				}
				this.bolts[num].IsAlive = true;
				this.bolts[num].Position.X = Utils.NextFloat(this.random) * ((float)Main.maxTilesX * 16f + 4000f) - 2000f;
				this.bolts[num].Position.Y = Utils.NextFloat(this.random) * 500f;
				this.bolts[num].Depth = Utils.NextFloat(this.random) * 8f + 2f;
				this.bolts[num].Life = 30;
			}
			this.ticksUntilNextBolt--;
			for (int i = 0; i < this.bolts.Length; i++)
			{
				if (this.bolts[i].IsAlive)
				{
					UkkoClouds1.Bolt[] expr168cp0 = this.bolts;
					int expr168cp = i;
					expr168cp0[expr168cp].Life = expr168cp0[expr168cp].Life - 1;
					if (this.bolts[i].Life <= 0)
					{
						this.bolts[i].IsAlive = false;
					}
				}
			}
		}

		public override Color OnTileColor(Color inColor)
		{
			return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, this.Intensity * 0.5f));
		}

		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			Texture2D CloudTex = this.mod.GetTexture("Backgrounds/Skies/UkkoClouds1");
			Texture2D boltTexture = this.mod.GetTexture("Backgrounds/Skies/UkkoSkyBolt");
			Texture2D flashTexture = this.mod.GetTexture("Backgrounds/Skies/UkkoSkyFlash");
			Texture2D BeamTexture = this.mod.GetTexture("Backgrounds/Skies/UkkoSkyBeam");
			if (maxDepth >= 3.4028235E+38f && minDepth < 3.4028235E+38f)
			{
				Vector2 SkyPos = new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2));
				spriteBatch.Draw(CloudTex, SkyPos, null, new Color(200, 200, 200), 0f, new Vector2((float)(CloudTex.Width >> 1), (float)(CloudTex.Height >> 1)), 1f, SpriteEffects.None, 1f);
				Color white2 = Color.White;
				float num65 = 1f - Main.cloudAlpha * 1.5f;
				if (num65 < 0f)
				{
					num65 = 0f;
				}
				white2.R = (byte)((float)white2.R * num65);
				white2.G = (byte)((float)white2.G * num65);
				white2.B = (byte)((float)white2.B * num65);
				white2.A = (byte)((float)white2.A * num65);
			}
			float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
			Vector2 value3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
			for (int i = 0; i < this.bolts.Length; i++)
			{
				if (this.bolts[i].IsAlive && this.bolts[i].Depth > minDepth && this.bolts[i].Depth < maxDepth)
				{
					Vector2 value4 = new Vector2(1f / this.bolts[i].Depth, 0.9f / this.bolts[i].Depth);
					Vector2 position = (this.bolts[i].Position - value3) * value4 + value3 - Main.screenPosition;
					if (rectangle.Contains((int)position.X, (int)position.Y))
					{
						Texture2D texture = boltTexture;
						int life = this.bolts[i].Life;
						if (life > 26 && life % 2 == 0)
						{
							texture = flashTexture;
						}
						float scale2 = (float)life / 30f;
						spriteBatch.Draw(texture, position, null, Color.White * scale * scale2 * this.Intensity, 0f, Vector2.Zero, value4.X * 5f, SpriteEffects.None, 0f);
					}
				}
			}
			int num66 = -1;
			int num67 = 0;
			for (int j = 0; j < this._pillars.Length; j++)
			{
				float depth = this._pillars[j].Depth;
				if (num66 == -1 && depth < maxDepth)
				{
					num66 = j;
				}
				if (depth <= minDepth)
				{
					break;
				}
				num67 = j;
			}
			if (num66 == -1)
			{
				return;
			}
			Vector2 value5 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle2 = new Rectangle(-1000, -1000, 4000, 4000);
			float scale3 = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
			for (int k = num66; k < num67; k++)
			{
				Vector2 value6 = new Vector2(1f / this._pillars[k].Depth, 0.9f / this._pillars[k].Depth);
				Vector2 vector = this._pillars[k].Position;
				vector = (vector - value5) * value6 + value5 - Main.screenPosition;
				if (rectangle2.Contains((int)vector.X, (int)vector.Y))
				{
					float num68 = value6.X * 450f;
					spriteBatch.Draw(BeamTexture, vector, null, Color.White * 0.2f * scale3 * this.Intensity, 0f, Vector2.Zero, new Vector2(num68 / 70f, num68 / 45f), SpriteEffects.None, 0f);
				}
			}
		}

		public override float GetCloudAlpha()
		{
			return 1f - this.Intensity;
		}

		public override void Activate(Vector2 position, params object[] args)
		{
			this.Intensity = 0.002f;
			this.Active = true;
			this.bolts = new UkkoClouds1.Bolt[500];
			for (int i = 0; i < this.bolts.Length; i++)
			{
				this.bolts[i].IsAlive = false;
			}
			this._pillars = new UkkoClouds1.LightPillar[40];
			for (int j = 0; j < this._pillars.Length; j++)
			{
				this._pillars[j].Position.X = (float)j / (float)this._pillars.Length * ((float)Main.maxTilesX * 16f + 20000f) + Utils.NextFloat(this._random) * 40f - 20f - 20000f;
				this._pillars[j].Position.Y = Utils.NextFloat(this._random) * 200f - 2000f;
				this._pillars[j].Depth = Utils.NextFloat(this._random) * 8f + 7f;
			}
			Array.Sort<UkkoClouds1.LightPillar>(this._pillars, new Comparison<UkkoClouds1.LightPillar>(this.SortMethod));
		}

		private int SortMethod(UkkoClouds1.LightPillar pillar1, UkkoClouds1.LightPillar pillar2)
		{
			return pillar2.Depth.CompareTo(pillar1.Depth);
		}

		public override void Deactivate(params object[] args)
		{
			this.Active = false;
		}

		public override void Reset()
		{
			this.Active = false;
		}

		public override bool IsActive()
		{
			return this.Active || this.Intensity > 0.001f;
		}

		private readonly UnifiedRandom random = new UnifiedRandom();

		private readonly Redemption mod = Redemption.Inst;

		public bool Active;

		public float Intensity;

		private UkkoClouds1.Bolt[] bolts;

		public int ticksUntilNextBolt;

		private UkkoClouds1.LightPillar[] _pillars;

		private UnifiedRandom _random = new UnifiedRandom();

		private struct Bolt
		{
			public Vector2 Position;

			public float Depth;

			public int Life;

			public bool IsAlive;
		}

		private struct LightPillar
		{
			public Vector2 Position;

			public float Depth;
		}
	}
}
