﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RESTAPI.CustomeActionFilter
{
    public class ValidateModel  :ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.ModelState.IsValid==false) {

                context.Result = new BadRequestResult();
            }
        }
    }
}
