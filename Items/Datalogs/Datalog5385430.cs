using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class Datalog5385430 : ModItem
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
			base.DisplayName.SetDefault("Data Log #5385430");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'The SoS got attacked by some Space Pirates.]\n[c/aee6f3:Not like I care, I destroyed their ships so what can they do now?]\n[c/aee6f3:The SoS's scanner picked up a lifeform in the engine room, so I should probably check it out.]\n[c/aee6f3:I can't be asked to do anything really. But whatever.']");
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
