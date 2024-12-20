using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Ranged
{
	public class ShadeKnifePro : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Weapons/PostML/Ranged/ShadeKnife";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shade Knife");
		}

		public override void SetDefaults()
		{
			base.projectile.ranged = true;
			base.projectile.width = 34;
			base.projectile.height = 34;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.penetrate = 1;
		}

		public override void AI()
		{
			base.projectile.rotation += base.projectile.velocity.X / 40f * (float)base.projectile.direction;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
			float[] ai = base.projectile.ai;
			int num = 0;
			float num2 = ai[num] + 1f;
			ai[num] = num2;
			if (num2 >= 20f)
			{
				Main.PlaySound(SoundID.NPCDeath39.WithVolume(0.5f), (int)base.projectile.position.X, (int)base.projectile.position.Y);
				if (Main.myPlayer == base.projectile.owner)
				{
					for (int i = 0; i < Main.rand.Next(2, 4); i++)
					{
						Projectile.NewProjectile(base.projectile.Center, new Vector2(Utils.NextFloat(Main.rand, -8f, 8f), Utils.NextFloat(Main.rand, -8f, 8f)), ModContent.ProjectileType<CandleLightPro>(), base.projectile.damage / 2, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
					}
				}
				base.projectile.Kill();
			}
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = (height = 10);
			return true;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			return new bool?(projHitbox.Intersects(targetHitbox));
		}

		public override void Kill(int timeLeft)
		{
			Vector2 usePos = base.projectile.position;
			Vector2 rotVector = Utils.ToRotationVector2(base.projectile.rotation - MathHelper.ToRadians(90f));
			usePos += rotVector * 16f;
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustDirect(usePos, base.projectile.width, base.projectile.height, 261, 0f, 0f, 0, default(Color), 1f);
				dust.position = (dust.position + base.projectile.Center) / 2f;
				dust.velocity += rotVector * 2f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
				usePos -= rotVector * 8f;
			}
		}
	}
}
