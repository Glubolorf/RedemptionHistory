using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Items;
using Redemption.Items.Placeable.Banners.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class Xenoling : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenoling");
			Main.npcFrameCount[base.npc.type] = 5;
		}

		public override void SetDefaults()
		{
			base.npc.width = 70;
			base.npc.height = 34;
			base.npc.damage = 70;
			base.npc.defense = 28;
			base.npc.lifeMax = 2200;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 400f;
			base.npc.knockBackResist = 0.25f;
			base.npc.aiStyle = 41;
			this.aiType = 174;
			this.animationType = 174;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<XenolingBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/XenolingGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/XenolingGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				for (int i = 0; i < 15; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<Starlite>(), Main.rand.Next(1, 5), false, 0, false, false);
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<Bile>(), 1, false, 0, false, false);
			}
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
	}
}
