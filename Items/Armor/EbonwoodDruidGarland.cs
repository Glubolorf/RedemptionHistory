using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class EbonwoodDruidGarland : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ebondruid's Garland");
			base.Tooltip.SetDefault("5% increased druidic damage\n4% increased druidic critical strike chance");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 22;
			base.item.height = 20;
			base.item.value = 1050;
			base.item.rare = 1;
			base.item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.05f;
			druidDamagePlayer.druidCrit += 4;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("EbonwoodDruidBreastplate") && legs.type == base.mod.ItemType("EbonwoodDruidLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Throws seedbags faster & Thorns effect";
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).fasterSeedbags = true;
			player.thorns = 1f;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(619, 25);
			modRecipe.AddIngredient(86, 8);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
