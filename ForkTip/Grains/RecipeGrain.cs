// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecipeGrain.cs" company="Dapr Labs">
//   Copyright 2014, Dapr Labs Pty. Ltd.
// </copyright>
// <summary>
//   Orleans grain implementation class RecipeGrain
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForkTip.Grains
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ForkTip.GrainInterfaces;
    using ForkTip.Models;

    using Orleans;

    /// <summary>
    /// Orleans grain implementation class RecipeGrain
    /// </summary>
    [StorageProvider(ProviderName = "AzureTableStore")]
    public class RecipeGrain : ProtectedGrainBase<Recipe>, IRecipeGrain
    {
        /// <summary>
        /// This method is called at the end of the process of activating a grain.
        /// It is called before any messages have been dispatched to the grain.
        /// For grains with declared persistent state, this method is called after the State property has been populated.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        public override async Task ActivateAsync()
        {
            this.State.Value.Ingredients = this.State.Value.Ingredients ?? new List<string>();
            this.State.Value.Directions = this.State.Value.Directions ?? new List<string>();
            await base.ActivateAsync();
        }

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
        public async Task<bool> TryInitialize(Recipe value, string newPassword)
        {
            var result = string.IsNullOrEmpty(this.State.Password);
            if (result)
            {
                this.State.Value = value;
                this.State.Password = newPassword;
                this.State.Value.Id = this.GetPrimaryKey();
                this.State.Value.Ingredients = this.State.Value.Ingredients ?? new List<string>();
                this.State.Value.Directions = this.State.Value.Directions ?? new List<string>();
                await this.State.WriteStateAsync();
            }

            return result;
        }

        /// <summary>
        /// Forks this instance, returning the identifier of the forked instance.
        /// </summary>
        /// <param name="newPassword">
        /// The new password.
        /// </param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        public async Task<Guid> Fork(string newPassword)
        {
            Guid result;
            while (true)
            {
                result = Guid.NewGuid();
                var grain = RecipeGrainFactory.GetGrain(result);
                if (await grain.TryInitialize(this.State.Value, this.State.Password))
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the state of this instance.
        /// </summary>
        /// <returns>The state of this instance.</returns>
        public Task<Recipe> Get()
        {
            return Task.FromResult(this.State.Value);
        }

        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="value">The new value.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        public async Task SetName(string password, string value)
        {
            this.CheckPassword(password);
            this.State.Value.Name = value;
            await this.State.WriteStateAsync();
        }


        /// <summary>
        /// Sets the description.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="value">The new value.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        public async Task SetDescription(string password, string value)
        {
            this.CheckPassword(password);
            this.State.Value.Description = value;
            await this.State.WriteStateAsync();
        }

        /// <summary>
        /// Sets the author.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="value">The new value.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        public async Task SetAuthor(string password, string value)
        {
            this.CheckPassword(password);
            this.State.Value.Author = value;
            await this.State.WriteStateAsync();
        }

        /// <summary>
        /// Sets the directions.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="value">The new value.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        public async Task SetDirections(string password, List<string> value)
        {
            this.CheckPassword(password);
            this.State.Value.Directions = value ?? new List<string>();

            await this.State.WriteStateAsync();
        }

        /// <summary>
        /// Sets the ingredients.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="value">The new value.</param>
        /// <returns>A <see cref="Task"/> representing the work performed.</returns>
        public async Task SetIngredients(string password, List<string> value)
        {
            this.CheckPassword(password);
            this.State.Value.Ingredients = value ?? new List<string>();
            await this.State.WriteStateAsync();
        }

        /// <summary>
        /// Checks the password against the provided password.
        /// </summary>
        /// <param name="password">The password.</param>
        private void CheckPassword(string password)
        {
            if (!string.IsNullOrEmpty(this.State.Password) && !string.Equals(this.State.Password, password, StringComparison.Ordinal))
            {
                throw new AccessViolationException("Incorrect password.");
            }
        }
    }
}