// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Recipe.cs" company="Dapr Labs">
//   Copyright 2014, Dapr Labs Pty. Ltd.
// </copyright>
// <summary>
//   Describes a recipe.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForkTip.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Describes a recipe.
    /// </summary>
    [Serializable]
    public class Recipe
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the creator's name.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ingredients.
        /// </summary>
        public List<string> Ingredients { get; set; }

        /// <summary>
        /// Gets or sets the directions.
        /// </summary>
        public List<string> Directions { get; set; }
    }
}