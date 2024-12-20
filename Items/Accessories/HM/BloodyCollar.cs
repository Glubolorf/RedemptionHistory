using System;
using Redemption.Items.Materials.HM;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class BloodyCollar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bloody Collar");
			base.Tooltip.SetDefault("'It's shining brightly, reflecting the air through flesh and blood.'\nUpon striking a foe, you have a chance to emit a wave of bloody energy, robbing the life of those around you");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 28;
			base.item.value = Item.sellPrice(0, 4, 0, 0);
			base.item.rare = 6;
			base.item.accessory = true;
			base.item.defense = 4;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(ModContent.ItemType<ScarletBar>(), 5);
			modRecipe.AddIngredient(1225, 10);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).bloodyCollar = true;
		}
	}
}
