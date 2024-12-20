using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Weapons.HM.Druid;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class SpiritualGuide : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spiritual Guide");
			base.Tooltip.SetDefault("'You are filled with balance...'\nSummons a Spirit Golem head that circles around you, shooting bolts at enemies\nIncreases spirits summoned by 3\nSpirits home in on enemies\n[c/bdffff:Spirit Level +4]");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 34;
			base.item.height = 56;
			base.item.value = Item.sellPrice(0, 4, 50, 0);
			base.item.rare = 7;
			base.item.accessory = true;
			this.spiritWeapon = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SacredCross", 1);
			modRecipe.AddIngredient(520, 15);
			modRecipe.AddIngredient(521, 15);
			modRecipe.AddIngredient(2766, 10);
			modRecipe.AddRecipeGroup("Redemption:Plant", 8);
			modRecipe.AddIngredient(null, "PowerCellWristband", 1);
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
					if (slot != i && player.armor[i].type == ModContent.ItemType<TinCross>())
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
					if (slot != i && player.armor[i].type == ModContent.ItemType<CopperCross>())
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
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritLevel += 4;
			redePlayer.spiritExtras += 3;
			redePlayer.spiritHoming = true;
			redePlayer.spiritGolemCross = true;
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritGolemCross && player.ownedProjectileCounts[ModContent.ProjectileType<SpiritGolemHead>()] == 0)
			{
				Projectile.NewProjectile(player.position, Vector2.Zero, ModContent.ProjectileType<SpiritGolemHead>(), 50, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
