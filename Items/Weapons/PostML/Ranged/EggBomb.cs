using System;
using Redemption.Projectiles.Ranged;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Ranged
{
	public class EggBomb : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Egg Bomb");
			base.Tooltip.SetDefault("'Takes egging to a whole new level'");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 22;
			base.item.damage = 600;
			base.item.maxStack = 999;
			base.item.value = 150;
			base.item.useStyle = 1;
			base.item.useAnimation = 10;
			base.item.useTime = 10;
			base.item.UseSound = SoundID.Item7;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.thrown = true;
			base.item.shootSpeed = 14f;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<EggBombPro2>();
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}
	}
}
