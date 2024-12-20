using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption.Items.DruidDamageClass
{
	public class AncientWorldStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient World Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nSummon an Ancient Obelisk at your cursor point\nThe obelisk emits a force-field that increases player's defence and endurance when near\nCan only place one at a time");
			Item.staff[base.item.type] = true;
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 44;
			base.item.height = 46;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.mana = 100;
			base.item.value = Item.buyPrice(0, 2, 0, 0);
			base.item.rare = 1;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/WorldTree1");
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("WorldTree1");
			base.item.shootSpeed = 0f;
			base.item.buffType = base.mod.BuffType("WorldStaveCooldownDebuff");
			base.item.buffTime = 1000;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(base.mod.BuffType("WorldStaveCooldownDebuff"));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld;
			return true;
		}
	}
}
