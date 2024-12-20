using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class TheKeepersCrown : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Keeper's Crown");
			base.Tooltip.SetDefault("'Infused with shadows...'\nIncreases crit by 6%");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.expert = true;
			base.item.rare = 4;
			base.item.defense = 3;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicCrit += 6;
			player.meleeCrit += 6;
			player.rangedCrit += 6;
			player.thrownCrit += 6;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = true);
		}
	}
}
