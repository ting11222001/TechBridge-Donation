using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TechBridgeDonation.API.CustomActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // If the model state is invalid, return a 400 Bad Request with the validation errors
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
