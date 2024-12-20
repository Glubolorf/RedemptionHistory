using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class Datalog545675 : ModItem
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
			base.DisplayName.SetDefault("Data Log #545675");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'Wow, this planet blew my expectations away...]\n[c/aee6f3:I have named it Alatar V. It's very small, and on the surface it just looks barren.]\n[c/aee6f3:However, it's cave systems are beautiful. Like, there's so many colours and valuable ores.]\n[c/aee6f3:I've been exploring them overtime for probably years now. But that's fine,]\n[c/aee6f3:Not like I got anything better to do.]\n[c/aee6f3:I'll be leaving this planet soon and moving onto the next solar system.']");
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
