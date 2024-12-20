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
			base.Tooltip.SetDefault("[c/bdffff:---Druid Class---]\nRelease a bond of souls\nGets buffed from soul-related armoury");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 11f;
			base.item.crit = 4;
			base.item.damage = 12;
			base.item.knockBack = 0f;
			base.item.useStyle = 1;
			base.item.useAnimation = 24;
			base.item.useTime = 24;
			base.item.width = 42;
			base.item.height = 38;
			base.item.rare = 1;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 90, 50);
			base.item.shoot = base.mod.ProjectileType("MagSoulboundPro");
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().wanderingSoulSet)
			{
				base.item.damage = 21;
				base.item.autoReuse = true;
				base.item.shootSpeed = 16f;
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().shadeSet)
			{
				base.item.damage = 400;
				base.item.autoReuse = true;
				base.item.shootSpeed = 18f;
			}
			else
			{
				base.item.damage = 11;
				base.item.autoReuse = false;
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
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(705, 5);
			modRecipe2.AddIngredient(null, "SmallLostSoul", 3);
			modRecipe2.AddIngredient(177, 1);
			modRecipe2.AddTile(null, "DruidicAltarTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
