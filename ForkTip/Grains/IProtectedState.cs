// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Parameterized Grain state.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForkTip.Grains
{
    using Orleans;

    /// <summary>
    /// Parameterized Grain state.
    /// </summary>
    /// <typeparam name="TGrainState">
    /// The underlying grain state.
    /// </typeparam>
    public interface IProtectedState<TGrainState> : IGrainState
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        TGrainState Value { get; set; }
        
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        string Password { get; set; }
    }
}