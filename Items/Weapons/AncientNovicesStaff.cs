using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class AncientNovicesStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Novice's Staff");
			base.Tooltip.SetDefault("'A simple wooden staff with a white crystal at the top'\nCasts the Ember spell");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 9;
			base.item.magic = true;
			base.item.mana = 3;
			base.item.width = 36;
			base.item.height = 36;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = 2500;
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item20;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("Ember");
			base.item.shootSpeed = 16f;
		}
	}
}
