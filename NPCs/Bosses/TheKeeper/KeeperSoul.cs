using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.NPCs.Bosses.EaglecrestGolem;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.TheKeeper
{
	public class KeeperSoul : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Keeper");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 106;
			base.projectile.height = 140;
			base.projectile.friendly = false;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 500;
		}

		public override void AI()
		{
			Redemption.GirusSilence = true;
			Player player = Main.player[base.projectile.owner];
			if (base.projectile.timeLeft < 180)
			{
				for (int i = 0; i < 6; i++)
				{
					double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
					this.vector.X = (float)(Math.Sin(angle) * 100.0);
					this.vector.Y = (float)(Math.Cos(angle) * 100.0);
					Dust dust2 = Main.dust[Dust.NewDust(base.projectile.Center + this.vector, 2, 2, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 3f)];
					dust2.noGravity = true;
					dust2.velocity = -base.projectile.DirectionTo(dust2.position) * 10f;
				}
				for (int j = 0; j < 2; j++)
				{
					double angle2 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
					this.vector.X = (float)(Math.Sin(angle2) * 150.0);
					this.vector.Y = (float)(Math.Cos(angle2) * 150.0);
					Dust dust3 = Main.dust[Dust.NewDust(base.projectile.Center + this.vector, 2, 2, 261, 0f, 0f, 100, default(Color), 3f)];
					dust3.noGravity = true;
					dust3.velocity = -base.projectile.DirectionTo(dust3.position) * 10f;
				}
				player.GetModPlayer<ShakeScreen>().shakeSubtle = true;
			}
		}

		public override void Kill(int timeleft)
		{
			int dustType = 261;
			int pieCut = 40;
			for (int i = 0; i < pieCut; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			Redemption.GirusSilence = false;
		}

		public Vector2 vector;
	}
}
