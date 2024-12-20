using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.Summon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Summon
{
	public class AncientMirrorShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Mirror Shield");
			base.Tooltip.SetDefault("Summons a mirror to reflect most projectiles\nIf the projectile is bigger than the mirror, the mirror will break");
		}

		public override void SetDefaults()
		{
			base.item.mana = 100;
			base.item.width = 28;
			base.item.height = 50;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 1;
			base.item.value = Item.sellPrice(0, 45, 0, 0);
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = ModContent.ProjectileType<AncientMirrorShieldPro>();
			base.item.shootSpeed = 0f;
			base.item.buffType = ModContent.BuffType<AncientMirrorBuff>();
			base.item.buffTime = 36000;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return player.ownedProjectileCounts[ModContent.ProjectileType<AncientMirrorShieldPro>()] == 0;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientPowerCore", 14);
			modRecipe.AddIngredient(170, 6);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
