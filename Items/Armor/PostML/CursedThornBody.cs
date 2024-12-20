using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class CursedThornBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorn Breastplate");
			base.Tooltip.SetDefault("+75 max life\n15% increased melee and ranged damage\nAttackers also take damage");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 24;
			base.item.value = Item.sellPrice(0, 30, 0, 0);
			base.item.defense = 36;
		}

		public override void UpdateEquip(Player player)
		{
			player.statLifeMax2 += 75;
			player.meleeDamage *= 1.15f;
			player.rangedDamage *= 1.15f;
			player.thorns *= 0.75f;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CursedThorns", 18);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
