using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.ChickenArmy;
using Redemption.Items;
using Redemption.Items.DruidDamageClass.v08;
using Redemption.Items.Weapons.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.ChickenInvasion
{
	public class ChickmanArchmage : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chickman Archmage");
			Main.npcFrameCount[base.npc.type] = 15;
		}

		public override void SetDefaults()
		{
			base.npc.width = 32;
			base.npc.height = 40;
			base.npc.friendly = false;
			base.npc.damage = 85;
			base.npc.defense = 15;
			base.npc.lifeMax = 8500;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 1000f;
			base.npc.knockBackResist = 0.2f;
			base.npc.aiStyle = 3;
			this.aiType = 73;
			this.animationType = 271;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore1"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				if (base.npc.FindBuffIndex(24) != -1)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<FriedChicken>(), Main.rand.Next(1, 2), false, 0, false, false);
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<Archcloth>(), Main.rand.Next(1, 3), false, 0, false, false);
			}
			if (Main.rand.Next(150) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<ArchmagesEnchantment>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(1200) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<ChickLauncher>(), 1, false, 0, false, false);
			}
			if (ChickWorld.chickArmy)
			{
				ChickWorld.ChickPoints++;
			}
		}

		public override void AI()
		{
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 700f && Main.rand.Next(150) == 0 && !this.specialAttack)
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
					CombatText.NewText(base.npc.getRect(), Color.LightPink, "Arcane Chickens!", true, true);
				}
				if (this.attackTimer == 21)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.5f), (int)base.npc.position.X, (int)base.npc.position.Y);
						int p = Projectile.NewProjectile(new Vector2(base.npc.position.X + 2f, base.npc.position.Y + 12f), new Vector2(-2f, 1f), ModContent.ProjectileType<ArcaneChickenPro1>(), 45, 3f, 255, 0f, 0f);
						Main.projectile[p].netUpdate = true;
					}
					else
					{
						Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.5f), (int)base.npc.position.X, (int)base.npc.position.Y);
						int p2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 2f, base.npc.position.Y + 12f), new Vector2(2f, 1f), ModContent.ProjectileType<ArcaneChickenPro1>(), 45, 3f, 255, 0f, 0f);
						Main.projectile[p2].netUpdate = true;
					}
				}
				if (this.attackTimer == 24)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.5f), (int)base.npc.position.X, (int)base.npc.position.Y);
						int p3 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 4f, base.npc.position.Y + 26f), new Vector2(-2f, 0f), ModContent.ProjectileType<ArcaneChickenPro1>(), 45, 3f, 255, 0f, 0f);
						Main.projectile[p3].netUpdate = true;
					}
					else
					{
						Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.5f), (int)base.npc.position.X, (int)base.npc.position.Y);
						int p4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 4f, base.npc.position.Y + 26f), new Vector2(2f, 0f), ModContent.ProjectileType<ArcaneChickenPro1>(), 45, 3f, 255, 0f, 0f);
						Main.projectile[p4].netUpdate = true;
					}
				}
				if (this.attackTimer == 27)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.5f), (int)base.npc.position.X, (int)base.npc.position.Y);
						int p5 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 0f, base.npc.position.Y + 44f), new Vector2(-2f, -1f), ModContent.ProjectileType<ArcaneChickenPro1>(), 45, 3f, 255, 0f, 0f);
						Main.projectile[p5].netUpdate = true;
					}
					else
					{
						Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.5f), (int)base.npc.position.X, (int)base.npc.position.Y);
						int p6 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 0f, base.npc.position.Y + 44f), new Vector2(2f, -1f), ModContent.ProjectileType<ArcaneChickenPro1>(), 45, 3f, 255, 0f, 0f);
						Main.projectile[p6].netUpdate = true;
					}
				}
				if (this.attackTimer >= 42)
				{
					this.specialAttack = false;
					this.attackTimer = 0;
					this.attackFrame = 0;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D attackAni = base.mod.GetTexture("NPCs/ChickenInvasion/ChickmanArchmageMagic");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.specialAttack)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.specialAttack)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = attackAni.Height / 1;
				int y6 = num214 * this.attackFrame;
				Main.spriteBatch.Draw(attackAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, attackAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)attackAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		private bool specialAttack;

		private int attackFrame;

		private int attackTimer;
	}
}
