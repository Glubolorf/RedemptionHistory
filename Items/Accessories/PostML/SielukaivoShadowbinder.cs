using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Tiles.Furniture.Shade;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PostML
{
	public class SielukaivoShadowbinder : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sielukaivo Shadowbinder");
			base.Tooltip.SetDefault("'A gift, a curse, but not my own...'\nAbsorbs the souls of victims slain\nVictims below 5000 life are too weak to be contained\nUp to 100 Shadowbound Souls can be contained at once");
		}

		public override void SetDefaults()
		{
			base.item.width = 40;
			base.item.height = 40;
			base.item.value = Item.sellPrice(0, 15, 50, 0);
			base.item.accessory = true;
			base.item.rare = 11;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<SielukaivoShadowbinderTile>();
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).shadowBinder = true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Player player = Main.player[Main.myPlayer];
			string text = "Souls Captured: " + player.GetModPlayer<RedePlayer>().shadowBinderCharge + "/100";
			TooltipLine line = new TooltipLine(base.mod, "text1", text)
			{
				overrideColor = new Color?(Color.DarkGray)
			};
			tooltips.Insert(2, line);
		}
	}
}
