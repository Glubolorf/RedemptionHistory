using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class Datalog1000000 : ModItem
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
			base.DisplayName.SetDefault("Data Log #1000000");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'Today is the millionth day in space. When I was writing that down,]\n[c/aee6f3:I had a dumb moment where I thought it was a million years... But no.]\n[c/aee6f3:It has only been 2739.7 years, so... 364,000,000‬ days left... It feels like forever,]\n[c/aee6f3:and yet it's only been 0.27% of a million. Why am I still doing this. What's the point anymore?]\n[c/aee6f3:Every day is a pain, I just want to eat, I want to sleep...]\n[c/aee6f3:I would say I want to be human again, but to be honest... I don't even want to be alive anymore.']");
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
