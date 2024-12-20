using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Redemption.Items.Materials.HM;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Wasteland
{
	public class BloatedGoldfish : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bloated Goldfish");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 26;
			base.npc.height = 26;
			base.npc.damage = 40;
			base.npc.defense = 8;
			base.npc.lifeMax = 200;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.value = 400f;
			base.npc.knockBackResist = 0.7f;
			base.npc.aiStyle = 16;
			this.aiType = 57;
			base.npc.noGravity = true;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<BloatedGoldfishBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteGore"), 1f);
				for (int i = 0; i < 5; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<Starlite>(), 1, false, 0, false, false);
			}
		}

		public override void AI()
		{
			if (Main.player[base.npc.target].Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 30;
				if (base.npc.frame.Y > 90)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
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
