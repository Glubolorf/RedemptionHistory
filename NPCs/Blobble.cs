using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class Blobble : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blobble");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 22;
			base.npc.height = 18;
			base.npc.friendly = false;
			base.npc.damage = 2;
			base.npc.defense = 25;
			base.npc.lifeMax = 11;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 60f;
			base.npc.knockBackResist = 0.6f;
			base.npc.aiStyle = 1;
			this.aiType = 183;
			this.animationType = 302;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 4, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 4, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 4, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 4, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			if (this.flatBlobble)
			{
				this.flatCounter++;
				if (this.flatCounter > 20)
				{
					this.flatFrame++;
					this.flatCounter = 0;
				}
				if (this.flatFrame >= 2)
				{
					this.flatFrame = 0;
				}
			}
			if (this.hatBlobble || this.hatBlobble2 || this.hatBlobble3 || this.hatBlobble4 || this.hatBlobble5)
			{
				this.hatCounter++;
				if (this.hatCounter > 20)
				{
					this.hatFrame++;
					this.hatCounter = 0;
				}
				if (this.hatFrame >= 2)
				{
					this.hatFrame = 0;
				}
			}
			if (!this.change)
			{
				int changeChoice = Main.rand.Next(100);
				if (changeChoice == 0)
				{
					this.flatBlobble = true;
					this.change = true;
					return;
				}
				if (changeChoice > 0 && changeChoice < 5)
				{
					this.hatBlobble = true;
					this.change = true;
					return;
				}
				if (changeChoice >= 5 && changeChoice < 10)
				{
					this.hatBlobble2 = true;
					this.change = true;
					return;
				}
				if (changeChoice >= 10 && changeChoice < 15)
				{
					this.hatBlobble3 = true;
					this.change = true;
					return;
				}
				if (changeChoice >= 15 && changeChoice < 20)
				{
					this.hatBlobble4 = true;
					this.change = true;
					return;
				}
				if (changeChoice >= 20 && changeChoice < 25)
				{
					this.hatBlobble5 = true;
					this.change = true;
					return;
				}
				if (changeChoice > 25)
				{
					this.change = true;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D flatAni = base.mod.GetTexture("NPCs/BlobbleFlat");
			Texture2D hat1Ani = base.mod.GetTexture("NPCs/Varients/BlobbleHat");
			Texture2D hat2Ani = base.mod.GetTexture("NPCs/Varients/BlobbleHat2");
			Texture2D hat3Ani = base.mod.GetTexture("NPCs/Varients/BlobbleHat3");
			Texture2D hat4Ani = base.mod.GetTexture("NPCs/Varients/BlobbleHat4");
			Texture2D hat5Ani = base.mod.GetTexture("NPCs/Varients/BlobbleHat5");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.flatBlobble && !this.hatBlobble && !this.hatBlobble2 && !this.hatBlobble3 && !this.hatBlobble4 && !this.hatBlobble5)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.flatBlobble)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = flatAni.Height / 2;
				int y6 = num214 * this.flatFrame;
				Main.spriteBatch.Draw(flatAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, flatAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)flatAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hatBlobble)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y - 6f);
				int num215 = hat1Ani.Height / 2;
				int y7 = num215 * this.hatFrame;
				Main.spriteBatch.Draw(hat1Ani, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, hat1Ani.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)hat1Ani.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hatBlobble2)
			{
				Vector2 drawCenter3 = new Vector2(base.npc.Center.X, base.npc.Center.Y - 6f);
				int num216 = hat2Ani.Height / 2;
				int y8 = num216 * this.hatFrame;
				Main.spriteBatch.Draw(hat2Ani, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, hat2Ani.Width, num216)), drawColor, base.npc.rotation, new Vector2((float)hat2Ani.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hatBlobble3)
			{
				Vector2 drawCenter4 = new Vector2(base.npc.Center.X, base.npc.Center.Y - 6f);
				int num217 = hat3Ani.Height / 2;
				int y9 = num217 * this.hatFrame;
				Main.spriteBatch.Draw(hat3Ani, drawCenter4 - Main.screenPosition, new Rectangle?(new Rectangle(0, y9, hat3Ani.Width, num217)), drawColor, base.npc.rotation, new Vector2((float)hat3Ani.Width / 2f, (float)num217 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hatBlobble4)
			{
				Vector2 drawCenter5 = new Vector2(base.npc.Center.X, base.npc.Center.Y - 6f);
				int num218 = hat4Ani.Height / 2;
				int y10 = num218 * this.hatFrame;
				Main.spriteBatch.Draw(hat4Ani, drawCenter5 - Main.screenPosition, new Rectangle?(new Rectangle(0, y10, hat4Ani.Width, num218)), drawColor, base.npc.rotation, new Vector2((float)hat4Ani.Width / 2f, (float)num218 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hatBlobble5)
			{
				Vector2 drawCenter6 = new Vector2(base.npc.Center.X, base.npc.Center.Y - 6f);
				int num219 = hat5Ani.Height / 2;
				int y11 = num219 * this.hatFrame;
				Main.spriteBatch.Draw(hat5Ani, drawCenter6 - Main.screenPosition, new Rectangle?(new Rectangle(0, y11, hat5Ani.Width, num219)), drawColor, base.npc.rotation, new Vector2((float)hat5Ani.Width / 2f, (float)num219 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDaySlime.Chance * 0.005f;
		}

		public bool change;

		public bool flatBlobble;

		public int flatCounter;

		public int flatFrame;

		public bool hatBlobble;

		public bool hatBlobble2;

		public bool hatBlobble3;

		public bool hatBlobble4;

		public bool hatBlobble5;

		private int hatFrame;

		private int hatCounter;
	}
}
