using Simple.Services;
using Locadora.Services;
using System.Collections.Generic;
using Simple.Patterns;

namespace Locadora.Services
{
    public partial interface ISystemService : IService
    {
        IList<TaskRunner.Result> Check();
    }
}