using Microsoft.AspNetCore.Mvc.Filters;

namespace FundooNotesApp.CustomFilterRepo
{
    public class FundooExceptionFilterAttribute:ExceptionFilterAttribute,IExceptionFilter
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

        }
        //Check the Exception Type
        //if()
    }
}
