using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Minibosses.MossyGoliath
{
	public class TastySteakPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tasty Steak");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(24);
			this.aiType = 24;
			base.projectile.melee = false;
			base.projectile.ranged = false;
			base.projectile.thrown = false;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 160;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromSeedbag = true;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.2f;
			Point point = Utils.ToTileCoordinates(base.projectile.Bottom);
			float[] localAI = base.projectile.localAI;
			int num = 0;
			float num2 = localAI[num] + 1f;
			localAI[num] = num2;
			if (num2 == 140f)
			{
				if (Main.tile[point.X, point.Y + 1].type == 59 || Main.tile[point.X, point.Y].type == 60)
				{
					this.damage = 40;
				}
				base.projectile.velocity.X = 0f;
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.position.Y + 66f), new Vector2(0f, -4f), ModContent.ProjectileType<MossGoliath_nom>(), base.projectile.damage + this.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 5, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.6f;
			}
		}

		public int damage;
	}
}
