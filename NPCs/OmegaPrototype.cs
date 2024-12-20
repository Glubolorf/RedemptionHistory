using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class OmegaPrototype : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Prototype Omega");
			Main.npcFrameCount[base.npc.type] = 20;
		}

		public override void SetDefaults()
		{
			base.npc.width = 40;
			base.npc.height = 58;
			base.npc.damage = 100;
			base.npc.friendly = false;
			base.npc.defense = 75;
			base.npc.lifeMax = 6000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = (float)Item.buyPrice(0, 2, 50, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 3;
			this.aiType = 482;
			this.animationType = 482;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("OmegaPrototypeSilverBanner");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/OPrototypeSilverGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/OPrototypeSilverGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/OPrototypeSilverGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/OPrototypeSilverGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/OPrototypeSilverGore4"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			if (this.attackStart)
			{
				this.attackCounter++;
				if (this.attackCounter > 5)
				{
					this.attackFrame++;
					this.attackCounter = 0;
				}
				if (this.attackFrame >= 10)
				{
					this.attackFrame = 0;
				}
			}
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			float num = base.npc.Distance(Main.player[base.npc.target].Center);
			if (num <= 300f && Main.rand.Next(125) == 0 && !this.clawAttack)
			{
				this.clawAttack = true;
			}
			if (!this.clawAttack)
			{
				base.npc.aiStyle = 3;
				this.attackStart = false;
			}
			if (this.clawAttack)
			{
				this.attackTimer++;
				this.attackStart = true;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.attackTimer == 1 && !Config.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.Red, "Slice!", true, true);
				}
				if (this.attackTimer == 20)
				{
					Main.PlaySound(SoundID.Item71, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num2 = 5f;
					Vector2 vector;
					vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num3 = 50;
					int num4 = base.mod.ProjectileType("OmegaWave3");
					float num5 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
					int num6 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0), (float)(Math.Sin((double)num5) * (double)num2 * -1.0), num4, num3, 0f, 0, 0f, 0f);
					Main.projectile[num6].netUpdate = true;
				}
				if (this.attackTimer >= 50)
				{
					this.clawAttack = false;
					this.attackStart = false;
					this.attackTimer = 0;
					this.attackCounter = 0;
					this.attackFrame = 0;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/OmegaPrototype_Glow");
			Texture2D texture2 = base.mod.GetTexture("NPCs/OmegaPrototypeAttack");
			Texture2D texture3 = base.mod.GetTexture("NPCs/OmegaPrototypeAttack_Glow");
			SpriteEffects spriteEffects = (base.npc.spriteDirection == -1) ? 0 : 1;
			if (!this.attackStart)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, spriteEffects, 0f);
			}
			if (this.attackStart)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture2.Height / 10;
				int num2 = num * this.attackFrame;
				Main.spriteBatch.Draw(texture2, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture2.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
				Main.spriteBatch.Draw(texture3, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture2.Width, num)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * ((Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && RedeWorld.downedVlitch3) ? 0.01f : 0f);
		}

		private int attackFrame;

		private bool attackStart;

		private int attackCounter;

		private bool clawAttack;

		private int attackTimer;
	}
}
