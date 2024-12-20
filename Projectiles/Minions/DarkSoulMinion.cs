using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class DarkSoulMinion : Minion2
	{
		public override void SetDefaults()
		{
			base.projectile.netImportant = true;
			base.projectile.CloneDefaults(388);
			this.aiType = 388;
			base.projectile.width = 26;
			base.projectile.height = 20;
			base.projectile.friendly = true;
			base.projectile.minion = true;
			base.projectile.minionSlots = 1f;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 300;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			ProjectileID.Sets.MinionTargettingFeature[base.projectile.type] = true;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dark Soul");
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCDeath6, base.projectile.position);
			Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<MiniDarkSoulPro>(), 5, 2f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<MiniDarkSoulPro>(), 5, 2f, Main.myPlayer, 0f, 0f);
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<MiniDarkSoulPro>(), 5, 2f, Main.myPlayer, 0f, 0f);
			}
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<MiniDarkSoulPro>(), 5, 2f, Main.myPlayer, 0f, 0f);
			}
		}

		public override void AI()
		{
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 0, default(Color), 1f);
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.velocity.X != oldVelocity.X)
			{
				base.projectile.velocity.X = oldVelocity.X;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y)
			{
				base.projectile.velocity.Y = oldVelocity.Y;
			}
			return false;
		}

		public override void CheckActive()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if (player.dead)
			{
				modPlayer.darkSoulMinion = false;
			}
			if (modPlayer.darkSoulMinion)
			{
				base.projectile.timeLeft = 2;
			}
		}
	}
}
