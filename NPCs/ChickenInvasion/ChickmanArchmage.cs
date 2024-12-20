using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.ChickenArmy;
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
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("FriedChicken"), Main.rand.Next(1, 2), false, 0, false, false);
				}
				if (ChickWorld.chickArmy)
				{
					ChickWorld.ChickPoints2++;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("Archcloth"), Main.rand.Next(1, 3), false, 0, false, false);
			}
			if (Main.rand.Next(150) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ArchmagesEnchantment"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(1200) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ChickLauncher"), 1, false, 0, false, false);
			}
		}

		public override void AI()
		{
			float num = base.npc.Distance(Main.player[base.npc.target].Center);
			if (num <= 700f && Main.rand.Next(150) == 0 && !this.specialAttack)
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
				if (this.attackTimer == 1 && !Config.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.LightPink, "Arcane Chickens!", true, true);
				}
				if (this.attackTimer == 21)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.5f), (int)base.npc.position.X, (int)base.npc.position.Y);
						int num2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 2f, base.npc.position.Y + 12f), new Vector2(-2f, 1f), base.mod.ProjectileType("ArcaneChickenPro1"), 45, 3f, 255, 0f, 0f);
						Main.projectile[num2].netUpdate = true;
					}
					else
					{
						Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.5f), (int)base.npc.position.X, (int)base.npc.position.Y);
						int num3 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 2f, base.npc.position.Y + 12f), new Vector2(2f, 1f), base.mod.ProjectileType("ArcaneChickenPro1"), 45, 3f, 255, 0f, 0f);
						Main.projectile[num3].netUpdate = true;
					}
				}
				if (this.attackTimer == 24)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.5f), (int)base.npc.position.X, (int)base.npc.position.Y);
						int num4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 4f, base.npc.position.Y + 26f), new Vector2(-2f, 0f), base.mod.ProjectileType("ArcaneChickenPro1"), 45, 3f, 255, 0f, 0f);
						Main.projectile[num4].netUpdate = true;
					}
					else
					{
						Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.5f), (int)base.npc.position.X, (int)base.npc.position.Y);
						int num5 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 4f, base.npc.position.Y + 26f), new Vector2(2f, 0f), base.mod.ProjectileType("ArcaneChickenPro1"), 45, 3f, 255, 0f, 0f);
						Main.projectile[num5].netUpdate = true;
					}
				}
				if (this.attackTimer == 27)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.5f), (int)base.npc.position.X, (int)base.npc.position.Y);
						int num6 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 0f, base.npc.position.Y + 44f), new Vector2(-2f, -1f), base.mod.ProjectileType("ArcaneChickenPro1"), 45, 3f, 255, 0f, 0f);
						Main.projectile[num6].netUpdate = true;
					}
					else
					{
						Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.5f), (int)base.npc.position.X, (int)base.npc.position.Y);
						int num7 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 0f, base.npc.position.Y + 44f), new Vector2(2f, -1f), base.mod.ProjectileType("ArcaneChickenPro1"), 45, 3f, 255, 0f, 0f);
						Main.projectile[num7].netUpdate = true;
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
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/ChickenInvasion/ChickmanArchmageMagic");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.specialAttack)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.specialAttack)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 1;
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
