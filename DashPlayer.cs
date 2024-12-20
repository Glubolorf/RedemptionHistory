using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption
{
	public class DashPlayer : ModPlayer
	{
		public DashType ActiveDash { get; private set; }

		public override void ResetEffects()
		{
			this.plasmaShield = false;
			this.infectedThornshield = false;
		}

		public override void NaturalLifeRegen(ref float regen)
		{
			DashType dash = this.FindDashes();
			if (dash != DashType.None)
			{
				base.player.dash = 0;
				if (base.player.pulley)
				{
					this.DashMovement(dash);
				}
			}
		}

		private void DashEnd()
		{
		}

		private void DashMovement(DashType dash)
		{
			if (base.player.dashDelay > 0)
			{
				if (this.ActiveDash != DashType.None)
				{
					this.DashEnd();
					this.ActiveDash = DashType.None;
					return;
				}
			}
			else if (base.player.dashDelay < 0)
			{
				float speedCap = 20f;
				float decayCapped = 0.992f;
				float speedMax = Math.Max(base.player.accRunSpeed, base.player.maxRunSpeed);
				float decayMax = 0.96f;
				int delay = 20;
				DashType activeDash = this.ActiveDash;
				if (activeDash != DashType.Infected)
				{
					if (activeDash == DashType.Holoshield)
					{
						if (this.holoshieldHit < 0)
						{
							Rectangle hitbox = new Rectangle((int)((double)base.player.position.X + (double)base.player.velocity.X * 0.5 - 4.0), (int)((double)base.player.position.Y + (double)base.player.velocity.Y * 0.5 - 4.0), base.player.width + 8, base.player.height + 8);
							for (int i = 0; i < 200; i++)
							{
								NPC npc = Main.npc[i];
								if (npc.active && !npc.dontTakeDamage && !npc.friendly && hitbox.Intersects(npc.Hitbox) && (npc.noTileCollide || Collision.CanHit(base.player.position, base.player.width, base.player.height, npc.position, npc.width, npc.height)))
								{
									DruidDamagePlayer.ModPlayer(base.player);
									float damage = 20f * base.player.meleeDamage;
									float knockback = 8f;
									bool crit = false;
									if (base.player.kbGlove)
									{
										knockback *= 2f;
									}
									if (base.player.kbBuff)
									{
										knockback *= 1.5f;
									}
									if (Main.rand.Next(100) < base.player.meleeCrit)
									{
										crit = true;
									}
									int hitDirection = (base.player.velocity.X < 0f) ? -1 : 1;
									if (base.player.whoAmI == Main.myPlayer)
									{
										npc.StrikeNPC((int)damage, knockback, hitDirection, crit, false, false);
										if (Main.netMode != 0)
										{
											NetMessage.SendData(28, -1, -1, null, i, damage, knockback, (float)hitDirection, 0, 0, 0);
										}
									}
									base.player.dashDelay = 30;
									base.player.velocity.X = (float)(-(float)hitDirection) * 1f;
									base.player.velocity.Y = -4f;
									base.player.immune = true;
									base.player.immuneTime = 10;
									this.holoshieldHit = i;
								}
							}
							for (int j = 0; j < 1000; j++)
							{
								Projectile proj = Main.projectile[j];
								if (proj.active && proj.hostile && !proj.friendly && proj.damage < 100 && proj.velocity.Length() > 0f && hitbox.Intersects(proj.Hitbox) && (!proj.tileCollide || Collision.CanHit(base.player.position, base.player.width, base.player.height, proj.position, proj.width, proj.height)))
								{
									if (!Main.dedServ)
									{
										Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Reflect").WithVolume(0.5f).WithPitchVariance(0.1f), -1, -1);
									}
									proj.damage *= 2;
									proj.velocity = -proj.velocity;
									proj.friendly = true;
									proj.hostile = false;
									int hitDirection2 = (base.player.velocity.X < 0f) ? -1 : 1;
									base.player.dashDelay = 30;
									base.player.velocity.X = (float)(-(float)hitDirection2) * 1f;
									base.player.velocity.Y = -4f;
									base.player.immune = true;
									base.player.immuneTime = 10;
									this.holoshieldHit = j;
								}
							}
						}
					}
				}
				else if (this.infectedShieldHit < 0)
				{
					Dust.NewDust(base.player.position, base.player.width, base.player.height, 74, 0f, 0f, 0, default(Color), 1f);
					Dust.NewDust(base.player.position, base.player.width, base.player.height, 74, 0f, 0f, 0, default(Color), 1f);
					Dust.NewDust(base.player.position, base.player.width, base.player.height, 74, 0f, 0f, 0, default(Color), 1f);
					Rectangle hitbox2 = new Rectangle((int)((double)base.player.position.X + (double)base.player.velocity.X * 0.5 - 4.0), (int)((double)base.player.position.Y + (double)base.player.velocity.Y * 0.5 - 4.0), base.player.width + 8, base.player.height + 8);
					for (int k = 0; k < 200; k++)
					{
						NPC npc2 = Main.npc[k];
						if (npc2.active && !npc2.dontTakeDamage && !npc2.friendly && hitbox2.Intersects(npc2.Hitbox) && (npc2.noTileCollide || Collision.CanHit(base.player.position, base.player.width, base.player.height, npc2.position, npc2.width, npc2.height)))
						{
							DruidDamagePlayer modPlayer = DruidDamagePlayer.ModPlayer(base.player);
							float damage2 = 40f * modPlayer.druidDamage;
							float knockback2 = 10f;
							bool crit2 = false;
							if (base.player.kbGlove)
							{
								knockback2 *= 2f;
							}
							if (base.player.kbBuff)
							{
								knockback2 *= 1.5f;
							}
							if (Main.rand.Next(100) < modPlayer.druidCrit)
							{
								crit2 = true;
							}
							int hitDirection3 = (base.player.velocity.X < 0f) ? -1 : 1;
							if (base.player.whoAmI == Main.myPlayer)
							{
								npc2.AddBuff(ModContent.BuffType<XenomiteDebuff>(), 600, false);
								if (Utils.NextBool(Main.rand, 5))
								{
									npc2.AddBuff(ModContent.BuffType<XenomiteDebuff2>(), 300, false);
								}
								npc2.StrikeNPC((int)damage2, knockback2, hitDirection3, crit2, false, false);
								if (Main.netMode != 0)
								{
									NetMessage.SendData(28, -1, -1, null, k, damage2, knockback2, (float)hitDirection3, 0, 0, 0);
								}
							}
							base.player.dashDelay = 30;
							base.player.velocity.X = (float)(-(float)hitDirection3) * 1f;
							base.player.velocity.Y = -4f;
							base.player.immune = true;
							base.player.immuneTime = 10;
							this.infectedShieldHit = k;
						}
					}
				}
				if (this.ActiveDash != DashType.None)
				{
					if (speedCap < speedMax)
					{
						speedCap = speedMax;
					}
					base.player.vortexStealthActive = false;
					if (base.player.velocity.X > speedCap || base.player.velocity.X < -speedCap)
					{
						base.player.velocity.X = base.player.velocity.X * decayCapped;
						return;
					}
					if (base.player.velocity.X > speedMax || base.player.velocity.X < -speedMax)
					{
						base.player.velocity.X = base.player.velocity.X * decayMax;
						return;
					}
					base.player.dashDelay = delay;
					if (base.player.velocity.X < 0f)
					{
						base.player.velocity.X = -speedMax;
						return;
					}
					if (base.player.velocity.X > 0f)
					{
						base.player.velocity.X = speedMax;
						return;
					}
				}
			}
			else if (dash != DashType.None && base.player.whoAmI == Main.myPlayer)
			{
				sbyte dir = 0;
				bool dashInput = false;
				if (base.player.dashTime > 0)
				{
					base.player.dashTime--;
				}
				else if (base.player.dashTime < 0)
				{
					base.player.dashTime++;
				}
				if (base.player.controlRight && base.player.releaseRight)
				{
					if (base.player.dashTime > 0)
					{
						dir = 1;
						dashInput = true;
						base.player.dashTime = 0;
					}
					else
					{
						base.player.dashTime = 15;
					}
				}
				else if (base.player.controlLeft && base.player.releaseLeft)
				{
					if (base.player.dashTime < 0)
					{
						dir = -1;
						dashInput = true;
						base.player.dashTime = 0;
					}
					else
					{
						base.player.dashTime = -15;
					}
				}
				if (dashInput)
				{
					this.PerformDash(dash, dir, true);
				}
			}
		}

		public override void PostUpdateRunSpeeds()
		{
			this.DashMovement(this.FindDashes());
		}

		internal void PerformDash(DashType dash, sbyte dir, bool local = true)
		{
			float velocity = (float)dir;
			if (dash != DashType.Infected)
			{
				if (dash == DashType.Holoshield)
				{
					this.holoshieldHit = -1;
					velocity *= 14f;
				}
			}
			else
			{
				this.infectedShieldHit = -1;
				Dust.NewDust(base.player.position, base.player.width, base.player.height, 74, 0f, 0f, 0, default(Color), 1f);
				Dust.NewDust(base.player.position, base.player.width, base.player.height, 74, 0f, 0f, 0, default(Color), 1f);
				velocity *= 20f;
				for (int i = 0; i < 10; i++)
				{
					int dust = Dust.NewDust(base.player.position, base.player.width, base.player.height, 74, 0f, 0f, 100, default(Color), 2f);
					Dust dust2 = Main.dust[dust];
					dust2.position.X = dust2.position.X + (float)Main.rand.Next(-5, 6);
					Dust dust3 = Main.dust[dust];
					dust3.position.Y = dust3.position.Y + (float)Main.rand.Next(-5, 6);
					Main.dust[dust].velocity *= 0.2f;
				}
			}
			base.player.velocity.X = velocity;
			Point feet = Utils.ToTileCoordinates(base.player.Center + new Vector2((float)((int)dir * (base.player.width >> 1) + 2), base.player.gravDir * (float)(-(float)base.player.height) * 0.5f + base.player.gravDir * 2f));
			Point legs = Utils.ToTileCoordinates(base.player.Center + new Vector2((float)((int)dir * (base.player.width >> 1) + 2), 0f));
			if (WorldGen.SolidOrSlopedTile(feet.X, feet.Y) || WorldGen.SolidOrSlopedTile(legs.X, legs.Y))
			{
				base.player.velocity.X = base.player.velocity.X / 2f;
			}
			base.player.dashDelay = -1;
			this.ActiveDash = dash;
			if (!local || Main.netMode == 0)
			{
				return;
			}
			ModPacket packet = Redemption.Inst.GetPacket(ModMessageType.Dash, 3);
			packet.Write((byte)base.player.whoAmI);
			packet.Write((byte)dash);
			packet.Write(dir);
			packet.Send(-1, -1);
		}

		public DashType FindDashes()
		{
			if (base.player.mount.Active)
			{
				return DashType.None;
			}
			if (this.infectedThornshield)
			{
				return DashType.Infected;
			}
			if (this.plasmaShield)
			{
				return DashType.Holoshield;
			}
			return DashType.None;
		}

		public bool infectedThornshield;

		public bool plasmaShield;

		public int infectedShieldHit;

		public int holoshieldHit;
	}
}
