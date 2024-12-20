using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class EggStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.Tooltip.SetDefault("Summons a Combat Chicken to fight for you.");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1;
			base.item.summon = true;
			base.item.mana = 5;
			base.item.width = 32;
			base.item.height = 32;
			base.item.useTime = 36;
			base.item.useAnimation = 36;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 2, 50);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = base.mod.ProjectileType("CombatChicken");
			base.item.shootSpeed = 10f;
			base.item.buffType = base.mod.BuffType("CombatChickenBuff");
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
