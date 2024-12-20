using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class ThankYouLetter : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Letter");
			base.Tooltip.SetDefault("It reads - '[c/E5F3FB:Thank you for playing the mod, hope you had fun.]\n[c/E5F3FB:The mod is still in development so many things can change, and new content will be added, so stay tuned.]\n[c/E5F3FB:  -] [c/F9E5FB:Hallam] & [c/E5FBE5:Tied]'");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 22;
			base.item.maxStack = 30;
			base.item.value = 0;
			base.item.rare = 1;
		}
	}
}
