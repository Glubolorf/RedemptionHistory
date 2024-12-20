using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	public class TinCross : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tin Cross");
			base.Tooltip.SetDefault("5% increased druidic damage\n[c/bdffff:Spirit Level +1]");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 22;
			base.item.height = 38;
			base.item.value = Item.sellPrice(0, 0, 15, 0);
			base.item.rare = 1;
			base.item.accessory = true;
			this.spiritWeapon = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(703, 5);
			modRecipe.AddIngredient(965, 10);
			modRecipe.AddIngredient(null, "SmallLostSoul", 2);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == ModContent.ItemType<CopperCross>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<SpiritualRelic>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<SacredCross>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<SpiritualGuide>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<LastBurden>())
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.05f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).spiritLevel++;
		}
	}
}
