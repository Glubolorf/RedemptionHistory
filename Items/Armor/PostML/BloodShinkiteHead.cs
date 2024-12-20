using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class BloodShinkiteHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shinkite Bloodhelm");
			base.Tooltip.SetDefault("10% increased damage\nIncreased life regen");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 26;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.defense = 20;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.allDamage += 0.1f;
			player.lifeRegen++;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<BloodShinkiteBody>() && legs.type == ModContent.ItemType<BloodShinkiteLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Dealing damage has a chance for enemies to spawn healing blood orbs.\nPlayers touching a blood orb will restore life";
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).bloodShinkiteSet = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ShinkiteBlood", 12);
			modRecipe.AddIngredient(172, 35);
			modRecipe.AddIngredient(792, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
