using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Furniture.Misc
{
	public class SoulCandlesPro : ModProjectile
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
			base.DisplayName.SetDefault("Soul Candles");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 100;
			base.projectile.height = 100;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			if (!this.Parent.active())
			{
				base.projectile.Kill();
			}
			base.projectile.timeLeft = 10;
			for (int i = 0; i < 200; i++)
			{
				this.target = Main.npc[i];
				if (!this.target.boss && this.target.Distance(base.projectile.Center) <= 100f && Lists.IsSoulless.Contains(this.target.type))
				{
					player.ApplyDamageToNPC(this.target, 9999, 0f, 0, false);
				}
			}
			for (int p = 0; p < 255; p++)
			{
				this.playerTarget = Main.player[p];
				if (this.playerTarget.active && !this.playerTarget.dead && this.playerTarget.Distance(base.projectile.Center) <= 100f)
				{
					if (this.playerTarget.lifeRegen > 0)
					{
						this.playerTarget.lifeRegen = 0;
					}
					if (this.playerTarget.lifeRegenCount > 0)
					{
						this.playerTarget.lifeRegenCount = 0;
					}
					this.playerTarget.lifeRegenTime = 0;
				}
			}
			for (int j = 0; j < 2; j++)
			{
				double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector.X = (float)(Math.Sin(angle) * 100.0);
				this.vector.Y = (float)(Math.Cos(angle) * 100.0);
				Dust dust2 = Main.dust[Dust.NewDust(base.projectile.Center + this.vector, 2, 2, 261, 0f, 0f, 100, default(Color), 1f)];
				dust2.noGravity = true;
				dust2.velocity = -base.projectile.DirectionTo(dust2.position) * 4f;
			}
		}

		public Tile Parent;

		private NPC target;

		private Player playerTarget;

		private Vector2 vector;
	}
}
