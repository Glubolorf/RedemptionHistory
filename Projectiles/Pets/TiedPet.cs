using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Pets
{
	public class TiedPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tied");
			Main.projFrames[base.projectile.type] = 16;
			Main.projPet[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(236);
			base.projectile.width = 20;
			base.projectile.height = 34;
			this.aiType = 236;
		}

		public override bool PreAI()
		{
			Main.player[base.projectile.owner].dino = false;
			return true;
		}

		public override void AI()
		{
			if (base.projectile.velocity.Y >= 1f || base.projectile.velocity.Y <= -1f)
			{
				if (base.projectile.frame < 8)
				{
					base.projectile.frame = 8;
				}
				Projectile projectile = base.projectile;
				int num = projectile.frameCounter + 1;
				projectile.frameCounter = num;
				if (num >= 5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile2 = base.projectile;
					num = projectile2.frame + 1;
					projectile2.frame = num;
					if (num >= 15)
					{
						base.projectile.frame = 8;
					}
				}
			}
			else if (base.projectile.velocity.X == 0f)
			{
				base.projectile.frame = 0;
			}
			else
			{
				base.projectile.frameCounter += (int)(base.projectile.velocity.X * 0.5f);
				if (base.projectile.frameCounter >= 5 || base.projectile.frameCounter <= -5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile3 = base.projectile;
					int num = projectile3.frame + 1;
					projectile3.frame = num;
					if (num >= 7)
					{
						base.projectile.frame = 1;
					}
				}
			}
			Player player = Main.player[base.projectile.owner];
			if ((player.ZoneDesert || player.ZoneUndergroundDesert || player.ZoneUnderworldHeight) && Main.rand.Next(4) == 0)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 33, 0f, 0f, 100, default(Color), 1f);
				Main.dust[dustIndex].noGravity = false;
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = 0f;
				dust.velocity.Y = 0f;
			}
			if (Main.rand.Next(20000) == 0)
			{
				switch (Main.rand.Next(7))
				{
				case 0:
					CombatText.NewText(base.projectile.getRect(), Color.Green, "Boo!", false, false);
					break;
				case 1:
					CombatText.NewText(base.projectile.getRect(), Color.Green, "Got a sauna?", false, false);
					break;
				case 2:
					CombatText.NewText(base.projectile.getRect(), Color.Green, "spooky scary skeletons~", false, false);
					break;
				case 3:
					CombatText.NewText(base.projectile.getRect(), Color.Green, "noniin...", false, false);
					break;
				case 4:
					CombatText.NewText(base.projectile.getRect(), Color.Green, "My ladle knows no mercy.", false, false);
					break;
				case 5:
					CombatText.NewText(base.projectile.getRect(), Color.Green, "Too dapper for the dagger", false, false);
					break;
				case 6:
					CombatText.NewText(base.projectile.getRect(), Color.Green, "ded", false, false);
					break;
				}
			}
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead)
			{
				modPlayer.tiedPet = false;
			}
			if (modPlayer.tiedPet)
			{
				base.projectile.timeLeft = 2;
			}
		}
	}
}
