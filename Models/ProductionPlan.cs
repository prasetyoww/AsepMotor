namespace ProductionPlanner.Models
{
    public class ProductionPlan
    {
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }

        public int[] GetPlanArray()
        {
            return new int[] { Monday, Tuesday, Wednesday, Thursday, Friday };
        }

        public void SetPlanArray(int[] plan)
        {
            if (plan.Length >= 5)
            {
                Monday = plan[0];
                Tuesday = plan[1];
                Wednesday = plan[2];
                Thursday = plan[3];
                Friday = plan[4];
            }
        }
    }
}

//namespace ProductionPlanner.ViewModels
//{
//    public class ProductionPlanViewModel
//    {
//        public ProductionPlan OriginalPlan { get; set; }
//        public ProductionPlan AdjustedPlan { get; set; }
//    }
//}
