using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Soulless
{
	public class LaughingMaskSmall : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Laughing Mask");
			Main.npcFrameCount[base.npc.type] = 3;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 8000;
			base.npc.damage = 80;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 20;
			base.npc.height = 26;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.HitSound = SoundID.NPCHit48;
			base.npc.DeathSound = SoundID.NPCDeath50;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 20; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
			}
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			BaseAI.LookAt((player != null && player.active && !player.dead) ? player.Center : (base.npc.Center + base.npc.velocity), base.npc, 0, 0f, 0.1f, false);
			if (base.npc.ai[1] == 1f)
			{
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 30;
					if (base.npc.frame.Y > 60)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 30;
					}
				}
			}
			else
			{
				base.npc.frame.Y = 0;
			}
			float[] ai = base.npc.ai;
			int num55 = 0;
			float num56 = ai[num55] + 1f;
			ai[num55] = num56;
			if (num56 % 60f == 0f)
			{
				double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector.X = (float)(Math.Sin(angle) * 700.0);
				this.vector.Y = (float)(Math.Cos(angle) * 700.0);
			}
			if (Vector2.Distance(base.npc.Center, player.Center) < 90f && base.npc.ai[1] == 0f)
			{
				if (!Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MaskLaugh1").WithVolume(0.5f).WithPitchVariance(0.1f), -1, -1);
				}
				float Speed = 15f;
				Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int damage = 28;
				int type = 293;
				float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
				int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
				Main.projectile[num54].netUpdate = true;
				base.npc.ai[1] = 1f;
			}
			if (base.npc.ai[1] == 1f)
			{
				base.npc.velocity = -base.npc.DirectionTo(player.Center) * 7f;
				if (Vector2.Distance(base.npc.Center, player.Center) > 2000f)
				{
					base.npc.active = false;
					return;
				}
			}
			else
			{
				this.Move(new Vector2(this.vector.X, this.vector.Y));
			}
		}

		public void Move(Vector2 offset)
		{
			Player player = Main.player[base.npc.target];
			if (Vector2.Distance(base.npc.Center, player.Center) < 1000f)
			{
				this.speed = 2f;
			}
			else
			{
				this.speed = 14f;
			}
			Vector2 move = player.Center + offset - base.npc.Center;
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

		private Vector2 vector;
	}
}
