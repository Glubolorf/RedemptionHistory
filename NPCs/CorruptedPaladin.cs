using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class CorruptedPaladin : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Paladin");
			Main.npcFrameCount[base.npc.type] = 9;
		}

		public override void SetDefaults()
		{
			base.npc.width = 60;
			base.npc.height = 66;
			base.npc.friendly = false;
			base.npc.damage = 150;
			base.npc.defense = 60;
			base.npc.lifeMax = 6500;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath6;
			base.npc.value = (float)Item.buyPrice(0, 5, 0, 0);
			base.npc.knockBackResist = 0.01f;
			base.npc.aiStyle = 3;
			this.aiType = 3;
			this.animationType = 290;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("CorruptedPaladinBanner");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedPaladinGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedPaladinGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedPaladinGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedPaladinGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedPaladinGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedPaladinGore6"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedPaladinGore7"), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 266, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 266, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			if (this.throwStart)
			{
				this.throwCounter++;
				if (this.throwCounter > 15)
				{
					this.throwFrame++;
					this.throwCounter = 0;
				}
				if (this.throwFrame >= 3)
				{
					this.throwFrame = 0;
				}
			}
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			float num = base.npc.Distance(Main.player[base.npc.target].Center);
			if (num <= 500f && Main.rand.Next(125) == 0 && !this.throwAttack)
			{
				this.throwAttack = true;
			}
			if (!this.throwAttack)
			{
				base.npc.aiStyle = 3;
				this.throwStart = false;
			}
			if (this.throwAttack)
			{
				this.throwTimer++;
				this.throwStart = true;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.throwTimer == 15)
				{
					Main.PlaySound(SoundID.Item71, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num2 = 7f;
					Vector2 vector;
					vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num3 = 50;
					int num4 = base.mod.ProjectileType("CorruptedPaladinHammerPro2");
					float num5 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
					int num6 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0), (float)(Math.Sin((double)num5) * (double)num2 * -1.0), num4, num3, 0f, 0, 0f, 0f);
					Main.projectile[num6].netUpdate = true;
				}
				if (this.throwTimer >= 30)
				{
					this.throwAttack = false;
					this.throwStart = false;
					this.throwTimer = 0;
					this.throwCounter = 0;
					this.throwFrame = 0;
				}
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * ((NPC.downedPlantBoss && RedeWorld.downedInfectedEye && Main.hardMode) ? 0.005f : 0f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/CorruptedPaladin_Glow");
			Texture2D texture2 = base.mod.GetTexture("NPCs/CorruptedPaladinThrow");
			Texture2D texture3 = base.mod.GetTexture("NPCs/CorruptedPaladinThrow_Glow");
			SpriteEffects spriteEffects = (base.npc.spriteDirection == -1) ? 0 : 1;
			if (!this.throwStart)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, spriteEffects, 0f);
			}
			if (this.throwStart)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture2.Height / 3;
				int num2 = num * this.throwFrame;
				Main.spriteBatch.Draw(texture2, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture2.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
				Main.spriteBatch.Draw(texture3, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture2.Width, num)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		private bool throwStart;

		private int throwFrame;

		private int throwCounter;

		private bool throwAttack;

		private int throwTimer;
	}
}
