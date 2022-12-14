using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Altkom.Shopper.Api.Authorization;

// public class MinimumAgeRequirement : IAuthorizationRequirement  // mark interface (zaznaczający)
// {
//     public byte MinimumAge { get; private set; }

//     public MinimumAgeRequirement(byte minimumAge)
//     {
//         MinimumAge = minimumAge;
//     }
// }

public record MinimumAgeRequirement(byte MinimumAge) : IAuthorizationRequirement;
