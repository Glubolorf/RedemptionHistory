using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class BoneLeviathanFlail : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bone Leviathan Flail");
		}

		public override void SetDefaults()
		{
			base.item.width = 44;
			base.item.height = 46;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 5;
			base.item.noMelee = true;
			base.item.useStyle = 5;
			base.item.useAnimation = 40;
			base.item.useTime = 40;
			base.item.knockBack = 7.5f;
			base.item.damage = 60;
			base.item.scale = 2f;
			base.item.noUseGraphic = true;
			base.item.shoot = base.mod.ProjectileType("BoneFlailHeadPro");
			base.item.shootSpeed = 24.5f;
			base.item.UseSound = SoundID.Item1;
			base.item.melee = true;
			base.item.channel = true;
		}
	}
}
