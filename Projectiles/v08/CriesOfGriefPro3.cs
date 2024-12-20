using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class CriesOfGriefPro3 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/v08/CriesOfGriefPro2";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cries of Grief");
			Main.projFrames[base.projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 42;
			base.projectile.height = 42;
			base.projectile.friendly = true;
			base.projectile.penetrate = 2;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = false;
			base.projectile.alpha = 0;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num489 = projectile.frameCounter + 1;
			projectile.frameCounter = num489;
			if (num489 >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num489 = projectile2.frame + 1;
				projectile2.frame = num489;
				if (num489 >= 2)
				{
					base.projectile.frame = 0;
				}
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritPierce)
			{
				base.projectile.penetrate = 4;
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f);
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			base.projectile.localAI[0] += 1f;
			int num487 = (int)base.projectile.ai[0];
			Vector2 vector36 = new Vector2(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num490 = Main.player[num487].Center.X - vector36.X;
			float num488 = Main.player[num487].Center.Y - vector36.Y;
			Math.Sqrt((double)(num490 * num490 + num488 * num488));
			if (base.projectile.position.X < Main.player[num487].position.X + (float)Main.player[num487].width && base.projectile.position.X + (float)base.projectile.width > Main.player[num487].position.X && base.projectile.position.Y < Main.player[num487].position.Y + (float)Main.player[num487].height && base.projectile.position.Y + (float)base.projectile.height > Main.player[num487].position.Y && base.projectile.owner == Main.myPlayer)
			{
				Main.player[num487].AddBuff(base.mod.BuffType("BlackenedHeartBuff2"), 300, true);
			}
			base.projectile.alpha += 5;
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(base.mod.BuffType("BlackenedHeartDebuff"), 260, false);
		}
	}
}
