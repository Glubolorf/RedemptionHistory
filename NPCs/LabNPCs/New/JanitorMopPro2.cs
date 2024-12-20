using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class JanitorMopPro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mop");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 30;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 300;
			base.projectile.ignoreWater = false;
			base.projectile.tileCollide = true;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			if (base.projectile.velocity.X > 0f)
			{
				base.projectile.rotation += 0.5f;
			}
			else
			{
				base.projectile.rotation -= 0.5f;
			}
			if (base.projectile.localAI[0] >= 30f)
			{
				base.projectile.friendly = true;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			for (int i = 0; i < 4; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 78, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.3f;
			}
			if (base.projectile.velocity.X != oldVelocity.X)
			{
				base.projectile.velocity.X = -oldVelocity.X;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y)
			{
				base.projectile.velocity.Y = -oldVelocity.Y;
			}
			base.projectile.velocity *= 0.95f;
			return false;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (target.type == ModContent.NPCType<JanitorBot>())
			{
				target.AddBuff(ModContent.BuffType<JanitorStun>(), 5, false);
			}
		}

		public override void Kill(int timeleft)
		{
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			for (int num468 = 0; num468 < 8; num468++)
			{
				num468 = Dust.NewDust(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), base.projectile.width, base.projectile.height, 78, -base.projectile.velocity.X * 0.2f, -base.projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
			}
		}
	}
}
