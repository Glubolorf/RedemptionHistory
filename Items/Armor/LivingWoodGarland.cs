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
	public class LivingWoodGarland : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Wood Garland");
			base.Tooltip.SetDefault("3% increased druidic damage\n2% druidic critical strike chance");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 20;
			base.item.height = 12;
			base.item.value = 50;
			base.item.rare = 0;
			base.item.defense = 1;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.03f;
			druidDamagePlayer.druidCrit += 2;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("LivingWoodBody") && legs.type == base.mod.ItemType("LivingWoodLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "7% druidic damage, Immune to poison";
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.07f;
			player.buffImmune[20] = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LivingTwig", 18);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
