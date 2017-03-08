/// @license MIT License <https://opensource.org/licenses/MIT>
/// @copyright Copyright (C) Turbo Labz 2017 - All rights reserved
/// Source from this file can be used as per the license agreement
/// Open source
/// 
/// @author Mubeen Iqbal <mubeen@turbolabz.com>
/// @company Turbo Labz <http://turbolabz.com>
/// @date 2017-03-08 10:45:59 UTC+05:00
/// 
/// @description
/// [add_description_here]

namespace TurboLabz.UnityStateMachine
{
    public class Transition<TState, TTrigger> : ITransition<TState, TTrigger>
    {
        public TState source { get; private set; }
        public TState destination { get; private set; }
        public TTrigger trigger { get; private set; }

        /// <summary>
        /// Construct a transition.
        /// </summary>
        /// <param name="source">The state transitioned from.</param>
        /// <param name="destination">The state transitioned to.</param>
        /// <param name="trigger">The trigger that caused the transition.</param>
        public Transition(TState source, TState destination, TTrigger trigger)
        {
            this.source = source;
            this.destination = destination;
            this.trigger = trigger;
        }

        public bool isReentry
        {
            get
            {
                return source.Equals(destination);
            }
        }
    }
}
