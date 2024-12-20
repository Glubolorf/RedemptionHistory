using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.PreHM
{
	public class AncientStonePillar_Pro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Gladestone Pillar");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 34;
			base.projectile.height = 110;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 180;
			base.projectile.alpha = 255;
			base.projectile.tileCollide = false;
			base.projectile.hide = true;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindNPCsAndTiles.Add(index);
		}

		public override void AI()
		{
			if (Main.rand.Next(2) == 0 && base.projectile.localAI[0] < 30f)
			{
				Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 1, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 2f);
			}
			if (base.projectile.velocity.Length() != 0f)
			{
				base.projectile.hostile = true;
			}
			else
			{
				base.projectile.hostile = false;
			}
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] < 30f)
			{
				base.projectile.alpha -= 10;
			}
			if (base.projectile.localAI[0] == 30f)
			{
				base.projectile.hostile = true;
				if (!Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/EarthBoom").WithVolume(0.3f), (int)base.projectile.position.X, (int)base.projectile.position.Y);
				}
				Projectile projectile = base.projectile;
				projectile.velocity.Y = projectile.velocity.Y - 10f;
			}
			if (base.projectile.localAI[0] == 40f)
			{
				base.projectile.velocity.Y = 0f;
			}
			if (base.projectile.localAI[0] > 45f)
			{
				base.projectile.alpha += 10;
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
				}
			}
			for (int p = 0; p < 255; p++)
			{
				this.clearCheck = Main.player[p];
				if (base.projectile.velocity.Length() != 0f && Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck.position, this.clearCheck.Size))
				{
					this.clearCheck.velocity.Y = base.projectile.velocity.Y * 1.5f;
				}
			}
		}

		private Player clearCheck;
	}
}
