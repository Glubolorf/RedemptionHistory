using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class PureIronWarhammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pure-Iron Warhammer");
			base.Tooltip.SetDefault("'Due to the way this tool is forged, the hammer is exceptionally cold...'\nCauses Frostburn to enemies, but makes the wielder Chilled");
		}

		public override void SetDefaults()
		{
			base.item.damage = 45;
			base.item.melee = true;
			base.item.width = 46;
			base.item.height = 44;
			base.item.useTime = 13;
			base.item.useAnimation = 39;
			base.item.hammer = 70;
			base.item.useStyle = 1;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 8, 25, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "PureIron", 16);
			modRecipe.AddTile(null, "GathicCryoFurnaceTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(44, 600, false);
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(46, 60, true);
		}
	}
}
