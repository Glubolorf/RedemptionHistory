using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class WispArmour : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wisp's Chestplate");
			base.Tooltip.SetDefault("[c/bdffff:---Druid Class---]\n8% increased druidic damage\n8% increased druidic critical strike chance\n4% damage reduction\nSpirits shoot faster\n[c/bdffff:Spirit Level +2]");
		}

		public override void SetDefaults()
		{
			base.item.width = 42;
			base.item.height = 38;
			base.item.value = Item.sellPrice(0, 6, 0, 0);
			base.item.rare = 8;
			base.item.defense = 15;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.08f;
			druidDamagePlayer.druidCrit += 8;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.fasterSpirits = true;
			player.endurance += 0.04f;
			redePlayer.spiritLevel += 2;
		}

		public override bool DrawBody()
		{
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SoulOfBloom", 25);
			modRecipe.AddIngredient(1508, 15);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
