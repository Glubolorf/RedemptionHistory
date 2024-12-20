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
	public class Garland5 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Adamantite Garland");
			base.Tooltip.SetDefault("14% increased druidic damage\n8% increased druidic critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 20;
			base.item.value = Item.sellPrice(0, 3, 0, 0);
			base.item.rare = 4;
			base.item.defense = 7;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.14f;
			druidDamagePlayer.druidCrit += 8;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawOutlines = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == 403 && legs.type == 404;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Staves cast faster, Throws seedbags faster, Spirits shoot faster";
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.staveSpeed += 0.05f;
			redePlayer.fasterSeedbags = true;
			redePlayer.fasterSpirits = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(391, 12);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
