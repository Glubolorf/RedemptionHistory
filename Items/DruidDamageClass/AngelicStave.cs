using System;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class AngelicStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Angelic Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Born from ashes of the undead'\nShoots a holy javalin that unleashes Angelic Darts");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 62;
			base.item.width = 58;
			base.item.height = 58;
			base.item.useTime = 18;
			base.item.useAnimation = 18;
			base.item.useStyle = 5;
			base.item.shootSpeed = 16f;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.crit = 4;
			base.item.knockBack = 6f;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType<AngelicPointer>();
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				base.item.useTime = 14;
				base.item.useAnimation = 14;
			}
			else
			{
				base.item.useTime = 18;
				base.item.useAnimation = 18;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1571, 1);
			modRecipe.AddIngredient(null, "DruidDagger", 100);
			modRecipe.AddIngredient(null, "SoulOfBloom", 80);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
