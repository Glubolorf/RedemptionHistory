using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Soulless
{
	public class Echo : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Echo");
			Main.npcFrameCount[base.npc.type] = 10;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 1;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 16;
			base.npc.height = 32;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.dontTakeDamage = true;
			base.npc.npcSlots = 0f;
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			if (player.Center.X > base.npc.Center.X)
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
				npc.frame.Y = npc.frame.Y + 54;
				if (base.npc.frame.Y >= 540)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			this.Move(new Vector2(0f, 0f), 2f);
			if (base.npc.Distance(player.Center) < 200f)
			{
				base.npc.ai[0] = 1f;
			}
			if (base.npc.ai[0] == 1f)
			{
				base.npc.alpha += 4;
				if (base.npc.alpha >= 255)
				{
					base.npc.active = false;
				}
			}
		}

		public void Move(Vector2 offset, float speed = 2f)
		{
			Vector2 move = Main.player[base.npc.target].Center + offset - base.npc.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			float turnResistance = 40f;
			move = (base.npc.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = this.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			base.npc.velocity = move;
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}
	}
}
