using System;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Druid.Seedbags
{
	public class StarfruitSeedbag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Starfruit Seedbag");
			base.Tooltip.SetDefault("Tosses a seed that grows into a Starfruit Plant");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 300;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 36;
			base.item.useAnimation = 36;
			base.item.useStyle = 1;
			base.item.mana = 15;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 85, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<StarFruitSeed>();
			base.item.shootSpeed = 18f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 3;
		}
	}
}
