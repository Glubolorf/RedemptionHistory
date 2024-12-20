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
	public class BloomDruidHood : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blossom Druid's Hood");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n8% increased druidic damage\nIncreases maximum mana by 40\nSpirits shoot faster\nImmune to Poison & Venom");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 28;
			base.item.value = 100000;
			base.item.rare = 7;
			base.item.defense = 15;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.08f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.fasterSpirits = true;
			player.statManaMax2 += 40;
			player.buffImmune[20] = true;
			player.buffImmune[70] = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("BloomDruidBody") && legs.type == base.mod.ItemType("BloomDruidLeggings");
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawOutlines = true;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Has a chance to unleash an explosive seed upon getting hit";
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.seedHit = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SoulOfBloom", 15);
			modRecipe.AddIngredient(null, "ForestCore", 4);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
