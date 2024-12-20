using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Critters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Critters
{
	public class SandskinSpider : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sandskin Spider");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 32;
			base.npc.height = 18;
			base.npc.defense = 4;
			base.npc.lifeMax = 5;
			base.npc.HitSound = SoundID.NPCHit22;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.4f;
			base.npc.aiStyle = 7;
			this.aiType = 46;
			this.animationType = 219;
			base.npc.friendly = true;
			base.npc.catchItem = (short)ModContent.ItemType<SandskinSpiderItem>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override bool? CanBeHitByItem(Player player, Item item)
		{
			return new bool?(true);
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return new bool?(true);
		}

		public override void AI()
		{
			Point point = Utils.ToTileCoordinates(base.npc.position);
			float num = base.npc.Distance(Main.player[base.npc.target].Center);
			if (num <= 500f && Main.rand.Next(200) == 0 && this.digThing == 0 && (Main.tile[point.X, point.Y + 2].type == 53 || Main.tile[point.X, point.Y + 2].type == 112 || Main.tile[point.X, point.Y + 2].type == 234))
			{
				this.digThing = 1;
			}
			if (num > 500f && Main.rand.Next(400) == 0 && this.digThing == 1 && this.digFrame >= 10)
			{
				this.digThing = 2;
			}
			if (this.digThing == 1)
			{
				this.digCounter++;
				if (this.digCounter > 10)
				{
					this.digFrame++;
					this.digCounter = 0;
				}
				if (this.digFrame >= 11)
				{
					this.digFrame = 10;
				}
				base.npc.velocity.X = 0f;
				if (this.digFrame >= 10 && Main.tile[point.X, point.Y + 2].type != 53 && Main.tile[point.X, point.Y + 2].type != 112 && Main.tile[point.X, point.Y + 2].type != 234)
				{
					this.digThing = 2;
				}
				if (this.digFrame < 10)
				{
					for (int i = 0; i < 2; i++)
					{
						int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 268, 0f, 0f, 100, default(Color), 1f);
						Dust dust = Main.dust[dustIndex2];
						dust.velocity.Y = dust.velocity.Y * 2.6f;
						Dust dust2 = Main.dust[dustIndex2];
						dust2.velocity.X = dust2.velocity.X * 0f;
					}
					base.npc.dontTakeDamage = false;
				}
				else
				{
					base.npc.dontTakeDamage = true;
				}
			}
			if (this.digThing == 2)
			{
				this.digCounter++;
				if (this.digCounter > 10)
				{
					this.digFrame--;
					this.digCounter = 0;
				}
				if (this.digFrame < 0)
				{
					this.digFrame = 0;
					this.digThing = 0;
				}
				base.npc.velocity.X = 0f;
				if (this.digFrame < 10)
				{
					base.npc.dontTakeDamage = false;
					for (int j = 0; j < 2; j++)
					{
						int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 268, 0f, 0f, 100, default(Color), 1f);
						Dust dust3 = Main.dust[dustIndex3];
						dust3.velocity.Y = dust3.velocity.Y * 2.6f;
						Dust dust4 = Main.dust[dustIndex3];
						dust4.velocity.X = dust4.velocity.X * 0f;
					}
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D digAni = base.mod.GetTexture("NPCs/Critters/SandskinSpiderDig");
			int spriteDirection = base.npc.spriteDirection;
			if (this.digThing == 0)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.digThing >= 1)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y + 2f);
				int num214 = digAni.Height / 11;
				int y6 = num214 * this.digFrame;
				Main.spriteBatch.Draw(digAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, digAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)digAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDayDesert.Chance * ((!RedeConfigClient.Instance.NoSpidersInMyTerrariaMod) ? 1.2f : 0f);
		}

		public int digFrame;

		public int digCounter;

		public int digThing;
	}
}
