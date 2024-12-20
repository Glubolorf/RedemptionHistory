using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class Datalog3650000 : ModItem
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
			base.DisplayName.SetDefault("Data Log #3650000");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'Today is the 10,000th year in space, only 1% of a million...]\n[c/aee6f3:I feel like living beings shouldn't be allowed to live for this long.]\n[c/aee6f3:A hundred years for a human is forever, and I've been around for 100x that!]\n[c/aee6f3:I've redesigned my robotic body again, but I still haven't figured out how to get into my]\n[c/aee6f3:human mind and get rid of this STUPID HUNGER. I DON'T HAVE A STOMACH, WHY AM I HUNGRY!?]\n[c/aee6f3:I DON'T HAVE EYES, WHY AM I SO TIRED!? WHY DO I HAVE TO LIVE THROUGH THIS DAMN IT!?']");
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
