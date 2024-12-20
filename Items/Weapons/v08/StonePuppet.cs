using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class StonePuppet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Stone Puppet");
			base.Tooltip.SetDefault("Summons a little Eaglecrest Rockpile to fight for you.");
		}

		public override void SetDefaults()
		{
			base.item.damage = 430;
			base.item.summon = true;
			base.item.mana = 15;
			base.item.width = 22;
			base.item.height = 30;
			base.item.useTime = 35;
			base.item.useAnimation = 35;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.knockBack = 2f;
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = ModContent.ProjectileType<EaglecrestMinion>();
			base.item.shootSpeed = 10f;
			base.item.buffType = ModContent.BuffType<EaglecrestMinionBuff>();
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
