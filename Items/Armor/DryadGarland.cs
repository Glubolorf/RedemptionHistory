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
	public class DryadGarland : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dryad's Garland");
			base.Tooltip.SetDefault("2% increased druidic damage\n2% increased druidic critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 24;
			base.item.value = 50;
			base.item.rare = 1;
			base.item.defense = 3;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.02f;
			druidDamagePlayer.druidCrit += 2;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DryadChestplate>() && legs.type == ModContent.ItemType<DryadLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Staves cast faster";
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).staveSpeed += 0.05f;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(9, 20);
			modRecipe.AddIngredient(27, 4);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
