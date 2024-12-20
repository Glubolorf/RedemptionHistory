using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class AncientPowerSurgeHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Power Surge Helmet");
			base.Tooltip.SetDefault("15% increased minion damage\n5% increased magic damage\n+4 minion capacity\n12% reduced mana usage\n+100 max mana");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 30;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.defense = 22;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions += 4;
			player.magicDamage *= 1.05f;
			player.minionDamage *= 1.15f;
			player.manaCost *= 0.88f;
			player.statManaMax2 += 100;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
			player.armorEffectDrawOutlines = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("AncientPowerSurgeBody") && legs.type == base.mod.ItemType("AncientPowerSurgeLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Dealing damage has a 10% chance to fire out homing energy orbs at enemies.";
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).powerSurgeSet = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientPowerCore", 12);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
