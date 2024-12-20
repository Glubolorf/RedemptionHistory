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
			Player player = Main.player[base.npc.target];
			player.GetModPlayer<RedePlayer>();
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
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 500f && Main.rand.Next(125) == 0 && !this.throwAttack)
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
					float Speed = 7f;
					Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage = 50;
					int type = base.mod.ProjectileType("CorruptedPaladinHammerPro2");
					float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
					Main.projectile[num54].netUpdate = true;
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
			return SpawnCondition.OverworldNightMonster.Chance * ((!RedeWorld.girusCloaked && NPC.downedPlantBoss && RedeWorld.downedInfectedEye && Main.hardMode) ? 0.005f : 0f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/CorruptedPaladin_Glow");
			Texture2D throwAni = base.mod.GetTexture("NPCs/CorruptedPaladinThrow");
			Texture2D throwGlow = base.mod.GetTexture("NPCs/CorruptedPaladinThrow_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			if (!this.throwStart)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			}
			if (this.throwStart)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = throwAni.Height / 3;
				int y6 = num214 * this.throwFrame;
				Main.spriteBatch.Draw(throwAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, throwAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)throwAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(throwGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, throwAni.Width, num214)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)throwAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
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
