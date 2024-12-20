using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	[AutoloadEquip(new EquipType[]
	{
		9
	})]
	public class NebWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebuleus Wings");
			base.Tooltip.SetDefault("Allows flight and slow fall\nUse dyes to make it look fabulous!");
		}

		public override void SetDefaults()
		{
			base.item.width = 70;
			base.item.height = 58;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.accessory = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 3;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 220;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.95f;
			ascentWhenRising = 0.35f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 5f;
			constantAscend = 0.2f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 13f;
			acceleration *= 4f;
		}
	}
}
