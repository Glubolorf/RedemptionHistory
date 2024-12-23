﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Druid
{
	public class SpectralLord : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lunatic Vision");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 178;
			base.projectile.height = 226;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 300;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 255;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.scale *= 0f;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = false;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			base.projectile.netUpdate = true;
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
			base.projectile.position.X = player.Center.X - 89f;
			base.projectile.position.Y = player.Center.Y - 113f;
			if (base.projectile.scale < 1f)
			{
				base.projectile.alpha -= 5;
				base.projectile.scale += 0.02f;
				return;
			}
			base.projectile.scale = 1f;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 1f)
			{
				Main.PlaySound(SoundID.NPCDeath62, (int)player.position.X, (int)player.position.Y);
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), new Vector2(0f, 0f), ModContent.ProjectileType<SpectralLordEyes>(), base.projectile.damage * 2, 0f, Main.myPlayer, 0f, 0f);
			}
			base.projectile.alpha += 5;
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
		}
	}
}
