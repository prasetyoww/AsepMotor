using Microsoft.AspNetCore.Mvc;
using ProductionPlanner.Models;
using ProductionPlanner.ViewModels;

namespace ProductionPlanner.Controllers
{
    public class ProductionController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new ProductionPlanViewModel
            {
                OriginalPlan = new ProductionPlan(),
                AdjustedPlan = new ProductionPlan()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(ProductionPlanViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int[] originalPlan = viewModel.OriginalPlan.GetPlanArray();
                int[] adjustedPlan = DistributeProduction(originalPlan);
                viewModel.AdjustedPlan = new ProductionPlan();
                viewModel.AdjustedPlan.SetPlanArray(adjustedPlan);

                // Debugging: Check values
                System.Diagnostics.Debug.WriteLine("Original Plan: " + string.Join(",", originalPlan));
                System.Diagnostics.Debug.WriteLine("Adjusted Plan: " + string.Join(",", adjustedPlan));
            }
            else
            {
                viewModel.AdjustedPlan = new ProductionPlan();
            }

            return View(viewModel);
        }

        private int[] DistributeProduction(int[] plan)
        {
            int totalProduction = plan.Sum();
            int days = plan.Length;
            int avgProduction = totalProduction / days;
            int remainder = totalProduction % days;

            int[] result = new int[days];
            for (int i = 0; i < days; i++)
            {
                result[i] = avgProduction;
            }

            int[] sortedIndices = plan
                .Select((value, index) => new { Value = value, Index = index })
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Index)
                .Select(x => x.Index)
                .ToArray();

            for (int i = 0; i < remainder; i++)
            {
                result[sortedIndices[i]]++;
            }

            return result;
        }
    }
}
