using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class Datalog182500 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Datalogs/Datalog1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Data Log #182500");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'Today marks 500 years away from home.]\n[c/aee6f3:It took 5.5 years but I'm at the next planet - Alkonost. I expected to get there faster,]\n[c/aee6f3:but I REALLY underestimated the amount of fuel this giant spaceship would consume.]\n[c/aee6f3:I'm certain I wouldn't have made that mistake if I just didn't feel so terrible!]\n[c/aee6f3:Living like this is absolute hell. I'll write down this planet's statistics next data log.']");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 2));
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 30;
			base.item.maxStack = 1;
			base.item.value = 0;
			base.item.rare = 9;
		}
	}
}
