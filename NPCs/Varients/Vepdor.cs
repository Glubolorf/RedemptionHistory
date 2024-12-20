using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Varients
{
	public class Vepdor : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vepdor's Remains");
			Main.npcFrameCount[base.npc.type] = Main.npcFrameCount[271];
		}

		public override void SetDefaults()
		{
			base.npc.width = 44;
			base.npc.height = 60;
			base.npc.damage = 40;
			base.npc.defense = 2;
			base.npc.lifeMax = 255;
			base.npc.HitSound = SoundID.NPCHit2;
			base.npc.DeathSound = SoundID.NPCDeath2;
			base.npc.value = (float)Item.buyPrice(0, 1, 2, 3);
			base.npc.knockBackResist = 0.3f;
			base.npc.aiStyle = 3;
			this.aiType = 271;
			this.animationType = 271;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("SkeletonDuellerBanner");
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
				if (this.attackFrame >= 11)
				{
					this.attackFrame = 0;
				}
			}
			this.timer++;
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			if (this.timer >= 160)
			{
				base.npc.ai[0] = 650f;
				base.npc.TargetClosest(true);
				this.timer = 0;
			}
			if (base.npc.ai[2] != 0f && base.npc.ai[3] != 0f)
			{
				Main.PlaySound(SoundID.Item8, base.npc.position);
				for (int i = 0; i < 15; i++)
				{
					int num = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 27, 0f, 0f, 100, default(Color), 2.5f);
					Main.dust[num].velocity *= 3f;
					Main.dust[num].noGravity = true;
				}
				base.npc.position.X = base.npc.ai[2] * 16f - (float)(base.npc.width / 2) + 8f;
				base.npc.position.Y = base.npc.ai[3] * 16f - (float)base.npc.height;
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				base.npc.ai[2] = 0f;
				base.npc.ai[3] = 0f;
				Main.PlaySound(SoundID.Item8, base.npc.position);
				for (int j = 0; j < 15; j++)
				{
					int num2 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 27, 0f, 0f, 100, default(Color), 2.5f);
					Main.dust[num2].velocity *= 3f;
					Main.dust[num2].noGravity = true;
				}
			}
			if (Math.Abs(base.npc.position.X - Main.player[base.npc.target].position.X) + Math.Abs(base.npc.position.Y - Main.player[base.npc.target].position.Y) > 2000f)
			{
				base.npc.ai[0] = 650f;
			}
			if (base.npc.ai[0] >= 650f && Main.netMode != 1)
			{
				base.npc.ai[0] = 1f;
				int num3 = (int)Main.player[base.npc.target].position.X / 16;
				int num4 = (int)Main.player[base.npc.target].position.Y / 16;
				int num5 = (int)base.npc.position.X / 16;
				int num6 = (int)base.npc.position.Y / 16;
				int num7 = 40;
				int num8 = 0;
				for (int k = 0; k < 100; k++)
				{
					num8++;
					int num9 = Main.rand.Next(num3 - num7, num3 + num7);
					int num10 = Main.rand.Next(num4 - num7, num4 + num7);
					for (int l = num10; l < num4 + num7; l++)
					{
						if ((num9 < num3 - 12 || num9 > num3 + 12) && (l < num6 - 1 || l > num6 + 1 || num9 < num5 - 1 || num9 > num5 + 1) && Main.tile[num9, l].nactive())
						{
							bool flag = true;
							if (Main.tile[num9, l - 1].lava())
							{
								flag = false;
							}
							if (flag && Main.tileSolid[(int)Main.tile[num9, l].type] && !Collision.SolidTiles(num9 - 1, num9 + 1, l - 4, l - 1))
							{
								base.npc.ai[1] = 20f;
								base.npc.ai[2] = (float)num9;
								base.npc.ai[3] = (float)l - 1f;
								break;
							}
						}
					}
				}
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[1] > 0f)
			{
				base.npc.ai[1] -= 1f;
			}
			float num11 = base.npc.Distance(Main.player[base.npc.target].Center);
			if (num11 <= 75f && !Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).skeletonFriendly && Main.rand.Next(20) == 0 && !this.specialAttack)
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
					CombatText.NewText(base.npc.getRect(), Color.Purple, "Slice! Thrust!", true, true);
				}
				if (this.attackTimer == 6)
				{
					if (base.npc.direction == -1)
					{
						int num12 = Projectile.NewProjectile(base.npc.position.X + -14f, base.npc.position.Y + 4f, 0f, 0f, base.mod.ProjectileType("DamagePro2"), 20, 3f, 255, 0f, 0f);
						Main.projectile[num12].netUpdate = true;
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					else
					{
						int num13 = Projectile.NewProjectile(base.npc.position.X + 48f, base.npc.position.Y + 4f, 0f, 0f, base.mod.ProjectileType("DamagePro2"), 20, 3f, 255, 0f, 0f);
						Main.projectile[num13].netUpdate = true;
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
				}
				if (this.attackTimer == 24)
				{
					if (base.npc.direction == -1)
					{
						int num14 = Projectile.NewProjectile(base.npc.position.X + -14f, base.npc.position.Y + 28f, 0f, 0f, base.mod.ProjectileType("DamagePro1"), 20, 3f, 255, 0f, 0f);
						Main.projectile[num14].netUpdate = true;
						Main.PlaySound(SoundID.Item19, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					else
					{
						int num15 = Projectile.NewProjectile(base.npc.position.X + 48f, base.npc.position.Y + 28f, 0f, 0f, base.mod.ProjectileType("DamagePro1"), 20, 3f, 255, 0f, 0f);
						Main.projectile[num15].netUpdate = true;
						Main.PlaySound(SoundID.Item19, (int)base.npc.position.X, (int)base.npc.position.Y);
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

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					int num = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 27, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num].velocity *= 2.4f;
				}
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
			}
			Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 30, (int)base.npc.position.Y + 34, base.mod.NPCType("LostSoul2"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (Main.rand.Next(5) == 0)
			{
				base.npc.ai[0] = 650f;
				base.npc.TargetClosest(true);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Varients/VepdorSlash");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.specialAttack)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.specialAttack)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 11;
				int num2 = num * this.attackFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return !Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).skeletonFriendly;
		}

		private bool specialAttack;

		private int attackFrame;

		private int attackCounter;

		private int attackTimer;

		private int timer;
	}
}
