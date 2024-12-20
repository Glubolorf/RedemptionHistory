using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class NebDefeated : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebuleus, Angel of the Cosmos");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 90;
			base.projectile.height = 78;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = true;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 20)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.velocity.X = 0f;
			if (RedeWorld.downedNebuleus || RedeConfigClient.Instance.NoBossText)
			{
				if (base.projectile.localAI[0] >= 120f)
				{
					base.projectile.Kill();
					return;
				}
			}
			else
			{
				if (base.projectile.localAI[0] == 60f)
				{
					Redemption.ShowText(base.projectile, 3, 21, 200);
					Redemption.GirusSilence = true;
				}
				if (base.projectile.localAI[0] == 260f)
				{
					Redemption.ShowText(base.projectile, 3, 22, 200);
				}
				if (base.projectile.localAI[0] == 460f)
				{
					Redemption.ShowText(base.projectile, 3, 23, 200);
				}
				if (base.projectile.localAI[0] == 660f)
				{
					Redemption.ShowText(base.projectile, 3, 24, 140);
				}
				if (base.projectile.localAI[0] == 800f)
				{
					Redemption.ShowText(base.projectile, 3, 25, 200);
				}
				if (base.projectile.localAI[0] == 1000f)
				{
					Redemption.ShowText(base.projectile, 3, 26, 280);
				}
				if (base.projectile.localAI[0] == 1280f)
				{
					Redemption.ShowText(base.projectile, 3, 27, 200);
				}
				if (base.projectile.localAI[0] == 1480f)
				{
					Redemption.ShowText(base.projectile, 3, 28, 180);
				}
				if (base.projectile.localAI[0] >= 1660f)
				{
					base.projectile.Kill();
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 0f)
			{
				base.projectile.velocity.X = oldVelocity.X * --0f;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0f)
			{
				base.projectile.velocity.Y = oldVelocity.Y * --0f;
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<NebDefeated2>(), 0, 3f, 255, 0f, 0f);
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			if (!RedeWorld.downedNebuleus)
			{
				RedeWorld.redemptionPoints -= 4;
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						CombatText.NewText(player2.getRect(), Color.Red, "-4", true, false);
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == ModContent.ItemType<RedemptionTeller>())
							{
								Main.NewText("<Chalice of Alignment> ... You... Oh no... I don't know if you can redeem yourself from that.", Color.DarkGoldenrod, false);
							}
						}
					}
				}
			}
			RedeWorld.downedNebuleus = true;
			Redemption.GirusSilence = false;
		}
	}
}
