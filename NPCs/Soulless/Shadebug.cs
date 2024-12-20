using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Soulless
{
	public class Shadebug : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadebug");
			Main.npcFrameCount[base.npc.type] = 9;
		}

		public override void SetDefaults()
		{
			base.npc.width = 22;
			base.npc.height = 14;
			base.npc.damage = 60;
			base.npc.defense = 30;
			base.npc.lifeMax = 2500;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath32;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 3;
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			if (base.npc.velocity.X > 0f)
			{
				base.npc.spriteDirection = -1;
			}
			else
			{
				base.npc.spriteDirection = 1;
			}
			if (base.npc.velocity.X == 0f)
			{
				base.npc.frameCounter = 0.0;
				base.npc.frame.Y = 0;
				return;
			}
			if (base.npc.frame.Y == 0)
			{
				base.npc.frame.Y = 16;
			}
			base.npc.frameCounter += (double)(base.npc.velocity.X * 0.5f);
			if (base.npc.frameCounter >= 5.0 || base.npc.frameCounter <= -5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 16;
				if (base.npc.frame.Y > 128)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 16;
				}
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 8; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 1.6f;
				}
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			target.AddBuff(ModContent.BuffType<BlackenedHeartDebuff>(), Main.rand.Next(10, 15), true);
		}
	}
}
