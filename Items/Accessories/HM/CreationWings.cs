using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	[AutoloadEquip(new EquipType[]
	{
		9
	})]
	public class CreationWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Creation Wings");
			base.Tooltip.SetDefault("Allows flight and slow fall");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 28;
			base.item.value = Item.sellPrice(0, 8, 0, 0);
			base.item.rare = 10;
			base.item.accessory = true;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 180;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 9f;
			acceleration *= 2.5f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3467, 10);
			modRecipe.AddIngredient(null, "CreationFragment", 14);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
