using System;

namespace SimpleGameCliCore
{
	public static class GenderClass
	{
		public static Gender ParseGender(string toParse)
		{
			if(toParse.Contains("Male"))
				return Gender.Male;
			else if(toParse.Contains("Female"))
				return Gender.Female;
			else
				return Gender.Unknown;
		}
        public static string GetGenderPronoun(Gender gen)
        {
            switch(gen)
            {
                case(Gender.Male):
                    return "he";
                case(Gender.Female):
                    return "she";
                case(Gender.Unknown):
                    return "it";
            }
            return null;
        }
        public static string GetPosessivePronoun(Gender gen)
        {
            switch (gen)
            {
                case (Gender.Male):
                    return "his";
                case (Gender.Female):
                    return "her";
                case (Gender.Unknown):
                    return "their";
            }
            return null;
        }
	}
	public enum Gender
	{
		Male, Female, Unknown
	}
}
