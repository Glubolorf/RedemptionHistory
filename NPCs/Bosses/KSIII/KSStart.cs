using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class KSStart : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("");
		}

		public override void SetDefaults()
		{
			base.npc.width = 42;
			base.npc.height = 106;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.noTileCollide = true;
			base.npc.noGravity = true;
			base.npc.immortal = true;
			base.npc.dontTakeDamage = true;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			float obj = base.npc.ai[0];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					if (!2f.Equals(obj))
					{
						if (3f.Equals(obj))
						{
							base.npc.active = false;
							NPC npc = base.npc;
							npc.velocity.Y = npc.velocity.Y + 10f;
							return;
						}
						if (!4f.Equals(obj))
						{
							return;
						}
						if (RedeWorld.slayerDeath < 2)
						{
							RedeWorld.slayerDeath = 2;
						}
						base.npc.Center = new Vector2(player.position.X + 200f, player.position.Y - 80f);
						if (base.npc.ai[2] % 3f == 0f)
						{
							int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y - 800f), base.npc.width, base.npc.height + 750, 92, 0f, 0f, 100, default(Color), 1f);
							Main.dust[dustIndex2].velocity *= 1f;
							Main.dust[dustIndex2].noGravity = true;
						}
						float[] ai = base.npc.ai;
						int num = 2;
						float num2 = ai[num] + 1f;
						ai[num] = num2;
						if (num2 >= 120f)
						{
							Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
							DustHelper.DrawDustImage(new Vector2(base.npc.Center.X, base.npc.Center.Y), 92, 0.2f, "Redemption/Effects/DustImages/WarpShape", 3f, true, 0f);
							for (int i = 0; i < 30; i++)
							{
								int dustIndex3 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 92, 0f, 0f, 100, default(Color), 3f);
								Main.dust[dustIndex3].velocity *= 6f;
								Main.dust[dustIndex3].noGravity = true;
							}
							base.npc.netUpdate = true;
							base.npc.SetDefaults(ModContent.NPCType<KS3_Body>(), -1f);
						}
					}
					else
					{
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 30f)
						{
							this.Shoot(new Vector2(player.Center.X + 90f, player.Center.Y - 50f), ModContent.ProjectileType<KS3_HeadHologram>(), 0, Vector2.Zero, false, false, SoundID.Item1.WithVolume(0f), 0f, "", 0f, 0f);
						}
						if (base.npc.ai[2] > 760f)
						{
							if (RedeWorld.slayerDeath < 2)
							{
								RedeWorld.slayerDeath = 2;
							}
							base.npc.ai[0] = 3f;
							base.npc.ai[1] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
					}
				}
				else
				{
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 10f || base.npc.ai[2] == 30f)
					{
						NPC.NewNPC((int)player.Center.X + Main.rand.Next(70, 180), (int)player.Center.Y - Main.rand.Next(800, 850), ModContent.NPCType<ScannerDrone>(), 0, 0f, 0f, 0f, (float)base.npc.whoAmI, 255);
					}
					if (base.npc.ai[2] > 30f && !NPC.AnyNPCs(ModContent.NPCType<ScannerDrone>()))
					{
						if (base.npc.ai[1] >= 2f)
						{
							this.Shoot(base.npc.Center, ModContent.ProjectileType<KS3_DroneKillCheck>(), 0, Vector2.Zero, false, false, SoundID.Item1.WithVolume(0f), 0f, "", 0f, 0f);
							base.npc.ai[0] = 4f;
							base.npc.ai[1] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						if (RedeWorld.slayerDeath < 1)
						{
							RedeWorld.slayerDeath = 1;
							base.npc.ai[0] = 3f;
						}
						else
						{
							base.npc.ai[0] = 2f;
						}
						base.npc.ai[1] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
				}
				return;
			}
			if (RedeWorld.redemptionPoints >= 0)
			{
				if (RedeWorld.slayerDeath <= 1 && !RedeConfigClient.Instance.NoLoreElements)
				{
					base.npc.ai[0] = 1f;
				}
				else if (RedeWorld.slayerDeath > 1 || RedeConfigClient.Instance.NoLoreElements)
				{
					base.npc.ai[0] = 4f;
				}
				base.npc.netUpdate = true;
				return;
			}
			base.npc.ai[0] = 4f;
			base.npc.netUpdate = true;
		}

		public void Shoot(Vector2 position, int projType, int damage, Vector2 velocity, bool aimed, bool customSound, LegacySoundStyle sound, float speed = 0f, string soundString = "", float ai0 = 0f, float spread = 0f)
		{
			Player player = Main.player[base.npc.target];
			if (aimed)
			{
				if (customSound)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, soundString), (int)base.npc.position.X, (int)base.npc.position.Y);
					}
				}
				else
				{
					Main.PlaySound(sound, (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				float rotation = (float)Math.Atan2((double)(position.Y - player.Center.Y), (double)(position.X - player.Center.X));
				int num54 = Projectile.NewProjectile(position.X, position.Y, (float)(Math.Cos((double)rotation) * (double)speed * -1.0) + spread, (float)(Math.Sin((double)rotation) * (double)speed * -1.0) + spread, projType, damage / 3, 0f, Main.myPlayer, ai0, 0f);
				Main.projectile[num54].netUpdate = true;
				return;
			}
			if (customSound)
			{
				if (!Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, soundString), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
			}
			else
			{
				Main.PlaySound(sound, (int)base.npc.position.X, (int)base.npc.position.Y);
			}
			int p = Projectile.NewProjectile(position, velocity, projType, damage / 3, 0f, Main.myPlayer, ai0, 0f);
			Main.projectile[p].netUpdate = true;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public override bool CheckDead()
		{
			base.npc.life = 1;
			return false;
		}

		public override bool CheckActive()
		{
			return base.npc.ai[0] == 3f;
		}
	}
}
