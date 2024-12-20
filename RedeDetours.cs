using System;
using On.Terraria;
using Terraria;

namespace Redemption
{
	public static class RedeDetours
	{
		public static void Initialize()
		{
			Main.DrawProjectiles += new Main.hook_DrawProjectiles(RedeDetours.Main_DrawProjectiles);
			Projectile.NewProjectile_float_float_float_float_int_int_float_int_float_float += new Projectile.hook_NewProjectile_float_float_float_float_int_int_float_int_float_float(RedeDetours.Projectile_NewProjectile);
		}

		private static void Main_DrawProjectiles(Main.orig_DrawProjectiles orig, Main self)
		{
			Redemption.TrailManager.DrawTrails(Main.spriteBatch);
			orig.Invoke(self);
		}

		private static int Projectile_NewProjectile(Projectile.orig_NewProjectile_float_float_float_float_int_int_float_int_float_float orig, float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner, float ai0, float ai1)
		{
			int index = orig.Invoke(X, Y, SpeedX, SpeedY, Type, Damage, KnockBack, Owner, ai0, ai1);
			Projectile projectile = Main.projectile[index];
			if (Main.netMode != 2)
			{
				Redemption.TrailManager.DoTrailCreation(projectile);
			}
			return index;
		}
	}
}
