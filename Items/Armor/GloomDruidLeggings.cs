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
	public class GloomDruidLeggings : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gloom Leggings");
			base.Tooltip.SetDefault("5% increased druidic damage\nLife regen slightly increased\nMana regen slightly increased");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 22;
			base.item.height = 16;
			base.item.value = 7000;
			base.item.rare = 2;
			base.item.defense = 6;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.05f;
			player.lifeRegen += 2;
			player.manaRegen += 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "GloomMushroom", 15);
			modRecipe.AddIngredient(154, 25);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
