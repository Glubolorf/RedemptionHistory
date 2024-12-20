using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class BloomDruidLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blossom Druid's Leggings");
			base.Tooltip.SetDefault("8% increased druidic damage\n8% increased druidic critical strike chance\nIncreased life regen");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = 70000;
			base.item.rare = 7;
			base.item.defense = 11;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.08f;
			druidDamagePlayer.druidCrit += 8;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			player.lifeRegen += 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SoulOfBloom", 20);
			modRecipe.AddIngredient(null, "ForestCore", 6);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
