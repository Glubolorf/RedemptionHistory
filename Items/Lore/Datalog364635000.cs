using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class Datalog364635000 : ModItem
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
			base.DisplayName.SetDefault("Data Log #364635000");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'Only 1000 years until a million, and I can return home.]\n[c/aee6f3:I've already set my course, but the problem is, because of that wormhole,]\n[c/aee6f3:I don't know which way is home... All I can do now is go to a random direction]\n[c/aee6f3:and hope for the best. But the galaxy is vast, I fear by the time I reach home again,]\n[c/aee6f3:The next reset would've already started, and I'd have to wait another million years...]\n[c/aee6f3:If that happens, I won't try anymore, I'll just give up.']");
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
