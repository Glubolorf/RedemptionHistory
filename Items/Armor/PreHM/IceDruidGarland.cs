using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class IceDruidGarland : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Coldblooded Druid's Garland");
			base.Tooltip.SetDefault("3% increased druidic damage\n2% increased druidic critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 22;
			base.item.value = 850;
			base.item.rare = 1;
			base.item.defense = 3;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.03f;
			druidDamagePlayer.druidCrit += 2;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<IceDruidBody>() && legs.type == ModContent.ItemType<IceDruidLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "All your plants consider the tundra thier native terrain and infict frostburn";
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).frostburnSeedbag = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(664, 25);
			modRecipe.AddIngredient(57, 8);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(664, 25);
			modRecipe2.AddIngredient(1257, 8);
			modRecipe2.AddTile(null, "DruidicAltarTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
