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
	public class FlowerCrowns8_2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dusk Flower Crown");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Purple and orange, dusk shimmers'\nHas a chance to throw an extra seed");
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
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).extraSeed = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == 83 && legs.type == 79;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "15% increased druidic damage";
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.15f;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(706, 6);
			modRecipe.AddIngredient(314, 3);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
