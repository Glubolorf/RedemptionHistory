using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class CorpseWalkerPriest : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corpse-Walker Priest");
			Main.npcFrameCount[base.npc.type] = 15;
		}

		public override void SetDefaults()
		{
			base.npc.width = 50;
			base.npc.height = 52;
			base.npc.friendly = false;
			base.npc.damage = 17;
			base.npc.defense = 2;
			base.npc.lifeMax = 45;
			base.npc.HitSound = SoundID.NPCHit2;
			base.npc.DeathSound = SoundID.NPCDeath2;
			base.npc.value = 28f;
			base.npc.knockBackResist = 0.3f;
			base.npc.aiStyle = 3;
			this.aiType = 271;
			this.animationType = 271;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("CorpseWalkerPriestBanner");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorpseWGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorpseWGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorpseWGore3"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (Main.netMode != 1 && base.npc.life <= 0 && Main.rand.Next(2) == 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 20, (int)base.npc.position.Y + 30, base.mod.NPCType("LostSoul1"), 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		public override void AI()
		{
			if (this.specialAttack)
			{
				this.attackCounter++;
				if (this.attackCounter > 3)
				{
					this.attackFrame++;
					this.attackCounter = 0;
				}
				if (this.attackFrame >= 14)
				{
					this.attackFrame = 0;
				}
			}
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 300f && !Main.LocalPlayer.GetModPlayer<RedePlayer>().skeletonFriendly && Main.rand.Next(150) == 0 && !this.specialAttack)
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
				if (this.attackTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.Gold, "Redemptive Sparks!", true, true);
				}
				if (this.attackTimer == 21)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p = Projectile.NewProjectile(new Vector2(base.npc.position.X + 2f, base.npc.position.Y + 12f), new Vector2(-6f, 1f), base.mod.ProjectileType("RedeSparkPro2"), 10, 3f, 255, 0f, 0f);
						Main.projectile[p].netUpdate = true;
					}
					else
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 2f, base.npc.position.Y + 12f), new Vector2(6f, 1f), base.mod.ProjectileType("RedeSparkPro2"), 10, 3f, 255, 0f, 0f);
						Main.projectile[p2].netUpdate = true;
					}
				}
				if (this.attackTimer == 24)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p3 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 4f, base.npc.position.Y + 26f), new Vector2(-6f, 0f), base.mod.ProjectileType("RedeSparkPro2"), 10, 3f, 255, 0f, 0f);
						Main.projectile[p3].netUpdate = true;
					}
					else
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 4f, base.npc.position.Y + 26f), new Vector2(6f, 0f), base.mod.ProjectileType("RedeSparkPro2"), 10, 3f, 255, 0f, 0f);
						Main.projectile[p4].netUpdate = true;
					}
				}
				if (this.attackTimer == 27)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p5 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 0f, base.npc.position.Y + 44f), new Vector2(-6f, -1f), base.mod.ProjectileType("RedeSparkPro2"), 10, 3f, 255, 0f, 0f);
						Main.projectile[p5].netUpdate = true;
					}
					else
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p6 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 0f, base.npc.position.Y + 44f), new Vector2(6f, -1f), base.mod.ProjectileType("RedeSparkPro2"), 10, 3f, 255, 0f, 0f);
						Main.projectile[p6].netUpdate = true;
					}
				}
				if (this.attackTimer >= 42)
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
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D attackAni = base.mod.GetTexture("NPCs/CorpseWalkerPriestAttack");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.specialAttack)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.specialAttack)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = attackAni.Height / 14;
				int y6 = num214 * this.attackFrame;
				Main.spriteBatch.Draw(attackAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, attackAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)attackAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return !Main.LocalPlayer.GetModPlayer<RedePlayer>().skeletonFriendly;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.DesertCave.Chance * 0.015f;
		}

		private bool specialAttack;

		private int attackFrame;

		private int attackCounter;

		private int attackTimer;
	}
}
