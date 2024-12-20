using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class StarGodSky : CustomSky
	{
		public override void OnLoad()
		{
			StarGodSky.SkyTex = TextureManager.Load("NPCs/Bosses/Nebuleus/StarGodSky");
		}

		public override void Update(GameTime gameTime)
		{
			if (this.Active)
			{
				this.Intensity = Math.Min(1f, 0.01f + this.Intensity);
				return;
			}
			this.Intensity = Math.Max(0f, this.Intensity - 0.01f);
		}

		public override Color OnTileColor(Color inColor)
		{
			Vector4 vector = inColor.ToVector4();
			return new Color(Vector4.Lerp(vector, Vector4.One, this.Intensity * 0.5f));
		}

		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 3.4028235E+38f && minDepth < 3.4028235E+38f && !Main.dayTime)
			{
				Vector2 vector;
				vector..ctor((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2));
				spriteBatch.Draw(StarGodSky.SkyTex, vector, null, new Color(102, 20, 80), 0f, new Vector2((float)(StarGodSky.SkyTex.Width >> 1), (float)(StarGodSky.SkyTex.Height >> 1)), 1f, 0, 1f);
				double num = Main.time / 32400.0;
				int screenWidth = Main.screenWidth;
				int width = Main.moonTexture[Main.moonType].Width;
				int width2 = Main.moonTexture[Main.moonType].Width;
				Color white = Color.White;
				double num2 = Main.time / 32400.0;
				float num3 = 1f - Main.cloudAlpha * 1.5f;
				if (num3 < 0f)
				{
					num3 = 0f;
				}
				white.R = (byte)((float)white.R * num3);
				white.G = (byte)((float)white.G * num3);
				white.B = (byte)((float)white.B * num3);
				white.A = (byte)((float)white.A * num3);
			}
		}

		private bool UpdateStarGodIndex()
		{
			int num = ModLoader.GetMod("Redemption").NPCType("Nebuleus");
			if (this.StarGodIndex >= 0 && Main.npc[this.StarGodIndex].active && Main.npc[this.StarGodIndex].type == this.StarGodIndex)
			{
				return true;
			}
			this.StarGodIndex = -1;
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == num)
				{
					this.StarGodIndex = i;
					break;
				}
			}
			return this.StarGodIndex != -1;
		}

		public override float GetCloudAlpha()
		{
			return 1f - this.Intensity;
		}

		public override void Activate(Vector2 position, params object[] args)
		{
			this.Intensity = 0.002f;
			this.Active = true;
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

		public static Texture2D SkyTex;

		public bool Active;

		public float Intensity;

		private int StarGodIndex = -1;

		private UnifiedRandom _random = new UnifiedRandom();
	}
}
