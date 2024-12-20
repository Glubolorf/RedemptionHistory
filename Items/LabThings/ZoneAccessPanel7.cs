using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class ZoneAccessPanel7 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/LabThings/ZoneAccessPanel7";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lab Access Panel - Master");
			BaseUtility.AddTooltips(base.item, new string[]
			{
				"Opens up all sectors of the lab"
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
			if (RedeWorld.labAccess7)
			{
				Main.NewText("Laser security already deactivated", Color.Cyan, false);
				return true;
			}
			RedeWorld.labAccess7 = true;
			string text = "Mods.Redemption.Lasers7";
			Color cyan = Color.Cyan;
			if (Main.netMode == 2)
			{
				NetMessage.BroadcastChatMessage(NetworkText.FromKey(text, new object[0]), cyan, -1);
			}
			else if (Main.netMode == 0)
			{
				Main.NewText("All laser security systems has been deactivated", cyan, false);
			}
			Mod inst = Redemption.inst;
			Dictionary<Color, int> dictionary = new Dictionary<Color, int>();
			dictionary[new Color(220, 255, 255)] = inst.TileType("DeactivatedLaserTile");
			dictionary[new Color(255, 0, 0)] = inst.TileType("DeactivatedLaserV2Tile");
			dictionary[Color.Black] = -1;
			TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(inst.GetTexture("WorldGeneration/LabAccess7"), dictionary, null, null, null, null);
			Point point;
			point..ctor((int)((float)Main.maxTilesX * 0.6f), (int)((float)Main.maxTilesY * 0.65f));
			texGenerator.Generate(point.X, point.Y, true, true);
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
