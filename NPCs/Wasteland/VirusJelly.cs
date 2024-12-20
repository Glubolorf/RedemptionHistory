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
	public class VirusJelly : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plague Physalia");
			Main.npcFrameCount[base.npc.type] = 7;
		}

		public override void SetDefaults()
		{
			base.npc.width = 50;
			base.npc.height = 86;
			base.npc.damage = 75;
			base.npc.defense = 38;
			base.npc.lifeMax = 1150;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.value = 2000f;
			base.npc.knockBackResist = 0.35f;
			base.npc.aiStyle = 18;
			this.aiType = 103;
			this.animationType = 103;
			base.npc.noGravity = true;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<PlagueJellyBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<Starlite>(), Main.rand.Next(2, 4), false, 0, false, false);
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
