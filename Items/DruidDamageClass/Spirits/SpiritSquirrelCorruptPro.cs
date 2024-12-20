using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	public class SpiritSquirrelCorruptPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Spirit Squirrel");
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
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 5)
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
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritPierce)
			{
				base.projectile.penetrate = 6;
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.2f / 255f, (float)(255 - base.projectile.alpha) * 0.2f / 255f, (float)(255 - base.projectile.alpha) * 0.4f / 255f);
			Projectile projectile3 = base.projectile;
			projectile3.velocity.Y = projectile3.velocity.Y + 0.4f;
			Projectile projectile4 = base.projectile;
			projectile4.velocity.X = projectile4.velocity.X * 1.008f;
			base.projectile.velocity.X = MathHelper.Clamp(base.projectile.velocity.X, -10f, 10f);
			return false;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14.WithVolume(0.3f), base.projectile.position);
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 27, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.9f;
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[base.projectile.owner];
			int critChance = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref critChance);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref critChance);
			if (critChance >= 100 || Main.rand.Next(1, 101) <= critChance)
			{
				crit = true;
			}
			if (!Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritPierce)
			{
				base.projectile.Kill();
			}
			target.AddBuff(39, 180, false);
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
