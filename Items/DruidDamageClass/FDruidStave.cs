using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class FDruidStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Forest Druid's Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots a leaf");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 48;
			base.item.width = 56;
			base.item.height = 60;
			base.item.useTime = 27;
			base.item.useAnimation = 27;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 8f;
			base.item.value = Item.sellPrice(0, 2, 35, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = 206;
			base.item.shootSpeed = 12f;
			base.item.autoReuse = true;
			base.item.useTurn = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).burnStaves)
			{
				target.AddBuff(24, 180, false);
			}
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				base.item.useTime = 23;
				base.item.useAnimation = 23;
			}
			else
			{
				base.item.useTime = 27;
				base.item.useAnimation = 27;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ForestCore", 8);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
