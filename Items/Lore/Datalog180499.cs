using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class Datalog180499 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Lore/Datalog1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Data Log #180499");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'My god how have I not died from this pain yet,]\n[c/aee6f3:it just keeps growing. Whenever I think it can't get any worse the next day it does!]\n[c/aee6f3:On brighter news my bigass spaceship is finished. Now I can leave this planet.]\n[c/aee6f3:I'm getting real sick of snow, the old world was nothing but snow as well, I just want some greenery for once.]\n[c/aee6f3:Unfortunately my next planet is even further from the sun so I'm not really hopeful...']");
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
