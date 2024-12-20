using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class BlisterBubblePro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Toxic Bubble");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(523);
			this.aiType = 523;
			base.projectile.width = 32;
			base.projectile.height = 32;
			base.projectile.penetrate = 1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 160;
		}

		public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 256, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num].noGravity = true;
			}
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item54, base.projectile.position);
			for (int i = 0; i < 30; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 256, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num].velocity *= 1.9f;
			}
			if (Main.netMode != 1)
			{
				if (Main.rand.Next(50) == 0)
				{
					int num2 = NPC.NewNPC((int)base.projectile.position.X, (int)base.projectile.position.Y, base.mod.NPCType("Stage2Scientist"), 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (Main.rand.Next(3) == 0)
				{
					int num3 = NPC.NewNPC((int)base.projectile.position.X, (int)base.projectile.position.Y, base.mod.NPCType("Blisterling"), 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, num3, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			base.projectile.Kill();
		}
	}
}
