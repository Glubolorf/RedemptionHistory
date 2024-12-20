using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.ChickenInvasion
{
	public class ChickenSwarmer : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chicken Swarmer");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 32;
			base.npc.height = 30;
			base.npc.friendly = false;
			base.npc.damage = 80;
			this.aiType = 0;
			base.npc.defense = 60;
			base.npc.lifeMax = 4000;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 15; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 5, 0f, 0f, 100, default(Color), 2.2f);
					Main.dust[dustIndex].velocity *= 1.8f;
				}
				if (base.npc.FindBuffIndex(24) != -1)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<FriedChicken>(), 1, false, 0, false, false);
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 3.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 32;
				if (base.npc.frame.Y > 96)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			this.Target();
			this.DespawnHandler();
			this.Move(new Vector2(0f, 0f));
			base.npc.spriteDirection = base.npc.direction;
			base.npc.rotation = base.npc.velocity.X * 0.05f;
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void Move(Vector2 offset)
		{
			this.speed = 14f;
			Vector2 move = this.player.Center + offset - base.npc.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			float turnResistance = 30f;
			move = (base.npc.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			base.npc.velocity = move;
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		private Player player;

		private float speed;
	}
}
