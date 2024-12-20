using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class Datalog3 : ModItem
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
			base.DisplayName.SetDefault("Data Log #3");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'Strange.]\n[c/aee6f3:I'm starting to get symptoms of thirst and tiredness...]\n[c/aee6f3:This shouldn't be happening, as a robot, I shouldn't require these basic human needs!]\n[c/aee6f3:If for whatever reason these needs still affect me, this could make this voyage even worse.]\n[c/aee6f3:I can't go to sleep without eyes, I can't drink without a mouth, I can't eat without a digestive system...]\n[c/aee6f3:So I have no way of stopping these symptoms.]\n[c/aee6f3:I'm still 2 years from reaching the nearest planet - Nabu III]\n[c/aee6f3:Actually, I should come up with a new name for my robotic body... Something other than Survival Robot Mk. 78.']");
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
