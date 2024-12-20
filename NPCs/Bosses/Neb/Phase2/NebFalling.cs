using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb.Phase2
{
	public class NebFalling : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebuleus, Angel of the Cosmos");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 64;
			base.projectile.height = 62;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 58, 0f, 0f, 100, default(Color), 3f);
			}
			base.projectile.localAI[0] += 1f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.2f;
			base.projectile.rotation += 0.01f;
			player.GetModPlayer<ScreenPlayer>().ScreenFocusPosition = base.projectile.Center;
			player.GetModPlayer<ScreenPlayer>().lockScreen = true;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override void Kill(int timeLeft)
		{
			if (base.projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<NebDefeat>(), 0, 3f, Main.myPlayer, 0f, 0f);
			}
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 58, 0f, 0f, 100, default(Color), 4f);
				Main.dust[dustIndex].velocity *= 1.9f;
			}
		}
	}
}
