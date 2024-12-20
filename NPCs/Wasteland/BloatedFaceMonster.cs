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
	public class BloatedFaceMonster : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bloated Face Monster");
			Main.npcFrameCount[base.npc.type] = 14;
		}

		public override void SetDefaults()
		{
			base.npc.width = 36;
			base.npc.height = 66;
			base.npc.damage = 45;
			base.npc.defense = 20;
			base.npc.lifeMax = 350;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.value = 500f;
			base.npc.knockBackResist = 0.2f;
			base.npc.aiStyle = 3;
			this.aiType = 181;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<BloatedFaceMonsterBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/BloatedFaceMonsterGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/BloatedFaceMonsterGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/BloatedFaceMonsterGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteGore"), 1f);
				for (int i = 0; i < 15; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
			}
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
			if (Main.player[base.npc.target].Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			if (base.npc.velocity.Y == 0f)
			{
				if (base.npc.frame.Y < 70)
				{
					base.npc.frame.Y = 70;
				}
				base.npc.frameCounter += (double)(base.npc.velocity.X * 0.5f);
				if (base.npc.frameCounter >= 3.0 || base.npc.frameCounter <= -3.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 70;
					if (base.npc.frame.Y > 910)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 70;
						return;
					}
				}
			}
			else if (base.npc.frame.Y >= 70)
			{
				base.npc.frame.Y = 0;
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<Starlite>(), Main.rand.Next(1, 3), false, 0, false, false);
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
