using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class MagSoulbound : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Magnetic Soulbound");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nThrows a Soulbound Soul\nReleases a Lost Soul when hitting a target\n[c/d9eeed:-While Lost Soul set is equipped-]\nReleases an extra Lost Soul\n[c/d8f5dc:-While Wandering Soul set is equipped-]\nBuffed damage and releases 3 extra Lost Souls");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 11f;
			base.item.crit = 4;
			base.item.damage = 9;
			base.item.knockBack = 0f;
			base.item.useStyle = 1;
			base.item.useAnimation = 24;
			base.item.useTime = 24;
			base.item.width = 42;
			base.item.height = 38;
			base.item.rare = 1;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 90, 50);
			base.item.shoot = base.mod.ProjectileType("MagSoulboundPro");
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).wanderingSoulSet)
			{
				base.item.damage = 21;
			}
			else
			{
				base.item.damage = 11;
			}
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(21, 5);
			modRecipe.AddIngredient(null, "SmallLostSoul", 3);
			modRecipe.AddIngredient(177, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(705, 5);
			modRecipe.AddIngredient(null, "SmallLostSoul", 3);
			modRecipe.AddIngredient(177, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
