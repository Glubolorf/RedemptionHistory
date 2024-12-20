using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Wasteland;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Hostile
{
	internal class NukeShockwave : ModProjectile
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
			base.DisplayName.SetDefault("Shockwave");
		}

		public override void SetDefaults()
		{
			base.projectile.width = (base.projectile.height = 1000);
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.hostile = true;
			base.projectile.friendly = true;
			base.projectile.scale = 1f;
			base.projectile.timeLeft = 240;
			base.projectile.penetrate = -1;
		}

		public override void AI()
		{
			for (int i = 0; i < 30; i++)
			{
				Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 6, base.projectile.velocity.X, base.projectile.velocity.Y, 0, default(Color), 3f);
				Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 31, base.projectile.velocity.X, base.projectile.velocity.Y, 0, default(Color), 3f);
			}
			base.projectile.height += Math.Abs((int)(base.projectile.velocity.X / 4f));
			base.projectile.width += Math.Abs((int)(base.projectile.velocity.Y / 4f));
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(24, 900, false);
			Vector2 groundZeroPos = new Vector2(base.projectile.ai[0], base.projectile.ai[1]);
			Vector2 throwVelocity = target.Center - groundZeroPos;
			throwVelocity.Normalize();
			throwVelocity *= base.projectile.velocity.Length() / 4f;
			target.velocity += throwVelocity;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (!target.behindBackWall)
			{
				target.AddBuff(24, 600, true);
				target.AddBuff(ModContent.BuffType<SkinBurnDebuff>(), 900, true);
				target.AddBuff(ModContent.BuffType<RadiationDebuff>(), 300, true);
				Vector2 groundZeroPos = new Vector2(base.projectile.ai[0], base.projectile.ai[1]);
				Vector2 throwVelocity = target.Center - groundZeroPos;
				throwVelocity.Normalize();
				throwVelocity *= base.projectile.velocity.Length() / 4f;
				target.velocity += throwVelocity;
			}
			Main.PlaySound(Redemption.Inst.GetLegacySoundSlot(50, "Sounds/Custom/NukeExplosion"), target.Center);
		}
	}
}
