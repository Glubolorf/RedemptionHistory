using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Lab
{
	public class Blisterling : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blisterling");
			Main.npcFrameCount[base.npc.type] = 6;
		}

		public override void SetDefaults()
		{
			base.npc.width = 32;
			base.npc.height = 28;
			base.npc.friendly = false;
			base.npc.damage = 90;
			base.npc.defense = 0;
			base.npc.lifeMax = 750;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 0f;
			base.npc.noGravity = true;
			base.npc.knockBackResist = 0.1f;
			base.npc.aiStyle = 16;
			this.aiType = 58;
			this.animationType = 58;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff>(), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff2>(), Main.rand.Next(250, 500), true);
			}
		}
	}
}
