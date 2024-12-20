using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Pets
{
	public class HalPetSPEEN : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/Pets/HalPet";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("SPEEN");
			Main.projFrames[base.projectile.type] = 16;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 20;
			base.projectile.height = 34;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 600;
		}

		public override void AI()
		{
			if (base.projectile.frame < 8)
			{
				base.projectile.frame = 8;
			}
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 15)
				{
					base.projectile.frame = 8;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			int dustType = 58;
			for (int i = 0; i < 8; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 4f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / 8f * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			int dustType2 = 59;
			for (int j = 0; j < 10; j++)
			{
				int dustID2 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType2, 0f, 0f, 100, Color.White, 4f);
				Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)j / 10f * 6.28f);
				Main.dust[dustID2].noLight = false;
				Main.dust[dustID2].noGravity = true;
			}
			int dustType3 = 60;
			for (int k = 0; k < 12; k++)
			{
				int dustID3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType3, 0f, 0f, 100, Color.White, 4f);
				Main.dust[dustID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)k / 12f * 6.28f);
				Main.dust[dustID3].noLight = false;
				Main.dust[dustID3].noGravity = true;
			}
			int dustType4 = 62;
			for (int l = 0; l < 14; l++)
			{
				int dustID4 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType4, 0f, 0f, 100, Color.White, 4f);
				Main.dust[dustID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(16f, 0f), (float)l / 14f * 6.28f);
				Main.dust[dustID4].noLight = false;
				Main.dust[dustID4].noGravity = true;
			}
		}

		public override bool? CanHitNPC(NPC target)
		{
			return new bool?(!target.boss && !target.friendly);
		}
	}
}
