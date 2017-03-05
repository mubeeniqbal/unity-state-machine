/// @license MIT License <https://opensource.org/licenses/MIT>
/// @copyright Copyright (C) Turbo Labz 2017 - All rights reserved
/// Open source
/// 
/// @author Mubeen Iqbal <mubeen@turbolabz.com>
/// @company Turbo Labz <http://turbolabz.com>
/// @date 2017-02-23 15:18:09 UTC+05:00
/// 
/// @description
/// [add_description_here]

using System;
using System.Collections.Generic;

namespace TurboLabz.UnityStateMachine
{
    public class StateConfiguration<TState, TTrigger> : IStateConfiguration<TState, TTrigger>
    {
        public StateMachine<TState, TTrigger> machine { get; private set; }
        public IStateRepresentation<TState, TTrigger> stateRepresentation { get; private set; }

        public TState state
        {
            get
            {
                return stateRepresentation.state;
            }
        }

        public StateConfiguration(StateMachine<TState, TTrigger> machine, TState state)
        {
            this.machine = machine;
            stateRepresentation = new StateRepresentation<TState, TTrigger>(state);
        }

        public IStateConfiguration<TState, TTrigger> Permit(TTrigger trigger, TState state)
        {
            stateRepresentation.AddTransition(trigger, state);
            return this;
        }

        public IStateConfiguration<TState, TTrigger> PermitIf(TTrigger trigger, TState state, Func<bool> guard)
        {
            if (guard() == true)
            {
                Permit(trigger, state);
            }

            return this;
        }

        public IStateConfiguration<TState, TTrigger> PermitReentry(TTrigger trigger)
        {
            Permit(trigger, state);
            return this;
        }

        public IStateConfiguration<TState, TTrigger> PermitReentryIf(TTrigger trigger, Func<bool> guard)
        {
            if (guard() == true)
            {
                PermitReentry(trigger);
            }

            return this;
        }

        public IStateConfiguration<TState, TTrigger> OnActivate(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "action parameter must not be null");
            }

            stateRepresentation.AddActivationAction(action);
            return this;
        }

        public IStateConfiguration<TState, TTrigger> OnDeactivate(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "action parameter must not be null");
            }

            stateRepresentation.AddDeactivationAction(action);
            return this;
        }

        public IStateConfiguration<TState, TTrigger> OnEnter(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "action parameter must not be null");
            }

            stateRepresentation.AddEntryAction(action);
            return this;
        }

        public IStateConfiguration<TState, TTrigger> OnExit(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "action parameter must not be null");
            }

            stateRepresentation.AddExitAction(action);
            return this;
        }

        public IStateConfiguration<TState, TTrigger> SubstateOf(TState superstate)
        {
            return null;
        }
    }
}
