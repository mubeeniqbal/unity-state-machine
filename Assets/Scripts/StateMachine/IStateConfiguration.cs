/// @license MIT License <https://opensource.org/licenses/MIT>
/// @copyright Copyright (C) Turbo Labz 2017 - All rights reserved
/// Source from this file can be used as per the license agreement
/// Open source
/// 
/// @author Mubeen Iqbal <mubeen@turbolabz.com>
/// @company Turbo Labz <http://turbolabz.com>
/// @date 2017-02-23 15:16:22 UTC+05:00
/// 
/// @description
/// [add_description_here]

using System;

namespace TurboLabz.UnityStateMachine
{
    public interface IStateConfiguration<TState, TTrigger>
    {
        /// <summary>
        /// The machine that is configured with this configuration.
        /// </summary>
        StateMachine<TState, TTrigger> machine { get; }

        /// <summary>
        /// The state representation for the underlying state.
        /// </summary>
        IStateRepresentation<TState, TTrigger> stateRepresentation { get; }

        /// <summary>
        /// The state that is configured with this configuration.
        /// </summary>
        TState state { get; }

        /// <summary>
        /// Accept the specified trigger and transition to the destination
        /// state.
        /// </summary>
        /// <param name="trigger">The accepted trigger.</param>
        /// <param name="state">The state that the trigger will
        /// cause a transition to.</param>
        /// <returns>The receiver.</returns>
        IStateConfiguration<TState, TTrigger> Permit(TTrigger trigger, TState state);

        /// <summary>
        /// Accept the specified trigger and transition to the destination
        /// state.
        /// </summary>
        /// <param name="trigger">The accepted trigger.</param>
        /// <param name="state">The state that the trigger will
        /// cause a transition to.</param>
        /// <param name="guard">Function that must return true in order for
        /// the trigger to be accepted.</param>
        /// <returns>The reciever.</returns>
        IStateConfiguration<TState, TTrigger> PermitIf(TTrigger trigger, TState state, Func<bool> guard);

        /// <summary>
        /// Accept the specified trigger, execute exit actions and
        /// re-execute entry actions.
        /// Reentry behaves as though the configured state transitions to an
        /// identical sibling state.
        /// </summary>
        /// <param name="trigger">The accepted trigger.</param>
        /// <returns>The reciever.</returns>
        /// <remarks>
        /// Applies to the current state only. Will not re-execute
        /// superstate actions, or cause actions to execute transitioning
        /// between super- and sub-states.
        /// </remarks>
        IStateConfiguration<TState, TTrigger> PermitReentry(TTrigger trigger);

        /// <summary>
        /// Accept the specified trigger, execute exit actions and
        /// re-execute entry actions.
        /// Reentry behaves as though the configured state transitions to an
        /// identical sibling state.
        /// </summary>
        /// <param name="trigger">The accepted trigger.</param>
        /// <param name="guard">Function that must return true in order for
        /// the trigger to be accepted.</param>
        /// <param name="guardDescription">Guard description</param>
        /// <returns>The reciever.</returns>
        /// <remarks>
        /// Applies to the current state only. Will not re-execute
        /// superstate actions, or cause actions to execute transitioning
        /// between super- and sub-states.
        /// </remarks>
        IStateConfiguration<TState, TTrigger> PermitReentryIf(TTrigger trigger, Func<bool> guard);

        /// <summary>
        /// Specify an action that will execute when transitioning into
        /// the configured state.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <returns>The receiver.</returns>
        IStateConfiguration<TState, TTrigger> OnEnter(Action action);

        /// <summary>
        /// Specify an action that will execute when transitioning out of
        /// the configured state.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <returns>The receiver.</returns>
        IStateConfiguration<TState, TTrigger> OnExit(Action action);

        /// <summary>
        /// Sets the super state that the configured state is a substate of.
        /// </summary>
        /// <remarks>
        /// Substates inherit the allowed transitions of their super state.
        /// When entering directly into a substate from outside of the
        /// super state, entry actions for the super state are executed.
        /// Likewise when leaving from the substate to outside the super state,
        /// exit actions for the super state will execute.
        /// </remarks>
        /// <param name="superState">The super state.</param>
        /// <returns>The receiver.</returns>
        IStateConfiguration<TState, TTrigger> SubstateOf(TState superState);
    }
}
