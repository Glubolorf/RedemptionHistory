using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class ZoneAccessPanel4 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/LabThings/ZoneAccessPanel4";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("(Old) Lab Access Panel - Sigma");
			BaseUtility.AddTooltips(base.item, new string[]
			{
				"Opens up the sigma sector of the lab"
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
			if (RedeWorld.labAccess4)
			{
				Main.NewText("Sigma laser security already deactivated", Color.Cyan, false);
				return true;
			}
			RedeWorld.labAccess4 = true;
			string key = "Mods.Redemption.Lasers4";
			Color messageColor = Color.Cyan;
			if (Main.netMode == 2)
			{
				NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, new object[0]), messageColor, -1);
			}
			else if (Main.netMode == 0)
			{
				Main.NewText("Sigma sector laser security has been deactivated", messageColor, false);
			}
			Mod mod = Redemption.inst;
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(220, 255, 255)] = mod.TileType("DeactivatedLaserTile");
			colorToTile[new Color(255, 0, 0)] = mod.TileType("DeactivatedLaserV2Tile");
			colorToTile[new Color(150, 150, 150)] = -2;
			colorToTile[Color.Black] = -1;
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/LabAccess4"), colorToTile, null, null, null, null);
			Point origin = new Point((int)((float)Main.maxTilesX * 0.6f), (int)((float)Main.maxTilesY * 0.65f));
			texGenerator.Generate(origin.X, origin.Y, true, true);
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			return true;
		}

		public override void Update(ref float gravity, ref float maxFallSpeed)
		{
			if (RedeWorld.labAccess7)
			{
				base.item.active = false;
			}
		}
	}
}
