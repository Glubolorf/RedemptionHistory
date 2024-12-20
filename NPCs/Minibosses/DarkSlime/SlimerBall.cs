using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Minibosses.DarkSlime
{
	public class SlimerBall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Slimer Sludge");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 28;
			base.projectile.friendly = false;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 120;
		}

		public override void AI()
		{
			base.projectile.rotation += base.projectile.velocity.X / 40f * (float)base.projectile.direction;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCDeath1, base.projectile.position);
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 98, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			}
			if (Main.netMode != 1)
			{
				int j = NPC.NewNPC((int)base.projectile.Center.X, (int)base.projectile.Center.Y, 121, 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[j].lifeMax = 400;
				Main.npc[j].life = 400;
				Main.npc[j].noTileCollide = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(23, -1, -1, null, j, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}
	}
}
