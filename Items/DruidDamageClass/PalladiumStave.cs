using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class PalladiumStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Palladium Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots an orange bolt");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 40;
			base.item.width = 42;
			base.item.height = 42;
			base.item.useTime = 32;
			base.item.useAnimation = 32;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 1, 84, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = 122;
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
				base.item.useTime = 28;
				base.item.useAnimation = 28;
			}
			else
			{
				base.item.useTime = 32;
				base.item.useAnimation = 32;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(1184, 8);
			modRecipe.AddIngredient(9, 20);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
