// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecipeController.cs" company="Dapr Labs">
//   Copyright 2014, Dapr Labs Pty. Ltd.
// </copyright>
// <summary>
//   The recipe controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForkTip.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using System.Web.Http;

    using ForkTip.GrainInterfaces;
    using ForkTip.Models;

    /// <summary>
    /// The recipe controller.
    /// </summary>
    [RoutePrefix("api/v1/recipe")]
    public class RecipeController : ApiController
    {
        /// <summary>
        /// Returns the instance with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The instance with the specified <paramref name="id"/>.
        /// </returns>
        [HttpGet, Route("{id:guid}")]
        public async Task<Recipe> Get(Guid id)
        {
            var grain = RecipeGrainFactory.GetGrain(id);
            return await grain.Get();
        }

        /// <summary>
        /// Forks the instance with the specified <paramref name="id"/>, returning the identifier of the new instance.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>The identifier of the new instance.</returns>
        [HttpPost, Route("{id:guid}")]
        public async Task<Guid> Fork(Guid id, string newPassword)
        {
            var grain = RecipeGrainFactory.GetGrain(id);
            return await grain.Fork(newPassword);
        }

        /// <summary>Sets the name.</summary>
        /// <param name="id">The id.</param>
        /// <param name="password">The password.</param>
        /// <param name="value">The name.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        [HttpPut, Route("{id:guid}/name")]
        public async Task SetName(Guid id, [FromUri] string password, [FromBody] string value)
        {
            Validate(value);
            var grain = RecipeGrainFactory.GetGrain(id);
            await grain.SetName(password, value);
        }

        /// <summary>Sets the description.</summary>
        /// <param name="id">The id.</param>
        /// <param name="password">The password.</param>
        /// <param name="value">The description.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        [HttpPut, Route("{id:guid}/description")]
        public async Task SetDescription(Guid id, [FromUri] string password, [FromBody] string value)
        {
            Validate(value);
            var grain = RecipeGrainFactory.GetGrain(id);
            await grain.SetDescription(password, value);
        }

        /// <summary>Sets the author.</summary>
        /// <param name="id">The id.</param>
        /// <param name="password">The password.</param>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        [HttpPut, Route("{id:guid}/author")]
        public async Task SetAuthor(Guid id, [FromUri] string password, [FromBody] string value)
        {
            Validate(value);
            var grain = RecipeGrainFactory.GetGrain(id);
            await grain.SetAuthor(password, value);
        }

        /// <summary>Sets the ingredients.</summary>
        /// <param name="id">The id.</param>
        /// <param name="password">The password.</param>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        [HttpPut, Route("{id:guid}/ingredients")]
        public async Task SetIngredients(Guid id, [FromUri] string password, [FromBody] List<string> value)
        {
            Validate(value);
            var grain = RecipeGrainFactory.GetGrain(id);
            await grain.SetIngredients(password, value);
        }

        /// <summary>Sets the directions.</summary>
        /// <param name="id">The id.</param>
        /// <param name="password">The password.</param>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        [HttpPut, Route("{id:guid}/directions")]
        public async Task SetDirections(Guid id, [FromUri] string password, [FromBody] List<string> value)
        {
            Validate(value);
            var grain = RecipeGrainFactory.GetGrain(id);
            await grain.SetDirections(password, value);
        }


        /// <summary>
        /// Validates the provided values, throwing an <see cref="InvalidDataException"/> on error.
        /// </summary>
        /// <param name="values">The values to validate.</param>
        private static void Validate(List<string> values)
        {
            if (values != null)
            {
                values.ForEach(Validate);
            }
        }

        /// <summary>
        /// Validates the provided value, throwing an <see cref="InvalidDataException"/> on error.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        private static void Validate(string value)
        {
        }
    }
}