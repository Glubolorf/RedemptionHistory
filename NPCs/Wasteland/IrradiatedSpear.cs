using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Wasteland
{
	public class IrradiatedSpear : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Irradiated Spear");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 94;
			base.npc.height = 108;
			base.npc.damage = 90;
			base.npc.defense = 40;
			base.npc.lifeMax = 1000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath6;
			base.npc.value = 1000f;
			base.npc.knockBackResist = 0.2f;
			base.npc.aiStyle = 23;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<IrradiatedSpearBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 15; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(100) == 0 || (Main.expertMode && Main.rand.Next(50) == 0))
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 891, 1, false, 0, false, false);
			}
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 10.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 114;
				if (base.npc.frame.Y > 114)
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
			if (Main.rand.Next(3) == 0)
			{
				target.AddBuff(23, Main.rand.Next(240, 420), true);
			}
		}
	}
}
