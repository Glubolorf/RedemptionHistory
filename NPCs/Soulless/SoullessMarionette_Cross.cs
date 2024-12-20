using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Soulless
{
	public class SoullessMarionette_Cross : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Marionette Cross");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 16500;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 52;
			base.npc.height = 32;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.netAlways = true;
			base.npc.HitSound = SoundID.NPCHit48;
			base.npc.DeathSound = SoundID.NPCDeath50;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 30; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public override bool CheckActive()
		{
			Player player = Main.player[base.npc.target];
			return !player.active || player.dead;
		}

		public override void AI()
		{
			Entity entity = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			if (entity.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 7.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc3 = base.npc;
				npc3.frame.Y = npc3.frame.Y + 72;
				if (base.npc.frame.Y > 216)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			int doll = (int)base.npc.ai[0];
			if (doll < 0 || doll >= 200 || !Main.npc[doll].active || Main.npc[doll].type != ModContent.NPCType<SoullessMarionette_Doll>())
			{
				base.npc.active = false;
			}
			base.npc.netUpdate = true;
			NPC npc2 = Main.npc[(int)base.npc.ai[0]];
			base.npc.ai[1] += 1f;
			if (base.npc.ai[1] % 30f == 0f)
			{
				int steps = (int)base.npc.Distance(npc2.Center) / 32;
				for (int i = 0; i < steps; i++)
				{
					if (Utils.NextBool(Main.rand, 8))
					{
						Dust dust = Dust.NewDustDirect(Vector2.Lerp(base.npc.Center, npc2.Center, (float)i / (float)steps), 2, 2, 261, 0f, 0f, 0, default(Color), 2f);
						dust.velocity *= 0f;
						dust.noGravity = true;
					}
				}
			}
			if (base.npc.Distance(npc2.Center) > 800f)
			{
				npc2.velocity = npc2.DirectionTo(base.npc.position) * 4f;
			}
		}

		public Player player;
	}
}
