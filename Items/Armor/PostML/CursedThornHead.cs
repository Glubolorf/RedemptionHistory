using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class CursedThornHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorn Helmet");
			base.Tooltip.SetDefault("10% increased melee and ranged damage\n20% chance not to consume ammo\n12% reduced mana usage\n+25 max life");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 30;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.defense = 26;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.ammoCost80 = true;
			player.meleeDamage *= 1.1f;
			player.rangedDamage *= 1.1f;
			player.statLifeMax2 += 25;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
			player.armorEffectDrawOutlines = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("CursedThornBody") && legs.type == base.mod.ItemType("CursedThornLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "10% increased damage reduction while above 30% life\n15% increased damage reduction while below 30% life\n8% increased melee and ranged damage\n6% increased melee and ranged critical strike chance\n10% increased melee speed\nCritical strikes will summon a rising thorn at the target";
			if (player.statLife > (int)((float)player.statLifeMax2 * 0.7f))
			{
				player.endurance += 0.1f;
			}
			if (player.statLife < (int)((float)player.statLifeMax2 * 0.3f))
			{
				player.endurance += 0.15f;
			}
			player.meleeDamage += 0.08f;
			player.rangedDamage += 0.08f;
			player.meleeCrit += 6;
			player.rangedCrit += 6;
			player.meleeSpeed += 0.1f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).cursedThornSet = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CursedThorns", 12);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
