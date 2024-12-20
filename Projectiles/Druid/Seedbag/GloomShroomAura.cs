using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Redemption.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class GloomShroomAura : ModProjectile
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
			base.DisplayName.SetDefault("Gloom Shroom Aura");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 200;
			base.projectile.height = 200;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 20;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromSeedbag = true;
		}

		public override void AI()
		{
			if (Main.rand.Next(4) == 0)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, ModContent.DustType<ShroomDust1>(), base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			for (int p = 0; p < 200; p++)
			{
				this.clearCheck = Main.npc[p];
				if (!this.clearCheck.immortal && !this.clearCheck.dontTakeDamage && Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck.position, this.clearCheck.Size))
				{
					this.clearCheck.AddBuff(ModContent.BuffType<GloomShroomDebuff>(), 200, false);
					Dust dust = Dust.NewDustDirect(this.clearCheck.position, this.clearCheck.width, this.clearCheck.height, ModContent.DustType<ShroomDust1>(), 0f, 0f, 100, default(Color), 1f);
					dust.velocity = -this.clearCheck.DirectionTo(dust.position);
				}
			}
		}

		private NPC clearCheck;
	}
}
