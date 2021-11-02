using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.Weather.ByDay
{
    public class GetWeatherByWeekDayRequestValidator : AbstractValidator<GetWeatherByWeekDayRequestHandler.Request>
    {
        public GetWeatherByWeekDayRequestValidator()
        {
            RuleFor(x => x.Day).NotEmpty().WithMessage("Day cannot be empty");
            RuleFor(x => x.Day).IsEnumName(typeof(DayOfWeek), false).WithMessage("Must be a valid day");
        }
    }
}
