namespace BusinessLogic
{
    public class Helper
    {
        public static string GetStars(int counter)
        {
            string val;
            switch (counter)
            {
                case 1:
                    val = "***";
                    break;
                case 2:
                    val = "**";
                    break;
                case 3:
                    val = "*";
                    break;
                default:
                    val = "-";
                    break;
            }
            return val;
        }
    }
}
