using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class DruidsCharmMidnight : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Druid's Charm");
			base.Tooltip.SetDefault("'Midnight'\n10% increased druidic damage\nNegates fall damage\nGrants immunity to fire blocks\nForms an spirit skull to aid you upon being attacked\nSpirits shoot faster\nSpirits pierce through more targets\nSpirits home in on enemies\n75% decrease to all other damage types\n[c/bdffff:Spirit Level +2]");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 52;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 6;
			base.item.accessory = true;
			base.item.defense = 2;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == ModContent.ItemType<DruidsCharmDawn>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<DruidsCharmDusk>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<DruidsCharm>())
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.1f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritSkull1 = true;
			redePlayer.fasterSpirits = true;
			redePlayer.spiritHoming = true;
			redePlayer.spiritPierce = true;
			player.noFallDmg = true;
			player.buffImmune[67] = true;
			player.magicDamage *= 0.25f;
			player.meleeDamage *= 0.25f;
			player.minionDamage *= 0.25f;
			player.rangedDamage *= 0.25f;
			player.thrownDamage *= 0.25f;
			redePlayer.spiritLevel += 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LotusHorseshoe3", 1);
			modRecipe.AddIngredient(null, "ForestCore", 15);
			modRecipe.AddIngredient(1225, 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
