/// @license MIT License <https://opensource.org/licenses/MIT>
/// @copyright Copyright (C) Turbo Labz 2017 - All rights reserved
/// Source from this file can be used as per the license agreement
/// Open source
/// 
/// @author Mubeen Iqbal <mubeen@turbolabz.com>
/// @company Turbo Labz <http://turbolabz.com>
/// @date 2017-03-08 11:51:35 UTC+05:00
/// 
/// @description
/// [add_description_here]

namespace TurboLabz.UnityStateMachine
{
    public interface ITransition<TState, TTrigger>
    {
        /// <summary>
        /// The state transitioned from.
        /// </summary>
        TState source { get; }

        /// <summary>
        /// The state transitioned to.
        /// </summary>
        TState destination { get; }

        /// <summary>
        /// The trigger that caused the transition.
        /// </summary>
        TTrigger trigger { get; }

        /// <summary>
        /// True if the transition is a re-entry, i.e. the identity transition.
        /// </summary>
        bool isReentry { get; }
    }
}
