using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Soulless
{
	public class SoullessMarionette_Doll : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Soulless Marionette");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 11000;
			base.npc.damage = 90;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 36;
			base.npc.height = 92;
			base.npc.value = (float)Item.buyPrice(0, 1, 0, 0);
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
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 96;
				if (base.npc.frame.Y > 288)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			this.Move(new Vector2(0f, 0f));
			if (base.npc.ai[1] == 0f)
			{
				int Minion = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y - 170, ModContent.NPCType<SoullessMarionette_Cross>(), 0, (float)base.npc.whoAmI, 0f, 0f, 0f, 255);
				Main.npc[Minion].netUpdate = true;
				base.npc.ai[1] = 1f;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<SoullessMarionette_Cross>()))
			{
				base.npc.dontTakeDamage = true;
				return;
			}
			base.npc.dontTakeDamage = false;
			base.npc.netUpdate = true;
		}

		public void Move(Vector2 offset)
		{
			Entity entity = Main.player[base.npc.target];
			if (NPC.AnyNPCs(ModContent.NPCType<SoullessMarionette_Cross>()))
			{
				this.speed = 2f;
			}
			else
			{
				this.speed = 14f;
			}
			Vector2 move = entity.Center + offset - base.npc.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			float turnResistance = 40f;
			move = (base.npc.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			base.npc.velocity = move;
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public Player player;

		public float speed;
	}
}
