using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class MythrilStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Mythril Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots a green bolt");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 43;
			base.item.width = 44;
			base.item.height = 44;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 2, 7, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = 124;
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
				base.item.useTime = 24;
				base.item.useAnimation = 24;
			}
			else
			{
				base.item.useTime = 28;
				base.item.useAnimation = 28;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(382, 8);
			modRecipe.AddIngredient(9, 20);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
