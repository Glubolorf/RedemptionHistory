using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class GreenPigron : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pigron");
			Main.npcFrameCount[base.npc.type] = 14;
		}

		public override void SetDefaults()
		{
			base.npc.width = 80;
			base.npc.height = 54;
			base.npc.damage = 70;
			base.npc.friendly = false;
			base.npc.defense = 18;
			base.npc.lifeMax = 440;
			base.npc.HitSound = SoundID.NPCHit27;
			base.npc.DeathSound = SoundID.NPCDeath30;
			base.npc.value = 300f;
			base.npc.knockBackResist = 0.4f;
			base.npc.aiStyle = 2;
			this.aiType = 170;
			this.animationType = 170;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 25; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 4.6f;
				}
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(base.mod.BuffType("XenomiteDebuff2"), Main.rand.Next(250, 500), true);
			}
		}
	}
}
