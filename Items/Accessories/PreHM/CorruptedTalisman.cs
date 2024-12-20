using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PreHM
{
	public class CorruptedTalisman : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hateful Talisman");
			base.Tooltip.SetDefault("'You feel uncomfortable holding this...'\nCorrupts all level 4 or lower spirits.");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 26;
			base.item.height = 42;
			base.item.value = Item.sellPrice(0, 0, 20, 0);
			base.item.rare = 2;
			base.item.accessory = true;
			this.spiritWeapon = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(67, 5);
			modRecipe.AddIngredient(68, 15);
			modRecipe.AddIngredient(56, 5);
			modRecipe.AddIngredient(null, "SmallLostSoul", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).corruptedTalisman = true;
		}
	}
}
