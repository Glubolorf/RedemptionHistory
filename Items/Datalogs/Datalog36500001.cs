using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class Datalog36500001 : ModItem
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
			base.DisplayName.SetDefault("Data Log #36500001");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'It's been 10% of a million years now. Yay.]\n[c/aee6f3:I accidently skipped a day in the data logs so 10% was really yesterday, but not like I care.]\n[c/aee6f3:I have explored... 2853 planets now, and they are starting to all look the same.]\n[c/aee6f3:I'm sick of ice planets, sick of lush green ones, sick of barren ones... I guess I'm not satisfied anymore.']");
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
