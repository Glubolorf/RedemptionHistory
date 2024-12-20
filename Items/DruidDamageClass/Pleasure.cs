using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class Pleasure : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pleasure (O-02-98)-W");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'If you look for a pleasure that can not be tolerated, the end reaches the loss of self.'\nAttacks inflict Enjoyment, decreasing life of those hit until death");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 48;
			base.item.width = 60;
			base.item.height = 60;
			base.item.useTime = 32;
			base.item.useAnimation = 32;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).burnStaves)
			{
				target.AddBuff(24, 180, false);
			}
			target.AddBuff(base.mod.BuffType("EnjoymentDebuff"), 18000, false);
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
			modRecipe.AddIngredient(210, 20);
			modRecipe.AddIngredient(null, "LunarCrescentStave", 1);
			modRecipe.AddIngredient(292, 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
