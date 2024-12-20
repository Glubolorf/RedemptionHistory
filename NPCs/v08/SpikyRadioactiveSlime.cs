using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Dusts;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class SpikyRadioactiveSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spiked Radioactive Slime");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 52;
			base.npc.height = 42;
			base.npc.friendly = false;
			base.npc.damage = 70;
			base.npc.defense = 0;
			base.npc.lifeMax = 650;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 700f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 1;
			base.npc.netAlways = true;
			this.aiType = 138;
			this.animationType = 302;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 25; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex2].velocity *= 4.6f;
				}
			}
			int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 100, default(Color), 2f);
			Main.dust[dustIndex3].velocity *= 1.6f;
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || Main.expertMode)
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff>(), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff2>(), Main.rand.Next(250, 500), true);
			}
		}

		public override void AI()
		{
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 500f && Main.rand.Next(60) == 0 && !this.spike)
			{
				this.spike = true;
			}
			if (this.spike)
			{
				for (int i = 0; i < 6; i++)
				{
					int p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-4 + Main.rand.Next(0, 5)), (float)(-1 + Main.rand.Next(-4, 0)), ModContent.ProjectileType<RSlimeSpikePro1>(), 20, 3f, 255, 0f, 0f);
					Main.projectile[p].netUpdate = true;
				}
				this.spike = false;
			}
		}

		private bool spike;
	}
}
