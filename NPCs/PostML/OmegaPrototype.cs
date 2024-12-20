using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Banners;
using Redemption.Projectiles.Hostile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.PostML
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
			this.bannerItem = ModContent.ItemType<OmegaPrototypeSilverBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/OPrototypeSilverGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/OPrototypeSilverGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/OPrototypeSilverGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/OPrototypeSilverGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/OPrototypeSilverGore4"), 1f);
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
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 300f && Main.rand.Next(125) == 0 && !this.clawAttack)
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
				if (this.attackTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.Red, "Slice!", true, true);
				}
				if (this.attackTimer == 20)
				{
					Main.PlaySound(SoundID.Item71, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed = 5f;
					Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage = 50;
					int type = ModContent.ProjectileType<OmegaWave3>();
					float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
					Main.projectile[num54].netUpdate = true;
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
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/PostML/OmegaPrototype_Glow");
			Texture2D attackAni = base.mod.GetTexture("NPCs/PostML/OmegaPrototypeAttack");
			Texture2D attackGlow = base.mod.GetTexture("NPCs/PostML/OmegaPrototypeAttack_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			if (!this.attackStart)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			}
			if (this.attackStart)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = attackAni.Height / 10;
				int y6 = num214 * this.attackFrame;
				Main.spriteBatch.Draw(attackAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, attackAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)attackAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(attackGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, attackAni.Width, num214)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)attackAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * ((!RedeWorld.girusCloaked && Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && RedeWorld.downedVlitch3) ? 0.01f : 0f);
		}

		private int attackFrame;

		private bool attackStart;

		private int attackCounter;

		private bool clawAttack;

		private int attackTimer;
	}
}
