using System;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Magic
{
	public class CosmosChainWeapon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cosmic Chain");
			base.Tooltip.SetDefault("Sends out a Chain of the Cosmos to stop enemies in their tracks\nDoesn't freeze bosses");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 30;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.noMelee = true;
			base.item.useStyle = 1;
			base.item.useAnimation = 20;
			base.item.useTime = 20;
			base.item.knockBack = 0f;
			base.item.damage = 90;
			base.item.noUseGraphic = true;
			base.item.shoot = ModContent.ProjectileType<CosmosChainF1>();
			base.item.shootSpeed = 26f;
			base.item.UseSound = SoundID.Item125;
			base.item.magic = true;
			base.item.mana = 8;
			base.item.autoReuse = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 3;
		}
	}
}
