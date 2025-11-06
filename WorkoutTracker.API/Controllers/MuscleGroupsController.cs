using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Data.Entities;
using WorkoutTracker.Data.Repositories;

// We must wrap our controller in a "namespace"
namespace WorkoutTracker.API.Controllers
{
    // --- THIS IS THE FIX for your 404 ---
    // This "label" tells .NET this class is an API Controller
    [ApiController] 
    // This "label" sets the URL to "api/MuscleGroups"
    [Route("api/[controller]")] 
    // --- END OF FIX ---

    public class MuscleGroupsController : ControllerBase 
    {
        private readonly MuscleGroupRepository _muscleGroupRepo;

        // The constructor asks for the Repository
        public MuscleGroupsController(MuscleGroupRepository muscleGroupRepo)
        {
            _muscleGroupRepo = muscleGroupRepo;
        }

        // This function runs when you send a GET request
        [HttpGet] 
        public async Task<IActionResult> GetAllMuscleGroups() 
        {
            // It calls the repository to get the data
            var muscleGroups = await _muscleGroupRepo.GetAllAsync();
            // It returns the data as JSON
            return Ok(muscleGroups); 
        }
    }
}