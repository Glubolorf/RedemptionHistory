using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Tiles.Tiles;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class ZoneAccessPanel2A : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Usable/ZoneAccessPanel2";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lab Access Panel - Beta");
			BaseUtility.AddTooltips(base.item, new string[]
			{
				"Opens up the beta sector of the lab"
			});
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 20;
			base.item.maxStack = 1;
			base.item.rare = 7;
			base.item.value = BaseUtility.CalcValue(0, 0, 0, 0, false);
			base.item.useStyle = 4;
			base.item.useAnimation = 45;
			base.item.useTime = 45;
		}

		public override bool UseItem(Player player)
		{
			if (RedeWorld.labAccess[1])
			{
				Main.NewText("Beta laser security already deactivated", Color.Cyan, false);
				return true;
			}
			RedeWorld.labAccess[1] = true;
			string key = "Mods.Redemption.Lasers2";
			Color messageColor = Color.Cyan;
			if (Main.netMode == 2)
			{
				NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, new object[0]), messageColor, -1);
			}
			else if (Main.netMode == 0)
			{
				Main.NewText("Beta sector laser security has been deactivated", messageColor, false);
			}
			Mod inst = Redemption.Inst;
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(220, 255, 255)] = ModContent.TileType<DeactivatedLaserTile>();
			colorToTile[new Color(255, 0, 0)] = ModContent.TileType<DeactivatedLaserV2Tile>();
			colorToTile[Color.Black] = -1;
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(inst.GetTexture("WorldGeneration/ALabAccess2"), colorToTile, null, null, null, null, null, null);
			Point origin = new Point((int)((float)Main.maxTilesX * 0.55f), (int)((float)Main.maxTilesY * 0.65f));
			texGenerator.Generate(origin.X, origin.Y, true, true);
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			return true;
		}
	}
}
