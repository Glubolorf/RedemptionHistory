using System;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Wasteland
{
	public class XenomiteBeast : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Beast");
			Main.npcFrameCount[base.npc.type] = Main.npcFrameCount[423];
		}

		public override void SetDefaults()
		{
			base.npc.width = 64;
			base.npc.height = 36;
			base.npc.damage = 90;
			base.npc.defense = 30;
			base.npc.lifeMax = 450;
			base.npc.HitSound = SoundID.NPCHit19;
			base.npc.DeathSound = SoundID.NPCDeath5;
			base.npc.value = 2000f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 26;
			this.aiType = 86;
			this.animationType = 423;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteBeastGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteBeastGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteBeastGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteBeastGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/HostileXenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/HostileXenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/HostileXenomiteGore"), 1f);
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
