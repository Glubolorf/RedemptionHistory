using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	public class LastBurden : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Last Burden");
			base.Tooltip.SetDefault("[c/bdffff:---Druid Class---]\n'It crumbles in your hand...'\nIncreases spirits summoned to max\nSpirits home in on enemies\n[c/bdffff:Spirit Level +5]");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(3, 6));
		}

		public override void SetDefaults()
		{
			base.item.width = 42;
			base.item.height = 56;
			base.item.value = Item.sellPrice(0, 7, 50, 0);
			base.item.rare = 11;
			base.item.accessory = true;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color?(RedeColor.SoullessColour);
				}
			}
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == base.mod.ItemType("TinCross"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("SpiritualRelic"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("SacredCross"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("SpiritualGuide"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("CopperCross"))
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SpiritualGuide", 1);
			modRecipe.AddIngredient(null, "SmallShadesoul", 8);
			modRecipe.AddIngredient(null, "Shadesoul", 2);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritLevel += 5;
			redePlayer.spiritExtras += 4;
			redePlayer.spiritHoming = true;
		}
	}
}
