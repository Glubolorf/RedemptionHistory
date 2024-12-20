using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class GraniteCluster : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Granite Cluster");
			Main.npcFrameCount[base.npc.type] = 5;
		}

		public override void SetDefaults()
		{
			base.npc.width = 80;
			base.npc.height = 48;
			base.npc.friendly = false;
			base.npc.damage = 30;
			base.npc.defense = 10;
			base.npc.lifeMax = 165;
			base.npc.HitSound = SoundID.NPCHit53;
			base.npc.DeathSound = SoundID.NPCDeath56;
			base.npc.value = 200f;
			base.npc.knockBackResist = 0.01f;
			base.npc.aiStyle = 2;
			this.aiType = 34;
			this.animationType = 34;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 240, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 240, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 240, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 240, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 240, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 240, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 240, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 240, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 261, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 261, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 261, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 261, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 261, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 261, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 240, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 240, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 261, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("LostSoul2"), 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		public override void NPCLoot()
		{
			Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 28f), new Vector2(0f, -6f), 435, 15, 3f, 255, 0f, 0f);
			Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 28f), new Vector2(0f, 6f), 435, 15, 3f, 255, 0f, 0f);
			Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 28f), new Vector2(-6f, 0f), 435, 15, 3f, 255, 0f, 0f);
			Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 28f), new Vector2(6f, 0f), 435, 15, 3f, 255, 0f, 0f);
			Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 28f), new Vector2(4f, 4f), 435, 15, 3f, 255, 0f, 0f);
			Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 28f), new Vector2(4f, -4f), 435, 15, 3f, 255, 0f, 0f);
			Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 28f), new Vector2(-4f, 4f), 435, 15, 3f, 255, 0f, 0f);
			Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 28f), new Vector2(-4f, -4f), 435, 15, 3f, 255, 0f, 0f);
		}

		public override void AI()
		{
			if (this.shieldUp)
			{
				this.shieldCounter++;
				if (this.shieldCounter > 3)
				{
					this.shieldFrame++;
					this.shieldCounter = 0;
				}
				if (this.shieldFrame >= 7)
				{
					this.shieldFrame = 0;
				}
			}
			base.npc.rotation += 0.04f;
			this.shieldTimer++;
			if (this.shieldTimer == 160)
			{
				this.shieldUp = true;
			}
			if (this.shieldTimer >= 280)
			{
				this.shieldUp = false;
				this.shieldTimer = 0;
			}
			if (this.shieldUp)
			{
				base.npc.dontTakeDamage = true;
			}
			if (!this.shieldUp)
			{
				base.npc.dontTakeDamage = false;
			}
			if (Main.rand.Next(50) == 0)
			{
				Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 357, 15, 3f, 255, 0f, 0f);
			}
			if (Main.rand.Next(50) == 0)
			{
				Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 435, 15, 3f, 255, 0f, 0f);
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 368 && RedeWorld.downedTheKeeper) ? 0.2f : 0f);
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(1) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(144, 60, true);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/GraniteClusterShield");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.shieldUp)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.shieldUp)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 7;
				int num2 = num * this.shieldFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		private bool shieldUp;

		private int shieldFrame;

		private int shieldCounter;

		private int shieldTimer;
	}
}
