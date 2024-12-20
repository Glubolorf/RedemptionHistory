using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class FatPirate : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pirate Cook");
			Main.npcFrameCount[base.npc.type] = 16;
		}

		public override void SetDefaults()
		{
			base.npc.width = 48;
			base.npc.height = 63;
			base.npc.friendly = false;
			base.npc.damage = 35;
			base.npc.defense = 16;
			base.npc.lifeMax = 550;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 380f;
			base.npc.knockBackResist = 0.02f;
			base.npc.aiStyle = 3;
			this.aiType = 212;
			this.animationType = 212;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("PirateSpiceTraderBanner");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FatPirateGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FatPirateGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FatPirateGore3"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Pirates.Chance * 0.02f;
		}

		public override void AI()
		{
			if (this.specialAttack)
			{
				this.attackCounter++;
				if (this.attackCounter > 10)
				{
					this.attackFrame++;
					this.attackCounter = 0;
				}
				if (this.attackFrame >= 12)
				{
					this.attackFrame = 0;
				}
			}
			if (Main.rand.Next(120) == 0 && !this.specialAttack)
			{
				this.specialAttack = true;
			}
			if (!this.specialAttack)
			{
				base.npc.aiStyle = 3;
			}
			if (this.specialAttack)
			{
				this.attackTimer++;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.attackTimer == 60)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item7, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2((float)(-6 + Main.rand.Next(-6, 0)), (float)(-4 + Main.rand.Next(-4, 0))), base.mod.ProjectileType("BarrelPro"), 40, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item7, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 44f), new Vector2((float)(6 + Main.rand.Next(0, 6)), (float)(-4 + Main.rand.Next(-4, 0))), base.mod.ProjectileType("BarrelPro"), 40, 3f, 255, 0f, 0f);
					}
				}
				if (this.attackTimer >= 120)
				{
					this.specialAttack = false;
					this.attackTimer = 0;
					this.attackCounter = 0;
					this.attackFrame = 0;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/FatPirateAttack");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.specialAttack)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.specialAttack)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 12;
				int num2 = num * this.attackFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		private bool specialAttack;

		private int attackFrame;

		private int attackCounter;

		private int attackTimer;
	}
}
