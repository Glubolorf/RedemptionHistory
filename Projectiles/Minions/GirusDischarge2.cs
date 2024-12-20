using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class GirusDischarge2 : ModProjectile
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
			base.DisplayName.SetDefault("Discharge");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 4;
			base.projectile.height = 4;
			base.projectile.aiStyle = 0;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.ranged = true;
			base.projectile.extraUpdates = 100;
			base.projectile.timeLeft = 800;
			base.projectile.penetrate = 1;
			base.projectile.tileCollide = false;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] > 9f)
			{
				for (int num447 = 0; num447 < 4; num447++)
				{
					Vector2 vector33 = base.projectile.position;
					vector33 -= base.projectile.velocity * ((float)num447 * 0.25f);
					base.projectile.alpha = 255;
					int num448 = Dust.NewDust(vector33, base.projectile.width, base.projectile.height, 235, 0f, 0f, 200, default(Color), 1f);
					Main.dust[num448].position = vector33;
					Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.013f;
					Main.dust[num448].velocity *= 0.2f;
					Main.dust[num448].noGravity = true;
				}
			}
			foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
			{
				if (base.projectile != proj && !proj.friendly && !proj.minion)
				{
					proj.Kill();
					base.projectile.Kill();
				}
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (!target.boss && target.knockBackResist != 0f)
			{
				target.AddBuff(ModContent.BuffType<StunnedDebuff>(), 60, false);
			}
		}
	}
}
