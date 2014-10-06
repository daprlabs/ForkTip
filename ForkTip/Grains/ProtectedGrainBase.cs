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
    public class ProtectedGrainBase<TGrainState> : Grain<IProtectedState<TGrainState>>
    {
    }
}
