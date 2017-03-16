/// @license MIT License <https://opensource.org/licenses/MIT>
/// @copyright Copyright (C) Turbo Labz 2017 - All rights reserved
/// Source from this file can be used as per the license agreement
/// Open source
/// 
/// @author Mubeen Iqbal <mubeen@turbolabz.com>
/// @company Turbo Labz <http://turbolabz.com>
/// @date 2017-02-20 14:40:36 UTC+05:00
/// 
/// @description
/// [add_description_here]

using System;
using System.Collections.Generic;

namespace TurboLabz.UnityStateMachine
{
    public partial class StateMachine<TState, TTrigger> : IStateMachine<TState, TTrigger>
    {
        private IDictionary<TState, IStateConfiguration<TState, TTrigger>> _stateConfigurations = new Dictionary<TState, IStateConfiguration<TState, TTrigger>>();
        private Action _onTransition;

        public TState currentState { get; private set; }

        public ICollection<TTrigger> permittedTriggers
        {
            get
            {
                return currentStateRepresentation.permittedTriggers;
            }
        }

        private IStateRepresentation<TState, TTrigger> currentStateRepresentation
        {
            get
            {
                return GetStateRepresentation(currentState);
            }
        }

        public StateMachine(TState initialState)
        {
            currentState = initialState;
        }

        public IStateConfiguration<TState, TTrigger> Configure(TState state)
        {
            if (_stateConfigurations.ContainsKey(state))
            {
                return _stateConfigurations[state];
            }
            else
            {
                IStateConfiguration<TState, TTrigger> configuration = new StateConfiguration<TState, TTrigger>(this, state);
                _stateConfigurations.Add(state, configuration);
                return configuration;
            }
        }

        public void Fire(TTrigger trigger)
        {
            if (!permittedTriggers.Contains(trigger))
            {
                throw new NotSupportedException("'" + trigger + "' trigger is not configured for '" + currentState + "' state.");
            }

            TState oldState = currentState;
            TState newState = currentStateRepresentation.GetTransitionState(trigger);
            IStateRepresentation<TState, TTrigger> oldStateRepresentation = GetStateRepresentation(oldState);
            IStateRepresentation<TState, TTrigger> newStateRepresentation = GetStateRepresentation(newState);

            if (_onTransition != null)
            {
                _onTransition();
            }

            ITransition<TState, TTrigger> transition = new Transition<TState, TTrigger>(oldState, newState, trigger);
            oldStateRepresentation.OnExit(transition);
            newStateRepresentation.OnEnter(transition);
            currentState = newState;
        }

        public bool IsInState(TState state)
        {
            return currentState.Equals(state);
        }

        public bool CanFire(TTrigger trigger)
        {
            return currentStateRepresentation.CanHandle(trigger);
        }

        public void OnTransitioned(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "Action parameter must not be null.");
            }

            _onTransition = action;
        }

        public override string ToString()
        {
            string triggers = string.Empty;

            foreach (TTrigger trigger in permittedTriggers)
            {
                triggers += trigger + ", ";
            }

            return ("Current state: " + currentState + " | Permitted triggers: " + triggers);
        }

        public IStateRepresentation<TState, TTrigger> GetStateRepresentation(TState state)
        {
            if (!_stateConfigurations.ContainsKey(state))
            {
                throw new NotSupportedException("State " + state + " is not configured yet so no representation exists for it!");
            }

            return _stateConfigurations[state].stateRepresentation;
        }
    }
}
