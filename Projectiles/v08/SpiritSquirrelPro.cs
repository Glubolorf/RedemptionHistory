using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class SpiritSquirrelPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spirit Squirrel");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 44;
			base.projectile.height = 22;
			base.projectile.friendly = true;
			base.projectile.timeLeft = 180;
			base.projectile.penetrate = 2;
		}

		public override bool PreAI()
		{
			if (++base.projectile.frameCounter >= 3)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 5)
				{
					base.projectile.frame = 0;
				}
			}
			if (base.projectile.velocity.X < 0f)
			{
				base.projectile.spriteDirection = 1;
			}
			else
			{
				base.projectile.spriteDirection = -1;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).spiritPierce)
			{
				base.projectile.penetrate = 6;
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.2f / 255f, (float)(255 - base.projectile.alpha) * 0.2f / 255f, (float)(255 - base.projectile.alpha) * 0.4f / 255f);
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.4f;
			Projectile projectile2 = base.projectile;
			projectile2.velocity.X = projectile2.velocity.X * 1.008f;
			base.projectile.velocity.X = MathHelper.Clamp(base.projectile.velocity.X, -10f, 10f);
			return false;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14.WithVolume(0.3f), base.projectile.position);
			for (int i = 0; i < 10; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 68, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num].velocity *= 1.9f;
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[base.projectile.owner];
			int crit2 = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref crit2);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref crit2);
			if (crit2 >= 100 || Main.rand.Next(1, 101) <= crit2)
			{
				crit = true;
			}
			if (!Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).spiritPierce)
			{
				base.projectile.Kill();
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (oldVelocity.X != base.projectile.velocity.X)
			{
				base.projectile.velocity.X = -oldVelocity.X;
			}
			return false;
		}
	}
}
