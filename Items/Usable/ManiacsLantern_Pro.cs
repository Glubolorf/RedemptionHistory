using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class ManiacsLantern_Pro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Maniac's Lantern");
			Main.projFrames[base.projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 26;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 10)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 2)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.rotation = base.projectile.velocity.X * 0.05f;
			base.projectile.timeLeft = 10;
			if (player.HeldItem.type != ModContent.ItemType<ManiacsLantern>() || !player.active || player.dead)
			{
				base.projectile.Kill();
			}
			if (player.direction == 1)
			{
				base.projectile.spriteDirection = 1;
			}
			else
			{
				base.projectile.spriteDirection = -1;
			}
			Lighting.AddLight(base.projectile.Center, 1f, 1f, 1f);
			if (base.projectile.localAI[0] == 0f)
			{
				this.moveY = player.Center.Y - 40f;
				base.projectile.localAI[0] = 1f;
			}
			base.projectile.MoveToVector2(new Vector2((player.direction == 1) ? (player.Center.X + 40f) : (player.Center.X - 40f), this.moveY), 20f);
			this.MoveClamp();
			if (base.projectile.Distance(player.Center) > 2000f)
			{
				base.projectile.Kill();
			}
			for (int p = 0; p < 200; p++)
			{
				this.target = Main.npc[p];
				if (Lists.IsSoulless.Contains(this.target.type) && !this.target.boss && !this.target.friendly && base.projectile.Distance(this.target.Center) < 140f)
				{
					this.target.velocity -= RedeHelper.PolarVector(0.3f, Utils.ToRotation(base.projectile.Center - this.target.Center));
				}
			}
		}

		public void MoveClamp()
		{
			Player player = Main.player[base.projectile.owner];
			if (this.moveY < player.Center.Y - 40f)
			{
				this.moveY = player.Center.Y - 40f;
				return;
			}
			if (this.moveY > player.Center.Y + 10f)
			{
				this.moveY = player.Center.Y + 10f;
			}
		}

		public float moveY;

		private NPC target;
	}
}
