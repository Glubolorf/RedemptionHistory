using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	internal class DaggerStabPro : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Weapons/PostML/Melee/DaggerOfOathkeeper";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dagger of the Oathkeeper");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 40;
			base.projectile.height = 40;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.ownerHitCheck = true;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 60;
		}

		public override void AI()
		{
			Player projOwner = Main.player[base.projectile.owner];
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] >= 40f && this.newX != 0f && base.projectile.localAI[0] < 80f)
			{
				this.newX += -4f;
			}
			if (this.newX == 0f || base.projectile.localAI[0] >= 60f)
			{
				this.newX = 0f;
				base.projectile.Kill();
			}
			base.projectile.rotation = MathHelper.ToRadians(225f) * (float)projOwner.direction;
			base.projectile.direction = projOwner.direction;
			base.projectile.spriteDirection = projOwner.direction;
			projOwner.heldProj = base.projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			base.projectile.position.X = projOwner.Center.X - (float)(base.projectile.width / 2) + this.newX * (float)projOwner.direction;
			base.projectile.position.Y = projOwner.Center.Y - (float)(base.projectile.height / 2);
		}

		public override void Kill(int timeLeft)
		{
			Player projOwner = Main.player[base.projectile.owner];
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.Bottom.Y), base.projectile.width, 2, 5, 0f, 0f, 100, default(Color), 2f);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X * (-6f * (float)projOwner.direction);
			}
			Main.PlaySound(SoundID.NPCDeath19, base.projectile.position);
			projOwner.AddBuff(ModContent.BuffType<DaggerBuff>(), 7200, true);
		}

		public float newX = 60f;
	}
}
