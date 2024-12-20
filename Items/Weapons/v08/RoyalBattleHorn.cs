using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class RoyalBattleHorn : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Royal Battle Horn");
			base.Tooltip.SetDefault("Summons a Chicken Swarmer to fight for you.");
		}

		public override void SetDefaults()
		{
			base.item.damage = 250;
			base.item.summon = true;
			base.item.mana = 10;
			base.item.width = 30;
			base.item.height = 30;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.knockBack = 2f;
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = base.mod.ProjectileType("ChickenSwarmerMinion");
			base.item.shootSpeed = 10f;
			base.item.buffType = base.mod.BuffType("ChickenSwarmerMinionBuff");
			base.item.buffTime = 3600;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
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
