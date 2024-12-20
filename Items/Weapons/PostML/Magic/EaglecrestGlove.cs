using System;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Magic
{
	public class EaglecrestGlove : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eaglecrest Glove");
			base.Tooltip.SetDefault("'Don't like a guy? Throw a boulder at him!'\nThrows an Eaglecrest Boulder");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1500;
			base.item.magic = true;
			base.item.mana = 20;
			base.item.width = 28;
			base.item.height = 34;
			base.item.useTime = 40;
			base.item.useAnimation = 40;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.knockBack = 14f;
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.UseSound = SoundID.Item88;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<EaglecrestBoulder>();
			base.item.shootSpeed = 12f;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}
	}
}
