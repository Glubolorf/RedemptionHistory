using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.Items.Placeable.Furniture.Misc;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Misc
{
	public class SoulCandlesTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.Origin = new Point16(0, 2);
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Soul Candles");
			base.AddMapEntry(new Color(140, 140, 140), name);
			this.dustType = ModContent.DustType<VoidFlame>();
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player localPlayer = Main.LocalPlayer;
			Main.tile[i, j];
			if (!Enumerable.Any<Projectile>(Main.projectile, (Projectile projectile) => projectile.type == ModContent.ProjectileType<SoulCandlesPro>() && (projectile.modProjectile as SoulCandlesPro).Parent == Main.tile[i, j] && projectile.active) && Main.netMode != 1)
			{
				int aura = Projectile.NewProjectile(new Vector2((float)(i * 16 + 8), (float)(j * 16 + 16)), Vector2.Zero, ModContent.ProjectileType<SoulCandlesPro>(), 0, 0f, Main.myPlayer, 0f, 0f);
				(Main.projectile[aura].modProjectile as SoulCandlesPro).Parent = Main.tile[i, j];
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (Main.tile[i, j].frameX < 36)
			{
				r = 0.7f;
				g = 0.7f;
				b = 0.8f;
			}
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<SoulCandles>(), 1, false, 0, false, false);
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			Vector2 zero = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			int height = (tile.frameY == 36) ? 18 : 16;
			Main.spriteBatch.Draw(base.mod.GetTexture("Tiles/Furniture/Misc/SoulCandlesTile_Glow"), new Vector2((float)(i * 16 - (int)Main.screenPosition.X), (float)(j * 16 - (int)Main.screenPosition.Y)) + zero, new Rectangle?(new Rectangle((int)tile.frameX, (int)tile.frameY, 16, height)), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
	}
}
