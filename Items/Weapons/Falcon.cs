using System;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Falcon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Falcon");
			base.Tooltip.SetDefault("'A warhammer that can create earthquakes... Very tiny earthquakes...'\n[c/1c4dff:Rare]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 34;
			base.item.melee = true;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 32;
			base.item.useAnimation = 32;
			base.item.hammer = 70;
			base.item.useStyle = 1;
			base.item.knockBack = 8f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 5;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<FalconPro1>(), base.item.damage, base.item.knockBack, Main.myPlayer, 0f, 0f);
		}
	}
}
