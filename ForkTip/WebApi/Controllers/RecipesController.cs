// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecipesController.cs" company="Dapr Labs">
//   Copyright 2014, Dapr Labs Pty. Ltd.
// </copyright>
// <summary>
//   The recipes controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForkTip.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;

    using ForkTip.GrainInterfaces;

    /// <summary>
    /// The recipes controller.
    /// </summary>
    [RoutePrefix("api/v1/recipe")]
    public class RecipesController : ApiController
    {
        /// <summary>
        /// Creates a new recipe, returning its identifier.
        /// </summary>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The identifier of the newly created producer.
        /// </returns>
        [HttpPost, Route("")]
        public async Task<Guid> Post([FromUri] string password)
        {
            var grain = RecipeGrainFactory.GetGrain(Guid.Empty);
            return await grain.Fork(password);
        }
    }
}