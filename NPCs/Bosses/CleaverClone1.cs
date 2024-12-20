using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	public class CleaverClone1 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Phantom Cleaver");
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 1;
			base.npc.damage = 120;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 46;
			base.npc.height = 280;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.npcSlots = 1f;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.netAlways = true;
			base.npc.alpha = 225;
			base.npc.dontTakeDamage = true;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 30; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num].velocity *= 1.6f;
				}
			}
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			this.Move(new Vector2(0f, -250f));
			base.npc.ai[1] += 1f;
			if (base.npc.ai[1] <= 120f)
			{
				base.npc.alpha -= 8;
			}
			if (base.npc.alpha <= 60)
			{
				this.summoned = true;
			}
			if (base.npc.ai[1] >= 120f)
			{
				this.targeted = true;
				base.npc.netUpdate = true;
			}
			if (this.targeted && !this.Smash)
			{
				base.npc.velocity.X = 0f;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] <= 50f)
				{
					base.npc.velocity.Y = 0f;
				}
				if (base.npc.ai[2] > 50f)
				{
					NPC npc = base.npc;
					npc.velocity.Y = npc.velocity.Y + 20f;
				}
				if (base.npc.ai[2] > 56f)
				{
					this.Smash = true;
					base.npc.netUpdate = true;
				}
			}
			if (this.Smash)
			{
				base.npc.velocity.X = 0f;
				NPC npc2 = base.npc;
				npc2.velocity.Y = npc2.velocity.Y + 10f;
				base.npc.alpha += 20;
				if (base.npc.alpha >= 255)
				{
					base.npc.active = false;
					base.npc.netUpdate = true;
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return this.summoned && !this.Smash;
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void Move(Vector2 offset)
		{
			if (!this.targeted || !this.Smash)
			{
				this.speed = 20f;
			}
			Vector2 vector = this.player.Center + offset;
			Vector2 vector2 = vector - base.npc.Center;
			float num = this.Magnitude(vector2);
			if (num > this.speed)
			{
				vector2 *= this.speed / num;
			}
			float num2 = 3f;
			vector2 = (base.npc.velocity * num2 + vector2) / (num2 + 1f);
			num = this.Magnitude(vector2);
			if (num > this.speed)
			{
				vector2 *= this.speed / num;
			}
			base.npc.velocity = vector2;
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
				}
			}
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		private Player player;

		private float speed;

		private bool targeted;

		private bool Smash;

		private bool summoned;
	}
}
