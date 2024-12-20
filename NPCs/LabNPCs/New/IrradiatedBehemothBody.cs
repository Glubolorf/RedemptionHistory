using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class IrradiatedBehemothBody : ModNPC
	{
		public override void ScaleExpertStats(int playerXPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = base.npc.lifeMax;
			base.npc.damage = base.npc.damage;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Irradiated Behemoth");
		}

		public override void SetDefaults()
		{
			base.npc.width = 412;
			base.npc.height = 56;
			base.npc.friendly = false;
			base.npc.damage = 500;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.aiStyle = -1;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.dontTakeDamage = true;
			base.npc.behindTiles = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 50; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 3f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 1.9f;
				}
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/IBGoreFlesh"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/IBGoreFlesh"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/IBGoreFlesh"), 1f);
				for (int j = 0; j < 10; j++)
				{
					Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/IBGoreGoo"), 1f);
				}
			}
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
			Entity entity = Main.player[base.npc.target];
			int boss = (int)base.npc.ai[0];
			if (boss < 0 || boss >= 200 || !Main.npc[boss].active || Main.npc[boss].type != base.mod.NPCType("IrradiatedBehemoth2"))
			{
				base.npc.active = false;
			}
			base.npc.netUpdate = true;
			(Main.npc[boss].Center - base.npc.Center).Normalize();
			NPC npc2 = Main.npc[(int)base.npc.ai[0]];
			base.npc.Center = npc2.Center;
			base.npc.ai[2] += 1f;
			if (base.npc.ai[2] <= 120f)
			{
				base.npc.alpha -= 4;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[2] > 120f)
			{
				base.npc.ai[1] = 1f;
				base.npc.ai[2] = 0f;
				base.npc.netUpdate = true;
			}
			if (entity.Center.Y < base.npc.Center.Y && Main.rand.Next(5) == 0)
			{
				int projID = Projectile.NewProjectile(base.npc.Center.X + (float)Main.rand.Next(-250, 250), base.npc.Center.Y + (float)Main.rand.Next(-30, 30), 0f, (float)Main.rand.Next(-20, -10), base.mod.ProjectileType("GreenGasPro2"), 100, 1f, 255, 0f, 0f);
				Main.projectile[projID].netUpdate = true;
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return base.npc.ai[1] == 1f;
		}
	}
}
