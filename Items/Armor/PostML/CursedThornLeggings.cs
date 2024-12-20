using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class CursedThornLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorn Greaves");
			base.Tooltip.SetDefault("50% increased movement speed\n10% increased melee and ranged damage\n20% increased ranged critical strike chance\n20% increased melee speed");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 16;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.defense = 30;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed *= 1.5f;
			player.meleeDamage *= 1.1f;
			player.rangedDamage *= 1.1f;
			player.rangedCrit += 20;
			player.meleeSpeed *= 1.2f;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CursedThorns", 16);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
