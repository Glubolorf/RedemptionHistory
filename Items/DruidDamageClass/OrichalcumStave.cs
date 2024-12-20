using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class OrichalcumStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Orichalcum Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots a pink bolt");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 46;
			base.item.width = 44;
			base.item.height = 44;
			base.item.useTime = 29;
			base.item.useAnimation = 29;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 2, 53, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = 121;
			base.item.shootSpeed = 12f;
			base.item.autoReuse = true;
			base.item.useTurn = true;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				base.item.useTime = 25;
				base.item.useAnimation = 25;
			}
			else
			{
				base.item.useTime = 29;
				base.item.useAnimation = 29;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(1191, 8);
			modRecipe.AddIngredient(9, 20);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
