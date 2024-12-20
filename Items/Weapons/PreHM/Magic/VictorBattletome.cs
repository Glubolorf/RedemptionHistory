using System;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Magic
{
	public class VictorBattletome : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Victor Battletome");
			base.Tooltip.SetDefault("'An ancient spellbook that unleashes a boulder to flatten your enemies...'\n[c/1c4dff:Rare]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 35;
			base.item.magic = true;
			base.item.mana = 20;
			base.item.width = 28;
			base.item.height = 32;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 12f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item21;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<AncientBoulder>();
			base.item.shootSpeed = 7f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 5;
		}
	}
}
