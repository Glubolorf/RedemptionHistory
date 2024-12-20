using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class LifeFruitOfThorns : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fruit of Thorns");
			base.Tooltip.SetDefault("Summons an empowered Thorn, Bane of the Forest\nOnly usable at day\nRequires Thorn, Bane of the Forest to be defeated\nNot consumable");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 46;
			base.item.maxStack = 1;
			base.item.noUseGraphic = true;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = false;
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

		public override bool CanUseItem(Player player)
		{
			return Main.dayTime && RedeWorld.downedThorn && !NPC.AnyNPCs(base.mod.NPCType("Thorn")) && !NPC.AnyNPCs(base.mod.NPCType("ThornPZ"));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CursedThornsF", 12);
			modRecipe.AddIngredient(1291, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, base.mod.NPCType("ThornPZ"));
			Main.PlaySound(15, player.position, 0);
			return true;
		}
	}
}
