using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class NebEye1 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eye of the Cosmos");
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 1;
			base.npc.damage = 80;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 74;
			base.npc.height = 82;
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

		public override void AI()
		{
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			this.startTimer++;
			if (this.startTimer <= 120)
			{
				base.npc.alpha -= 8;
			}
			if (this.startTimer == 60)
			{
				Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
				float num = 16f;
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num2 = 50;
				int num3 = base.mod.ProjectileType("PNebula1");
				int num4 = base.mod.ProjectileType("PNebula2");
				int num5 = base.mod.ProjectileType("PNebula3");
				float num6 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num6) * (double)num * -1.0), (float)(Math.Sin((double)num6) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
				Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num6) * (double)num * -1.0), (float)(Math.Sin((double)num6) * (double)num * -1.0), num4, num2, 0f, 0, 0f, 0f);
				Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num6) * (double)num * -1.0), (float)(Math.Sin((double)num6) * (double)num * -1.0), num5, num2, 0f, 0, 0f, 0f);
			}
			if (this.startTimer >= 120)
			{
				int num7 = 58;
				int num8 = 8;
				for (int i = 0; i < num8; i++)
				{
					int num9 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num7, 0f, 0f, 100, Color.White, 3f);
					Main.dust[num9].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)num8 * 6.28f);
					Main.dust[num9].noLight = false;
					Main.dust[num9].noGravity = true;
				}
				base.npc.active = false;
			}
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		private Player player;

		private int startTimer;
	}
}
