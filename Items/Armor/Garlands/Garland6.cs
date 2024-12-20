using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Garlands
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class Garland6 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Titanium Garland");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n16% increased druidic damage\n7% increased druidic critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 26;
			base.item.value = Item.sellPrice(0, 3, 0, 0);
			base.item.rare = 4;
			base.item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.16f;
			druidDamagePlayer.druidCrit += 7;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == 1218 && legs.type == 1219;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Staves swing faster, Throws seedbags faster, Spirits shoot faster\nBriefly become invulnerable after striking an enemy";
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.fasterStaves = true;
			redePlayer.fasterSeedbags = true;
			redePlayer.fasterSpirits = true;
			player.onHitDodge = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = true);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1198, 13);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
