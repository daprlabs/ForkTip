// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatefulGrainBase.cs" company="">
//   
// </copyright>
// <summary>
//   The stateful grain base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForkTip.Grains
{
    using Orleans;

    /// <summary>
    /// The stateful grain base.
    /// </summary>
    /// <typeparam name="TGrainState">
    /// The underlying grain state.
    /// </typeparam>
    public class ProtectedGrainBase<TGrainState> : GrainBase<IProtectedState<TGrainState>>
    {
    }

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
