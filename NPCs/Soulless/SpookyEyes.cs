using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Soulless
{
	public class SpookyEyes : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eyes");
			Main.npcFrameCount[base.npc.type] = 12;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 1;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 130;
			base.npc.height = 34;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.dontTakeDamage = true;
			base.npc.immortal = true;
			base.npc.chaseable = false;
			base.npc.npcSlots = 0f;
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			if (base.npc.ai[1] == 0f)
			{
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 36;
					if (base.npc.frame.Y > 144)
					{
						base.npc.frame.Y = 144;
						if (Main.rand.Next(60) == 0)
						{
							base.npc.ai[1] = 1f;
						}
					}
				}
			}
			else if (base.npc.ai[1] == 1f)
			{
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc2 = base.npc;
					npc2.frame.Y = npc2.frame.Y + 36;
					if (base.npc.frame.Y > 252)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 144;
						base.npc.ai[1] = 0f;
					}
				}
			}
			else if (base.npc.ai[1] == 2f)
			{
				if (base.npc.frame.Y < 252)
				{
					base.npc.frame.Y = 252;
				}
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc3 = base.npc;
					npc3.frame.Y = npc3.frame.Y + 36;
					if (base.npc.frame.Y > 396)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 396;
						base.npc.alpha = 255;
						base.npc.active = false;
					}
				}
			}
			if (Vector2.Distance(base.npc.Center, player.Center) < 800f)
			{
				base.npc.ai[1] = 2f;
			}
		}
	}
}
