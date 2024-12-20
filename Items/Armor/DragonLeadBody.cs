using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
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
			base.Tooltip.SetDefault("[c/daf73a:---Druid Class---]\n3% increased ranged damage\n6% increased druidic damage\nStaves swing faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 22;
			base.item.value = Item.sellPrice(0, 8, 0, 0);
			base.item.rare = 4;
			base.item.defense = 7;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedDamage *= 1.03f;
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.6f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).fasterStaves = true;
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
