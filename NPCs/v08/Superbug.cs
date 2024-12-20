﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class Superbug : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Superbug");
		}

		public override void SetDefaults()
		{
			base.npc.width = 50;
			base.npc.height = 86;
			base.npc.damage = 65;
			base.npc.defense = 18;
			base.npc.lifeMax = 2400;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0.35f;
			base.npc.aiStyle = 22;
			this.aiType = 182;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("SuperbugBanner");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 5; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("Bile"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(100) == 0 || (Main.expertMode && Main.rand.Next(50) == 0))
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 892, 1, false, 0, false, false);
			}
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			base.npc.rotation += 0.06f;
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 800f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] >= 300f && base.npc.scale == 1f)
				{
					int Split = NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-12, 12), (int)base.npc.Center.Y + Main.rand.Next(-12, 12), base.mod.NPCType("Superbug"), 0, 0f, 0f, 0f, 0f, 255);
					int Split2 = NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-12, 12), (int)base.npc.Center.Y + Main.rand.Next(-12, 12), base.mod.NPCType("Superbug"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Split].lifeMax /= 2;
					Main.npc[Split].life /= 2;
					Main.npc[Split].damage -= 5;
					Main.npc[Split].scale *= 0.95f;
					Main.npc[Split2].lifeMax /= 2;
					Main.npc[Split2].life /= 2;
					Main.npc[Split2].damage -= 5;
					Main.npc[Split2].scale *= 0.95f;
					for (int i = 0; i < 10; i++)
					{
						int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 3f);
						Main.dust[dustIndex2].velocity *= 2.6f;
					}
					Main.PlaySound(SoundID.NPCDeath1, base.npc.position);
					base.npc.active = false;
				}
				if (base.npc.ai[3] >= 300f && base.npc.scale < 1f)
				{
					NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-12, 12), (int)base.npc.Center.Y + Main.rand.Next(-12, 12), base.mod.NPCType("Superbug2"), 0, 0f, 0f, 0f, 0f, 255);
					NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-12, 12), (int)base.npc.Center.Y + Main.rand.Next(-12, 12), base.mod.NPCType("Superbug2"), 0, 0f, 0f, 0f, 0f, 255);
					for (int j = 0; j < 10; j++)
					{
						int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 3f);
						Main.dust[dustIndex3].velocity *= 2.6f;
					}
					Main.PlaySound(SoundID.NPCDeath1, base.npc.position);
					base.npc.active = false;
				}
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || Main.expertMode)
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
