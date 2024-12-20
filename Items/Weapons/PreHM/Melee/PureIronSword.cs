using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class PureIronSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pure-Iron Sword");
			base.Tooltip.SetDefault("'Due to the way this weapon is forged, the sword is exceptionally cold...'\nCauses Frostburn to enemies, but makes the wielder Chilled");
		}

		public override void SetDefaults()
		{
			base.item.damage = 32;
			base.item.melee = true;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 12;
			base.item.useAnimation = 12;
			base.item.useStyle = 1;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 8, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "PureIron", 10);
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
