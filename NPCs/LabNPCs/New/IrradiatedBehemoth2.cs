using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	[AutoloadBossHead]
	public class IrradiatedBehemoth2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Irradiated Behemoth");
			Main.npcFrameCount[base.npc.type] = 6;
		}

		public override void SetDefaults()
		{
			base.npc.width = 146;
			base.npc.height = 86;
			base.npc.friendly = false;
			base.npc.damage = 60;
			base.npc.defense = 0;
			base.npc.lifeMax = 26000;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.value = (float)Item.buyPrice(0, 10, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.alpha = 255;
			base.npc.boss = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.netAlways = true;
			base.npc.behindTiles = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 20; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/IBGoreHand"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/IBGoreHand1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/IBGoreHead"), 1f);
				for (int j = 0; j < 10; j++)
				{
					Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/IBGoreGoo"), 1f);
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 58;
			RedeWorld.downedIBehemoth = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			if (!RedeWorld.labAccess3)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ZoneAccessPanel3A"), 1, false, 0, false, false);
			}
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 108;
				if (base.npc.frame.Y > 540)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			if (!NPC.AnyNPCs(base.mod.NPCType("IrradiatedBehemothBody")))
			{
				int minion = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y + 30, base.mod.NPCType("IrradiatedBehemothBody"), 0, (float)base.npc.whoAmI, 0f, 0f, 0f, 255);
				Main.npc[minion].netUpdate = true;
			}
			if (base.npc.ai[1] == 0f)
			{
				base.npc.ai[0] += 1f;
				if (base.npc.ai[0] <= 120f)
				{
					if (base.npc.ai[0] == 1f)
					{
						if (!Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/SpookyNoise").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
						}
						if (Main.netMode != 1)
						{
							Vector2 newPos = new Vector2(231f, -270f);
							base.npc.Center = base.npc.position + newPos;
							base.npc.netUpdate = true;
						}
					}
					base.npc.alpha -= 4;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[0] > 120f)
				{
					base.npc.ai[1] = 1f;
					base.npc.ai[0] = 0f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 1f)
			{
				base.npc.ai[0] += 1f;
				if (base.npc.ai[0] >= 60f && base.npc.ai[0] < 90f && Main.rand.Next(2) == 0)
				{
					float Speed = 8f;
					Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage = 34;
					int type = base.mod.ProjectileType("GreenGloopPro3");
					float rotation = (float)Math.Atan2((double)(vector8.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector8.X - (this.player.position.X + (float)this.player.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-1, 1), type, damage, 0f, 0, 0f, 0f);
					Main.projectile[num54].netUpdate = true;
				}
				if (base.npc.ai[0] == 180f || base.npc.ai[0] == 300f)
				{
					int p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 5f), base.mod.ProjectileType("GreenGasPro2"), 34, 3f, 255, 0f, 0f);
					int p2 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-2f, 5f), base.mod.ProjectileType("GreenGasPro2"), 34, 3f, 255, 0f, 0f);
					int p3 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(2f, 5f), base.mod.ProjectileType("GreenGasPro2"), 34, 3f, 255, 0f, 0f);
					Main.projectile[p].netUpdate = true;
					Main.projectile[p2].netUpdate = true;
					Main.projectile[p3].netUpdate = true;
				}
				if (base.npc.ai[0] == 240f)
				{
					int p4 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(1f, 5f), base.mod.ProjectileType("GreenGasPro2"), 34, 3f, 255, 0f, 0f);
					int p5 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-1f, 5f), base.mod.ProjectileType("GreenGasPro2"), 34, 3f, 255, 0f, 0f);
					Main.projectile[p4].netUpdate = true;
					Main.projectile[p5].netUpdate = true;
				}
				if (base.npc.ai[0] == 260f || base.npc.ai[0] == 20f)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (base.npc.ai[0] >= 480f && base.npc.ai[0] < 540f && Main.rand.Next(2) == 0)
				{
					int p6 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2((float)Main.rand.Next(-2, 2), (float)Main.rand.Next(0, 2)), base.mod.ProjectileType("GreenGloopPro2"), 34, 3f, 255, 0f, 0f);
					Main.projectile[p6].netUpdate = true;
				}
				if (base.npc.ai[0] >= 660f)
				{
					base.npc.ai[0] = 0f;
				}
			}
			base.npc.ai[3] += 1f;
			if (base.npc.ai[3] < 1200f)
			{
				if ((double)base.npc.velocity.Y > 0.06)
				{
					NPC npc2 = base.npc;
					npc2.velocity.Y = npc2.velocity.Y - 0.06f;
					return;
				}
				NPC npc3 = base.npc;
				npc3.velocity.Y = npc3.velocity.Y + 0.06f;
				return;
			}
			else if (base.npc.ai[3] >= 1200f && base.npc.ai[3] < 2400f)
			{
				if ((double)base.npc.velocity.Y > 0.24)
				{
					NPC npc4 = base.npc;
					npc4.velocity.Y = npc4.velocity.Y - 0.12f;
					return;
				}
				NPC npc5 = base.npc;
				npc5.velocity.Y = npc5.velocity.Y + 0.12f;
				return;
			}
			else if (base.npc.ai[3] >= 2400f && base.npc.ai[3] < 3600f)
			{
				if ((double)base.npc.velocity.Y > 0.74)
				{
					NPC npc6 = base.npc;
					npc6.velocity.Y = npc6.velocity.Y - 0.24f;
					return;
				}
				NPC npc7 = base.npc;
				npc7.velocity.Y = npc7.velocity.Y + 0.24f;
				return;
			}
			else
			{
				if (base.npc.ai[3] < 3600f || base.npc.ai[3] >= 4800f)
				{
					if (base.npc.ai[3] >= 9000f)
					{
						base.npc.active = false;
					}
					return;
				}
				if ((double)base.npc.velocity.Y > 1.18)
				{
					NPC npc8 = base.npc;
					npc8.velocity.Y = npc8.velocity.Y - 0.48f;
					return;
				}
				NPC npc9 = base.npc;
				npc9.velocity.Y = npc9.velocity.Y + 0.48f;
				return;
			}
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
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.52f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.5f);
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return base.npc.ai[1] >= 1f;
		}

		private Player player;
	}
}
