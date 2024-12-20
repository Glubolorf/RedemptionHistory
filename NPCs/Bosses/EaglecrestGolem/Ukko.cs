using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.NPCBuffs;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Placeable.Trophies;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.PostML.Druid.Staves;
using Redemption.Items.Weapons.PostML.Magic;
using Redemption.Items.Weapons.PostML.Summon;
using Redemption.NPCs.Bosses.Thorn;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	[AutoloadBossHead]
	public class Ukko : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ukko");
			Main.npcFrameCount[base.npc.type] = 6;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 320000;
			base.npc.damage = 130;
			base.npc.defense = 35;
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 8, 0, 0);
			base.npc.aiStyle = -1;
			base.npc.width = 88;
			base.npc.height = 108;
			base.npc.HitSound = SoundID.NPCHit41;
			base.npc.DeathSound = SoundID.NPCDeath43;
			base.npc.noTileCollide = true;
			base.npc.noGravity = true;
			base.npc.lavaImmune = true;
			base.npc.boss = true;
			this.bossBag = ModContent.ItemType<UkkoBag>();
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 3544;
			RedeWorld.downedEaglecrestGolemPZ = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			damage *= 0.75;
			return true;
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<UkonKirvesTrophy>(), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<UkkoMask>(), 1, false, 0, false, false);
			}
			int num = Main.rand.Next(3);
			if (num == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<StonePuppet>(), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<EaglecrestGlove>(), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<AncientPowerStave>(), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<ViisaanKantele>(), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<UkonRuno>(), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<AncientPowerCore>(), Main.rand.Next(9, 18), false, 0, false, false);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void AI()
		{
			RedeUkkoAkka.begin = false;
			this.Rain();
			this.Target();
			this.DespawnHandler();
			Player player = Main.player[base.npc.target];
			for (int i = this.oldPos.Length - 1; i > 0; i--)
			{
				this.oldPos[i] = this.oldPos[i - 1];
				this.oldrot[i] = this.oldrot[i - 1];
			}
			this.oldPos[0] = base.npc.Center;
			this.oldrot[0] = base.npc.rotation;
			if (base.npc.ai[1] == 7f)
			{
				base.npc.rotation = Utils.ToRotation(base.npc.velocity);
				if (base.npc.velocity.X < 0f)
				{
					base.npc.spriteDirection = -1;
				}
				else
				{
					base.npc.spriteDirection = 1;
				}
			}
			else
			{
				base.npc.rotation = 0f;
				if (player.Center.X > base.npc.Center.X)
				{
					base.npc.spriteDirection = 1;
				}
				else
				{
					base.npc.spriteDirection = -1;
				}
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 6.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 112;
				if (base.npc.frame.Y > 560)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (base.npc.ai[3] == 1f)
			{
				this.frameCounters++;
				if (this.frameCounters > 6)
				{
					this.wandFrame++;
					this.frameCounters = 0;
				}
				if (this.wandFrame >= 9)
				{
					base.npc.ai[3] = 0f;
					this.wandFrame = 0;
					this.frameCounters = 0;
				}
			}
			if (base.npc.ai[3] == 2f)
			{
				this.frameCounters++;
				if (this.frameCounters > 3)
				{
					this.chariotFrame++;
					this.frameCounters = 0;
				}
				if (this.chariotFrame >= 9)
				{
					this.chariotFrame = 0;
				}
			}
			if (base.npc.ai[3] == 3f)
			{
				this.frameCounters++;
				if (this.frameCounters > 5)
				{
					this.dischargeFrame++;
					this.frameCounters = 0;
				}
				if (this.dischargeFrame >= 12)
				{
					this.frameCounters = 0;
					base.npc.ai[3] = 0f;
					this.dischargeFrame = 0;
				}
			}
			Vector2 ThunderwavePos = new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 700f) : (player.Center.X + 700f), player.Center.Y);
			new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 600f) : (player.Center.X + 600f), player.Center.Y + 400f);
			Vector2 EarthProtectPos = new Vector2(player.Center.X, player.Center.Y - 400f);
			if (base.npc.ai[0] == 0f)
			{
				if (this.mendingCooldown > 0)
				{
					this.mendingCooldown--;
				}
				if (this.stoneskinCooldown > 0 && !base.npc.HasBuff(ModContent.BuffType<StoneskinBuff>()))
				{
					this.stoneskinCooldown--;
				}
				if (this.chariotCooldown > 0)
				{
					this.chariotCooldown--;
				}
				if (this.burstCooldown > 0)
				{
					this.burstCooldown--;
				}
				if (this.teamCooldown > 0)
				{
					this.teamCooldown--;
				}
				RedeUkkoAkka.TAbubbles = false;
				RedeUkkoAkka.TAearthProtection = false;
				this.MoveVector2 = this.Pos();
				this.MoveVector3 = this.ChargePos();
				base.npc.ai[0] += 1f;
				return;
			}
			if (base.npc.ai[0] != 1f)
			{
				if (base.npc.ai[0] == 2f)
				{
					switch ((int)base.npc.ai[1])
					{
					case 0:
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 8f)
						{
							base.npc.ai[3] = 1f;
							int p = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<UkkoStrike>(), 36, 3f, Main.myPlayer, 0f, 0f);
							Main.projectile[p].netUpdate = true;
						}
						if (base.npc.ai[2] >= 60f)
						{
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 1:
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 2f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/WindLong1").WithVolume(0.5f).WithPitchVariance(0.1f), -1, -1);
						}
						if (base.npc.ai[2] % 2f == 0f && base.npc.ai[2] > 8f && base.npc.ai[2] < 48f)
						{
							base.npc.ai[3] = 1f;
							for (int j = 0; j < 2; j++)
							{
								int p2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, Utils.NextFloat(Main.rand, -8f, 8f), Utils.NextFloat(Main.rand, -8f, 8f), ModContent.ProjectileType<UkkoGust>(), 0, 0f, 255, 0f, 0f);
								Main.projectile[p2].netUpdate = true;
							}
						}
						if (base.npc.ai[2] >= 50f)
						{
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 2:
						if (base.npc.ai[2] == 0f)
						{
							if (Vector2.Distance(base.npc.Center, ThunderwavePos) < 100f)
							{
								base.npc.velocity = Utils.RotatedBy(base.npc.DirectionTo(player.Center), 1.5707963267948966, default(Vector2)) * 40f;
								base.npc.ai[2] = 1f;
								base.npc.netUpdate = true;
								return;
							}
							base.npc.MoveToVector2(ThunderwavePos, 30f);
							return;
						}
						else
						{
							base.npc.ai[2] += 1f;
							if (Main.raining ? (base.npc.ai[2] < 30f) : (base.npc.ai[2] < 20f))
							{
								base.npc.velocity -= Utils.RotatedBy(base.npc.velocity, 1.5707963267948966, default(Vector2)) * base.npc.velocity.Length() / base.npc.Distance(player.Center);
							}
							else
							{
								base.npc.velocity *= 0f;
							}
							if (Main.raining ? (base.npc.ai[2] % 3f == 0f && base.npc.ai[2] < 30f) : (base.npc.ai[2] % 5f == 0f && base.npc.ai[2] < 20f))
							{
								if (!Main.dedServ)
								{
									Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap2").WithVolume(0.5f).WithPitchVariance(0.1f), -1, -1);
								}
								float Speed = 18f;
								Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
								float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
								int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-1, 1), ModContent.ProjectileType<UkkoThunderwave>(), 30, 0f, 0, (float)base.npc.whoAmI, 0f);
								Main.projectile[num54].netUpdate = true;
							}
							if (Main.raining ? (base.npc.ai[2] >= 35f) : (base.npc.ai[2] >= 25f))
							{
								base.npc.ai[0] = 0f;
								base.npc.ai[2] = 0f;
								base.npc.netUpdate = true;
								return;
							}
						}
						break;
					case 3:
						if (base.npc.life >= (int)((float)base.npc.lifeMax * 0.6f))
						{
							base.npc.ai[1] = (float)Main.rand.Next(17);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 2f)
						{
							base.npc.ai[3] = 1f;
							if (!Main.dedServ)
							{
								Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Thunder1").WithVolume(1.2f).WithPitchVariance(0.1f), -1, -1);
							}
						}
						if (base.npc.ai[2] > 40f && base.npc.ai[2] < 160f)
						{
							if (Vector2.Distance(base.npc.Center, this.MoveVector2) < 10f)
							{
								this.MoveVector2 = this.Pos();
								base.npc.velocity *= 0f;
								base.npc.netUpdate = true;
							}
							else
							{
								base.npc.MoveToVector2(this.MoveVector2, 30f);
							}
						}
						if ((Main.raining ? (base.npc.ai[2] % 15f == 0f) : (base.npc.ai[2] % 20f == 0f)) && base.npc.ai[2] > 40f && base.npc.ai[2] < 160f)
						{
							int p3 = Projectile.NewProjectile(player.Center.X + (float)Main.rand.Next(-300, 300), player.Center.Y + (float)Main.rand.Next(-300, 300), 0f, 0f, ModContent.ProjectileType<UkkoStrike>(), 36, 3f, Main.myPlayer, 0f, 0f);
							Main.projectile[p3].netUpdate = true;
						}
						if (base.npc.ai[2] >= 180f)
						{
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 4:
						if (base.npc.life >= (int)((float)base.npc.lifeMax * 0.5f) || this.mendingCooldown != 0)
						{
							base.npc.ai[1] = (float)Main.rand.Next(17);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 2f)
						{
							base.npc.ai[3] = 1f;
							if (!Main.dedServ)
							{
								Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Quake1").WithVolume(0.9f).WithPitchVariance(0.1f), -1, -1);
							}
						}
						if (base.npc.ai[2] < 60f)
						{
							for (int k = 0; k < 2; k++)
							{
								Dust dust = Dust.NewDustDirect(base.npc.position, base.npc.width, base.npc.height, 269, 0f, 0f, 100, default(Color), 3f);
								dust.velocity = -base.npc.DirectionTo(dust.position);
							}
							base.npc.life += 200;
							base.npc.HealEffect(200, true);
						}
						if (base.npc.ai[2] >= 120f)
						{
							this.mendingCooldown = 20;
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 5:
						if (base.npc.life >= (int)((float)base.npc.lifeMax * 0.8f) || this.stoneskinCooldown != 0)
						{
							base.npc.ai[1] = (float)Main.rand.Next(17);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 2f)
						{
							base.npc.ai[3] = 1f;
							if (!Main.dedServ)
							{
								Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Quake1").WithVolume(0.9f).WithPitchVariance(0.1f), -1, -1);
							}
						}
						if (base.npc.ai[2] < 60f)
						{
							Dust dust2 = Dust.NewDustDirect(base.npc.position, base.npc.width, base.npc.height, 269, 0f, 0f, 100, default(Color), 2f);
							dust2.velocity = -base.npc.DirectionTo(dust2.position);
							base.npc.AddBuff(ModContent.BuffType<StoneskinBuff>(), 3600, false);
						}
						if (base.npc.ai[2] >= 90f)
						{
							this.stoneskinCooldown = 10;
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 6:
						if (!player.ZoneHoly)
						{
							base.npc.ai[1] = (float)Main.rand.Next(17);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 1f)
						{
							base.npc.ai[3] = 1f;
						}
						if (base.npc.ai[2] == 30f)
						{
							for (int l = 0; l < 10; l++)
							{
								int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 242, 0f, 0f, 100, default(Color), 3f);
								Main.dust[dustIndex].velocity *= 4.2f;
							}
							for (int m = 0; m < 10; m++)
							{
								int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 228, 0f, 0f, 100, default(Color), 3f);
								Main.dust[dustIndex2].velocity *= 4.2f;
							}
						}
						if (base.npc.ai[2] > 80f && base.npc.ai[2] < 160f)
						{
							if (Main.rand.Next(8) == 0)
							{
								int p4 = Projectile.NewProjectile(player.Center.X + (float)Main.rand.Next(-600, -300), player.Center.Y + (float)Main.rand.Next(-600, 600), Utils.NextFloat(Main.rand, -1f, 1f), Utils.NextFloat(Main.rand, -1f, 1f), ModContent.ProjectileType<UkkoDancingLights>(), 0, 0f, Main.myPlayer, 0f, 0f);
								Main.projectile[p4].netUpdate = true;
							}
							if (Main.rand.Next(8) == 0)
							{
								int p5 = Projectile.NewProjectile(player.Center.X + (float)Main.rand.Next(300, 600), player.Center.Y + (float)Main.rand.Next(-600, 600), Utils.NextFloat(Main.rand, -1f, 1f), Utils.NextFloat(Main.rand, -1f, 1f), ModContent.ProjectileType<UkkoDancingLights>(), 0, 0f, Main.myPlayer, 0f, 0f);
								Main.projectile[p5].netUpdate = true;
							}
							if (Main.rand.Next(8) == 0)
							{
								int p6 = Projectile.NewProjectile(player.Center.X + (float)Main.rand.Next(-600, 600), player.Center.Y + (float)Main.rand.Next(-600, -300), Utils.NextFloat(Main.rand, -1f, 1f), Utils.NextFloat(Main.rand, -1f, 1f), ModContent.ProjectileType<UkkoDancingLights>(), 0, 0f, Main.myPlayer, 0f, 0f);
								Main.projectile[p6].netUpdate = true;
							}
							if (Main.rand.Next(8) == 0)
							{
								int p7 = Projectile.NewProjectile(player.Center.X + (float)Main.rand.Next(-600, 600), player.Center.Y + (float)Main.rand.Next(300, 600), Utils.NextFloat(Main.rand, -1f, 1f), Utils.NextFloat(Main.rand, -1f, 1f), ModContent.ProjectileType<UkkoDancingLights>(), 0, 0f, Main.myPlayer, 0f, 0f);
								Main.projectile[p7].netUpdate = true;
							}
						}
						if (base.npc.ai[2] >= 200f)
						{
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 7:
						if (this.chariotCooldown != 0)
						{
							base.npc.ai[1] = (float)Main.rand.Next(17);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[3] = 2f;
						if (base.npc.velocity.X < 0f)
						{
							base.npc.rotation += 3.1415927f;
						}
						if (base.npc.ai[2] == 0f)
						{
							if (Vector2.Distance(base.npc.Center, this.MoveVector3) < 200f)
							{
								if (!Main.dedServ)
								{
									Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Jyrina1").WithVolume(0.6f).WithPitchVariance(0.2f), -1, -1);
								}
								base.npc.velocity = base.npc.DirectionTo(player.Center) * 50f;
								base.npc.ai[2] = 1f;
								base.npc.netUpdate = true;
								return;
							}
							base.npc.MoveToVector2(this.MoveVector3, 30f);
							return;
						}
						else
						{
							base.npc.ai[2] += 1f;
							if (base.npc.ai[2] % 8f == 0f && base.npc.ai[2] < 50f)
							{
								Vector2 ai = RedeHelper.PolarVector(12f, -Utils.ToRotation(base.npc.velocity) + Utils.NextFloat(Main.rand, -0.2f, 0.2f));
								float ai2 = (float)Main.rand.Next(100);
								Projectile.NewProjectile(base.npc.Center, RedeHelper.PolarVector(12f, -Utils.ToRotation(base.npc.velocity) + Utils.NextFloat(Main.rand, -0.2f, 0.2f)), ModContent.ProjectileType<UkkoLightning>(), base.npc.damage / 4, 0f, Main.myPlayer, Utils.ToRotation(ai), ai2);
							}
							if (base.npc.ai[2] >= 50f && this.dashCounter < 2)
							{
								this.dashCounter++;
								this.MoveVector3 = this.ChargePos();
								base.npc.ai[2] = 0f;
							}
							if (base.npc.ai[2] >= 20f && this.dashCounter >= 2)
							{
								int p8 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, base.npc.velocity.X, base.npc.velocity.Y, (base.npc.spriteDirection == -1) ? ModContent.ProjectileType<Jyrina>() : ModContent.ProjectileType<JyrinaOpp>(), 43, 3f, Main.myPlayer, 0f, 0f);
								Main.projectile[p8].netUpdate = true;
								this.chariotCooldown = 5;
								this.dashCounter = 0;
								base.npc.ai[3] = 0f;
								base.npc.ai[0] = 0f;
								base.npc.ai[2] = 0f;
								base.npc.netUpdate = true;
								return;
							}
						}
						break;
					case 8:
						if (!player.ZoneDesert)
						{
							base.npc.ai[1] = (float)Main.rand.Next(17);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 2f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/WindLong1").WithVolume(0.8f).WithPitchVariance(0.1f), -1, -1);
						}
						if (base.npc.ai[2] % 2f == 0f && base.npc.ai[2] > 8f && base.npc.ai[2] < 68f)
						{
							base.npc.ai[3] = 1f;
							for (int n = 0; n < 5; n++)
							{
								int p9 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, Utils.NextFloat(Main.rand, -10f, 10f), Utils.NextFloat(Main.rand, -10f, 10f), ModContent.ProjectileType<UkkoGust>(), 0, 0f, 255, 0f, 0f);
								Main.projectile[p9].netUpdate = true;
							}
						}
						if (base.npc.ai[2] >= 80f)
						{
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 9:
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] % 10f == 0f && base.npc.ai[2] < 50f)
						{
							Vector2 ai3 = RedeHelper.PolarVector(15f, Utils.NextFloat(Main.rand, 0f, 6.2831855f));
							float ai4 = (float)Main.rand.Next(100);
							Projectile.NewProjectile(base.npc.Center, RedeHelper.PolarVector(15f, Utils.NextFloat(Main.rand, 0f, 6.2831855f)), ModContent.ProjectileType<UkkoLightning>(), base.npc.damage / 4, 0f, Main.myPlayer, Utils.ToRotation(ai3), ai4);
						}
						if (base.npc.ai[2] >= 50f)
						{
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 10:
						if (base.npc.life >= (int)((float)base.npc.lifeMax * 0.8f))
						{
							base.npc.ai[1] = (float)Main.rand.Next(17);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 8f)
						{
							base.npc.ai[3] = 1f;
							int p10 = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<StormSummonerPro>(), 36, 3f, Main.myPlayer, (float)Main.rand.Next(4), 0f);
							Main.projectile[p10].netUpdate = true;
						}
						if (base.npc.ai[2] >= 80f)
						{
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 11:
						if (base.npc.life >= (int)((float)base.npc.lifeMax * 0.5f))
						{
							base.npc.ai[1] = (float)Main.rand.Next(17);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] < 60f)
						{
							Dust dust3 = Dust.NewDustDirect(base.npc.position, base.npc.width, base.npc.height, 269, 0f, 0f, 100, default(Color), 0.7f);
							dust3.velocity = -base.npc.DirectionTo(dust3.position);
						}
						if (base.npc.ai[2] == 60f)
						{
							if (!Main.dedServ)
							{
								Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap2").WithVolume(0.5f).WithPitchVariance(0.1f), -1, -1);
							}
							float Speed2 = 8f;
							Vector2 vector9 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
							float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
							int p11 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), ModContent.ProjectileType<DualcastBall>(), 36, 3f, Main.myPlayer, 0f, 0f);
							Main.projectile[p11].netUpdate = true;
							int p12 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), ModContent.ProjectileType<DualcastBall>(), 36, 3f, Main.myPlayer, 1f, 0f);
							Main.projectile[p12].netUpdate = true;
						}
						if (base.npc.life < (int)((float)base.npc.lifeMax * 0.3f))
						{
							if (base.npc.ai[2] == 90f)
							{
								if (!Main.dedServ)
								{
									Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap2").WithVolume(0.5f).WithPitchVariance(0.1f), -1, -1);
								}
								float Speed3 = 6f;
								Vector2 vector10 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
								float rotation3 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
								int p13 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), ModContent.ProjectileType<DualcastBall>(), 36, 3f, Main.myPlayer, 0f, 0f);
								Main.projectile[p13].netUpdate = true;
								int p14 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), ModContent.ProjectileType<DualcastBall>(), 36, 3f, Main.myPlayer, 1f, 0f);
								Main.projectile[p14].netUpdate = true;
							}
							if (base.npc.ai[2] >= 120f)
							{
								base.npc.ai[0] = 0f;
								base.npc.ai[2] = 0f;
								base.npc.netUpdate = true;
								return;
							}
						}
						else if (base.npc.ai[2] >= 90f)
						{
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 12:
						if (!player.ZoneSnow)
						{
							base.npc.ai[1] = (float)Main.rand.Next(17);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] < 160f)
						{
							Dust dust4 = Dust.NewDustDirect(base.npc.position, base.npc.width, base.npc.height, 56, 0f, 0f, 100, default(Color), 0.9f);
							dust4.velocity = -base.npc.DirectionTo(dust4.position);
						}
						if (base.npc.ai[2] >= 60f && base.npc.ai[2] <= 160f && Main.rand.Next(5) == 0)
						{
							int A = Main.rand.Next(-200, 200) * 6;
							int B = Main.rand.Next(-200, 200) - 1000;
							int p15 = Projectile.NewProjectile(player.Center.X + (float)A, player.Center.Y + (float)B, 2f, 4f, ModContent.ProjectileType<UkkoBlizzard>(), 26, 3f, Main.myPlayer, 0f, 0f);
							Main.projectile[p15].netUpdate = true;
						}
						if (base.npc.ai[2] >= 190f)
						{
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 13:
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 8f)
						{
							base.npc.ai[3] = 1f;
							int p16 = Projectile.NewProjectile(player.Center.X, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<UkkoRainCloud>(), 0, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[p16].netUpdate = true;
						}
						if (base.npc.ai[2] >= 60f)
						{
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 14:
						if (this.burstCooldown != 0)
						{
							base.npc.ai[1] = (float)Main.rand.Next(17);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 2f)
						{
							base.npc.ai[3] = 3f;
							if (!Main.dedServ)
							{
								Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Thunder1").WithVolume(0.9f).WithPitchVariance(0.1f), -1, -1);
							}
						}
						if (base.npc.ai[2] < 52f)
						{
							for (int i2 = 0; i2 < 2; i2++)
							{
								Dust dust5 = Dust.NewDustDirect(base.npc.position, base.npc.width, base.npc.height, 269, 0f, 0f, 100, default(Color), 1f);
								dust5.velocity = -base.npc.DirectionTo(dust5.position);
							}
						}
						if (base.npc.ai[2] == 52f)
						{
							if (!Main.dedServ)
							{
								Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap2").WithVolume(0.9f).WithPitchVariance(0.1f), -1, -1);
							}
							for (int i3 = -16; i3 <= 16; i3++)
							{
								Projectile.NewProjectile(base.npc.Center, 10f * Utils.RotatedBy(Vector2.UnitX, 0.19634954084936207 * (double)i3, default(Vector2)), ModContent.ProjectileType<UkkoThunderwave>(), 30, 0f, 0, (float)base.npc.whoAmI, 0f);
							}
						}
						if (base.npc.ai[2] == 58f)
						{
							if (!Main.dedServ)
							{
								Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap2").WithVolume(0.9f).WithPitchVariance(0.1f), -1, -1);
							}
							for (int i4 = -8; i4 <= 8; i4++)
							{
								Projectile.NewProjectile(base.npc.Center, 8f * Utils.RotatedBy(Vector2.UnitX, 0.39269908169872414 * (double)i4, default(Vector2)), ModContent.ProjectileType<UkkoThunderwave>(), 30, 0f, 0, (float)base.npc.whoAmI, 0f);
							}
						}
						if (base.npc.ai[2] >= 160f)
						{
							this.burstCooldown = 8;
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 15:
						if (this.teamCooldown != 0 || !NPC.AnyNPCs(ModContent.NPCType<Akka>()))
						{
							base.npc.ai[1] = (float)Main.rand.Next(17);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						RedeUkkoAkka.TAbubbles = true;
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 102f)
						{
							base.npc.ai[3] = 3f;
							if (!Main.dedServ)
							{
								Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Thunder1").WithVolume(0.9f).WithPitchVariance(0.1f), -1, -1);
							}
						}
						if (base.npc.ai[2] < 152f && base.npc.ai[2] > 102f)
						{
							for (int i5 = 0; i5 < 2; i5++)
							{
								Dust dust6 = Dust.NewDustDirect(base.npc.position, base.npc.width, base.npc.height, 269, 0f, 0f, 100, default(Color), 1f);
								dust6.velocity = -base.npc.DirectionTo(dust6.position);
							}
						}
						if (base.npc.ai[2] == 152f)
						{
							if (!Main.dedServ)
							{
								Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap1").WithVolume(0.9f).WithPitchVariance(0.1f), -1, -1);
							}
							Projectile.NewProjectile(base.npc.Center, new Vector2(0f, 0f), ModContent.ProjectileType<UkkoElectricBlast>(), 0, 0f, 0, (float)base.npc.whoAmI, 0f);
						}
						if (base.npc.ai[2] >= 180f)
						{
							RedeUkkoAkka.TAbubbles = false;
							this.teamCooldown = 18;
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 16:
						if (this.teamCooldown == 0 && NPC.AnyNPCs(ModContent.NPCType<Akka>()))
						{
							RedeUkkoAkka.TAearthProtection = true;
							base.npc.MoveToVector2(EarthProtectPos, 30f);
							base.npc.ai[2] += 1f;
							if (base.npc.ai[2] == 6f)
							{
								if (player.ZoneHoly)
								{
									int p17 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<EarthBarrier>(), 0, 0f, Main.myPlayer, 0f, 0f);
									Main.projectile[p17].netUpdate = true;
								}
								else if (player.ZoneCorrupt)
								{
									int p18 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<EarthBarrier>(), 0, 0f, Main.myPlayer, 1f, 0f);
									Main.projectile[p18].netUpdate = true;
								}
								else if (player.ZoneCrimson)
								{
									int p19 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<EarthBarrier>(), 0, 0f, Main.myPlayer, 3f, 0f);
									Main.projectile[p19].netUpdate = true;
								}
								else if (player.ZoneDesert)
								{
									int p20 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<EarthBarrier>(), 0, 0f, Main.myPlayer, 4f, 0f);
									Main.projectile[p20].netUpdate = true;
								}
								else
								{
									int p21 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<EarthBarrier>(), 0, 0f, Main.myPlayer, 2f, 0f);
									Main.projectile[p21].netUpdate = true;
								}
							}
							if (base.npc.ai[2] < 120f)
							{
								Dust dust7 = Dust.NewDustDirect(base.npc.position, base.npc.width, base.npc.height, 269, 0f, 0f, 100, default(Color), 0.7f);
								dust7.velocity = -base.npc.DirectionTo(dust7.position);
							}
							if (base.npc.ai[2] > 120f && base.npc.ai[2] % 20f == 0f)
							{
								if (!Main.dedServ)
								{
									Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap2").WithVolume(0.5f).WithPitchVariance(0.1f), -1, -1);
								}
								float Speed4 = Utils.NextFloat(Main.rand, 4f, 8f);
								Vector2 vector11 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
								float rotation4 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
								int p22 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), ModContent.ProjectileType<DualcastBall>(), 36, 3f, Main.myPlayer, 0f, 0f);
								Main.projectile[p22].netUpdate = true;
								int p23 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), ModContent.ProjectileType<DualcastBall>(), 36, 3f, Main.myPlayer, 1f, 0f);
								Main.projectile[p23].netUpdate = true;
							}
							if (base.npc.ai[2] >= 300f)
							{
								RedeUkkoAkka.TAearthProtection = false;
								this.teamCooldown = 18;
								base.npc.ai[0] = 0f;
								base.npc.ai[2] = 0f;
								base.npc.netUpdate = true;
								return;
							}
						}
						else
						{
							base.npc.ai[1] = (float)Main.rand.Next(17);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
						}
						break;
					default:
						return;
					}
				}
				return;
			}
			if (Vector2.Distance(base.npc.Center, this.MoveVector2) < 10f)
			{
				base.npc.velocity *= 0f;
				base.npc.ai[0] += 1f;
				base.npc.ai[1] = (float)Main.rand.Next(17);
				base.npc.netUpdate = true;
				return;
			}
			base.npc.MoveToVector2(this.MoveVector2, 30f);
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return base.npc.ai[1] == 7f;
		}

		public override bool CheckActive()
		{
			this.player = Main.player[base.npc.target];
			return !this.player.active || this.player.dead;
		}

		public Vector2 Pos()
		{
			return new Vector2((this.player.Center.X > base.npc.Center.X) ? (this.player.Center.X + (float)Main.rand.Next(-500, -300)) : (this.player.Center.X + (float)Main.rand.Next(300, 500)), this.player.Center.Y + (float)Main.rand.Next(-400, 200));
		}

		public Vector2 ChargePos()
		{
			return new Vector2((this.player.Center.X > base.npc.Center.X) ? (this.player.Center.X - 1400f) : (this.player.Center.X + 1400f), this.player.Center.Y + (float)Main.rand.Next(-80, 80));
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -20f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
		}

		private void Rain()
		{
			if ((Math.Abs(base.npc.position.X - Main.player[base.npc.target].position.X) > 6000f || Math.Abs(base.npc.position.Y - Main.player[base.npc.target].position.Y) > 6000f || Main.player[base.npc.target].dead) && this.StopRain == 0)
			{
				this.RainStop();
				this.StopRain = 1;
			}
			if (this.RunOnce == 0)
			{
				if (!Main.raining)
				{
					int num = 86400;
					int num2 = num / 24;
					Main.rainTime = Main.rand.Next(num2 * 8, num);
					if (Main.rand.Next(3) == 0)
					{
						Main.rainTime += Main.rand.Next(0, num2);
					}
					if (Main.rand.Next(4) == 0)
					{
						Main.rainTime += Main.rand.Next(0, num2 * 2);
					}
					if (Main.rand.Next(5) == 0)
					{
						Main.rainTime += Main.rand.Next(0, num2 * 2);
					}
					if (Main.rand.Next(6) == 0)
					{
						Main.rainTime += Main.rand.Next(0, num2 * 3);
					}
					if (Main.rand.Next(7) == 0)
					{
						Main.rainTime += Main.rand.Next(0, num2 * 4);
					}
					if (Main.rand.Next(8) == 0)
					{
						Main.rainTime += Main.rand.Next(0, num2 * 5);
					}
					float num3 = 1f;
					if (Main.rand.Next(2) == 0)
					{
						num3 += 0.05f;
					}
					if (Main.rand.Next(3) == 0)
					{
						num3 += 0.1f;
					}
					if (Main.rand.Next(4) == 0)
					{
						num3 += 0.15f;
					}
					if (Main.rand.Next(5) == 0)
					{
						num3 += 0.2f;
					}
					Main.rainTime = (int)((float)Main.rainTime * num3);
					Main.raining = true;
					if (Main.netMode != 0)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				this.RunOnce = 1;
			}
		}

		private void RainStop()
		{
			if (Main.raining)
			{
				Main.rainTime = 0;
				Main.raining = false;
				if (Main.netMode != 0)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/Ukko_Glow");
			Texture2D wandAni = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/Ukko_WandRaise");
			Texture2D wandGlow = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/Ukko_WandRaise_Glow");
			Texture2D dischargeAni = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/Ukko_Burst");
			Texture2D dischargeGlow = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/Ukko_Burst_Glow");
			Texture2D chariotAni = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/Ukko_Chariot");
			Texture2D chariotGlow = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/Ukko_Chariot_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
			if (base.npc.ai[3] == 0f)
			{
				for (int i = this.oldPos.Length - 1; i >= 0; i--)
				{
					float alpha = 1f - (float)(i + 1) / (float)(this.oldPos.Length + 2);
					spriteBatch.Draw(texture, this.oldPos[i] - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * (0.5f * alpha), this.oldrot[i], Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			}
			else if (base.npc.ai[3] == 1f)
			{
				int num214 = wandAni.Height / 9;
				int y6 = num214 * this.wandFrame;
				Vector2 drawCenter = new Vector2(base.npc.Center.X + 4f, base.npc.Center.Y - 16f);
				Main.spriteBatch.Draw(wandAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, wandAni.Width, num214)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)wandAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				spriteBatch.Draw(wandGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, wandAni.Width, num214)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)wandAni.Width / 2f, (float)num214 / 2f), base.npc.scale, effects, 0f);
			}
			else if (base.npc.ai[3] == 2f)
			{
				int num215 = chariotAni.Height / 9;
				int y7 = num215 * this.chariotFrame;
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				for (int j = this.oldPos.Length - 1; j >= 0; j--)
				{
					float alpha2 = 1f - (float)(j + 1) / (float)(this.oldPos.Length + 2);
					spriteBatch.Draw(chariotAni, this.oldPos[j] - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, chariotAni.Width, num215)), drawColor * (0.5f * alpha2), this.oldrot[j], new Vector2((float)chariotAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				Main.spriteBatch.Draw(chariotAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, chariotAni.Width, num215)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)chariotAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				spriteBatch.Draw(chariotGlow, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, chariotAni.Width, num215)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)chariotAni.Width / 2f, (float)num215 / 2f), base.npc.scale, effects, 0f);
			}
			else if (base.npc.ai[3] == 3f)
			{
				int num216 = dischargeAni.Height / 12;
				int y8 = num216 * this.dischargeFrame;
				Vector2 drawCenter3 = new Vector2(base.npc.Center.X + 4f, base.npc.Center.Y - 16f);
				Main.spriteBatch.Draw(dischargeAni, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, dischargeAni.Width, num216)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)dischargeAni.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				spriteBatch.Draw(dischargeGlow, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, dischargeAni.Width, num216)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)dischargeAni.Width / 2f, (float)num216 / 2f), base.npc.scale, effects, 0f);
			}
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 35; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 4.6f;
				}
			}
			int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 1f);
			Main.dust[dustIndex3].velocity *= 4.6f;
		}

		private Player player;

		private Vector2[] oldPos = new Vector2[3];

		private readonly float[] oldrot = new float[3];

		private int RunOnce;

		private int StopRain;

		public Vector2 MoveVector2;

		public Vector2 MoveVector3;

		public int wandFrame;

		public int frameCounters;

		public int mendingCooldown;

		public int stoneskinCooldown;

		public int chariotCooldown;

		public int burstCooldown;

		public int teamCooldown = 10;

		public int chariotFrame;

		public int dashCounter;

		public int dischargeFrame;
	}
}
