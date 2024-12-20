using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Redemption.Dusts;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Wasteland
{
	public class RadioactiveSlimer : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Radioactive Slimer");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 38;
			base.npc.height = 38;
			base.npc.damage = 50;
			base.npc.defense = 18;
			base.npc.lifeMax = 225;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 200f;
			base.npc.knockBackResist = 0.38f;
			base.npc.aiStyle = 14;
			base.npc.noGravity = true;
			this.aiType = 121;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<RadioactiveSlimerBanner>();
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
			base.npc.spriteDirection = base.npc.direction;
			base.npc.rotation = base.npc.velocity.X * 0.05f;
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 8.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 70;
				if (base.npc.frame.Y > 210)
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

		public override bool CheckDead()
		{
			base.npc.Transform(ModContent.NPCType<RadioactiveSlime>());
			base.npc.life = base.npc.lifeMax;
			return false;
		}
	}
}
