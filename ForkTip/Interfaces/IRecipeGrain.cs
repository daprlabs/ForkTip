// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Orleans grain communication interface IRecipeGrain
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForkTip.GrainInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ForkTip.Models;

    /// <summary>
    /// Orleans grain communication interface IRecipeGrain
    /// </summary>
    public interface IRecipeGrain : Orleans.IGrainWithGuidKey
    {
        /// <summary>
        /// Attempts to initialize this instance with the provided values, returning a value indicating whether or .
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="newPassword">
        /// The new password.
        /// </param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        Task<bool> TryInitialize(Recipe value, string newPassword);

        /// <summary>
        /// Forks this instance, returning the identifier of the forked instance.
        /// </summary>
        /// <param name="newPassword">
        /// The new password.
        /// </param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        Task<Guid> Fork(string newPassword);

        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="value">The new value.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        Task SetName(string password, string value);

        /// <summary>
        /// Sets the description.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="value">The new value.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        Task SetDescription(string password, string value);

        /// <summary>
        /// Sets the author.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="value">The new value.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        Task SetAuthor(string password, string value);

        /// <summary>
        /// Sets the ingredients.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="value">The new value.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        Task SetIngredients(string password, List<string> value);

        /// <summary>
        /// Sets the directions.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="value">The new value.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        Task SetDirections(string password, List<string> value);

        /// <summary>
        /// Returns the state of this instance.
        /// </summary>
        /// <returns>The state of this instance.</returns>
        Task<Recipe> Get();
    }
}
