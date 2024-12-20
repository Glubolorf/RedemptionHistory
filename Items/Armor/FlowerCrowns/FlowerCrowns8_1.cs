using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.FlowerCrowns
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class FlowerCrowns8_1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dawn Flower Crown");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Dawn brings the light back'\nStaves swing faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 10;
			base.item.value = 5000;
			base.item.rare = 1;
			base.item.defense = 3;
		}

		public override void UpdateEquip(Player player)
		{
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.fasterStaves = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == 83 && legs.type == 79;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "15% increased druidic damage";
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.15f;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(19, 6);
			modRecipe.AddIngredient(318, 3);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
