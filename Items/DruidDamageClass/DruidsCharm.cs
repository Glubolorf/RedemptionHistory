using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class DruidsCharm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Master Druid's Charm");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n5% increased druidic damage\nNegates fall damage\nGrants immunity to fire blocks\nForms an spirit skull to aid you upon being attacked\nStaves will burn targets\nSeedbags will inflict Frostburn\nSeedbags that throw only one seed will throw 2-3 seeds instead\nStaves swing faster\nThrows seedbags faster\nSpirits shoot faster\nSpirit summoning weapons will summon 2 extra spirits\nSpirits pierce through more targets\nSpirits home in on enemies\n95% decrease to all other damage types");
		}

		public override void SetDefaults()
		{
			base.item.width = 42;
			base.item.height = 52;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 7;
			base.item.accessory = true;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int num = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + num; i++)
				{
					if (slot != i && player.armor[i].type == base.mod.ItemType("DruidsCharmDawn"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("DruidsCharmDusk"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("DruidsCharmMidnight"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("DruidEmblem"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("LargeSeedPouch"))
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.frostburnSeedbag = true;
			redePlayer.burnStaves = true;
			redePlayer.spiritSkull1 = true;
			redePlayer.moreSeeds = true;
			redePlayer.fasterStaves = true;
			redePlayer.fasterSeedbags = true;
			redePlayer.fasterSpirits = true;
			redePlayer.moreSpirits = true;
			redePlayer.spiritHoming = true;
			redePlayer.spiritPierce = true;
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.25f;
			player.noFallDmg = true;
			player.buffImmune[67] = true;
			player.magicDamage *= 0.05f;
			player.meleeDamage *= 0.05f;
			player.minionDamage *= 0.05f;
			player.rangedDamage *= 0.05f;
			player.thrownDamage *= 0.05f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3467, 5);
			modRecipe.AddIngredient(null, "LargeSeedPouch", 1);
			modRecipe.AddIngredient(null, "DruidEmblem", 1);
			modRecipe.AddIngredient(null, "DruidsCharmDawn", 1);
			modRecipe.AddIngredient(null, "DruidsCharmDusk", 1);
			modRecipe.AddIngredient(null, "DruidsCharmMidnight", 1);
			modRecipe.AddIngredient(null, "ForestCore", 100);
			modRecipe.AddIngredient(null, "SoulOfBloom", 100);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
