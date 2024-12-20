using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class AncientPebble : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Pebble");
			base.Tooltip.SetDefault("Summons a flying pebble to fight for you.");
		}

		public override void SetDefaults()
		{
			base.item.damage = 25;
			base.item.summon = true;
			base.item.mana = 10;
			base.item.width = 20;
			base.item.height = 22;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.rare = 3;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 0, 60, 0);
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = base.mod.ProjectileType("AncientStoneMinion");
			base.item.shootSpeed = 10f;
			base.item.buffType = base.mod.BuffType("AncientStoneMinionBuff");
			base.item.buffTime = 3600;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return player.altFunctionUse != 2;
		}

		public override bool UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
		}
	}
}
