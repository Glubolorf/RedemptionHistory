using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class AttackHovercopterPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Attack Hovercopter");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 184;
			base.projectile.height = 50;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = 1;
			base.projectile.tileCollide = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			if (Main.rand.Next(5) == 0)
			{
				int num = Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 31, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Main.dust[num].noGravity = true;
			}
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.2f;
			base.projectile.rotation += 0.01f;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, base.projectile.position);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/AttackHovercopterGore1"), 1f);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/AttackHovercopterGore2"), 1f);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/AttackHovercopterGore3"), 1f);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/AttackHovercopterGore4"), 1f);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/AttackHovercopterGore5"), 1f);
			Gore.NewGore(base.projectile.position, -base.projectile.velocity * 1.5f, base.mod.GetGoreSlot("Gores/AttackHovercopterGore7"), 1f);
			for (int i = 0; i < 10; i++)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), 686, 40, 3f, 255, 0f, 0f);
			}
			for (int j = 0; j < 25; j++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 235, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num].velocity *= 1.9f;
			}
			for (int k = 0; k < 65; k++)
			{
				int num2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 5f);
				Main.dust[num2].velocity *= 4.8f;
			}
			for (int l = 0; l < 45; l++)
			{
				int num3 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 5f);
				Main.dust[num3].velocity *= 2.3f;
			}
		}
	}
}
