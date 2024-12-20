using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class GoldenOrange : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Golden Orange");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(261);
			this.aiType = 261;
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.magic = false;
			base.projectile.penetrate = 6;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			int num487 = (int)base.projectile.ai[0];
			Vector2 vector36 = new Vector2(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num489 = Main.player[num487].Center.X - vector36.X;
			float num488 = Main.player[num487].Center.Y - vector36.Y;
			Math.Sqrt((double)(num489 * num489 + num488 * num488));
			if (base.projectile.position.X < Main.player[num487].position.X + (float)Main.player[num487].width && base.projectile.position.X + (float)base.projectile.width > Main.player[num487].position.X && base.projectile.position.Y < Main.player[num487].position.Y + (float)Main.player[num487].height && base.projectile.position.Y + (float)base.projectile.height > Main.player[num487].position.Y)
			{
				Main.PlaySound(SoundID.Item2.WithVolume(0.3f), base.projectile.position);
				if (base.projectile.owner == Main.myPlayer)
				{
					Main.player[num487].AddBuff(26, 300, false);
				}
				for (int i = 0; i < 10; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 160, 0f, 0f, 100, default(Color), 1.8f);
					Main.dust[dustIndex].velocity *= 1.3f;
				}
				base.projectile.Kill();
			}
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 160, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
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
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().frostburnSeedbag)
			{
				target.AddBuff(44, 160, false);
			}
		}
	}
}
