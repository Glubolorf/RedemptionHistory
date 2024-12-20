using System;
using Redemption.Tiles.Ores;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
{
	public class PlutoniumBoom : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plutonium Boom");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 60;
			base.projectile.height = 60;
			base.projectile.friendly = true;
			base.projectile.alpha = 255;
			base.projectile.penetrate = -1;
			base.projectile.extraUpdates = 2;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			if (base.projectile.owner == Main.myPlayer)
			{
				this.Convert((int)(base.projectile.position.X + (float)(base.projectile.width / 2)) / 16, (int)(base.projectile.position.Y + (float)(base.projectile.height / 2)) / 16, 6);
			}
			if (base.projectile.timeLeft > 133)
			{
				base.projectile.timeLeft = 133;
			}
			base.projectile.rotation += 0.3f * (float)base.projectile.direction;
		}

		public void Convert(int i, int j, int size = 4)
		{
			for (int k = i - size; k <= i + size; k++)
			{
				for (int l = j - size; l <= j + size; l++)
				{
					if (WorldGen.InWorld(k, l, 1) && (double)(Math.Abs(k - i) + Math.Abs(l - j)) < Math.Sqrt((double)(size * size + size * size)))
					{
						int type = (int)Main.tile[k, l].type;
						if (TileID.Sets.Conversion.Stone[type] || TileID.Sets.Conversion.HardenedSand[type] || TileID.Sets.Conversion.Ice[type] || TileID.Sets.Conversion.Moss[type] || TileID.Sets.Conversion.Sand[type] || TileID.Sets.Conversion.Sandstone[type] || TileID.Sets.Conversion.Grass[type])
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<PlutoniumTile>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
					}
				}
			}
		}
	}
}
