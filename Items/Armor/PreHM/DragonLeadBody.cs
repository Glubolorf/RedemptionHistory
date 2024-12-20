using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class DragonLeadBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dragon-Lead Breastplate");
			base.Tooltip.SetDefault("6% increased druid and ranged damage \nThrow seedbags faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 22;
			base.item.value = Item.sellPrice(0, 8, 0, 0);
			base.item.rare = 4;
			base.item.defense = 7;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedDamage += 0.06f;
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.06f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).fasterSeedbags = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DragonLeadBar", 20);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
