using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.OmegaOblit
{
	[AutoloadBossHead]
	public class OmegaOblitIdle : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Omega Obliterator");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			this.aiType = 0;
			base.npc.lifeMax = 95500;
			base.npc.damage = 280;
			base.npc.defense = 50;
			base.npc.knockBackResist = 0f;
			base.npc.width = 112;
			base.npc.height = 178;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.npcSlots = 2f;
			base.npc.boss = true;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.buffImmune[base.mod.BuffType("UltraFlameDebuff")] = true;
			base.npc.buffImmune[base.mod.BuffType("EnjoymentDebuff")] = true;
			base.npc.netAlways = true;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.HitSound = SoundID.NPCHit42;
			base.npc.DeathSound = SoundID.NPCDeath14;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossVlitch2");
			this.bossBag = base.mod.ItemType("OmegaOblitBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.5f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.2f);
			base.npc.defense = base.npc.defense + numPlayers;
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X + 36f, base.npc.position.Y + 54f), 4, 4, 235, 0f, 0f, 0, default(Color), 1f);
			}
			if (!this.charging)
			{
				this.Move(new Vector2(-340f, 0f));
			}
			if (!this.start)
			{
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 15.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 178;
					if (base.npc.frame.Y > 178)
					{
						base.npc.frameCounter = 0.0;
					}
				}
			}
			if (this.charging)
			{
				this.chargeCounter++;
				if (this.chargeCounter > 15)
				{
					this.chargeFrame++;
					this.chargeCounter = 0;
				}
				if (this.chargeFrame >= 2)
				{
					this.chargeFrame = 0;
				}
			}
			if (this.charging2)
			{
				this.charge2Counter++;
				if (this.charge2Counter > 15)
				{
					this.charge2Frame++;
					this.charge2Counter = 0;
				}
				if (this.charge2Frame >= 2)
				{
					this.charge2Frame = 0;
				}
			}
			if (this.idleStart)
			{
				this.idleCounter++;
				if (this.idleCounter > 15)
				{
					this.idleFrame++;
					this.idleCounter = 0;
				}
				if (this.idleFrame >= 2)
				{
					this.idleFrame = 0;
				}
			}
			if (this.shooting)
			{
				this.shootCounter++;
				if (this.shootCounter > 15)
				{
					this.shootFrame++;
					this.shootCounter = 0;
				}
				if (this.shootFrame >= 8)
				{
					this.shootFrame = 0;
				}
			}
			if (this.laserFiring1)
			{
				this.laser1Counter++;
				if (this.laser1Counter > 15)
				{
					this.laser1Frame++;
					this.laser1Counter = 0;
				}
				if (this.laser1Frame >= 6)
				{
					this.laser1Frame = 4;
				}
			}
			if (this.laserFiring2)
			{
				this.laser2Counter++;
				if (this.laser2Counter > 15)
				{
					this.laser2Frame++;
					this.laser2Counter = 0;
				}
				if (this.laser2Frame >= 2)
				{
					this.laser2Frame = 0;
				}
			}
			if (this.attackMode == 0)
			{
				this.aiCounter++;
				base.npc.dontTakeDamage = true;
				this.idleStart = true;
			}
			if (this.attackMode == 1)
			{
				if (Main.rand.Next(250) == 0)
				{
					for (int i = 0; i < 20; i++)
					{
						int num = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				this.chargeTimer++;
				if (base.npc.life <= 71625)
				{
					if (this.chargeTimer == 300)
					{
						this.idleStart = false;
						this.charging = true;
						Vector2 vector;
						vector..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num2 = (float)Math.Atan2((double)(vector.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num2) * 26.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num2) * 26.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
						Color color = default(Color);
						Rectangle rectangle;
						rectangle..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num3 = 30;
						for (int j = 1; j <= num3; j++)
						{
							int num4 = Dust.NewDust(base.npc.position, rectangle.Width, rectangle.Height, 235, 0f, 0f, 100, color, 2.5f);
							Main.dust[num4].noGravity = false;
						}
						return;
					}
					if (this.chargeTimer == 330)
					{
						this.charging = false;
						this.charging2 = true;
						Vector2 vector2;
						vector2..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num5 = (float)Math.Atan2((double)(vector2.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector2.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num5) * 26.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num5) * 26.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
						Color color2 = default(Color);
						Rectangle rectangle2;
						rectangle2..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num6 = 30;
						for (int k = 1; k <= num6; k++)
						{
							int num7 = Dust.NewDust(base.npc.position, rectangle2.Width, rectangle2.Height, 235, 0f, 0f, 100, color2, 2.5f);
							Main.dust[num7].noGravity = false;
						}
						return;
					}
				}
				if (base.npc.life > 71625)
				{
					if (this.chargeTimer == 300)
					{
						this.idleStart = false;
						this.charging = true;
						Vector2 vector3;
						vector3..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num8 = (float)Math.Atan2((double)(vector3.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector3.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num8) * 24.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num8) * 24.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
						Color color3 = default(Color);
						Rectangle rectangle3;
						rectangle3..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num9 = 30;
						for (int l = 1; l <= num9; l++)
						{
							int num10 = Dust.NewDust(base.npc.position, rectangle3.Width, rectangle3.Height, 235, 0f, 0f, 100, color3, 2.5f);
							Main.dust[num10].noGravity = false;
						}
						return;
					}
					if (this.chargeTimer == 330)
					{
						this.charging = false;
						this.charging2 = true;
						Vector2 vector4;
						vector4..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num11 = (float)Math.Atan2((double)(vector4.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector4.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num11) * 24.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num11) * 24.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
						Color color4 = default(Color);
						Rectangle rectangle4;
						rectangle4..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num12 = 30;
						for (int m = 1; m <= num12; m++)
						{
							int num13 = Dust.NewDust(base.npc.position, rectangle4.Width, rectangle4.Height, 235, 0f, 0f, 100, color4, 2.5f);
							Main.dust[num13].noGravity = false;
						}
						return;
					}
				}
				if (this.chargeTimer >= 360)
				{
					this.attackMode = 2;
					this.charging2 = false;
				}
				if (this.attackMode == 2)
				{
					if (Main.rand.Next(300) == 0)
					{
						for (int n = 0; n < 20; n++)
						{
							int num14 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[num14].velocity *= 1.9f;
						}
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 70, 3f, 255, 0f, 0f);
					}
					this.plasmaTimer++;
					if (base.npc.life > 71625)
					{
						if (this.plasmaTimer <= 89)
						{
							this.idleStart = true;
						}
						if (this.plasmaTimer >= 90 && this.plasmaTimer <= 209)
						{
							this.shooting = true;
							this.idleStart = false;
						}
						if (this.plasmaTimer == 145)
						{
							Main.PlaySound(SoundID.Item92, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(4f, 0f), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 150)
						{
							Main.PlaySound(SoundID.Item92, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(8f, 0f), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 155)
						{
							Main.PlaySound(SoundID.Item92, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(12f, 0f), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer >= 210)
						{
							this.shooting = false;
							this.idleStart = true;
							this.attackMode = 1;
						}
						if (this.plasmaTimer == 210)
						{
							this.chargeTimer = Main.rand.Next(-200, 100);
							this.plasmaTimer = 0;
							this.shootCounter = 0;
							this.shootFrame = 0;
						}
					}
					if (base.npc.life <= 71625 && base.npc.life > 55000)
					{
						if (this.plasmaTimer <= 69)
						{
							this.idleStart = true;
						}
						if (this.plasmaTimer >= 70 && this.plasmaTimer <= 189)
						{
							this.shooting = true;
							this.idleStart = false;
						}
						if (this.plasmaTimer == 125)
						{
							Main.PlaySound(SoundID.Item92, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(8f, 0f), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(6f, 3f), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(6f, -3f), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 122)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(8f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 126)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(10f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 130)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(12f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 134)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(14f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 138)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(16f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 135)
						{
							Main.PlaySound(SoundID.Item92, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(8f, 0f), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(6f, 3f), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(6f, -3f), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer >= 190)
						{
							this.shooting = false;
							this.idleStart = true;
							this.attackMode = 1;
						}
						if (this.plasmaTimer == 190)
						{
							this.chargeTimer = Main.rand.Next(-150, 150);
							this.plasmaTimer = 0;
							this.shootCounter = 0;
							this.shootFrame = 0;
						}
					}
					if (base.npc.life <= 55000)
					{
						if (this.plasmaTimer <= 69)
						{
							this.idleStart = true;
						}
						if (this.plasmaTimer >= 70 && this.plasmaTimer <= 189)
						{
							this.shooting = true;
							this.idleStart = false;
						}
						if (this.plasmaTimer == 125)
						{
							Main.PlaySound(SoundID.Item92, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(0f, 0f), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 122)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(4f, 4f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(6f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(4f, -4f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 124)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(7f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 126)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(6f, 4f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(8f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(6f, -4f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 128)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(9f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 130)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(8f, 4f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(10f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(8f, -4f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(0f, 0f), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 132)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(11f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 134)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(10f, 4f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(12f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(10f, -4f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 136)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(13f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 138)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(12f, 4f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(14f, 0f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(12f, -4f), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer == 135)
						{
							Main.PlaySound(SoundID.Item92, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(0f, 0f), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
						}
						if (this.plasmaTimer >= 190)
						{
							this.shooting = false;
							this.idleStart = true;
							this.attackMode = 1;
						}
						if (this.plasmaTimer == 190)
						{
							this.chargeTimer = Main.rand.Next(-50, 150);
							this.plasmaTimer = 0;
							this.shootCounter = 0;
							this.shootFrame = 0;
						}
					}
				}
			}
			if (base.npc.life <= 50000 && !this.missileDone1)
			{
				this.missileLaunch1 = true;
			}
			if (this.missileLaunch1)
			{
				this.shooting = false;
				this.charging = false;
				this.charging2 = false;
				this.idleStart = true;
				this.chargeTimer = 0;
				this.plasmaTimer = 0;
				this.shootCounter = 0;
				this.shootFrame = 0;
				this.missileTimer1++;
				if (this.missileTimer1 == 30)
				{
					string text = "MISSILE BARRAGE INITIATED...";
					Color rarityRed = Colors.RarityRed;
					byte r = rarityRed.R;
					Color rarityRed2 = Colors.RarityRed;
					byte g = rarityRed2.G;
					Color rarityRed3 = Colors.RarityRed;
					Main.NewText(text, r, g, rarityRed3.B, false);
					NPC.NewNPC((int)base.npc.position.X, (int)base.npc.position.Y, base.mod.NPCType("OmegaMK2Droid1"), 0, 0f, 0f, 0f, 0f, 255);
					NPC.NewNPC((int)base.npc.position.X, (int)base.npc.position.Y, base.mod.NPCType("OmegaMK2Droid2"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.missileTimer1 == 120)
				{
					string text2 = "LAUNCHING IN 5 SECONDS...";
					Color rarityRed4 = Colors.RarityRed;
					byte r2 = rarityRed4.R;
					Color rarityRed5 = Colors.RarityRed;
					byte g2 = rarityRed5.G;
					rarityRed5 = Colors.RarityRed;
					Main.NewText(text2, r2, g2, rarityRed5.B, false);
					Main.PlaySound(SoundID.Item47, (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.missileTimer1 == 180)
				{
					Main.PlaySound(SoundID.Item47, (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.missileTimer1 == 240)
				{
					Main.PlaySound(SoundID.Item47, (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.missileTimer1 == 300)
				{
					Main.PlaySound(SoundID.Item47, (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.missileTimer1 == 360)
				{
					string text3 = "LAUNCHING MISSILE BARRAGE...";
					Color rarityRed5 = Colors.RarityRed;
					byte r3 = rarityRed5.R;
					rarityRed5 = Colors.RarityRed;
					byte g3 = rarityRed5.G;
					rarityRed5 = Colors.RarityRed;
					Main.NewText(text3, r3, g3, rarityRed5.B, false);
					Main.PlaySound(SoundID.Item47, (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.missileTimer1 == 420)
				{
					for (int num15 = 0; num15 < 20; num15++)
					{
						int num16 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num16].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.missileTimer1 == 422)
				{
					for (int num17 = 0; num17 < 20; num17++)
					{
						int num18 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num18].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(5f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.missileTimer1 == 425)
				{
					for (int num19 = 0; num19 < 20; num19++)
					{
						int num20 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num20].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(-5f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.missileTimer1 == 426)
				{
					for (int num21 = 0; num21 < 20; num21++)
					{
						int num22 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num22].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(-2f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.missileTimer1 == 428)
				{
					for (int num23 = 0; num23 < 20; num23++)
					{
						int num24 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num24].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(7f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.missileTimer1 == 431)
				{
					for (int num25 = 0; num25 < 20; num25++)
					{
						int num26 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num26].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(1f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.missileTimer1 == 432)
				{
					for (int num27 = 0; num27 < 20; num27++)
					{
						int num28 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num28].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(-8f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.missileTimer1 == 435)
				{
					for (int num29 = 0; num29 < 20; num29++)
					{
						int num30 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num30].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(6f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.missileTimer1 == 436)
				{
					for (int num31 = 0; num31 < 20; num31++)
					{
						int num32 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num32].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(2f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.missileTimer1 == 440)
				{
					for (int num33 = 0; num33 < 20; num33++)
					{
						int num34 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num34].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.missileTimer1 == 500)
				{
					this.chargeTimer = Main.rand.Next(-50, 150);
					this.plasmaTimer = 0;
					this.shootCounter = 0;
					this.shootFrame = 0;
					this.missileDone1 = true;
					this.missileLaunch1 = false;
				}
			}
			if (base.npc.life <= 10000 && !this.laserDone1)
			{
				this.laser1 = true;
			}
			if (this.laser1)
			{
				this.shooting = false;
				this.charging = false;
				this.charging2 = false;
				this.chargeTimer = 0;
				this.plasmaTimer = 0;
				this.shootCounter = 0;
				this.shootFrame = 0;
				this.laserTimer1++;
				if (this.laserTimer1 < 60)
				{
					this.idleStart = true;
				}
				if (this.laserTimer1 == 10)
				{
					string text4 = "CHANNELLING OMEGA WAVE...";
					Color rarityRed5 = Colors.RarityRed;
					byte r4 = rarityRed5.R;
					rarityRed5 = Colors.RarityRed;
					byte g4 = rarityRed5.G;
					rarityRed5 = Colors.RarityRed;
					Main.NewText(text4, r4, g4, rarityRed5.B, false);
				}
				if (this.laserTimer1 == 60)
				{
					string text5 = "10 SECONDS REMAINING...";
					Color rarityRed5 = Colors.RarityRed;
					byte r5 = rarityRed5.R;
					rarityRed5 = Colors.RarityRed;
					byte g5 = rarityRed5.G;
					rarityRed5 = Colors.RarityRed;
					Main.NewText(text5, r5, g5, rarityRed5.B, false);
				}
				if (this.laserTimer1 == 120)
				{
					string text6 = "9 SECONDS REMAINING...";
					Color rarityRed5 = Colors.RarityRed;
					byte r6 = rarityRed5.R;
					rarityRed5 = Colors.RarityRed;
					byte g6 = rarityRed5.G;
					rarityRed5 = Colors.RarityRed;
					Main.NewText(text6, r6, g6, rarityRed5.B, false);
				}
				if (this.laserTimer1 == 180)
				{
					string text7 = "8 SECONDS REMAINING...";
					Color rarityRed5 = Colors.RarityRed;
					byte r7 = rarityRed5.R;
					rarityRed5 = Colors.RarityRed;
					byte g7 = rarityRed5.G;
					rarityRed5 = Colors.RarityRed;
					Main.NewText(text7, r7, g7, rarityRed5.B, false);
				}
				if (this.laserTimer1 == 240)
				{
					string text8 = "7 SECONDS REMAINING...";
					Color rarityRed5 = Colors.RarityRed;
					byte r8 = rarityRed5.R;
					rarityRed5 = Colors.RarityRed;
					byte g8 = rarityRed5.G;
					rarityRed5 = Colors.RarityRed;
					Main.NewText(text8, r8, g8, rarityRed5.B, false);
				}
				if (this.laserTimer1 == 300)
				{
					string text9 = "6 SECONDS REMAINING...";
					Color rarityRed5 = Colors.RarityRed;
					byte r9 = rarityRed5.R;
					rarityRed5 = Colors.RarityRed;
					byte g9 = rarityRed5.G;
					rarityRed5 = Colors.RarityRed;
					Main.NewText(text9, r9, g9, rarityRed5.B, false);
				}
				if (this.laserTimer1 == 360)
				{
					string text10 = "5 SECONDS REMAINING...";
					Color rarityRed5 = Colors.RarityRed;
					byte r10 = rarityRed5.R;
					rarityRed5 = Colors.RarityRed;
					byte g10 = rarityRed5.G;
					rarityRed5 = Colors.RarityRed;
					Main.NewText(text10, r10, g10, rarityRed5.B, false);
				}
				if (this.laserTimer1 == 420)
				{
					string text11 = "4 SECONDS REMAINING...";
					Color rarityRed5 = Colors.RarityRed;
					byte r11 = rarityRed5.R;
					rarityRed5 = Colors.RarityRed;
					byte g11 = rarityRed5.G;
					rarityRed5 = Colors.RarityRed;
					Main.NewText(text11, r11, g11, rarityRed5.B, false);
				}
				if (this.laserTimer1 == 480)
				{
					string text12 = "3 SECONDS REMAINING...";
					Color rarityRed5 = Colors.RarityRed;
					byte r12 = rarityRed5.R;
					rarityRed5 = Colors.RarityRed;
					byte g12 = rarityRed5.G;
					rarityRed5 = Colors.RarityRed;
					Main.NewText(text12, r12, g12, rarityRed5.B, false);
				}
				if (this.laserTimer1 == 540)
				{
					string text13 = "GOODBYE TARGET...";
					Color rarityRed5 = Colors.RarityRed;
					byte r13 = rarityRed5.R;
					rarityRed5 = Colors.RarityRed;
					byte g13 = rarityRed5.G;
					rarityRed5 = Colors.RarityRed;
					Main.NewText(text13, r13, g13, rarityRed5.B, false);
				}
				if (this.laserTimer1 >= 660 && this.laserTimer1 < 940)
				{
					this.idleStart = false;
					this.laserFiring1 = true;
				}
				if (this.laserTimer1 == 650 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
				}
				if (this.laserTimer1 >= 735 && this.laserTimer1 <= 925)
				{
					this.laserSpamTimer++;
					if (this.laserSpamTimer >= 2)
					{
						Main.PlaySound(SoundID.Item33, (int)base.npc.position.X, (int)base.npc.position.Y);
						Dust.NewDust(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), 4, 4, 235, 0f, 0f, 0, default(Color), 1f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(20f, 0f), base.mod.ProjectileType("OmegaWave"), 200, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(10f, 10f), base.mod.ProjectileType("OmegaWave"), 200, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(10f, -10f), base.mod.ProjectileType("OmegaWave"), 200, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(-10f, 10f), base.mod.ProjectileType("OmegaWave"), 200, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(-10f, -10f), base.mod.ProjectileType("OmegaWave"), 200, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(-20f, 0f), base.mod.ProjectileType("OmegaWave"), 200, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(0f, 20f), base.mod.ProjectileType("OmegaWave"), 200, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 30f, base.npc.position.Y + 62f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaWave"), 200, 3f, 255, 0f, 0f);
						this.laserSpamTimer = 0;
					}
				}
				if (this.laserTimer1 >= 940 && this.laserTimer1 < 970)
				{
					this.laserFiring1 = false;
					this.laserFiring2 = true;
				}
				if (this.laserTimer1 >= 970)
				{
					this.chargeTimer = Main.rand.Next(-50, 150);
					this.plasmaTimer = 0;
					this.shootCounter = 0;
					this.shootFrame = 0;
					this.laserFiring1 = false;
					this.laserFiring2 = false;
					this.idleStart = true;
					this.laserDone1 = true;
					this.laser1 = false;
					string text14 = "CHEATER DETECTED...";
					Color rarityRed5 = Colors.RarityRed;
					byte r14 = rarityRed5.R;
					rarityRed5 = Colors.RarityRed;
					byte g14 = rarityRed5.G;
					rarityRed5 = Colors.RarityRed;
					Main.NewText(text14, r14, g14, rarityRed5.B, false);
				}
			}
			if (this.aiCounter == 1)
			{
				for (int num35 = 0; num35 < 100; num35++)
				{
					int num36 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num36].velocity *= 1.9f;
				}
			}
			if (this.aiCounter == 60)
			{
				string text15 = "TARGET FOUND...";
				Color rarityRed5 = Colors.RarityRed;
				byte r15 = rarityRed5.R;
				rarityRed5 = Colors.RarityRed;
				byte g15 = rarityRed5.G;
				rarityRed5 = Colors.RarityRed;
				Main.NewText(text15, r15, g15, rarityRed5.B, false);
			}
			if (this.aiCounter == 120)
			{
				string text16 = "PREPARE FOR OBLITERATION...";
				Color rarityRed5 = Colors.RarityRed;
				byte r16 = rarityRed5.R;
				rarityRed5 = Colors.RarityRed;
				byte g16 = rarityRed5.G;
				rarityRed5 = Colors.RarityRed;
				Main.NewText(text16, r16, g16, rarityRed5.B, false);
			}
			if (this.aiCounter == 260)
			{
				this.attackMode = 1;
				base.npc.dontTakeDamage = false;
			}
		}

		public override bool CheckDead()
		{
			base.npc.SetDefaults(base.mod.NPCType("OmegaOblitDamaged"), -1f);
			return false;
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void Move(Vector2 offset)
		{
			this.speed = 20f;
			Vector2 vector = this.player.Center + offset;
			Vector2 vector2 = vector - base.npc.Center;
			float num = this.Magnitude(vector2);
			if (num > this.speed)
			{
				vector2 *= this.speed / num;
			}
			float num2 = 10f;
			vector2 = (base.npc.velocity * num2 + vector2) / (num2 + 1f);
			num = this.Magnitude(vector2);
			if (num > this.speed)
			{
				vector2 *= this.speed / num;
			}
			base.npc.velocity = vector2;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/OmegaOblit/OmegaOblitIdle2");
			Texture2D texture2 = base.mod.GetTexture("NPCs/Bosses/OmegaOblit/OmegaOblitCharge");
			Texture2D texture3 = base.mod.GetTexture("NPCs/Bosses/OmegaOblit/OmegaOblitCharge2");
			Texture2D texture4 = base.mod.GetTexture("NPCs/Bosses/OmegaOblit/OmegaOblitShoot");
			Texture2D texture5 = base.mod.GetTexture("NPCs/Bosses/OmegaOblit/OmegaOblitLaser1");
			Texture2D texture6 = base.mod.GetTexture("NPCs/Bosses/OmegaOblit/OmegaOblitLaser2");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.charging || !this.charging2 || !this.shooting || !this.idleStart || !this.laserFiring1 || !this.laserFiring2)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.idleStart)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 2;
				int num2 = num * this.idleFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.charging)
			{
				Vector2 vector2;
				vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num3 = texture2.Height / 2;
				int num4 = num3 * this.chargeFrame;
				Main.spriteBatch.Draw(texture2, vector2 - Main.screenPosition, new Rectangle?(new Rectangle(0, num4, texture2.Width, num3)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num3 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.charging2)
			{
				Vector2 vector3;
				vector3..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num5 = texture3.Height / 2;
				int num6 = num5 * this.charge2Frame;
				Main.spriteBatch.Draw(texture3, vector3 - Main.screenPosition, new Rectangle?(new Rectangle(0, num6, texture3.Width, num5)), drawColor, base.npc.rotation, new Vector2((float)texture3.Width / 2f, (float)num5 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.shooting)
			{
				Vector2 vector4;
				vector4..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num7 = texture4.Height / 8;
				int num8 = num7 * this.shootFrame;
				Main.spriteBatch.Draw(texture4, vector4 - Main.screenPosition, new Rectangle?(new Rectangle(0, num8, texture4.Width, num7)), drawColor, base.npc.rotation, new Vector2((float)texture4.Width / 2f, (float)num7 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.laserFiring1)
			{
				Vector2 vector5;
				vector5..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num9 = texture5.Height / 6;
				int num10 = num9 * this.laser1Frame;
				Main.spriteBatch.Draw(texture5, vector5 - Main.screenPosition, new Rectangle?(new Rectangle(0, num10, texture5.Width, num9)), drawColor, base.npc.rotation, new Vector2((float)texture5.Width / 2f, (float)num9 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.laserFiring2)
			{
				Vector2 vector6;
				vector6..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num11 = texture6.Height / 2;
				int num12 = num11 * this.laser2Frame;
				Main.spriteBatch.Draw(texture6, vector6 - Main.screenPosition, new Rectangle?(new Rectangle(0, num12, texture6.Width, num11)), drawColor, base.npc.rotation, new Vector2((float)texture6.Width / 2f, (float)num11 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			return false;
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					this.deadTimer++;
					if (this.deadTimer == 2)
					{
						string text = "TARGET OBLITERATED... RETURNING TO GIRUS...";
						Color rarityRed = Colors.RarityRed;
						byte r = rarityRed.R;
						Color rarityRed2 = Colors.RarityRed;
						byte g = rarityRed2.G;
						Color rarityRed3 = Colors.RarityRed;
						Main.NewText(text, r, g, rarityRed3.B, false);
					}
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
				}
			}
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}

		private Player player;

		private float speed;

		public int timer;

		public bool DirRight;

		public bool DirLeft;

		private bool start;

		private bool charging;

		private int chargeFrame;

		private bool shooting;

		private int shootFrame;

		private int chargeCounter;

		private int shootCounter;

		private int aiCounter;

		private int attackMode;

		private bool idleStart;

		private int idleFrame;

		private int idleCounter;

		private int chargeTimer;

		private int chargeTimer2;

		private bool charging2;

		private int charge2Frame;

		private int charge2Counter;

		private int deadTimer;

		private int plasmaTimer;

		private bool phase2;

		private int plasmaTimer2;

		private int attackMode2;

		private int phaseTimer1;

		private bool missileLaunch1;

		private int missileTimer1;

		private bool missileDone1;

		private bool laser1;

		private bool laserDone1;

		private int laserTimer1;

		private bool laserFiring1;

		private int laserFrame;

		private int laser1Frame;

		private bool laserFiring2;

		private int laser2Frame;

		private int laser1Counter;

		private int laser2Counter;

		private int laserSpamTimer;
	}
}
